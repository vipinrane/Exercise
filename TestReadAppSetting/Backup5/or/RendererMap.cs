// 
// This framework is based on log4j see http://jakarta.apache.org/log4j
// Copyright (C) The Apache Software Foundation. All rights reserved.
//
// Modifications Copyright (C) 2001 Neoworks Limited. All rights reserved.
// For more information on Neoworks, please see <http://www.neoworks.com/>. 
//
// This software is published under the terms of the Apache Software
// License version 1.1, a copy of which has been included with this
// distribution in the LICENSE.txt file.
// 

using System;

namespace log4net.or
{
	/// <summary>
	/// Map class objects to an <see cref="IObjectRenderer"/>.
	/// </summary>
	public class RendererMap
	{
		private System.Collections.Hashtable m_map;
		private static IObjectRenderer s_defaultRenderer = new DefaultRenderer();

		/// <summary>
		/// Constructor
		/// </summary>
		public RendererMap() 
		{
			m_map = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		}

		/// <summary>
		/// Find the appropriate renderer for the class type of the
		/// <code>o</code> parameter. This is accomplished by calling the
		/// <see cref="Get"/> method. Once a renderer is found, it is
		/// applied on the object <code>o</code> and the result is returned
		/// as a <see cref="string"/>.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public string FindAndRender(object o) 
		{
			if(o == null)
			{
				return null;
			}
			else 
			{
				return Get(o.GetType()).DoRender(o);
			}
		}

		/// <summary>
		/// Syntactic sugar method that calls <see cref="Get"/> with the class of the object parameter.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public IObjectRenderer Get(Object o) 
		{
			if(o == null) 
			{
				return null;
			}
			else
			{
				return Get(o.GetType());
			}
		}
  
		/// <summary>
		/// Gets the renderer for the specified type
		/// </summary>
		/// <param name="clazz"></param>
		/// <returns></returns>
		public IObjectRenderer Get(Type clazz) 
		{
			//System.out.println("\nget: "+clazz);    
			IObjectRenderer r = null;

			Type c = clazz;
			for(; c != null; c = c.BaseType)
			{
				//System.out.println("Searching for class: "+c);
				r = (IObjectRenderer)m_map[c];
				if(r != null) 
				{
					break;
				}      
				r = SearchInterfaces(c);
				if(r != null)
				{
					break;
				}
			}

			// check for default renderer
			if (r == null)
			{
				r = s_defaultRenderer;
			}

			return r;
		}  

		/// <summary>
		/// Internal function to recusivly search interfaces
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private IObjectRenderer SearchInterfaces(Type c) 
		{
			//System.out.println("Searching interfaces of class: "+c);
    
			IObjectRenderer r = (IObjectRenderer)m_map[c];
			if(r != null) 
			{
				return r;
			} 
			else 
			{
				foreach(Type t in c.GetInterfaces())
				{
					r = SearchInterfaces(t);
					if(r != null)
					{
						return r; 
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Get the default renderer instance
		/// </summary>
		/// <returns></returns>
		public IObjectRenderer DefaultRenderer
		{
			get { return s_defaultRenderer; }
		}

		/// <summary>
		/// Clear the map of renderers
		/// </summary>
		public void Clear() 
		{
			m_map.Clear();
		}

		/// <summary>
		/// Register an <see cref="IObjectRenderer"/> for <code>clazz</code>. 
		/// </summary>
		/// <param name="clazz"></param>
		/// <param name="or"></param>
		public void Put(Type clazz, IObjectRenderer or) 
		{
			m_map[clazz] = or;
		}	
	}
}

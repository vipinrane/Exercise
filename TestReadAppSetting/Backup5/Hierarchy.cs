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

using log4net.or;
using log4net.spi;
using log4net.helpers;

namespace log4net
{
	/// <summary>
	/// Delegate used to handle category creation event notifications
	/// </summary>
	/// <param name="h">The Hierarchy in which the event handler is registered</param>
	/// <param name="cat">The category that has been created</param>
	public delegate void CategoryCreationEventHandler(Hierarchy h, Category cat);

	/// <summary>
	/// This class is specialized in retrieving categories by name and
	/// also maintaining the category hierarchy.
	/// </summary>
	/// <remarks>
	/// <p><em>The casual user should not have to deal with this class
	/// directly.</em> In fact, up until version 0.9.0, this class had
	/// default package access. However, if you are in an environment where
	/// multiple applications run in the same VM, then read on.</p>
	/// 
	/// <p>The structure of the category hierarchy is maintained by the
	/// <see cref="GetInstance"/> method. The hierarchy is such that children
	/// link to their parent but parents do not have any pointers to their
	/// children. Moreover, categories can be instantiated in any order, in
	/// particular descendant before ancestor.</p>
	/// 
	/// <p>In case a descendant is created before a particular ancestor,
	/// then it creates a provision node for the ancestor and adds itself
	/// to the provision node. Other descendants of the same ancestor add
	/// themselves to the previously created provision node.</p>
	/// </remarks>
	public class Hierarchy
	{

		#region Member Variables

		private ICategoryFactory m_defaultFactory;

		private System.Collections.Hashtable m_ht;
		private Category m_root;
		private RendererMap m_rendererMap;
  
		private bool m_disableOverride = false;
		private Priority m_disablePriority;
		private bool m_emittedNoAppenderWarning = false;
		private event CategoryCreationEventHandler m_categoryCreatedEvent;

		#endregion


		/// <summary>
		/// Create a new Category hierarchy.
		/// </summary>
		/// <param name="root">The root of the new hierarchy</param>
		internal Hierarchy(Category root) 
		{
			m_ht = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			m_root = root;

			// Don't disable any priority level by default.
			EnableAll();
			m_root.Hierarchy = this;
			m_rendererMap = new RendererMap();
			m_defaultFactory = new DefaultCategoryFactory();
		}

		/// <summary>
		/// Flag to indicate if we have already issued a warning
		/// about not having an appender warning.
		/// </summary>
		public bool EmittedNoAppenderWarning
		{
			get { return m_emittedNoAppenderWarning; }
			set { m_emittedNoAppenderWarning = value; }
		}

		/// <summary>
		/// Get the diable level
		/// </summary>
		public Priority DisablePriority
		{
			get { return m_disablePriority; }
		}

		/// <summary>
		/// Add an object renderer for a specific class. 
		/// </summary>
		/// <param name="classToRender">The type that will be rendered by the renderer supplied</param>
		/// <param name="or">The object renderer used to render the object</param>
		public void AddRenderer(Type classToRender, IObjectRenderer or) 
		{
			m_rendererMap.Put(classToRender, or);
		}
  
		/// <summary>
		/// This call will clear all category definitions from the internal
		/// hashtable. Invoking this method will irrevocably mess up the
		/// category hierarchy.
		/// 
		/// <p>You should <em>really</em> know what you are doing before
		/// invoking this method.</p>
		/// </summary>
		public void Clear() 
		{
			m_ht.Clear();
		}

		/// <summary>
		/// Check if the named category exists in the hierarchy. If so return
		/// its reference, otherwise returns <code>null</code>.
		/// </summary>
		/// <param name="name">The name of the category to lookup</param>
		/// <returns>The Category object with the name specified</returns>
		public Category Exists(string name) 
		{    
			return m_ht[new CategoryKey(name)] as Category;
		}

		/// <summary>
		/// Similar to <see cref="Disable(Priority)"/> except that the priority
		/// argument is given as a String.
		/// </summary>
		/// <param name="priorityStr">The priority to disable, as a string</param>
		public void Disable(string priorityStr) 
		{
			if(!m_disableOverride) 
			{  
				Priority p = Priority.Parse(priorityStr, null);
				if(p != null) 
				{
					m_disablePriority = p;
				} 
				else 
				{
					LogLog.Warn("Could not convert ["+priorityStr+"] to Priority.");
				}
			}
		}

		/// <summary>
		/// Disable all logging requests of priority <em>equal to or
		/// below</em> the priority parameter <code>p</code>, for
		/// <em>all</em> categories in this hierarchy. Logging requests of
		/// higher priority then <code>p</code> remain unaffected.
		/// 
		/// <p>The "disable" family of methods are there for speed. They
		/// allow printing methods such as debug, info, etc. to return
		/// immediately after an integer comparison without walking the
		/// category hierarchy. In most modern computers an integer
		/// comparison is measured in nanoseconds where as a category walk is
		/// measured in units of microseconds.</p>
		/// 
		/// <p>Configurators define alternate ways of overriding the
		/// disable override flag.</p>
		/// </summary>
		/// <param name="p">Disable all logging requests of priority <em>equal to or below</em> the priority parameter</param>
		public void Disable(Priority p) 
		{
			if(!m_disableOverride && (p != null)) 
			{
				m_disablePriority = p;
			}
		}
  
		/// <summary>
		/// Disable all logging requests regardless of category and priority.
		/// This method is equivalent to calling <see cref="Disable"/> with the
		/// argument <see cref="Priority.FATAL"/>, the highest possible priority.
		/// </summary>
		public void DisableAll() 
		{
			Disable(Priority.FATAL);
		}

		/// <summary>
		/// Disable all logging requests of priority DEBUG regardless of category.  
		/// This method is equivalent to calling <see cref="Disable"/> with the
		/// argument <see cref="Priority.DEBUG"/>, the highest possible priority.
		/// </summary>
		public void DisableDebug() 
		{
			Disable(Priority.DEBUG);
		}

		/// <summary>
		/// Disable all logging requests of priority DEBUG and INFO regardless of category.  
		/// This method is equivalent to calling <see cref="Disable"/> with the
		/// argument <see cref="Priority.INFO"/>, the highest possible priority.
		/// </summary>
		public void DisableInfo() 
		{
			Disable(Priority.INFO);
		}  

		/// <summary>
		/// Undoes the effect of calling any of <see cref="Disable"/>, <see cref="DisableAll"/>
		/// <see cref="DisableDebug"/> and <see cref="DisableInfo"/>
		/// methods. More precisely, invoking this method sets the Category
		/// class internal variable called <code>disable</code> to its
		/// default "off" value.
		/// </summary>
		public void EnableAll() 
		{
			m_disablePriority = Priority.OFF;
		}

		/// <summary>
		/// Event used to notify that a category has been created.
		/// </summary>
		public event CategoryCreationEventHandler CategoryCreated
		{
			add { m_categoryCreatedEvent += value; }
			remove { m_categoryCreatedEvent -= value; }
		}

		/// <summary>
		/// Sends a category creation event to all registered listeners
		/// </summary>
		/// <param name="category">The newly created category</param>
		private void FireCategoryCreationEvent(Category category) 
		{
			if (m_categoryCreatedEvent != null)
			{
				m_categoryCreatedEvent(this, category);
			}
		}

		/// <summary>
		/// Return a new category instance named as the first parameter using
		/// the default factory.
		/// 
		/// <p>If a category of that name already exists, then it will be
		/// returned.  Otherwise, a new category will be instantiated and
		/// then linked with its existing ancestors as well as children.</p>
		/// </summary>
		/// <param name="name">The name of the category to retrieve</param>
		/// <returns>The category object with the name specified</returns>
		public Category GetInstance(string name) 
		{
			return GetInstance(name, m_defaultFactory);
		}

		/// <summary>
		/// Return a new category instance named as the first parameter using
		/// <code>factory</code>.
		/// </summary>
		/// <remarks>
		/// If a category of that name already exists, then it will be
		/// returned. Otherwise, a new category will be instantiated by the
		/// <code>factory</code> parameter and linked with its existing
		/// ancestors as well as children.
		/// </remarks>
		/// <param name="name">The name of the category to retrieve</param>
		/// <param name="factory">The factory that will make the new category instance</param>
		/// <returns>The category object with the name specified</returns>
		public Category GetInstance(string name, ICategoryFactory factory) 
		{
			CategoryKey key = new CategoryKey(name);   
 
			// Synchronize to prevent write conflicts. Read conflicts (in
			// getChainedPriority method) are possible only if variable
			// assignments are non-atomic.
			Category category;
    
			lock(m_ht) 
			{
				Object o = m_ht[key];
				if(o == null) 
				{
					category = factory.MakeNewCategoryInstance(name);
					category.Hierarchy = this;
					m_ht[key] = category;      
					UpdateParents(category);
					FireCategoryCreationEvent(category);
					return category;
				} 
				else if(o is Category) 
				{
					return (Category) o;
				} 
				else if (o is ProvisionNode) 
				{
					//System.out.println("("+name+") ht.get(this) returned ProvisionNode");
					category = factory.MakeNewCategoryInstance(name);
					category.Hierarchy = this; 
					m_ht[key] = category;
					UpdateChildren((ProvisionNode) o, category);
					UpdateParents(category);	
					FireCategoryCreationEvent(category);
					return category;
				}
				else 
				{
					// It should be impossible to arrive here
					return null;  // but let's keep the compiler happy.
				}
			}
		}

		/// <summary>
		/// Returns all the currently defined categories in the default
		/// hierarchy as an <see cref="System.Collections.IEnumerator"/>.
		/// The root category is <em>not</em> included in the returned
		/// enumeration.
		/// </summary>
		/// <returns>All the defined categories</returns>
		public System.Collections.IEnumerator GetCurrentCategories() 
		{
			// The accumlation in v is necessary because not all elements in
			// ht are Category objects as there might be some ProvisionNodes
			// as well.
			System.Collections.IList v = new System.Collections.ArrayList(m_ht.Count);
    
			System.Collections.IEnumerator elems = m_ht.GetEnumerator();
			while(elems.MoveNext()) 
			{
				Object o = elems.Current;
				if(o is Category) 
				{
					v.Add(o);
				}
			}
			return v.GetEnumerator();
		}

		/// <value>
		/// RendererMap accesses the object renderer map for this hierarchy.
		/// </value>
		public RendererMap RendererMap
		{
			get { return m_rendererMap; }
		}

		/// <summary>
		/// Get the root of this hierarchy.
		/// </summary>
		public Category Root
		{
			get { return m_root; }
		}

		/// <summary>
		/// Override the shipped code flag if the <code>override</code>
		/// parameter is not null.
		/// </summary>
		/// <remarks>
		/// This method is intended to be used by configurators.
		/// <p>If the <code>override</code> paramter is <code>null</code>
		/// then there is nothing to do. Otherwise, set
		/// <code>Hiearchy.disable</code> to <code>false</code> if override
		/// has a value other than <code>false</code>.</p>
		/// </remarks>
		/// <param name="overrideStr">String that is either 'true' or 'false'</param>
		public void OverrideAsNeeded(string overrideStr) 
		{
			// If override is defined, any value other than false will be
			// interpreted as true.    
			if(overrideStr != null)
			{
				LogLog.Debug("Handling non-null disable override directive: \""+overrideStr +"\".");
				if(OptionConverter.ToBoolean(overrideStr, true)) 
				{
					LogLog.Debug("Overriding all disable methods.");
					m_disableOverride = true;
					m_disablePriority = Priority.OFF;
				}
			}
		}

		/// <summary>
		/// Reset all values contained in this hierarchy instance to their
		/// default.  This removes all appenders from all categories, sets
		/// the priority of all non-root categories to <code>null</code>,
		/// sets their additivity flag to <code>true</code> and sets the priority
		/// of the root category to {@link Priority#DEBUG DEBUG}.  Moreover,
		/// message disabling is set its default "off" value.
		/// 
		/// <p>Existing categories are not removed. They are just reset.</p>
		/// 
		/// <p>This method should be used sparingly and with care as it will
		/// block all logging until it is completed.</p>
		/// </summary>
		public void ResetConfiguration() 
		{
			Root.Priority = Priority.DEBUG;
			m_disablePriority = Priority.OFF;
    
			// the synchronization is needed to prevent JDK 1.2.x hashtable
			// surprises
			lock(m_ht) 
			{    
				Shutdown(); // nested locks are OK    
    
				System.Collections.IEnumerator cats = GetCurrentCategories();
				while(cats.MoveNext()) 
				{
					Category c = (Category) cats.Current;
					c.Priority = null;
					c.Additivity = true;
				}
			}
			m_rendererMap.Clear();
		}

		/// <summary>
		/// Set the default CategoryFactory instance.
		/// </summary>
		public ICategoryFactory CategoryFactory
		{
			set
			{
				if (value != null) 
				{
					m_defaultFactory = value;
				}
			}
		}

		/// <summary>
		/// Set the disable override value given a string.
		/// </summary>
		/// <param name="overrideStr">String that is either 'true' or 'false'</param>
		public void SetDisableOverride(string overrideStr) 
		{
			if(OptionConverter.ToBoolean(overrideStr, true)) 
			{
				LogLog.Debug("Overriding disable.");
				m_disableOverride = true;
				m_disablePriority = Priority.OFF;
			}
		}

		/// <summary>
		/// Shutting down a hierarchy will <em>safely</em> close and remove
		/// all appenders in all categories including the root category.
		/// 
		/// <p>Some appenders need to be closed before the
		/// application exists. Otherwise, pending logging events might be
		/// lost.</p>
		/// 
		/// <p>The <code>shutdown</code> method is careful to close nested
		/// appenders before closing regular appenders. This is allows
		/// configurations where a regular appender is attached to a category
		/// and again to a nested appender.</p>
		/// </summary>
		public void Shutdown() 
		{
			// begin by closing nested appenders
			Root.CloseNestedAppenders();

			lock(m_ht) 
			{
				System.Collections.IEnumerator cats = this.GetCurrentCategories();
				while(cats.MoveNext()) 
				{
					Category c = (Category) cats.Current;
					c.CloseNestedAppenders();
				}

				// then, remove all appenders
				Root.RemoveAllAppenders();
				cats = this.GetCurrentCategories();
				while(cats.MoveNext()) 
				{
					Category c = (Category) cats.Current;
					c.RemoveAllAppenders();
				}      
			}
		}

		/// <summary>
		/// Updates all the parents of the specified category
		/// </summary>
		/// <remarks>
		/// This method loops through all the *potential* parents of
		/// 'cat'. There 3 possible cases:
		/// <list type="number">
		///		<item>
		///			<term>No entry for the potential parent of 'cat' exists</term>
		///			<description>We create a ProvisionNode for this potential 
		///			parent and insert 'cat' in that provision node.</description>
		///		</item>
		///		<item>
		///			<term>There entry is of type Category for the potential parent.</term>
		///			<description>The entry is 'cat's nearest existing parent. We 
		///			update cat's parent field with this entry. We also break from 
		///			he loop because updating our parent's parent is our parent's 
		///			responsibility.</description>
		///		</item>
		///		<item>
		///			<term>There entry is of type ProvisionNode for this potential parent.</term>
		///			<description>We add 'cat' to the list of children for this 
		///			potential parent.</description>
		///		</item>
		/// </list>
		/// </remarks>
		/// <param name="cat">The category to update the parents for</param>
		private void UpdateParents(Category cat) 
		{
			String name = cat.Name;
			int length = name.Length;
			bool parentFound = false;
    
			// if name = "w.x.y.z", loop thourgh "w.x.y", "w.x" and "w", but not "w.x.y.z" 
			for(int i = name.LastIndexOf('.', length-1); i >= 0; i = name.LastIndexOf('.', i-1))  
			{
				string substr = name.Substring(0, i);

				CategoryKey key = new CategoryKey(substr); // simple constructor
				Object o = m_ht[key];
				// Create a provision node for a future parent.
				if(o == null) 
				{
					ProvisionNode pn = new ProvisionNode(cat);
					m_ht[key] = pn;
				} 
				else if(o is Category) 
				{
					parentFound = true;
					cat.Parent = (Category) o;
					break; // no need to update the ancestors of the closest ancestor
				} 
				else if(o is ProvisionNode) 
				{
					((ProvisionNode) o).Add(cat);
				} 
				else
				{
					Exception e = new Exception("unexpected object type " + o.GetType() + " in ht.");
					Console.Out.WriteLine(e.StackTrace);
				}
			}
			// If we could not find any existing parents, then link with root.
			if(!parentFound) 
			{
				cat.Parent = m_root;
			}
		}

		/// <summary>
		/// We update the links for all the children that placed themselves
		/// in the provision node 'pn'. The second argument 'cat' is a
		/// reference for the newly created Category, parent of all the
		/// children in 'pn'
		/// 
		/// <p>We loop on all the children 'c' in 'pn':</p>
		/// 
		/// 	<p>If the child 'c' has been already linked to a child of
		/// 	'cat' then there is no need to update 'c'.</p>
		/// 
		/// <p>Otherwise, we set cat's parent field to c's parent and set
		/// c's parent field to cat.</p>
		/// </summary>
		/// <param name="pn"></param>
		/// <param name="cat"></param>
		private void UpdateChildren(ProvisionNode pn, Category cat) 
		{
			int last = pn.Count;

			for(int i = 0; i < last; i++) 
			{
				Category c = (Category) pn[i];

				// Unless this child already points to a correct (lower) parent,
				// make cat.parent point to c.parent and c.parent to cat.
				if(!c.Parent.Name.StartsWith(cat.Name)) 
				{
					cat.Parent = c.Parent;
					c.Parent = cat;      
				}
			}
		}    

	}
}

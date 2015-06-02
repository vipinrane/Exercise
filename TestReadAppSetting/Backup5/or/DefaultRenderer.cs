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
	/// The default Renderer renders objects by calling their <code>ToString</code> method.
	/// </summary>
	public class DefaultRenderer : IObjectRenderer
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public DefaultRenderer()
		{
		}

		/// <summary>
		/// Render the object passed as parameter by calling its <code>ToString</code> method.
		/// </summary>
		/// <param name="obj">the object to render</param>
		/// <returns>the object rendered as a string</returns>
		public string DoRender(object obj) 
		{
			return obj.ToString();
		}

	}
}

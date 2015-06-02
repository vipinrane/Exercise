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

using log4net.helpers;


namespace log4net.spi
{
	/// <summary>
	/// RootCategory sits at the top of the category hierachy. It is a
	/// regular category except that it provides several guarantees.
	/// </summary>
	/// <remarks>
	/// First, it cannot be assigned a <code>null</code>
	/// priority. Second, since root category cannot have a parent, the
	/// <see cref="GetChainedPriority"/> method always returns the value of the
	/// priority field without walking the hierarchy.
	/// </remarks>
	public class RootCategory : Category
	{
		/// <summary>
		/// The root category names itself as "root". However, the root
		/// category cannot be retrieved by name.
		/// </summary>
		/// <param name="priority"></param>
		public RootCategory(Priority priority) : base("root")
		{
			Priority = priority;
		}

		/// <summary>
		/// Return the assigned priority value without walking the category hierarchy
		/// </summary>
		/// <returns></returns>
		public override Priority GetChainedPriority() 
		{
			return m_priority;
		}

		/// <summary>
		/// Setting a null value to the priority of the root category
		/// may have catastrophic results. We prevent this here.
		/// </summary>
		public override Priority Priority
		{
			set
			{
				if(value == null) 
				{
					LogLog.Error("You have tried to set a null priority to root.", new Exception());
				}
				else 
				{
					m_priority = value;
				}
			}
		}	
	}
}

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

using log4net;

namespace log4net.spi
{
	/// <summary>
	/// Implement this interface to create new instances of Category or a sub-class of Category.
	/// </summary>
	public interface ICategoryFactory
	{
		/// <summary>
		/// Construct a new Category instance with the name specified
		/// </summary>
		/// <param name="name">the name of the category</param>
		/// <returns>the instance for the name specified</returns>
		Category MakeNewCategoryInstance(string name);
	}
}

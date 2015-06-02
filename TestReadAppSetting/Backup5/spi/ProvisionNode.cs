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

namespace log4net.spi
{
	/// <summary>
	/// Summary description for ProvisionNode.
	/// </summary>
	internal class ProvisionNode : System.Collections.ArrayList
	{
		internal ProvisionNode(Category cat) : base()
		{
			this.Add(cat);
		}
	}
}

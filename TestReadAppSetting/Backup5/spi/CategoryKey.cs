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
	/// CategoryKey is heavily used internally to accelerate hash table searches.
	/// </summary>
	internal class CategoryKey
	{
		private string m_name;  
		private int m_hashCache;

		internal CategoryKey(String name) 
		{
			m_name = string.Intern(name);
			m_hashCache = name.GetHashCode();
		}

		internal string Value
		{
			get { return m_name; }
		}

		public override int GetHashCode() 
		{
			return m_hashCache;
		}

		public override bool Equals(object rArg) 
		{
			if(this == rArg)
			{
				return true;
			}
			if((rArg != null) && (rArg is CategoryKey)) 
			{
				// Compare reference types rather than String's overloaded ==
				return ((object)m_name == (object)((CategoryKey)rArg).m_name);
			}
			return false;
		}
	}	
}


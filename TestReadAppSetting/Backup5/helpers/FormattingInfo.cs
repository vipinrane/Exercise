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

namespace log4net.helpers
{
	/// <summary>
	/// FormattingInfo instances contain the information obtained when parsing 
	/// formatting modifiers in conversion modifiers.
	/// </summary>
	public class FormattingInfo
	{
		private int m_min = -1;
		private int m_max = 0x7FFFFFFF;
		private bool m_leftAlign = false;

		/// <summary>
		/// Minimum value
		/// </summary>
		public int Min
		{
			get { return m_min; }
			set { m_min = value; }
		}

		/// <summary>
		/// Maximum value
		/// </summary>
		public int Max
		{
			get { return m_max; }
			set { m_max = value; }
		}

		/// <summary>
		/// Flag indicating left align
		/// </summary>
		public bool LeftAlign
		{
			get { return m_leftAlign; }
			set { m_leftAlign = value; }
		}

		/// <summary>
		/// Set back to default values
		/// </summary>
		public void Reset() 
		{
			m_min = -1;
			m_max = 0x7FFFFFFF;
			m_leftAlign = false;      
		}

		/// <summary>
		/// Dump debug info
		/// </summary>
		public void Dump() 
		{
			LogLog.Debug("min="+m_min+", max="+m_max+", leftAlign="+m_leftAlign);
		}
	}
}

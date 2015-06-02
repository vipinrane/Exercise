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

using System.Text;

using log4net.spi;

namespace log4net.helpers
{
	/// <summary>
	/// PatternConverter is an abtract class that provides the
	/// formatting functionality that derived classes need.
	/// </summary>
	/// <remarks>
	/// Conversion specifiers in a conversion patterns are parsed to
	/// individual PatternConverters. Each of which is responsible for
	/// converting a logging event in a converter specific manner.
	/// </remarks>
	public abstract class PatternConverter
	{
		private PatternConverter m_next;
		private int m_min = -1;
		private int m_max = 0x7FFFFFFF;
		private bool m_leftAlign = false;

		/// <summary>
		/// Default constructor
		/// </summary>
		protected PatternConverter() 
		{  
		}
  
		/// <summary>
		/// Construct the pattern converter using a formatting info object
		/// </summary>
		/// <param name="fi">the formatting info object to use</param>
		protected PatternConverter(FormattingInfo fi) 
		{
			m_min = fi.Min;
			m_max = fi.Max;
			m_leftAlign = fi.LeftAlign;
		}

		/// <summary>
		/// the next patter converter in the chain
		/// </summary>
		public PatternConverter Next
		{
			get { return m_next; }
			set { m_next = value; }
		}

		/// <summary>
		/// Derived pattern converters must override this method in order to
		/// convert conversion specifiers in the correct way.
		/// </summary>
		/// <param name="loggingEvent"></param>
		/// <returns></returns>
		abstract protected String Convert(LoggingEvent loggingEvent);

		/// <summary>
		/// A template method for formatting in a converter specific way.
		/// </summary>
		/// <param name="sbuf"></param>
		/// <param name="e"></param>
		virtual public void Format(StringBuilder sbuf, LoggingEvent e) 
		{
			string s = Convert(e);

			if(s == null) 
			{
				if(0 < m_min)
				{
					SpacePad(sbuf, m_min);
				}
				return;
			}

			int len = s.Length;

			if(len > m_max)
			{
				sbuf.Append(s.Substring(len-m_max));
			}
			else if(len < m_min) 
			{
				if(m_leftAlign) 
				{	
					sbuf.Append(s);
					SpacePad(sbuf, m_min-len);
				}
				else 
				{
					SpacePad(sbuf, m_min-len);
					sbuf.Append(s);
				}
			}
			else
			{
				sbuf.Append(s);
			}
		}	

		static readonly string[] SPACES = {	" ", "  ", "    ", "        ",			//1,2,4,8 spaces
											"                ",						// 16 spaces
											"                                " };	// 32 spaces

		/// <summary>
		/// Fast space padding method.
		/// </summary>
		/// <param name="sbuf"></param>
		/// <param name="length"></param>
		public void SpacePad(StringBuilder sbuf, int length) 
		{
			while(length >= 32) 
			{
				sbuf.Append(SPACES[5]);
				length -= 32;
			}
    
			for(int i = 4; i >= 0; i--) 
			{	
				if((length & (1<<i)) != 0) 
				{
					sbuf.Append(SPACES[i]);
				}
			}
		}	
	}
}

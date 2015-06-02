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

namespace log4net.helpers
{
	/// <summary>
	/// Format the date specifed as a string: 'YYYY-MM-dd HH:mm:ss,SSS'
	/// </summary>
	public class ISO8601DateFormatter : AbsoluteTimeDateFormatter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ISO8601DateFormatter()
		{
		}

		/// <summary>
		/// Format the date specifed as a string: 'YYYY-MM-dd HH:mm:ss,SSS'
		/// </summary>
		/// <param name="date">the date to format</param>
		/// <param name="sbuf">the string builder to write to</param>
		/// <returns>the string builder passed</returns>
		override public StringBuilder FormatDate(DateTime date, StringBuilder sbuf)
		{
			sbuf.Append(date.Year);

			sbuf.Append('-');
			int month = date.Month;
			if (month < 10)
			{
				sbuf.Append('0');
			}
			sbuf.Append(month);
			sbuf.Append('-');

			int day = date.Day;
			if(day < 10) 
			{
				sbuf.Append('0');
			}
			sbuf.Append(day);
			sbuf.Append(' ');    

			return base.FormatDate(date, sbuf);		
		}
	}
}

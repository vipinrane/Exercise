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
	/// Formats a DateTime in the format "dd MMM YYYY HH:mm:ss,SSS" for example, "06 Nov 1994 15:49:37,459".
	/// </summary>
	public class DateTimeDateFormatter : AbsoluteTimeDateFormatter
	{
		/// <summary>
		/// Cache the list of month names
		/// </summary>
		private readonly string[] m_shortMonths;

		/// <summary>
		/// Constructor
		/// </summary>
		public DateTimeDateFormatter()
		{
			m_shortMonths = System.Globalization.DateTimeFormatInfo.InvariantInfo.AbbreviatedMonthNames;
		}

		/// <summary>
		/// Formats the date as: "dd MMM YYYY HH:mm:ss,SSS"
		/// </summary>
		/// <remarks>
		/// Formats a DateTime in the format "dd MMM YYYY HH:mm:ss,SSS" for example, "06 Nov 1994 15:49:37,459".
		/// </remarks>
		/// <param name="date">the date to format</param>
		/// <param name="sbuf">the string builder to write to</param>
		/// <returns>the string builder passed</returns>
		override public StringBuilder FormatDate(DateTime date, StringBuilder sbuf)
		{
			int day = date.Day;
			if(day < 10) 
			{
				sbuf.Append('0');
			}
			sbuf.Append(day);
			sbuf.Append(' ');        

			sbuf.Append(m_shortMonths[date.Month]);
			sbuf.Append(' ');    

			sbuf.Append(date.Year);
			sbuf.Append(' ');

			return base.FormatDate(date, sbuf);
		}
	}
}

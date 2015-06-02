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
	/// Summary description for IDateFormatter.
	/// </summary>
	public class SimpleDateFormatter : IDateFormatter
	{
		private readonly string m_formatString;

		/// <summary>
		/// Construct a simple date formatter with a format string
		/// </summary>
		/// <remarks>
		/// The format string must be compatible with the options
		/// that can be supplied to <see cref="DateTime.ToString"/>.
		/// </remarks>
		/// <param name="formatString">the format string</param>
		public SimpleDateFormatter(string formatString)
		{
			m_formatString = formatString;
		}

		/// <summary>
		/// Format the date using <see cref="DateTime.ToString"/>
		/// </summary>
		/// <param name="date">the date to convert to a string</param>
		/// <param name="buf">the builder to write to</param>
		/// <returns>the builder passed</returns>
		public StringBuilder FormatDate(DateTime date, StringBuilder buf)
		{
			buf.Append(date.ToString(m_formatString, System.Globalization.DateTimeFormatInfo.InvariantInfo));
			return buf;
		}
	}
}

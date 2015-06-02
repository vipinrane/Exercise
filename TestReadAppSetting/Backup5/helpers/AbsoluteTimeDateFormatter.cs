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
	/// Formats a {@link Date} in the format "HH:mm:ss,SSS" for example, "15:49:37,459".
	/// </summary>
	public class AbsoluteTimeDateFormatter : IDateFormatter
	{
		/// <summary>
		/// String constant used to specify AbsoluteTimeDateFormat in layouts. Current value is <b>ABSOLUTE</b>.
		/// </summary>
		public const string ABS_TIME_DATE_FORMAT = "ABSOLUTE";

		/// <summary>
		/// String constant used to specify DateTimeDateFormat in layouts.  Current value is <b>DATE</b>.
		/// </summary>
		public const string DATE_AND_TIME_DATE_FORMAT = "DATE";

		/// <summary>
		/// String constant used to specify ISO8601DateFormat in layouts. Current value is <b>ISO8601</b>.
		/// </summary>
		public const string ISO8601_DATE_FORMAT = "ISO8601";

		/// <summary>
		/// Render the date into a string. Format is "HH:mm:ss,SSS"
		/// </summary>
		/// <param name="date">the date to render into a string</param>
		/// <param name="sbuf">a string builder to write to</param>
		/// <returns>the string builder passed</returns>
		virtual public StringBuilder FormatDate(DateTime date, StringBuilder sbuf)
		{
			int hour = date.Hour;
			if(hour < 10) 
			{
				sbuf.Append('0');
			}
			sbuf.Append(hour);
			sbuf.Append(':');

			int mins = date.Minute;
			if(mins < 10) 
			{
				sbuf.Append('0');
			}
			sbuf.Append(mins);
			sbuf.Append(':');
    
			int secs = date.Second;
			if(secs < 10) 
			{
				sbuf.Append('0');
			}
			sbuf.Append(secs);
			sbuf.Append(',');
    
			int millis = date.Millisecond;
			if(millis < 100) 
			{
				sbuf.Append('0');
			}
			if(millis < 10) 
			{
				sbuf.Append('0');
			}
    		sbuf.Append(millis);

			return sbuf;
		}
	}
}

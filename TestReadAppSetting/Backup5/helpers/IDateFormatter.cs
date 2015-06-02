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
	public interface IDateFormatter
	{
		/// <summary>
		/// Format the date specifed as a string
		/// </summary>
		/// <param name="date">the date to format</param>
		/// <param name="buf">the string builder to write to</param>
		/// <returns>the string builder passed</returns>
		StringBuilder FormatDate(DateTime date, StringBuilder buf);
	}
}

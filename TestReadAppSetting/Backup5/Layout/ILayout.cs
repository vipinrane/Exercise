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
using log4net.spi;

namespace log4net.Layout
{
	/// <summary>
	/// Extend this abstract class to create your own log layout format.
	/// </summary>
	public interface ILayout
	{
		/// <summary>
		/// Implement this method to create your own layout format.
		/// </summary>
		/// <param name="loggingEvent">The event to format</param>
		/// <returns>returns the formatted event</returns>
		string Format(LoggingEvent loggingEvent);

		/// <summary>
		/// Returns the content type output by this layout. The base class returns "text/plain".
		/// </summary>
		/// <returns>the content type</returns>
		string ContentType { get; }

		/// <summary>
		/// Returns the header for the layout format. The base class returns <code>null</code>.
		/// </summary>
		/// <returns></returns>
		string Header { get; }

		/// <summary>
		/// Returns the footer for the layout format. The base class returns <code>null</code>.
		/// </summary>
		/// <returns></returns>
		string Footer { get; }

		/// <summary>
		/// If the layout handles the exception object contained within
		/// <see cref="LoggingEvent"/>, then the layout should return
		/// <code>false</code>. Otherwise, if the layout ignores exception
		/// object, then the layout should return <code>true</code>.
		/// </summary>
		/// <returns></returns>
		bool IgnoresException { get; }
	}
}

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
using log4net.Layout;
using log4net.Filter;

namespace log4net.Appender
{
	/// <summary>
	/// Implement this interface for your own strategies for printing log statements.
	/// </summary>
	public interface IAppender
	{
		/// <summary>
		/// Add a filter to the end of the filter list.
		/// </summary>
		void AddFilter(IFilter newFilter);

		/// <summary>
		/// Returns the head Filter. The Filters are organized in a linked list
		/// and so all Filters on this Appender are available through the result.
		/// </summary>
		/// <returns>the head Filter or null, if no Filters are present</returns>
		IFilter Filter { get; }

		/// <summary>
		/// Clear the list of filters by removing all the filters in it.
		/// </summary>
		void ClearFilters();

		/// <summary>
		/// Release any resources allocated within the appender such as file handles, 
		/// network connections, etc. 
		/// It is a programming error to append to a closed appender.
		/// </summary>
		void Close();

		/// <summary>
		/// Log in Appender specific way
		/// </summary>
		/// <param name="loggingEvent">the event to log</param>
		void DoAppend(LoggingEvent loggingEvent);

		/// <summary>
		/// Get the name of this appender. The name uniquely identifies the	appender.
		/// </summary>
		/// <returns>the name of the appender</returns>
		string Name { get; set; }

		/// <summary>
		/// Returns the <see cref="ErrorHandler"/> for this appender.
		/// </summary>
		/// <returns>The error handler used</returns>
		IErrorHandler ErrorHandler { get; set; }

		/// <summary>
		/// Returns this appenders layout.
		/// </summary>
		/// <returns>Gets the layout to use with the appender</returns>
		ILayout Layout { get; set; }

		/// <summary>
		/// Configurators call this method to determine if the appender 
		/// requires a layout. If this method returns <code>true</code>, 
		/// meaning that layout is required, then the configurator will 
		/// configure an layout using the configuration information at 
		/// its disposal.  If this method returns <code>false</code>, 
		/// meaning that a layout is not required, then layout configuration 
		/// will be	skipped even if there is available layout configuration 
		/// information at the disposal of the configurator..
		/// 
		/// <p>In the rather exceptional case, where the appender 
		/// implementation admits a layout but can also work without it, 
		/// then the appender should return <code>true</code>.</p>
		/// </summary>
		/// <returns>test if the appender requires layout</returns>
		bool RequiresLayout();

	}
}

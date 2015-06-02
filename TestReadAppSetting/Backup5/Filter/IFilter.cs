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

using log4net.spi;

namespace log4net.Filter
{
	/// <summary>
	/// Users should extend this class to implement customized logging
	/// event filtering. Note that <see cref="Category"/> and 
	/// <see cref="log4net.Appender.AppenderSkeleton"/>, the parent class of all standard
	/// appenders, have built-in filtering rules. It is suggested that you
	/// first use and understand the built-in rules before rushing to write
	/// your own custom filters.
	/// </summary>
	/// <remarks>
	/// <p>This abstract class assumes and also imposes that filters be
	/// organized in a linear chain. The <see cref="Decide"/>
	/// method of each filter is called sequentially, in the order of their 
	/// addition to the chain.</p>
	/// 
	/// <p>The <see cref="Decide"/> method must return one
	/// of the integer constants DENY, NEUTRAL or ACCEPT.</p>
	/// 
	/// <p>If the value DENY is returned, then the log event is dropped 
	/// immediately without consulting with the remaining filters. </p>
	/// 
	/// <p>If the value NEUTRAL is returned, then the next filter
	/// in the chain is consulted. If there are no more filters in the
	/// chain, then the log event is logged. Thus, in the presence of no
	/// filters, the default behaviour is to log all logging events.</p>
	/// 
	/// <p>If the value ACCEPT is returned, then the log
	/// event is logged without consulting the remaining filters. </p>
	/// 
	/// <p>The philosophy of log4net filters is largely inspired from the
	/// Linux ipchains. </p>
	/// </remarks>
	public interface IFilter : IOptionHandler
	{
		/// <summary>
		/// Decide if the logging event should be logged through an appender.
		/// </summary>
		/// <remarks>
		/// <p>If the decision is <code>DENY</code>, then the event will be
		/// dropped. If the decision is <code>NEUTRAL</code>, then the next
		/// filter, if any, will be invoked. If the decision is ACCEPT then
		/// the event will be logged without consulting with other filters in
		/// the chain.</p>
		/// </remarks>
		/// <param name="loggingEvent">The LoggingEvent to decide upon</param>
		/// <returns>The decision of the filter</returns>
		int Decide(LoggingEvent loggingEvent);

		/// <summary>
		/// Property to get and set the next filter in the filter
		/// chain of responsability.
		/// </summary>
		IFilter Next { get; set; }

	}
}

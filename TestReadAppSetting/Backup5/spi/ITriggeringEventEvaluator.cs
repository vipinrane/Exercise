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

namespace log4net.spi
{
	/// <summary>
	/// Implementions of this interface allow certain appenders to decide
	/// when to perform an appender specific action.
	/// </summary>
	public interface ITriggeringEventEvaluator
	{
		/// <summary>
		/// Is this the triggering event?
		/// </summary>
		/// <param name="loggingEvent">The event to chech</param>
		/// <returns>true if this event triggers the action</returns>
		bool IsTriggeringEvent(LoggingEvent loggingEvent);
	}
}

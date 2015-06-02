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
	/// This filter drops all logging events. 
	/// </summary>
	/// <remarks>
	/// You can add this filter to the end of a filter chain to
	/// switch from the default "accept all unless instructed otherwise"
	/// filtering behaviour to a "deny all unless instructed otherwise"
	/// behaviour.	
	/// </remarks>
	public class DenyAllFilter : FilterSkeleton
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public DenyAllFilter()
		{
		}

		/// <summary>
		/// Always returns the integer constant <see cref="FilterSkeleton.DENY"/>
		/// </summary>
		/// <remarks>
		/// Ignores the event being logged and just returns
		/// DENY. This can be used to change the default filter
		/// chain behaviour from ACCEPT to DENY. This filter
		/// should only be used as the last filter in the chain
		/// as any further filters will be ignored!
		/// </remarks>
		/// <param name="loggingEvent">he LoggingEvent to filter</param>
		/// <returns>Always returns <see cref="FilterSkeleton.DENY"/></returns>
		override public int Decide(LoggingEvent loggingEvent) 
		{
			return FilterSkeleton.DENY;
		}
	}
}

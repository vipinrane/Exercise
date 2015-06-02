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
using log4net.helpers;

namespace log4net.Filter
{
	/// <summary>
	/// This is a simple filter based on priority matching.
	/// </summary>
	/// <remarks>
	/// <p>The filter admits three options <b>PriorityMin</b> and <b>PriorityMax</b>
	/// that determine the range of priorites that are matched, and
	/// <b>AcceptOnMatch</b>. If there is a match between the range
	/// of priorities and the priority of the LoggingEvent, then the 
	/// Decide method returns ACCEPT in case the <b>AcceptOnMatch</b> 
	/// option value is set to <code>true</code>, if it is <code>false</code>
	///  then DENY is returned. If there is no match, DENY is returned.</p>
	/// </remarks>
	public class PriorityRangeFilter : FilterSkeleton
	{
		/// <summary>
		/// Flag to indicate the behaviour when matching a priority
		/// </summary>
		private bool m_acceptOnMatch = true;

		/// <summary>
		/// the minimum priority value to match
		/// </summary>
		private Priority m_priorityMin;

		/// <summary>
		/// the maximum priority value to match
		/// </summary>
		private Priority m_priorityMax;

		/// <summary>
		/// Default constuctor
		/// </summary>
		public PriorityRangeFilter()
		{
		}

		/// <summary>
		/// The AcceptOnMatch property is a flag that determines
		/// the behaviour when a matching Priority is found. If the
		/// flag is set to true then the filter will ACCEPT the 
		/// logging event, otherwise it will NEUTRAL the event.
		/// </summary>
		public bool AcceptOnMatch
		{
			get { return m_acceptOnMatch; }
			set { m_acceptOnMatch = value; }
		}

		/// <summary>
		/// Set the minimum matched priority
		/// </summary>
		public string PriorityMin
		{
			get { return m_priorityMin == null ? null : m_priorityMin.ToString(); }
			set { m_priorityMin = OptionConverter.ToPriority(value, null); }
		}

		/// <summary>
		/// Sets the maximum matched priority
		/// </summary>
		public string PriorityMax
		{
			get { return m_priorityMax == null ? null : m_priorityMax.ToString(); }
			set { m_priorityMax = OptionConverter.ToPriority(value, null); }
		}

		/// <summary>
		/// Check if the event should be logged.
		/// </summary>
		/// <remarks>
		/// If the priority of the logging event is outside the range
		/// matched by this filter then <see cref="FilterSkeleton.DENY"/>
		/// is returned. If the priority is matched then the value of
		/// <see cref="AcceptOnMatch"/> is checked. If it is true then
		/// <see cref="FilterSkeleton.ACCEPT"/> is returned, otherwise
		/// <see cref="FilterSkeleton.NEUTRAL"/> is returned.
		/// </remarks>
		/// <param name="loggingEvent">the logging event to check</param>
		/// <returns>see remarks</returns>
		override public int Decide(LoggingEvent loggingEvent) 
		{
			if(m_priorityMin != null) 
			{
				if (loggingEvent.Priority < m_priorityMin) 
				{
					// priority of event is less than minimum
					return FilterSkeleton.DENY;
				}
			}

			if(m_priorityMax != null) 
			{
				if (loggingEvent.Priority > m_priorityMax) 
				{
					// priority of event is greater than maximum
					return FilterSkeleton.DENY;
				}
			}

			if (m_acceptOnMatch) 
			{
				// this filter set up to bypass later filters and always return
				// accept if priority in range
				return FilterSkeleton.ACCEPT;
			}
			else 
			{
				// event is ok for this filter; allow later filters to have a look..
				return FilterSkeleton.NEUTRAL;
			}
		}

	}
}

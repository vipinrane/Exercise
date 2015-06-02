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
	/// This is a very simple filter based on priority matching.
	/// </summary>
	/// <remarks>
	/// <p>The filter admits two options <b>PriorityToMatch</b> and
	/// <b>AcceptOnMatch</b>. If there is an exact match between the value
	/// of the PriorityToMatch option and the priority of the 
	/// LoggingEvent, then the Decide method returns ACCEPT} in 
	/// case the <b>AcceptOnMatch</b> option value is set
	/// to <code>true</code>, if it is <code>false</code> then 
	/// DENY is returned.</p>
	/// </remarks>
	public class PriorityMatchFilter : FilterSkeleton
	{
		/// <summary>
		/// flag to indicate if the filter should ACCEPT on a match
		/// </summary>
		private bool m_acceptOnMatch = true;

		/// <summary>
		/// the Priority to match agains
		/// </summary>
		private Priority m_priorityToMatch;

		/// <summary>
		/// Default constructor
		/// </summary>
		public PriorityMatchFilter()
		{
		}

		/// <summary>
		/// The AcceptOnMatch property is a flag that determines
		/// the behaviour when a matching Priority is found. If the
		/// flag is set to true then the filter will ACCEPT the 
		/// logging event, otherwise it will DENY the event.
		/// </summary>
		public bool AcceptOnMatch
		{
			get { return m_acceptOnMatch; }
			set { m_acceptOnMatch = value; }
		}

		/// <summary>
		/// The priority that the filter will match
		/// </summary>
		public string PriorityToMatch
		{
			get { return m_priorityToMatch == null ? null : m_priorityToMatch.ToString(); }
			set { m_priorityToMatch = OptionConverter.ToPriority(value, null); }
		}

		/// <summary>
		/// Tests if the priority of the logging event matches that of the filter
		/// </summary>
		/// <remarks>
		/// If the priority of the event matches the priority of the
		/// filter then the result of the function depends on the
		/// value of <see cref="AcceptOnMatch"/>. If it is true then
		/// the function will return ACCEPT, it it is false then it
		/// will return DENY. If the priority does not match then
		/// the result will be the opposite of when it does match.
		/// </remarks>
		/// <param name="loggingEvent">the event to filter</param>
		/// <returns>see remarks</returns>
		override public int Decide(LoggingEvent loggingEvent) 
		{
			if(m_priorityToMatch == null) 
			{
				return FilterSkeleton.NEUTRAL;
			}
    
			bool matchOccured = false;
			if(m_priorityToMatch == loggingEvent.Priority) 
			{
				matchOccured = true;
			}

			if(m_acceptOnMatch ^ matchOccured) 
			{
				return FilterSkeleton.DENY;
			} 
			else 
			{
				return FilterSkeleton.ACCEPT;
			}
		}

	}
}

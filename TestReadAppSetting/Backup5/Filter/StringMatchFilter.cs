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
	/// Simple filter to match a string in the rendered message
	/// </summary>
	public class StringMatchFilter : FilterSkeleton
	{
		/// <summary>
		/// Flag to indicate the behaviour when we have a match
		/// </summary>
		private bool m_acceptOnMatch = true;

		/// <summary>
		/// The string to substring match against the message
		/// </summary>
		private string m_stringToMatch;

		/// <summary>
		/// Default constructor
		/// </summary>
		public StringMatchFilter()
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
		/// The string that will be substring matched against
		/// the rendered message. If the message contains this
		/// string then the filter will match.
		/// </summary>
		public string StringToMatch
		{
			get { return m_stringToMatch; }
			set { m_stringToMatch = value; }
		}

		/// <summary>
		/// Check if this filter should allow the event to be logged
		/// </summary>
		/// <remarks>
		/// The rendered message is mached agains the <see cref="StringToMatch"/>.
		/// If the <see cref="StringToMatch"/> occurs as a substring within
		/// the message then a match will have occured. If no match occures
		/// this function will return <see cref="FilterSkeleton.NEUTRAL"/>
		/// allowing other filters to check the event. If a match occurs then
		/// the value of <see cref="AcceptOnMatch"/> is checked. If it is
		/// true then <see cref="FilterSkeleton.ACCEPT"/> is returned otherwise
		/// <see cref="FilterSkeleton.DENY"/> is returned.
		/// </remarks>
		/// <param name="loggingEvent">the event being logged</param>
		/// <returns>see remarks</returns>
		override public int Decide(LoggingEvent loggingEvent) 
		{
			string msg = loggingEvent.RenderedMessage;

			if(msg == null || m_stringToMatch == null)
			{
				return FilterSkeleton.NEUTRAL;
			}
    
			if(msg.IndexOf(m_stringToMatch) == -1) 
			{
				return FilterSkeleton.NEUTRAL;
			} 
			else 
			{ 
				// we've got a match
				if(m_acceptOnMatch) 
				{
					return FilterSkeleton.ACCEPT;
				} 
				else 
				{
					return FilterSkeleton.DENY;
				}
			}
		}

	}
}

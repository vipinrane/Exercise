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
using log4net.Appender;

namespace log4net.helpers
{
	/// <summary>
	/// A straightforward implementation of the <see cref="IAppenderAttachable"/> interface.
	/// </summary>
	public class AppenderAttachedImpl : IAppenderAttachable
	{
		/// <summary>
		/// Array of appenders
		/// </summary>
		protected System.Collections.ArrayList m_appenderList;

		/// <summary>
		/// Default constructor
		/// </summary>
		public AppenderAttachedImpl()
		{
		}

		/// <summary>
		/// Attach an appender. If the appender is already in the list in won't be added again.
		/// </summary>
		/// <param name="newAppender">the appender to add</param>
		public void AddAppender(IAppender newAppender) 
		{
			// Null values for newAppender parameter are strictly forbidden.
			if(newAppender == null)
			{
				return;
			}
    
			if(m_appenderList == null) 
			{
				m_appenderList = new System.Collections.ArrayList(1);
			}
			if(!m_appenderList.Contains(newAppender))
			{
				m_appenderList.Add(newAppender);
			}
		}

		/// <summary>
		/// Call the <code>DoAppend</code> method on all attached appenders.
		/// </summary>
		/// <param name="loggingEvent">the event being logged</param>
		/// <returns>the number of appenders called</returns>
		public int AppendLoopOnAppenders(LoggingEvent loggingEvent) 
		{
			if(m_appenderList == null) 
			{
				return 0;
			}

			foreach(IAppender appender in m_appenderList)
			{
				appender.DoAppend(loggingEvent);
			}
			return m_appenderList.Count;
		}

		/// <summary>
		/// Get all attached appenders as an Enumeration. If there are 
		/// no attached appenders <code>null</code> is returned.
		/// </summary>
		/// <returns>An enumeration of attached appenders</returns>
		public System.Collections.IEnumerator GetAllAppenders() 
		{
			if(m_appenderList == null)
			{
				return null;
			}
			else 
			{
				return m_appenderList.GetEnumerator();
			}
		}

		/// <summary>
		/// Look for an attached appender named as <code>name</code>.
		/// </summary>
		/// <remarks>
		/// Return the appender with that name if in the list. Return null otherwise.
		/// </remarks>
		/// <param name="name">name of the appender to get</param>
		/// <returns>the appender with the name specified, or null</returns>
		public IAppender GetAppender(string name) 
		{
			if(m_appenderList == null || name == null)
			{
				return null;
			}

			foreach(IAppender appender in m_appenderList)
			{
				if (name == appender.Name)
				{
					return appender;
				}
			}
			return null;   
		}

		/// <summary>
		/// Remove all previously attached appenders
		/// </summary>
		public void RemoveAllAppenders() 
		{
			if(m_appenderList != null) 
			{
				foreach(IAppender appender in m_appenderList)
				{
					appender.Close();
				}
				m_appenderList.Clear();
				m_appenderList = null;      
			}
		}

		/// <summary>
		/// Remove the appender passed as parameter form the list of attached appenders
		/// </summary>
		/// <param name="appender">the appender object to remove</param>
		public void RemoveAppender(IAppender appender) 
		{
			if(appender == null || m_appenderList == null) 
			{
				return;
			}
			m_appenderList.Remove(appender);    
		}

		/// <summary>
		/// Remove the appender with the name passed as parameter form the list of appenders
		/// </summary>
		/// <param name="name">the name of the appender to remove</param>
		public void RemoveAppender(string name) 
		{
			RemoveAppender(GetAppender(name));
		}
	}
}

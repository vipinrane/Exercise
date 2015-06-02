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
using log4net.helpers;

namespace log4net.spi
{
	/// <summary>
	/// The internal representation of logging events. When an affirmative
	/// decision is made to log then a <code>LoggingEvent</code> instance
	/// is created. This instance is passed around to the different log4net
	/// components.
	/// </summary>
	/// <remarks>
	/// This class is of concern to those wishing to extend log4net. 
	/// </remarks>
	public class LoggingEvent
	{
		#region Static Member Variables

		private static readonly DateTime s_startTime = DateTime.Now;

		#endregion

		#region Member Variables

		/// <summary>
		/// Fully qualified name of the calling category class.
		/// </summary>
		public readonly String m_fqnOfCategoryClass;

		/// <summary>
		/// The category of the logging event. The category field is not
		/// serialized for performance reasons.
		/// It is set by the LoggingEvent constructor or set by a remote
		/// entity after deserialization.
		/// </summary>
		public Category m_category;

		/// <summary>
		/// The category name.
		/// </summary>
		public readonly string m_categoryName;

		/// <summary>
		/// Priority of logging event. Priority cannot be serializable
		/// because it is a flyweight.  Due to its special seralization it
		/// cannot be declared final either.
		/// </summary>
		public Priority m_priority;

		/// <summary>
		/// The nested diagnostic context (NDC) of logging event.
		/// </summary>
		private string m_ndc;

		/// <summary>
		/// Have we tried to do an NDC lookup? If we did, there is no need
		/// to do it again.  Note that its value is always false when
		/// serialized. Thus, a receiving SocketNode will never use it's own
		/// (incorrect) NDC. See also writeObject method.
		/// </summary>
		private bool m_ndcLookupRequired = true;

		/// <summary>
		/// The application supplied message of logging event.
		/// </summary>
		private object m_message;

		/// <summary>
		/// The application supplied message rendered through 
		/// the log4net object rendering mechanism.
		/// </summary>
		private string m_renderedMessage;

		/// <summary>
		/// The name of thread in which this logging event was generated
		/// </summary>
		private string m_threadName;

		/// <summary>
		/// This variable contains information about this event's throwable 
		/// </summary>
		private System.Diagnostics.StackTrace m_stackTraceInfo;

		/// <summary>
		/// The number of milliseconds elapsed from 1/1/1970 until logging event was created.
		/// </summary>
		private readonly DateTime m_timeStamp;

		/// <summary>
		/// Location information for the caller.
		/// </summary>
		private LocationInfo m_locationInfo;

		#endregion

		/// <summary>
		/// Instantiate a LoggingEvent from the supplied parameters.
		/// </summary>
		/// <remarks>
		/// Except <see cref="m_timeStamp"/> all the other fields of
		/// <code>LoggingEvent</code> are filled when actually needed.
		/// </remarks>
		/// <param name="fqnOfCategoryClass"></param>
		/// <param name="category">The category of this event</param>
		/// <param name="priority">The priority of this event</param>
		/// <param name="message">The message of this event</param>
		/// <param name="throwable">The throwable of this event</param>
		public LoggingEvent(string fqnOfCategoryClass, Category category, Priority priority, object message, Exception throwable) 
		{
			m_fqnOfCategoryClass = fqnOfCategoryClass;
			m_category = category;
			m_categoryName = category.Name;
			m_priority = priority;
			m_message = message;
			if(throwable != null) 
			{
				m_stackTraceInfo = new System.Diagnostics.StackTrace(throwable, 3, true);
			}
			m_timeStamp = DateTime.Now;
		}  

		/// <summary>
		/// The Priority of the logging event
		/// </summary>
		public Priority Priority
		{
			get { return m_priority; } 
		}

		/// <summary>
		/// The time of the event
		/// </summary>
		public DateTime TimeStamp
		{
			get { return m_timeStamp; }
		}

		/// <summary>
		/// The name of the category that logged the event
		/// </summary>
		public string CategoryName
		{
			get { return m_categoryName; }
		}

		/// <summary>
		/// Set the location information for this logging event. The collected
		/// information is cached for future use.
		/// </summary>
		/// <returns></returns>
		public LocationInfo LocationInformation
		{
			get
			{
				if(m_locationInfo == null) 
				{
					m_locationInfo = new LocationInfo(m_fqnOfCategoryClass);
				}
				return m_locationInfo;
			}
		}

		/// <summary>
		/// Return the message for this logging event.
		/// </summary>
		/// <remarks>
		/// Before serialization, the returned object is the message
		/// passed by the user to generate the logging event. After
		/// serialization, the returned value equals the String form of the
		/// message possibly after object rendering. 
		/// </remarks>
		/// <returns></returns>
		public object Message
		{
			get
			{
				if(m_message != null) 
				{
					return m_message;
				} 
				else 
				{
					return RenderedMessage;
				}
			}
		}

		/// <summary>
		/// Get the NDC
		/// </summary>
		/// <returns></returns>
		public string NestedContext
		{
			get
			{
				if(m_ndcLookupRequired) 
				{
					m_ndcLookupRequired = false;
					m_ndc = NDC.Get();
				}
				return m_ndc; 
			}
		}

		/// <summary>
		/// Get the message (rendered through the Layouts)
		/// </summary>
		/// <returns></returns>
		public string RenderedMessage
		{
			get
			{
				if(m_renderedMessage == null && m_message != null) 
				{
					if(m_message is string)
					{
						m_renderedMessage = (string)m_message;
					}
					else 
					{
						m_renderedMessage = m_category.Hierarchy.RendererMap.FindAndRender(m_message);
					}
				}
				return m_renderedMessage;
			}
		}  

		/// <summary>
		/// Returns the time when the application started, in milliseconds elapsed since 01.01.1970.
		/// </summary>
		/// <returns></returns>
		public static DateTime StartTime
		{
			get
			{
				return s_startTime;
			}
		}

		/// <summary>
		/// Get the name of the current thread
		/// </summary>
		/// <returns></returns>
		public String ThreadName
		{
			get
			{
				if(m_threadName == null)
				{
					m_threadName = System.Threading.Thread.CurrentThread.Name;
				}
				return m_threadName;
			}
		}

		/// <summary>
		/// Returns the throwable information contained within this
		/// event. May be <code>null</code> if there is no such information.
		/// </summary>
		/// <returns></returns>
		public System.Diagnostics.StackTrace ThrowableInformation
		{
			get
			{
				return m_stackTraceInfo;
			}
		}

		/// <summary>
		/// Return this event's exception's string[] representaion.
		/// </summary>
		/// <returns></returns>
		public string[] GetExceptionStrRep() 
		{
			if(m_stackTraceInfo == null)
			{
				return null;
			}
			else 
			{
				string[] result = new string[m_stackTraceInfo.FrameCount];

				for (int i=0; i<m_stackTraceInfo.FrameCount; i++)
				{
					result[i] = m_stackTraceInfo.GetFrame(i).ToString();
				}

				return result;
			}
		}
	}
}

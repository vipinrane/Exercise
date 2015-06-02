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
using log4net.Layout;
using log4net.Filter;
using log4net.spi;
using log4net.helpers;

namespace log4net.Appender
{
	/// <summary>
	/// Abstract superclass of the other appenders in the package. 
	/// This class provides the code for common functionality, such 
	/// as support for threshold filtering and support for general filters.
	/// </summary>
	public abstract class AppenderSkeleton : IAppender, IOptionHandler
	{
		#region Member Variables

		/// <summary>
		/// The layout variable does not need to be set if the appender implementation has its own layout.
		/// </summary>
		protected ILayout m_layout;

		/// <summary>
		/// Appenders are named.
		/// </summary>
		protected String m_name;

		/// <summary>
		/// There is no priority threshold filtering by default.
		/// </summary>
		protected Priority m_threshold;

		/// <summary>
		/// It is assumed and enforced that errorHandler is never null.
		/// </summary>
		protected IErrorHandler m_errorHandler = new OnlyOnceErrorHandler();

		/// <summary>
		/// The first filter in the filter chain. Set to <code>null</code> initially.
		/// </summary>
		protected IFilter m_headFilter;

		/// <summary>
		/// The last filter in the filter chain.
		/// </summary>
		protected IFilter m_tailFilter;

		/// <summary>
		/// Is this appender closed? 
		/// </summary>
		protected bool m_closed = false;

		#endregion


		/// <summary>
		/// Derived appenders should override this method if option structure requires it.
		/// </summary>
		public virtual void ActivateOptions() 
		{
		}

		/// <summary>
		/// Add a filter to end of the filter list.
		/// </summary>
		/// <param name="newFilter">The filter to add</param>
		public virtual void AddFilter(IFilter newFilter) 
		{
			if(m_headFilter == null) 
			{
				m_headFilter = m_tailFilter = newFilter;
			} 
			else 
			{
				m_tailFilter.Next = newFilter;
				m_tailFilter = newFilter;    
			}
		}

		/// <summary>
		/// Subclasses of <code>AppenderSkeleton</code> should implement this method 
		/// to perform actual logging. See also <see cref="AppenderSkeleton.DoAppend"/> method.
		/// </summary>
		/// <param name="loggingEvent">the event to append</param>
		abstract protected void Append(LoggingEvent loggingEvent);

		/// <summary>
		/// Release any resources allocated within the appender such as file handles, 
		/// network connections, etc. 
		/// It is a programming error to append to a closed appender.
		/// </summary>
		abstract public void Close();

		/// <summary>
		/// Tests if this appender requires an <see cref="log4net.Layout.ILayout">ILayout</see>
		/// object.
		/// </summary>
		/// <remarks>
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
		/// implementation admits a layout but can also work without it, then 
		/// the appender should return <code>true</code>.</p>
		/// </remarks>
		/// <returns>test if the appender requires layout</returns>
		abstract public bool RequiresLayout();


		/// <summary>
		/// Clear the filters chain
		/// </summary>
		public virtual void ClearFilters() 
		{
			m_headFilter = m_tailFilter = null;
		}

		/// <summary>
		/// Finalize this appender by calling the imlenentation's <code>close</code> method.
		/// </summary>
		protected void Finalize() 
		{
			// An appender might be closed then garbage collected. There is no
			// point in closing twice.
			if(!this.m_closed) 
			{
				LogLog.Debug("Finalizing appender named ["+m_name+"].");
				Close();
			}
		}

		/// <summary>
		/// Return the currently set <see cref="ErrorHandler"/> for this Appender.  
		/// </summary>
		/// <returns>the error hanlder used by this appender</returns>
		virtual public IErrorHandler ErrorHandler
		{
			get { return this.m_errorHandler; }
			set
			{
				lock(this)
				{
					if(value == null) 
					{
						// We do not throw exception here since the cause is probably a
						// bad config file.
						LogLog.Warn("You have tried to set a null error-handler.");
					} 
					else 
					{
						m_errorHandler = value;
					}
				}
			}
		}

		/// <summary>
		/// Returns the head Filter.
		/// </summary>
		/// <returns>the filter (chain) used by this appender</returns>
		public IFilter Filter
		{
			get { return m_headFilter; }
		}

		/// <summary>
		/// Return the first filter in the filter chain for this Appender. The return value may be <code>null</code> if no is filter is set.
		/// </summary>
		/// <returns>The head filter</returns>
		public IFilter GetFirstFilter() 
		{
			return m_headFilter;
		}

		/// <summary>
		/// Returns the layout of this appender. The value may be null.
		/// </summary>
		/// <returns>the layout used by this appender</returns>
		public virtual ILayout Layout
		{
			get { return m_layout; }
			set { m_layout = value; }
		}

		/// <summary>
		/// Returns the name of this Appender.
		/// </summary>
		/// <returns>the unique name of the appender</returns>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		/// <summary>
		/// Check whether the message priority is below the appender's 
		/// threshold. If there is no threshold set, then the return 
		/// value is always <code>true</code>.
		/// </summary>
		/// <param name="priority">Priority to test against</param>
		/// <returns></returns>
		public bool IsAsSevereAsThreshold(Priority priority) 
		{
			return ((m_threshold == null) || priority >= m_threshold);
		}


		/// <summary>
		/// This method performs threshold checks and invokes filters 
		/// before delegating actual logging to the subclasses specific 
		/// <see cref="AppenderSkeleton.Append"/> method.
		/// </summary>
		/// <param name="loggingEvent">the event to log</param>
		public void DoAppend(LoggingEvent loggingEvent) 
		{
			lock(this)
			{
				if(m_closed) 
				{
					LogLog.Error("Attempted to append to closed appender named ["+m_name+"].");
				}

				if(!IsAsSevereAsThreshold(loggingEvent.Priority)) 
				{
					return;
				}

				IFilter f = m_headFilter;

				while(f != null) 
				{
					switch(f.Decide(loggingEvent)) 
					{
						case FilterSkeleton.DENY: 
							return;		// Return without appending
						case FilterSkeleton.ACCEPT:
							f = null;	// Break out of the loop
							break;
						case FilterSkeleton.NEUTRAL:
							f = f.Next;	// Move to next filter
							break;
					}
				}

				this.Append(loggingEvent);
			}
		}

		/// <summary>
		/// Set the threshold priority. All log events with lower priority
		/// than the threshold priority are ignored by the appender.
		/// </summary>
		/// <remarks>
		/// <p>In configuration files this option is specified by setting the
		/// value of the <b>Threshold</b> option to a priority
		/// string, such as "DEBUG", "INFO" and so on.</p>
		/// </remarks>
		public Priority Threshold
		{
			get { return m_threshold; }
			set { m_threshold = value; }
		}
	}
}

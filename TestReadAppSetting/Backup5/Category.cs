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

using log4net.Appender;
using log4net.helpers;
using log4net.spi;

namespace log4net
{
	/// <summary>
	/// This is the central class in the log4net package. One of the
	/// distintive features of log4net are hierarchical categories and their
	/// evaluation.
	/// </summary>
	public class Category : IAppenderAttachable, ILog
	{

		#region Static Member Variables

		/// <summary>
		/// The hierarchy where categories are attached to by default.
		/// </summary>
		private readonly static Hierarchy s_defaultHierarchy = new Hierarchy(new RootCategory(Priority.DEBUG));

		/// <summary>
		/// The fully qualified name of the Category class.
		/// </summary>
		private readonly static string FQCN = typeof(Category).FullName;

		#endregion

		#region Member Variables

		/// <summary>
		/// The name of this category.
		/// </summary>
		private string m_name;  

		/// <summary>
		/// The assigned priority of this category. The 
		/// <code>priority</code> variable need not be 
		/// assined a value in which case it is inherited 
		/// form the hierarchy.
		/// </summary>
		protected Priority m_priority;

		/// <summary>
		/// The parent of this category. All categories have at least one ancestor which is the root category.
		/// </summary>
		protected Category m_parent;

		/// <summary>
		/// Categories need to know what Hierarchy they are in
		/// </summary>
		protected Hierarchy m_hierarchy;

		/// <summary>
		/// Helper implementation of the <see cref="IAppenderAttachable"/> interface
		/// </summary>
		private log4net.helpers.AppenderAttachedImpl m_aai;


		/// <summary>
		/// Additivity is set to true by default, that is children inherit
		/// the appenders of their ancestors by default. If this variable is
		/// set to <code>false</code> then the appenders found in the
		/// ancestors of this category are not used. However, the children
		/// of this category will inherit its appenders, unless the children
		/// have their additivity flag set to <code>false</code> too. See
		/// the user manual for more details.
		/// </summary>
		protected bool m_additive = true;
  
		#endregion

		/// <summary>
		/// This constructor created a new <code>Category</code> instance and
		/// sets its name.
		/// 
		/// <p>It is intended to be used by sub-classes only. You should not
		/// create categories directly.</p>
		/// 
		/// <p>This is incorrect as the DefaultCategoryFactory creates instances
		/// of Category objects.</p>
		/// </summary>
		/// <param name="name">The name of the category</param>
		internal Category(String name) 
		{
			m_name = name;
		}

		/// <summary>
		/// The parent category in the hierarchy
		/// </summary>
		public Category Parent
		{
			get { return m_parent; }
			set { m_parent = value; }
		}

		/// <summary>
		/// Add <code>newAppender</code> to the list of appenders of this
		/// Category instance.
		/// 
		/// <p>If <code>newAppender</code> is already in the list of
		/// appenders, then it won't be added again.</p>
		/// </summary>
		/// <param name="newAppender"></param>
		public void AddAppender(IAppender newAppender) 
		{
			lock(this)
			{
				if(m_aai == null) 
				{
					m_aai = new log4net.helpers.AppenderAttachedImpl();
				}
				m_aai.AddAppender(newAppender);
			}
		}

		/// <summary>
		/// Call the appenders in the hierrachy starting at
		/// <code>this</code>.  If no appenders could be found, emit a
		/// warning.
		/// </summary>
		/// <remarks>
		/// This method calls all the appenders inherited from the
		/// hierarchy circumventing any evaluation of whether to log or not
		/// to log the particular log request.
		/// </remarks>
		/// <param name="loggingEvent">the event to log</param>
		protected void CallAppenders(LoggingEvent loggingEvent) 
		{
			int writes = 0;

			for(Category c = this; c != null; c=c.m_parent) 
			{
				// Protected against simultaneous call to addAppender, removeAppender,...
				lock(c) 
				{
					if(c.m_aai != null) 
					{
						writes += c.m_aai.AppendLoopOnAppenders(loggingEvent);
					}
					if(!c.m_additive) 
					{
						break;
					}
				}
			}
			
			// No appenders in hierarchy, warn user only once.
			if(!m_hierarchy.EmittedNoAppenderWarning && writes == 0) 
			{
				LogLog.Error("No appenders could be found for category (" +this.Name + ").");
				LogLog.Error("Please initialize the log4net system properly.");
				m_hierarchy.EmittedNoAppenderWarning = true;
			}
		}

		/// <summary>
		/// Close all attached appenders implementing the IAppenderAttachable interface.
		/// </summary>
		internal void CloseNestedAppenders() 
		{
			lock(this)
			{
				System.Collections.IEnumerator e = this.GetAllAppenders();
				if(e != null) 
				{
					while(e.MoveNext()) 
					{
						IAppender a = (IAppender) e.Current;
						if(a is IAppenderAttachable) 
						{
							a.Close();
						}
					}
				}
			}
		}

		/// <summary>
		/// If the named category exists (in the default hierarchy) then it
		/// returns a reference to the category, otherwise it returns
		/// <code>null</code>.
		/// </summary>
		/// <param name="name">The fully qualified category name to look for</param>
		/// <returns>The category found, or null</returns>
		public static Category Exists(string name) 
		{    
			return s_defaultHierarchy.Exists(name);
		}

		/// <summary>
		/// Log a message object with the <see cref="Priority.DEBUG"/> priority.
		/// </summary>
		/// <remarks>
		/// <p>This method first checks if this category is <code>DEBUG</code>
		/// enabled by comparing the priority of this category with the 
		/// <see cref="Priority.DEBUG"/> priority. If this category is
		/// <code>DEBUG</code> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="log4net.or.IObjectRenderer"/>. It then proceeds to call all the
		/// registered appenders in this category and also higher in the
		/// hierarchy depending on the value of the additivity flag.</p>
		/// 
		/// <p><b>WARNING</b> Note that passing an <see cref="Exception"/> to this
		/// method will print the name of the <code>Exception</code> but no
		/// stack trace. To print a stack trace use the 
		/// <see cref="Debug(object,Exception)"/> form instead.</p>
		/// 
		/// <p>The conditional compliation constant 'LOG4NET' must be defined
		/// otherwise this method will have no effect.</p>
		/// </remarks>
		/// <param name="message">the message object to log</param>
		public void Debug(object message) 
		{
			Log(Priority.DEBUG, message);
		}
  
		/// <summary>
		/// Log a message object with the <code>DEBUG</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Debug(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">he exception to log, including its stack trace</param>
		public void Debug(object message, Exception t) 
		{
			Log(Priority.DEBUG, message, t);
		}

		/// <summary>
		/// Log a message object with the <see cref="Priority.INFO"/> priority.
		/// </summary>
		/// <remarks>
		/// <p>This method first checks if this category is <code>INFO</code>
		/// enabled by comparing the priority of this category with the 
		/// <see cref="Priority.INFO"/> priority. If this category is
		/// <code>INFO</code> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="log4net.or.IObjectRenderer"/>. It then proceeds to call all the
		/// registered appenders in this category and also higher in the
		/// hierarchy depending on the value of the additivity flag.</p>
		/// 
		/// <p><b>WARNING</b> Note that passing an <see cref="Exception"/> to this
		/// method will print the name of the <code>Exception</code> but no
		/// stack trace. To print a stack trace use the 
		/// <see cref="Info(object,Exception)"/> form instead.</p>
		/// 
		/// <p>The conditional compliation constant 'LOG4NET' must be defined
		/// otherwise this method will have no effect.</p>
		/// </remarks>
		/// <param name="message">the message object to log</param>
		public void Info(object message) 
		{
			Log(Priority.INFO, message);
		}
  
		/// <summary>
		/// Log a message object with the <code>INFO</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Info(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		public void Info(object message, Exception t) 
		{
			Log(Priority.INFO, message, t);
		}

		/// <summary>
		/// Log a message object with the <see cref="Priority.WARN"/> priority.
		/// </summary>
		/// <remarks>
		/// <p>This method first checks if this category is <code>WARN</code>
		/// enabled by comparing the priority of this category with the 
		/// <see cref="Priority.WARN"/> priority. If this category is
		/// <code>WARN</code> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="log4net.or.IObjectRenderer"/>. It then proceeds to call all the
		/// registered appenders in this category and also higher in the
		/// hierarchy depending on the value of the additivity flag.</p>
		/// 
		/// <p><b>WARNING</b> Note that passing an <see cref="Exception"/> to this
		/// method will print the name of the <code>Exception</code> but no
		/// stack trace. To print a stack trace use the 
		/// <see cref="Warn(object,Exception)"/> form instead.</p>
		/// 
		/// <p>The conditional compliation constant 'LOG4NET' must be defined
		/// otherwise this method will have no effect.</p>
		/// </remarks>
		/// <param name="message">the message object to log</param>
		public void Warn(object message) 
		{
			Log(Priority.WARN, message);
		}
  
		/// <summary>
		/// Log a message object with the <code>WARN</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Warn(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		public void Warn(object message, Exception t) 
		{
			Log(Priority.WARN, message, t);
		}

		/// <summary>
		/// Log a message object with the <see cref="Priority.ERROR"/> priority.
		/// </summary>
		/// <remarks>
		/// <p>This method first checks if this category is <code>ERROR</code>
		/// enabled by comparing the priority of this category with the 
		/// <see cref="Priority.ERROR"/> priority. If this category is
		/// <code>ERROR</code> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="log4net.or.IObjectRenderer"/>. It then proceeds to call all the
		/// registered appenders in this category and also higher in the
		/// hierarchy depending on the value of the additivity flag.</p>
		/// 
		/// <p><b>WARNING</b> Note that passing an <see cref="Exception"/> to this
		/// method will print the name of the <code>Exception</code> but no
		/// stack trace. To print a stack trace use the 
		/// <see cref="Error(object,Exception)"/> form instead.</p>
		/// 
		/// <p>The conditional compliation constant 'LOG4NET' must be defined
		/// otherwise this method will have no effect.</p>
		/// </remarks>
		/// <param name="message">the message object to log</param>
		public void Error(object message) 
		{
			Log(Priority.ERROR, message);
		}

		/// <summary>
		/// Log a message object with the <code>ERROR</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Error(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		public void Error(object message, Exception t) 
		{
			Log(Priority.ERROR, message, t);
		}

		/// <summary>
		/// Log a message object with the <see cref="Priority.FATAL"/> priority.
		/// </summary>
		/// <remarks>
		/// <p>This method first checks if this category is <code>FATAL</code>
		/// enabled by comparing the priority of this category with the 
		/// <see cref="Priority.FATAL"/> priority. If this category is
		/// <code>FATAL</code> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="log4net.or.IObjectRenderer"/>. It then proceeds to call all the
		/// registered appenders in this category and also higher in the
		/// hierarchy depending on the value of the additivity flag.</p>
		/// 
		/// <p><b>WARNING</b> Note that passing an <see cref="Exception"/> to this
		/// method will print the name of the <code>Exception</code> but no
		/// stack trace. To print a stack trace use the 
		/// <see cref="Fatal(object,Exception)"/> form instead.</p>
		/// 
		/// <p>The conditional compliation constant 'LOG4NET' must be defined
		/// otherwise this method will have no effect.</p>
		/// </remarks>
		/// <param name="message">the message object to log</param>
		public void Fatal(object message) 
		{
			Log(Priority.FATAL, message);
		}
  
		/// <summary>
		/// Log a message object with the <code>FATAL</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Fatal(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		public void Fatal(object message, Exception t) 
		{
			Log(Priority.FATAL, message, t);
		}
  
		/// <summary>
		/// This generic form is intended to be used by wrappers
		/// </summary>
		/// <param name="priority">The priority of the message to be logged</param>
		/// <param name="message">The message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		///[System.Diagnostics.Conditional("LOG4NET")]
		protected void Log(Priority priority, object message, Exception t) 
		{
			if(m_hierarchy.DisablePriority >= priority) 
			{
				return;
			}
			if(priority >= GetChainedPriority())
			{
				ForcedLog(FQCN, priority, message, t);
			}
		}
  
		/// <summary>
		/// This generic form is intended to be used by wrappers
		/// </summary>
		/// <param name="priority">The priority of the message to be logged</param>
		/// <param name="message">The message object to log</param>
		///[System.Diagnostics.Conditional("LOG4NET")]
		protected void Log(Priority priority, object message) 
		{
			if(m_hierarchy.DisablePriority >= priority) 
			{
				return;
			}
			if(priority >= GetChainedPriority())
			{
				ForcedLog(FQCN, priority, message, null);
			}
		}

		/// <summary>
		/// This is the most generic printing method. This generic form is intended to be used by wrappers
		/// </summary>
		/// <param name="callerFQCN">The wrapper class' fully qualified class name</param>
		/// <param name="priority">The priority of the message to be logged</param>
		/// <param name="message">The message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		///[System.Diagnostics.Conditional("LOG4NET")]
		protected void Log(string callerFQCN, Priority priority, object message, Exception t) 
		{
			if(m_hierarchy.DisablePriority >= priority) 
			{
				return;
			}
			if(priority >= GetChainedPriority())
			{
				ForcedLog(callerFQCN, priority, message, t);
			}
		}

		/// <summary>
		/// This method creates a new logging event and logs the event without further checks.
		/// </summary>
		/// <param name="fqcn">The wrapper class' fully qualified class name</param>
		/// <param name="priority">The priority of the message to be logged</param>
		/// <param name="message">The message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		protected void ForcedLog(string fqcn, Priority priority, object message, Exception t) 
		{
			CallAppenders(new LoggingEvent(fqcn, this, priority, message, t));
		}

		/// <summary>
		/// Additivity is set to true by default, that is children inherit
		/// the appenders of their ancestors by default. If this variable is
		/// set to <code>false</code> then the appenders found in the
		/// ancestors of this category are not used. However, the children
		/// of this category will inherit its appenders, unless the children
		/// have their additivity flag set to <code>false</code> too. See
		/// the user manual for more details.
		/// </summary>
		public bool Additivity
		{
			get { return m_additive; }
			set { m_additive = value; }
		}

		/// <summary>
		/// Get the appenders contained in this category as an 
		/// <see cref="System.Collections.IEnumerator"/>. If no appenders 
		/// can be found, then a <see cref="NullEnumerator"/> is returned.
		/// </summary>
		/// <returns>An enumeration of the appenders in this category</returns>
		public System.Collections.IEnumerator GetAllAppenders() 
		{
			lock(this)
			{
				if(m_aai == null)
				{
					return log4net.helpers.NullEnumerator.GetInstance();
				}
				else 
				{
					return m_aai.GetAllAppenders();
				}
			}
		}

		/// <summary>
		/// Look for the appender named as <code>name</code>
		/// </summary>
		/// <param name="name">The name of the appender to lookup</param>
		/// <returns>The appender with the name specified, or <code>null</code>.</returns>
		public IAppender GetAppender(string name) 
		{
			lock(this)
			{
				if(m_aai == null || name == null)
				{
					return null;
				}

				return m_aai.GetAppender(name);
			}
		}

		/// <summary>
		/// Starting from this category, search the category hierarchy for a
		/// non-null priority and return it. Otherwise, return the priority of the
		/// root category.
		/// <p>The Category class is designed so that this method executes as
		/// quickly as possible.</p>
		/// </summary>
		/// <returns>the nearest priority in the category hierarchy</returns>
		public virtual Priority GetChainedPriority() 
		{
			for(Category c = this; c != null; c=c.m_parent) 
			{
				if(c.m_priority != null) 
				{
					return c.m_priority;
				}
			}
			return null; // If reached will cause an NullPointerException.
		}

		/// <summary>
		/// Returns all the currently defined categories in the default
		/// hierarchy as an <see cref="System.Collections.IEnumerator"/>.
		/// </summary>
		/// <remarks>
		/// The root category is <em>not</em> included in the returned
		/// enumeration.
		/// </remarks>
		/// <returns>All the defined categories</returns>
		public static System.Collections.IEnumerator GetCurrentCategories() 
		{
			return s_defaultHierarchy.GetCurrentCategories();
		}

		/// <summary>
		/// Return the default Hierarchy instance.
		/// </summary>
		public static Hierarchy DefaultHierarchy
		{
			get { return s_defaultHierarchy; }
		}

		/// <summary>
		/// Get / Set the <see cref="Hierarchy"/> where this 
		/// <code>Category</code> instance is attached.
		/// </summary>
		public Hierarchy Hierarchy
		{
			get { return m_hierarchy; }
			set { m_hierarchy = value; }
		}

		/// <summary>
		/// Retrieve a category with named as the <code>name</code>
		/// parameter. If the named category already exists, then the
		/// existing instance will be reutrned. Otherwise, a new instance is
		/// created.
		/// <p>By default, categories do not have a set priority but inherit
		/// it from the hierarchy. This is one of the central features of
		/// log4net.</p>
		/// </summary>
		/// <param name="name">The name of the category to retrieve.</param>
		/// <returns>the category with the name specified</returns>
		public static Category GetInstance(string name)
		{
			return s_defaultHierarchy.GetInstance(name);
		}	

		/// <summary>
		/// Shorthand for <code>getInstance(clazz.FullName)</code>.
		/// </summary>
		/// <param name="clazz">The name of <code>clazz</code> will 
		/// be used as the name of the category to retrieve.</param>
		/// <returns>the category with the name specified</returns>
		public static Category GetInstance(Type clazz) 
		{
			return GetInstance(clazz.FullName);
		}	

		/// <summary>
		/// Like <see cref="GetInstance(string)"/> except that the type of category
		/// instantiated depends on the type returned by the 
		/// <see cref="log4net.spi.ICategoryFactory.MakeNewCategoryInstance"/>
		/// method of the <code>factory</code> parameter.
		/// </summary>
		/// <remarks>
		/// This method is intended to be used by sub-classes.
		/// </remarks>
		/// <param name="name">The name of the category to retrieve</param>
		/// <param name="factory">A <see cref="log4net.spi.ICategoryFactory"/> implementation 
		/// that will actually create a new Instance.</param>
		/// <returns>the category with the name specified</returns>
		public static Category GetInstance(string name, ICategoryFactory factory) 
		{
			return s_defaultHierarchy.GetInstance(name, factory);
		}	

		/// <summary>
		/// Return the category name.
		/// </summary>
		public string Name
		{
			get { return m_name; }
		}
    
		/// <summary>
		/// Returns the assigned <see cref="Priority"/>, if any, for this Category.  
		/// The assigned Priority, can be <code>null</code>
		/// </summary>
		virtual public Priority Priority
		{
			get { return m_priority; }
			set { m_priority = value; }
		}

		/// <summary>
		/// Return the root of the default category hierrachy.
		/// </summary>
		/// <remarks>
		/// The root category is always instantiated and available. It's
		/// name is "root".
		/// 
		/// <p>Nevertheless, calling <code>Category.GetInstance("root")</code>
		/// does not retrieve the root category but a category just under root 
		/// named "root".</p>
		/// </remarks>
		public static Category Root
		{
			get { return s_defaultHierarchy.Root; }
		}

		/// <summary>
		/// Check whether this category is enabled for the <code>DEBUG</code>
		/// priority.
		/// </summary>
		/// <remarks>
		/// <p>This function is intended to lessen the computational cost of
		/// disabled log debug statements.</p>
		/// 
		/// <p> For some <code>cat</code> Category object, when you write,</p>
		/// <pre>
		///    cat.Debug("This is entry number: " + i );
		/// </pre>
		/// 
		/// <p>You incur the cost constructing the message, concatenatiion in
		/// this case, regardless of whether the message is logged or not.</p>
		/// 
		/// <p>If you are worried about speed, then you should write</p>
		/// <pre>
		/// 	 if(cat.IsDebugEnabled()) { 
		/// 	   cat.Debug("This is entry number: " + i );
		/// 	 }
		/// </pre>
		/// 
		/// <p>This way you will not incur the cost of parameter
		/// construction if debugging is disabled for <code>cat</code>. On
		/// the other hand, if the <code>cat</code> is debug enabled, you
		/// will incur the cost of evaluating whether the category is debug
		/// enabled twice. Once in <code>IsDebugEnabled</code> and once in
		/// the <code>Debug</code>.  This is an insignificant overhead
		/// since evaluating a category takes about 1%% of the time it
		/// takes to actually log.</p>
		/// </remarks>
		/// <returns><code>true</code> if this category is debug enabled, <code>false</code> otherwise</returns>
		public bool IsDebugEnabled
		{
			get { return IsEnabledFor(Priority.DEBUG); }
		}
  
		/// <summary>
		/// Check whether this category is enabled for the INFO <see cref="Priority"/>.
		/// <seealso cref="Category.IsDebugEnabled"/>
		/// </summary>
		/// <returns>boolean True if this category is enabled for the <code>INFO</code> priority.</returns>
		public bool IsInfoEnabled
		{
			get { return IsEnabledFor(Priority.INFO); }
		}

		/// <summary>
		/// Check whether this category is enabled for a given <see cref="Priority"/> passed as parameter.
		/// <seealso cref="Category.IsDebugEnabled"/>
		/// </summary>
		/// <param name="priority">The priority to check</param>
		/// <returns>boolean True if this category is enabled for <code>priority</code>.</returns>
		public bool IsEnabledFor(Priority priority) 
		{
			if(m_hierarchy.DisablePriority >=  priority) 
			{
				return false;
			}
			return priority >= GetChainedPriority();
		}


		/// <summary>
		/// Remove all previously added appenders from this Category instance.
		/// <p>This is useful when re-reading configuration information.</p>
		/// </summary>
		public void RemoveAllAppenders() 
		{
			lock(this)
			{
				if(m_aai != null) 
				{
					m_aai.RemoveAllAppenders();
					m_aai = null;
				}
			}
		}

		/// <summary>
		/// Remove the appender passed as parameter form the list of appenders.
		/// </summary>
		/// <param name="appender">The appender to remove</param>
		public void RemoveAppender(IAppender appender) 
		{
			lock(this)
			{
				if(appender == null || m_aai == null) 
				{
					return;
				}
				m_aai.RemoveAppender(appender);
			}
		}

		/// <summary>
		/// Remove the appender passed as parameter form the list of appenders.
		/// </summary>
		/// <param name="name">The name of the appender to remove</param>
		public void RemoveAppender(string name) 
		{
			lock(this)
			{
				if(name == null || m_aai == null)
				{
					return;
				}
				m_aai.RemoveAppender(name);
			}
		}
  
		/// <summary>
		/// Calling this method will <em>safely</em> close and remove all
		/// appenders in all the categories including root contained in the
		/// default hierachy.
		/// 
		/// <p>Some appenders need to be closed before the application exists. 
		/// Otherwise, pending logging events might be lost.</p>
		/// 
		/// <p>The <code>shutdown</code> method is careful to close nested
		/// appenders before closing regular appenders. This is allows
		/// configurations where a regular appender is attached to a category
		/// and again to a nested appender.</p>
		/// </summary>
		public static void Shutdown() 
		{
			s_defaultHierarchy.Shutdown();
		}

	}

}

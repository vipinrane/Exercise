using System;

namespace log4net
{
	/// <summary>
	/// Summary description for ILog.
	/// </summary>
	public interface ILog
	{
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
		void Debug(object message);
  
		/// <summary>
		/// Log a message object with the <code>DEBUG</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Debug(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">he exception to log, including its stack trace</param>
		void Debug(object message, Exception t);

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
		void Info(object message);
  
		/// <summary>
		/// Log a message object with the <code>INFO</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Info(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		void Info(object message, Exception t);

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
		void Warn(object message);
  
		/// <summary>
		/// Log a message object with the <code>WARN</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Warn(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		void Warn(object message, Exception t);

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
		void Error(object message);

		/// <summary>
		/// Log a message object with the <code>ERROR</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Error(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		void Error(object message, Exception t);

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
		void Fatal(object message);
  
		/// <summary>
		/// Log a message object with the <code>FATAL</code> priority including
		/// the stack trace of the <see cref="Exception"/> <code>t</code> passes
		/// as a parameter.
		/// </summary>
		/// <seealso cref="Fatal(object)"/> form for more detailed information.
		/// <param name="message">the message object to log</param>
		/// <param name="t">the exception to log, including its stack trace</param>
		void Fatal(object message, Exception t);



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
		bool IsDebugEnabled { get; }
  
		/// <summary>
		/// Check whether this category is enabled for the INFO <see cref="Priority"/>.
		/// <seealso cref="ILog.IsDebugEnabled"/>
		/// </summary>
		/// <returns>boolean True if this category is enabled for the <code>INFO</code> priority.</returns>
		bool IsInfoEnabled { get; }

	}
}

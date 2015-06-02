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

namespace log4net.helpers
{
	/// <summary>
	/// This class used to output log statements from within the log4net package.
	/// </summary>
	/// <remarks>
	/// <p>Log4net components cannot make log4net logging calls. However, it is
	/// sometimes useful for the user to learn about what log4net is
	/// doing. You can enable log4net internal logging by setting the debug
	/// flag in the configurator.</p>
	/// 
	/// <p>All log4net internal debug calls go to <code>Console.Out</code>
	/// where as internal error messages are sent to <code>Console.Error</code>. 
	/// All internal messages are prepended with the string "log4net: ".</p>
	/// </remarks>
	public class LogLog
	{
		/// <summary>
		/// Prevent instance objects
		/// </summary>
		private LogLog()
		{
		}

		/// <summary>
		///  Default debug level
		/// </summary>
		protected static bool s_debugEnabled = false;

		/// <summary>
		/// In quietMode not even errors generate any output.
		/// </summary>
		private static bool s_quietMode = false;

		private const string PREFIX			= "log4net: ";
		private const string ERR_PREFIX		= "log4net:ERROR ";
		private const string WARN_PREFIX	= "log4net:WARN ";

		/// <summary>
		/// Allows to enable/disable log4net internal logging.
		/// </summary>
		/// <param name="enabled"></param>
		static public void SetInternalDebugging(bool enabled) 
		{
			s_debugEnabled = enabled;
		}

		/// <summary>
		/// This method is used to output log4net internal debug
		/// statements. Output goes to <code>Console.Out</code>.
		/// </summary>
		/// <param name="msg"></param>
		public static void Debug(string msg) 
		{
			if(s_debugEnabled && !s_quietMode) 
			{
				Console.Out.WriteLine(PREFIX+msg);
			}
		}

		/// <summary>
		/// This method is used to output log4net internal debug
		/// statements. Output goes to <code>Console.Out</code>.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="t"></param>
		public static void Debug(string msg, Exception t) 
		{
			if(s_debugEnabled && !s_quietMode) 
			{
				Console.Out.WriteLine(PREFIX+msg);
				if(t != null)
				{
					Console.Out.WriteLine(t.ToString());
				}
			}
		}
  
		/// <summary>
		/// This method is used to output log4net internal error
		/// statements. There is no way to disable error statements.
		/// Output goes to <code>Console.Error</code>.
		/// </summary>
		/// <param name="msg"></param>
		public static void Error(string msg) 
		{
			if(s_quietMode)
			{
				return;
			}
			Console.Out.WriteLine(ERR_PREFIX+msg);
		}  

		/// <summary>
		/// This method is used to output log4net internal error
		/// statements. There is no way to disable error statements.
		/// Output goes to <code>Console.Error</code>.  
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="t"></param>
		public static void Error(string msg, Exception t) 
		{
			if(s_quietMode)
			{
				return;
			}

			Console.Error.WriteLine(ERR_PREFIX+msg);
			if(t != null) 
			{
				Console.Error.WriteLine(t.ToString());
			}
		}  

		/// <summary>
		/// In quite mode no LogLog generates strictly no output, not even
		/// for errors. 
		/// </summary>
		/// <param name="quietMode">A true for not</param>
		public static void SetQuietMode(bool quietMode) 
		{
			s_quietMode = quietMode;
		}

		/// <summary>
		/// This method is used to output log4net internal warning
		/// statements. There is no way to disable warning statements.
		/// Output goes to <code>Console.Error</code>.
		/// </summary>
		/// <param name="msg"></param>
		public static void Warn(string msg) 
		{
			if(s_quietMode)
			{
				return;
			}
			Console.Out.WriteLine(WARN_PREFIX+msg);
		}  

		/// <summary>
		/// This method is used to output log4net internal warnings. There is
		/// no way to disable warning statements.  Output goes to
		/// <code>Console.Error</code>.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="t"></param>
		public static void Warn(string msg, Exception t) 
		{
			if(s_quietMode)
			{
				return;
			}

			Console.Out.WriteLine(WARN_PREFIX+msg);
			if(t != null) 
			{
				Console.Error.WriteLine(t.ToString());
			}
		}  
	}
}

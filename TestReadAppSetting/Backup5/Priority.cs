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

namespace log4net
{
	/// <summary>
	/// Defines the minimum set of priorities recognized by the system,	
	/// that is FATAL, ERROR, WARN, INFO and DEBUG.
	/// The <code>Priority</code> class may be subclassed to define a larger priority set.
	/// </summary>
	public class Priority
	{

		#region Static Member Variables

		private const int FATAL_INT = 50000;
		private const int ERROR_INT = 40000;
		private const int WARN_INT  = 30000;
		private const int INFO_INT  = 20000;
		private const int DEBUG_INT = 10000;
		private const int OFF_INT   =    -1;

		/// <summary>
		/// The <code>FATAL</code> priority designates very severe error events that will presumably lead the application to abort.
		/// </summary>
		public readonly static Priority FATAL = new Priority(FATAL_INT, "FATAL", 0);

		/// <summary>
		/// The <code>ERROR</code> priority designates error events that might still allow the application to continue running.
		/// </summary>
		public readonly static Priority ERROR = new Priority(ERROR_INT, "ERROR", 3);

		/// <summary>
		/// The <code>WARN</code> priority designates potentially harmful situations.
		/// </summary>
		public readonly static Priority WARN  = new Priority(WARN_INT, "WARN",  4);

		/// <summary>
		/// The <code>INFO</code> priority designates informational messages that highlight the progress of the application at coarse-grained level.
		/// </summary>
		public readonly static Priority INFO  = new Priority(INFO_INT, "INFO",  6);

		/// <summary>
		/// The <code>DEBUG</code> priority designates fine-grained informational events that are most useful to debug an application.
		/// </summary>
		public readonly static Priority DEBUG = new Priority(DEBUG_INT, "DEBUG", 7);

		/// <summary>
		/// The <code>OFF</code> priority designates a lower level priority than all the rest.
		/// </summary>
		internal readonly static Priority OFF = new Priority(OFF_INT, "OFF", -1);

		#endregion

		#region Member Variables

		private int m_level;
		private string m_levelStr;
		private int m_syslogEquivalent;

		#endregion


		/// <summary>
		/// Instantiate a priority object.
		/// </summary>
		/// <param name="level"></param>
		/// <param name="levelStr"></param>
		/// <param name="syslogEquivalent"></param>
		protected Priority(int level, String levelStr, int syslogEquivalent) 
		{
			m_level = level;
			m_levelStr = levelStr;
			m_syslogEquivalent = syslogEquivalent;
		}

		/// <summary>
		/// Return the syslog equivalent of this priority as an integer.
		/// </summary>
		/// <returns></returns>
		public int GetSyslogEquivalent() 
		{
			return m_syslogEquivalent;
		}

		/// <summary>
		/// Returns the string representation of this priority.
		/// </summary>
		/// <returns></returns>
		public override string ToString() 
		{
			return m_levelStr;
		}

		/// <summary>
		/// Override Equals to compare the levels of
		/// Priority objects. Defers to base class if
		/// the target object is not a Priority.
		/// </summary>
		/// <param name="o">The object to compare against</param>
		/// <returns>true if the objects are equal</returns>
		override public bool Equals(object o)
		{
			if (o is Priority)
			{
				return m_level == ((Priority)o).m_level;
			}
			else
			{
				return base.Equals(o);
			}
		}

		/// <summary>
		/// Returns a hash code that is sutable for use in a hashtree etc
		/// </summary>
		/// <returns>the hash of this object</returns>
		override public int GetHashCode()
		{
			return m_level;
		}

		/// <summary>
		/// Operator greater than that compares Priorities
		/// </summary>
		/// <param name="l">left hand side</param>
		/// <param name="r">right hand side</param>
		/// <returns>true if left hand side is greater than the right hand side</returns>
		public static bool operator > (Priority l, Priority r)
		{
			return l.m_level > r.m_level;
		}

		/// <summary>
		/// Operator less than that compares Priorities
		/// </summary>
		/// <param name="l">left hand side</param>
		/// <param name="r">right hand side</param>
		/// <returns>true if left hand side is less than the right hand side</returns>
		public static bool operator < (Priority l, Priority r)
		{
			return l.m_level < r.m_level;
		}

		/// <summary>
		/// Operator greater than or equal that compares Priorities
		/// </summary>
		/// <param name="l">left hand side</param>
		/// <param name="r">right hand side</param>
		/// <returns>true if left hand side is greater than or equal to the right hand side</returns>
		public static bool operator >= (Priority l, Priority r)
		{
			return l.m_level >= r.m_level;
		}

		/// <summary>
		/// Operator less than or equal that compares Priorities
		/// </summary>
		/// <param name="l">left hand side</param>
		/// <param name="r">right hand side</param>
		/// <returns>true if left hand side is less than or equal to the right hand side</returns>
		public static bool operator <= (Priority l, Priority r)
		{
			return l.m_level <= r.m_level;
		}

		/// <summary>
		/// Operator equals that compares Priorities
		/// </summary>
		/// <param name="l">left hand side</param>
		/// <param name="r">right hand side</param>
		/// <returns>true if left hand side is equal to the right hand side</returns>
		public static bool operator == (Priority l, Priority r)
		{
			if (((object)l) != null && ((object)r) != null)
			{
				return l.m_level == r.m_level;
			}
			else
			{
				return ((object)l) == ((object)r);
			}
		}

		/// <summary>
		/// Operator not equals that compares Priorities
		/// </summary>
		/// <param name="l">left hand side</param>
		/// <param name="r">right hand side</param>
		/// <returns>true if left hand side is not equal to the right hand side</returns>
		public static bool operator != (Priority l, Priority r)
		{
			if (((object)l) != null && ((object)r) != null)
			{
				return l.m_level != r.m_level;
			}
			else
			{
				return ((object)l) != ((object)r);
			}
		}

		/// <summary>
		/// Return all possible priorities as an array of Priority objects in descending order.
		/// </summary>
		/// <returns></returns>
		public static Priority[] GetAllPossiblePriorities() 
		{
			return new Priority[] {Priority.FATAL, Priority.ERROR, Priority.WARN, Priority.INFO, Priority.DEBUG};
		}

		/// <summary>
		/// Convert the string passed as argument to a priority. If the
		/// conversion fails, then this method returns <see cref="DEBUG"/>.
		/// </summary>
		/// <param name="sArg">The string to parse into a Priority</param>
		/// <returns>The Priority represented by the string argument</returns>
		public static Priority Parse(string sArg) 
		{
			return Parse(sArg, Priority.DEBUG);
		}

		/// <summary>
		/// Convert an integer passed as argument to a priority. If the
		/// conversion fails, then this method returns <see cref="DEBUG"/>.
		/// </summary>
		/// <param name="val">The int to convert to a Priority</param>
		/// <returns>The Priority represented by the argument</returns>
		public static Priority Parse(int val) 
		{
			return Parse(val, Priority.DEBUG);
		}

		/// <summary>
		/// Convert an integer passed as argument to a priority. If the
		/// conversion fails, then this method returns the specified default.
		/// </summary>
		/// <param name="val">The value to convert to a Prority</param>
		/// <param name="defaultPriority">The default Priority value to use</param>
		/// <returns>The Priority represented by the Argument</returns>
		public static Priority Parse(int val, Priority defaultPriority) 
		{
			switch(val) 
			{
				case DEBUG_INT: 
					return DEBUG;
				case INFO_INT: 
					return INFO;
				case WARN_INT: 
					return WARN;
				case ERROR_INT: 
					return ERROR;
				case FATAL_INT: 
					return FATAL;
				default: 
					return defaultPriority;
			}
		}

		/// <summary>
		/// Convert the string passed as argument to a priority. If the
		/// conversion fails, then this method returns the value of
		/// <code>defaultPriority</code>.
		/// </summary>
		/// <param name="sArg">The string to parse</param>
		/// <param name="defaultPriority">the default Priority to use</param>
		/// <returns>The Priority represented by the argument</returns>
		public static Priority Parse(string sArg, Priority defaultPriority) 
		{                  
			if(sArg == null)
			{
				return defaultPriority;
			}

			String s = sArg.ToUpper();

			if(s == "DEBUG")
			{
				return Priority.DEBUG; 
			}
			if(s == "INFO")
			{
				return Priority.INFO;
			}
			if(s == "WARN")
			{
				return Priority.WARN;  
			}
			if(s == "ERROR")
			{
				return Priority.ERROR;
			}
			if(s == "FATAL")
			{
				return Priority.FATAL;
			}
			return defaultPriority;
		}

	}
}

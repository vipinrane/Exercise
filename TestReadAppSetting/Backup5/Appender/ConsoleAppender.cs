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

using log4net.helpers;
using log4net.Layout;

namespace log4net.Appender
{
	/// <summary>
	/// ConsoleAppender appends log events to <code>Console.Out</code> 
	/// or <code>Console.Err</code> using a layout specified by the 
	/// user. The default target is <code>Console.Out</code>.
	/// </summary>
	public class ConsoleAppender : TextWriterAppender
	{
		/// <summary>
		/// The <code>Target</code> to use when writting to the Console standard out
		/// </summary>
		public const string CONSOLE_OUT = "Console.Out";

		/// <summary>
		/// The <code>Target</code> to use when writting to the Console standard error
		/// </summary>
		public const string CONSOLE_ERR = "Console.Error";

		/// <summary>
		/// The target that is being used by the appender
		/// </summary>
		private string m_target = CONSOLE_OUT;


		/// <summary>
		/// The default constructor does nothing.
		/// </summary>
		public ConsoleAppender() 
		{    
		}

		/// <summary>
		/// Initialise the appender with a specified layout
		/// </summary>
		/// <param name="layout">the layout to use with this appender</param>
		public ConsoleAppender(ILayout layout) : this(layout, CONSOLE_OUT)
		{
		}

		/// <summary>
		/// Initialise the appender with a specified layout and 
		/// console output stream.
		/// The console output stream must be <c>"Console.Out"</c>
		/// or <c>"Console.Error"</c>
		/// </summary>
		/// <param name="layout">the layout object to use</param>
		/// <param name="target">the console output stream</param>
		public ConsoleAppender(ILayout layout, String target) 
		{
			m_layout = layout;
    
			if (string.Compare(CONSOLE_OUT, target, true) == 0) 
			{
				Writer = Console.Out;	
			} 
			else if (string.Compare(CONSOLE_ERR, target, true) == 0) 
			{
				Writer = Console.Error;
			} 
			else 
			{
				TargetWarn(target);
			}
		}

		/// <summary>
		/// Target is the value of the console output stream.
		/// This is either <c>Console.Out</c> or <c>Console.Error</c>.
		/// </summary>
		public string Target
		{
			get { return m_target; }
			set
			{
				string v = value.Trim();
				
				if (string.Compare(CONSOLE_OUT, v, true) == 0) 
				{
					m_target = CONSOLE_OUT;
				} 
				else if (string.Compare(CONSOLE_ERR, v, true) == 0) 
				{
					m_target = CONSOLE_ERR;
				} 
				else 
				{
					TargetWarn(value);
				}  
			}
		}
  
		/// <summary>
		/// Internal method called to indicate an error with
		/// the console stream name provided.
		/// </summary>
		/// <param name="val">the erroneous console stream name</param>
		private void TargetWarn(string val) 
		{
			LogLog.Warn("["+val+"] should be Console.Out or Console.Error.");
			LogLog.Warn("Using Console.Out (default).");
		}
 
		/// <summary>
		/// Initialise the appender based on the options set
		/// </summary>
		public override void ActivateOptions() 
		{
			if(m_target == CONSOLE_OUT)
			{
				Writer = Console.Out;
			} 
			else 
			{
				Writer = Console.Error;
			}
		}

		/// <summary>
		/// Override the parent method to do nothing.
		/// </summary>
		override protected void CloseWriter() 
		{
		}
	}
}

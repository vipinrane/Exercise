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
using log4net;

namespace log4net.helpers
{
	/// <summary>
	/// The <code>OnlyOnceErrorHandler</code> implements log4net's default
	/// error handling policy which consists of emitting a message for the
	/// first error in an appender and ignoring all following errors.
	/// </summary>
	/// <remarks>
	/// <p>The error message is printed on <code>Console.Error</code>. </p>
	/// 
	/// <p>This policy aims at protecting an otherwise working application
	/// from being flooded with error messages when logging fails.</p>
	/// </remarks>
	public class OnlyOnceErrorHandler : IErrorHandler
	{
		const string WARN_PREFIX = "log4net warning: ";
		const string ERROR_PREFIX = "log4net error: ";

		private bool m_firstTime = true;

		/// <summary>
		/// No options to activate
		/// </summary>
		public void ActivateOptions() 
		{
		}

		/// <summary>
		/// Prints the message and the stack trace of the exception on <code>Console.Error</code>
		/// </summary>
		/// <param name="message">the error message</param>
		/// <param name="e">the exception</param>
		/// <param name="errorCode">the internal error code</param>
		public void Error(string message, Exception e, ErrorCodes errorCode) 
		{ 
			if(m_firstTime) 
			{
				LogLog.Error(message, e);
				m_firstTime = false;
			}
		}

		/// <summary>
		/// Print a the error message passed as parameter on <code>Console.Error</code>.
		/// </summary>
		/// <param name="message">the error message</param>
		public void Error(string message) 
		{
			if(m_firstTime) 
			{
				LogLog.Error(message);
				m_firstTime = false;
			}
		}	
	}
}

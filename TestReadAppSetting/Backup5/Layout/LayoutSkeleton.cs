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

namespace log4net.Layout
{
	/// <summary>
	/// Extend this abstract class to create your own log layout format.
	/// </summary>
	public abstract class LayoutSkeleton : ILayout, IOptionHandler
	{
		/// <summary>
		/// Platform end of line seperator
		/// </summary>
		public const string LINE_SEP = "\r\n";

		/// <summary>
		/// length of end of line seperator
		/// </summary>
		public const int LINE_SEP_LEN = 2;

		/// <summary>
		/// Implement this method to create your own layout format.
		/// </summary>
		/// <param name="loggingEvent">The event to format</param>
		/// <returns>returns the formatted event</returns>
		abstract public	string Format(LoggingEvent loggingEvent);

		/// <summary>
		/// Activate the options that were previously set with calls to option setters.
		/// </summary>
		/// <remarks>
		/// This allows to defer activiation of the options until all
		/// options have been set. This is required for components which have
		/// related options that remain ambigous until all are set.
		/// </remarks>
		abstract public void ActivateOptions();

		/// <summary>
		/// Returns the content type output by this layout. The base class returns "text/plain".
		/// </summary>
		/// <returns>the content type</returns>
		public virtual string ContentType
		{
			get { return "text/plain"; }
		}

		/// <summary>
		/// Returns the header for the layout format. The base class returns <code>null</code>.
		/// </summary>
		/// <returns></returns>
		public virtual string Header
		{
			get { return null; }
		}

		/// <summary>
		/// Returns the footer for the layout format. The base class returns <code>null</code>.
		/// </summary>
		/// <returns></returns>
		public virtual string Footer
		{
			get { return null; }
		}

		/// <summary>
		/// If the layout handles the exception object contained within
		/// <see cref="LoggingEvent"/>, then the layout should return
		/// <code>false</code>. Otherwise, if the layout ignores exception
		/// object, then the layout should return <code>true</code>.
		/// </summary>
		/// <returns></returns>
		abstract public	bool IgnoresException { get; }

	}
}

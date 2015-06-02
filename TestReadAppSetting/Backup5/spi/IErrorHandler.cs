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

namespace log4net.spi
{
	/// <summary>
	/// Appenders may delegate their error handling to ErrorHandlers.
	/// </summary>
	/// <remarks>
	/// Error handling is a particularly tedious to get right because by
	/// definition errors are hard to predict and to reproduce. 
	/// </remarks>
	public interface IErrorHandler
	{
		/// <summary>
		/// This method should handle the error. Information about the error
		/// condition is passed a parameter.
		/// </summary>
		/// <param name="message">The message assoicated with the error</param>
		/// <param name="e">The Exption that was thrown when the error occured</param>
		/// <param name="errorCode">The error code associated with the error</param>
		void Error(string message, Exception e, ErrorCodes errorCode);

		/// <summary>
		/// This method prints the error message passed as a parameter.
		/// </summary>
		/// <param name="message"></param>
		void Error(string message);
	}


	/// <summary>
	/// Defined error codes that can be passed to the <see cref="IErrorHandler.Error"/> method.
	/// </summary>
	public enum ErrorCodes : int
	{
		/// <summary>
		/// A general error
		/// </summary>
		GENERIC_FAILURE = 0,

		/// <summary>
		/// Error while writing output
		/// </summary>
		WRITE_FAILURE,

		/// <summary>
		/// Failed to flush file
		/// </summary>
		FLUSH_FAILURE,

		/// <summary>
		/// Failed to close file
		/// </summary>
		CLOSE_FAILURE,

		/// <summary>
		/// Unable to open output file
		/// </summary>
		FILE_OPEN_FAILURE,

		/// <summary>
		/// No layout specified
		/// </summary>
		MISSING_LAYOUT,

		/// <summary>
		/// Failed to parse address
		/// </summary>
		ADDRESS_PARSE_FAILURE
	}

}

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

using System.IO;
using log4net.spi;

namespace log4net.helpers
{
	/// <summary>
	/// QuietTextWriter does not throw exceptions when things go wrong. 
	/// Instead, it delegates error handling to its <see cref="IErrorHandler"/>.
	/// </summary>
	public class QuietTextWriter
	{
		/// <summary>
		/// The error handler instance to pass all errors to
		/// </summary>
		protected IErrorHandler m_errorHandler;

		/// <summary>
		/// The instance of the underlying TextWriter used for output
		/// </summary>
		protected TextWriter m_writer;

		/// <summary>
		/// Create a new QuietTextWriter using a writer and error handler
		/// </summary>
		/// <param name="writer">the writer to actualy write to</param>
		/// <param name="errorHandler">the error handler to report error to</param>
		public QuietTextWriter(TextWriter writer, IErrorHandler errorHandler) 
		{
			m_writer = writer;
			ErrorHandler = errorHandler;
		}

		/// <summary>
		/// Write a string to the output
		/// </summary>
		/// <param name="str">the string data to write to the output</param>
		public void Write(string str) 
		{
			try 
			{
				m_writer.Write(str);
			} 
			catch(Exception e) 
			{
				m_errorHandler.Error("Failed to write ["+str+"].", e, ErrorCodes.WRITE_FAILURE);
			}
		}

		/// <summary>
		/// Flush any buffered output
		/// </summary>
		public void Flush() 
		{
			try 
			{
				m_writer.Flush();
			} 
			catch(Exception e) 
			{
				m_errorHandler.Error("Failed to flush writer.", e, ErrorCodes.FLUSH_FAILURE);
			}	
		}

		/// <summary>
		/// The error handler that all errors are passed to
		/// </summary>
		public IErrorHandler ErrorHandler
		{
			set
			{
				if(value == null) 
				{
					// This is a programming error on the part of the enclosing appender.
					throw new Exception("Attempted to set null ErrorHandler.");
				} 
				else 
				{ 
					m_errorHandler = value;
				}
			}
		}	

		/// <summary>
		/// Close the underlying output writer
		/// </summary>
		public void Close()
		{
			m_writer.Close();
		}
	}
}

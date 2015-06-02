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

using log4net;
using log4net.Layout;
using log4net.spi;
using log4net.helpers;

namespace log4net.Appender
{
	/// <summary>
	/// FileAppender appends log events to a file. 
	/// </summary>
	/// <remarks>
	/// Logging events are sent to the file specified.
	/// The file can be opened in either append or
	/// overwite modes.
	/// </remarks>
	public class FileAppender : TextWriterAppender
	{
		/// <summary>
		/// Flag to indicate if we should append to the file
		/// or overwite the file. The default is to append
		/// </summary>
		protected bool m_appendToFile = true;

		/// <summary>
		/// The name of the log file.
		/// </summary>
		protected string m_fileName = null;

		/// <summary>
		/// Default constructor
		/// </summary>
		public FileAppender()
		{
		}

		/// <summary>
		/// Construct a new appender using the layout, file and append mode.
		/// </summary>
		/// <param name="layout">the layout to use with this appender</param>
		/// <param name="filename">the full path to the file to write to</param>
		/// <param name="append">flag to indicate if the file should be appended to</param>
		public FileAppender(ILayout layout, string filename, bool append) 
		{
			m_layout = layout;
			OpenFile(filename, append);
		}

		/// <summary>
		/// Construct a new appender using the layout and file specified.
		/// The file will be appended to.
		/// </summary>
		/// <param name="layout">the layout to use with this appender</param>
		/// <param name="filename">the full path to the file to write to</param>
		public FileAppender(ILayout layout, string filename) : this(layout, filename, true)
		{
		}

		/// <summary>
		/// File is the full path to the file that logging will be written to
		/// </summary>
		public string File
		{
			get { return m_fileName; }
			set { m_fileName = value.Trim(); }
		}

		/// <summary>
		/// AppendToFile is a flag that indicates weather the file should be
		/// appended to or overwitten. If the value is set to false then the
		/// file will be overwitten. If it is set to true then the file will
		/// be appended to. The default value is true.
		/// </summary>
		public bool AppendToFile
		{
			get { return m_appendToFile; }
			set { m_appendToFile = value; }
		}

		/// <summary>
		/// Activate the options on the file appender. This will
		/// case the file to be opened.
		/// </summary>
		override public void ActivateOptions() 
		{    
			if(m_fileName != null) 
			{
				try 
				{
					OpenFile(m_fileName, m_appendToFile);
				}
				catch(Exception e) 
				{
					m_errorHandler.Error("OpenFile("+m_fileName+","+m_appendToFile+") call failed.", e, ErrorCodes.FILE_OPEN_FAILURE);
				}
			} 
			else 
			{
				LogLog.Warn("File option not set for appender ["+m_name+"].");
				LogLog.Warn("Are you using FileAppender instead of ConsoleAppender?");
			}
		}

		/// <summary>
		/// Closes the previously opened file.
		/// </summary>
		protected void CloseFile() 
		{
			if(m_qtw != null) 
			{
				try 
				{
					m_qtw.Close();
				}
				catch(Exception e) 
				{
					// Exceptionally, it does not make sense to delegate to an
					// ErrorHandler. Since a closed appender is basically dead.
					LogLog.Error("Could not close " + m_qtw, e);
				}
			}
		}

		/// <summary>
		/// Sets and <i>opens</i> the file where the log output will
		/// go. The specified file must be writable.
		/// </summary>
		/// <remarks>
		/// If there was already an opened file, then the previous file
		/// is closed first.
		/// </remarks>
		/// <param name="fileName">The path to the log file</param>
		/// <param name="append">If true will append to fileName. Otherwise will truncate fileName</param>
		private void OpenFile(string fileName, bool append)
		{
			lock(this)
			{
				Reset();
				SetQWForFiles(new StreamWriter(fileName, append));

				m_fileName = fileName;
				m_appendToFile = append;

				WriteHeader();
			}
		}

		/// <summary>
		/// Sets the quiet writer being used.
		/// </summary>
		/// <remarks>
		/// This method can be overriden by sub classes.
		/// </remarks>
		/// <param name="writer">the writer to set</param>
		virtual protected void SetQWForFiles(TextWriter writer) 
		{
			m_qtw = new QuietTextWriter(writer, m_errorHandler);
		}

		/// <summary>
		/// Close any previously opened file and call the parent's <c>Reset</c>
		/// </summary>
		override protected void Reset() 
		{
			CloseFile();
			m_fileName = null;
			base.Reset();    
		}

	}
}

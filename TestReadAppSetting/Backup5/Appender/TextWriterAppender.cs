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
using log4net.helpers;
using log4net.or;
using log4net.spi;
using log4net.Layout;

using System.IO;

namespace log4net.Appender
{
	/// <summary>
	/// Summary description for TextWriterAppender.
	/// </summary>
	public class TextWriterAppender : AppenderSkeleton
	{
		/// <summary>
		/// Immediate flush means that the underlying writer or output stream
		/// will be flushed at the end of each append operation.
		/// </summary>
		/// <remarks>
		/// Immediate
		/// flush is slower but ensures that each append request is actually
		/// written. If <code>immediateFlush</code> is set to
		/// <code>false</code>, then there is a good chance that the last few
		/// logs events are not actually written to persistent media if and
		/// when the application crashes.
		/// 
		/// <p>The <code>immediateFlush</code> variable is set to
		/// <code>true</code> by default.</p>
		/// </remarks>
		protected bool m_immediateFlush = true;

		/// <summary>
		/// This is the <see cref="log4net.helpers.QuietTextWriter"/> where we will write to. 
		/// </summary>
		protected QuietTextWriter m_qtw;

		/// <summary>
		/// This default constructor does nothing
		/// </summary>
		public TextWriterAppender() 
		{
		}

		/// <summary>
		/// Instantiate a TextWriterAppender and set the output destination 
		/// to a new <see cref="StreamWriter"/> initialized with <code>os</code>
		/// as its <see cref="Stream"/>.
		/// </summary>
		/// <param name="layout">The layout to use with this appender</param>
		/// <param name="os">The Stream to output to</param>
		public TextWriterAppender(ILayout layout, Stream os) : this(layout, new StreamWriter(os))
		{
			
		}
  
		/// <summary>
		/// Instantiate a TextWriterAppender and set the output 
		/// destination to <code>writer</code>.
		/// <p>The <code>writer</code> must have been previously opened by the user.</p>
		/// </summary>
		/// <param name="layout">The layout to use with this appender</param>
		/// <param name="writer">The TextWriter to output to</param>
		public TextWriterAppender(ILayout layout, TextWriter writer) 
		{
			m_layout = layout;
			Writer = writer;
		}

		/// <summary>
		/// If the <b>ImmediateFlush</b> option is set to
		/// <code>true</code>, the appender will flush at the end of each
		/// write.
		/// </summary>
		/// <remarks>
		/// This is the default behavior. If the option is set to
		/// <code>false</code>, then the underlying stream can defer writing
		/// to physical medium to a later time. 
		/// 
		/// <p>Avoiding the flush operation at the end of each append results in
		/// a performance gain of 10 to 20 percent. However, there is safety
		/// tradeoff involved in skipping flushing. Indeed, when flushing is
		/// skipped, then it is likely that the last few log events will not
		/// be recorded on disk when the application exits. This is a high
		/// price to pay even for a 20% performance gain.</p>
		/// </remarks>
		public bool ImmediateFlush
		{
			get { return m_immediateFlush; }
			set { m_immediateFlush = value; }
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		public override void ActivateOptions() 
		{    
		}

		/// <summary>
		/// This method is called by the <see cref="AppenderSkeleton.DoAppend"/>
		/// method. 
		/// 
		/// <p>If the output stream exists and is writable then write a log
		/// statement to the output stream. Otherwise, write a single warning
		/// message to <code>System.err</code>.</p>
		/// 
		/// <p>The format of the output will depend on this appender's
		/// layout.</p>
		/// </summary>
		/// <param name="loggingEvent">the event to log</param>
		protected override void Append(LoggingEvent loggingEvent) 
		{
			// Reminder: the nesting of calls is:
			//
			//    doAppend()
			//      - check threshold
			//      - filter
			//      - append();
			//        - checkEntryConditions();
			//        - subAppend();

			if(!CheckEntryConditions()) 
			{
				return;
			}
			SubAppend(loggingEvent);
		} 

		/// <summary>
		/// This method determines if there is a sense in attempting to append.
		/// 
		/// <p>It checks whether there is a set output target and also if
		/// there is a set layout. If these checks fail, then the boolean
		/// value <code>false</code> is returned. </p>
		/// </summary>
		/// <returns>returns false if any of the preconditiond fail</returns>
		protected bool CheckEntryConditions() 
		{
			if(m_closed) 
			{
				LogLog.Warn("Not allowed to write to a closed appender.");
				return false;
			}

			if(m_qtw == null) 
			{
				m_errorHandler.Error("No output stream or file set for the appender named ["+ m_name+"].");
				return false;
			}
    
			if(m_layout == null) 
			{
				m_errorHandler.Error("No layout set for the appender named ["+ m_name+"].");
				return false;
			}
			return true;
		}

		/// <summary>
		/// Close this appender instance. The underlying stream or writer is also closed.
		/// <p>Closed appenders cannot be reused</p>
		/// </summary>
		override public void Close() 
		{
			lock(this)
			{
				if(m_closed)
				{
					return;
				}
				m_closed = true;
				WriteFooter();
				Reset();
			}
		}

		/// <summary>
		/// Close the underlying <see cref="TextWriter"/>
		/// </summary>
		virtual protected void CloseWriter() 
		{
			if(m_qtw != null) 
			{
				try 
				{
					m_qtw.Close();
				} 
				catch(Exception e) 
				{
					LogLog.Error("Could not close " + m_qtw, e); 
					// do need to invoke an error handler
					// at  this late stage
				}
			}
		}
  
		/// <summary>
		/// The <see cref="IErrorHandler"/> for this Appender and also the underlying <see cref="QuietTextWriter"/> if any. 
		/// </summary>
		override public IErrorHandler ErrorHandler
		{
			set
			{
				lock(this)
				{
					if(value == null) 
					{
						LogLog.Warn("You have tried to set a null error-handler.");
					} 
					else 
					{
						m_errorHandler = value;
						if(m_qtw != null) 
						{
							m_qtw.ErrorHandler = value;
						}
					}    
				}
			}
		}
  
		/// <summary>
		/// <p>Sets the Writer where the log output will go. The
		/// specified Writer must be opened by the user and be
		/// writable.</p>
		/// 
		/// <p>The <code>java.io.Writer</code> will be closed when the
		/// appender instance is closed.</p>
		/// 
		/// <p><b>WARNING:</b> Logging to an unopened Writer will fail.</p>
		/// </summary>
		virtual public TextWriter Writer
		{
			set
			{
				lock(this)
				{
					Reset();
					m_qtw = new QuietTextWriter(value, m_errorHandler);
					WriteHeader();
				}
			}
		}

		/// <summary>
		/// Actual writing occurs here.
		/// <p>Most subclasses of <code>WriterAppender</code> will need to 
		/// override this method.</p>
		/// </summary>
		/// <param name="loggingEvent">the event to log</param>
		protected void SubAppend(LoggingEvent loggingEvent) 
		{
			m_qtw.Write(m_layout.Format(loggingEvent));

			if(m_layout.IgnoresException) 
			{
				string[] s = loggingEvent.GetExceptionStrRep();
				if (s != null) 
				{
					int len = s.Length;
					for(int i = 0; i < len; i++) 
					{
						m_qtw.Write(s[i]);
						m_qtw.Write(LayoutSkeleton.LINE_SEP);
					}
				}
			}
 
			if(m_immediateFlush) 
			{
				m_qtw.Flush();
			} 
		}

		/// <summary>
		/// The WriterAppender requires a layout. Hence, this method returns <code>true</code>.
		/// </summary>
		/// <returns>true because this appender requires a layout</returns>
		override public bool RequiresLayout() 
		{
			return true;
		}

		/// <summary>
		/// Clear internal references to the writer and other variables.
		/// Subclasses can override this method for an alternate closing behavior.
		/// </summary>
		virtual protected void Reset() 
		{
			CloseWriter();
			m_qtw = null;
		}

		/// <summary>
		/// Write a footer as produced by the embedded layout's <see cref="log4net.Layout.ILayout.Footer"/> method.
		/// </summary>
		protected void WriteFooter() 
		{
			if(m_layout != null) 
			{
				string f = m_layout.Footer;
				if(f != null && m_qtw != null)
				{
					m_qtw.Write(f);
				}
			}
		}

		/// <summary>
		/// Write a header produced by the embedded layout's <see cref="log4net.Layout.ILayout.Header"/> method.
		/// </summary>
		protected void WriteHeader() 
		{
			if(m_layout != null) 
			{
				String h = m_layout.Header;
				if(h != null && m_qtw != null)
				{
					m_qtw.Write(h);
				}
			}
		}	
	}
}

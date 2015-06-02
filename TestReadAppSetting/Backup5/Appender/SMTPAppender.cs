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
using System.Web.Mail;
using System.Text;

namespace log4net.Appender
{
	/// <summary>
	/// Send an e-mail when a specific logging event occurs, typically on errors or fatal errors.
	/// </summary>
	/// <remarks>
	/// The number of logging events delivered in this e-mail depend on
	/// the value of <b>BufferSize</b> option. The
	/// <code>SMTPAppender</code> keeps only the last
	/// <code>BufferSize</code> logging events in its cyclic buffer. This
	/// keeps memory requirements at a reasonable level while still
	/// delivering useful application context.
	/// </remarks>
	public class SMTPAppender : AppenderSkeleton
	{
		private const int DEFAULT_BEFFER_SIZE = 512;
		private string m_to;
		private string m_from;
		private string m_subject;
		private string m_smtpHost;
		private int m_bufferSize = DEFAULT_BEFFER_SIZE;
		private bool m_locationInfo = false;

		private CyclicBuffer m_cb = new CyclicBuffer(DEFAULT_BEFFER_SIZE);

		private ITriggeringEventEvaluator m_evaluator;

		/// <summary>
		/// The default constructor will instantiate the appender with a
		/// {@link TriggeringEventEvaluator} that will trigger on events with
		/// priority ERROR or higher.
		/// </summary>
		public SMTPAppender() : this(new DefaultEvaluator())
		{	
		}

		/// <summary>
		/// Use <code>evaluator</code> passed as parameter as the {@link
		/// TriggeringEventEvaluator} for this SMTPAppender.  
		/// </summary>
		/// <param name="evaluator">the trigger to deliver the mail message</param>
		public SMTPAppender(ITriggeringEventEvaluator evaluator) 
		{
			m_evaluator = evaluator;
		}

		/// <summary>
		/// Activate the specified options
		/// </summary>
		override public void ActivateOptions() 
		{
		}
  
		/// <summary>
		/// Perform SMTPAppender specific appending actions, mainly adding
		/// the event to a cyclic buffer and checking if the event triggers
		/// an e-mail to be sent.
		/// </summary>
		/// <param name="loggingEvent"></param>
		override protected void Append(LoggingEvent loggingEvent) 
		{
			if(CheckEntryConditions()) 
			{
				// Force the event to load values now
				string ignoreString = loggingEvent.ThreadName;
				ignoreString = loggingEvent.NestedContext;

				if(m_locationInfo) 
				{
					LocationInfo ignore =  loggingEvent.LocationInformation;
				}

				m_cb.Append(loggingEvent);    

				if(m_evaluator.IsTriggeringEvent(loggingEvent)) 
				{
					SendBuffer();
				}
			}
		}

		/// <summary>
		/// This method determines if there is a sense in attempting to append.
		/// </summary>
		/// <remarks>
		/// It checks whether there is a set output target and also if
		/// there is a set layout. If these checks fail, then the boolean
		/// value <code>false</code> is returned. 
		/// </remarks>
		/// <returns></returns>
		protected bool CheckEntryConditions() 
		{
			if(m_evaluator == null) 
			{
				m_errorHandler.Error("No TriggeringEventEvaluator is set for appender ["+Name+"].");
				return false;
			}
   
			if(m_layout == null) 
			{
				m_errorHandler.Error("No layout set for appender named ["+Name+"].");
				return false;
			}
			return true;
		}

		/// <summary>
		/// Close
		/// </summary>
		override public void Close() 
		{
			lock(this)
			{
				m_closed = true;
			}
		}

		/// <summary>
		/// The <code>SMTPAppender</code> requires a {@link Layout layout}.
		/// </summary>
		/// <returns></returns>
		override public bool RequiresLayout() 
		{
			return true;
		}

		/// <summary>
		/// Send the contents of the cyclic buffer as an e-mail message.
		/// </summary>
		protected void SendBuffer() 
		{
			// Note: this code already owns the monitor for this
			// appender. This frees us from needing to synchronize on 'cb'.
			try 
			{      
				StringBuilder sbuf = new StringBuilder();

				string t = m_layout.Header;
				if(t != null)
				{
					sbuf.Append(t);
				}

				int len =  m_cb.Length;
				for(int i = 0; i < len; i++) 
				{
					LoggingEvent loggingEvent = m_cb.PopOldest();
					sbuf.Append(m_layout.Format(loggingEvent));

					if(m_layout.IgnoresException)
					{
						string[] strData = loggingEvent.GetExceptionStrRep();
						if (strData != null)
						{
							foreach(string str in strData)
							{
								sbuf.Append(str);
							}
						}
					}
				}

				t = m_layout.Footer;
				if(t != null)
				{
					sbuf.Append(t);
				}

				MailMessage mailMessage = new MailMessage();
				mailMessage.Body = sbuf.ToString();
				mailMessage.From = m_from;
				mailMessage.To = m_to;
				mailMessage.Subject = m_subject;

				if (m_smtpHost != null && m_smtpHost.Length > 0)
				{
					SmtpMail.SmtpServer = m_smtpHost;
				}

				SmtpMail.Send(mailMessage);
			} 
			catch(Exception e) 
			{
				LogLog.Error("Error occured while sending e-mail notification.", e);
			}
		}
  
		/// <summary>
		/// Returns value of the <b>EvaluatorClass</b> option
		/// The <b>EvaluatorClass</b> option takes a string value
		/// representing the name of the class implementing the {@link
		/// ITriggeringEventEvaluator} interface. A corresponding object will
		/// be instantiated and assigned as the triggering event evaluator
		/// for the SMTPAppender.
		/// </summary>
		public string EvaluatorClass 
		{
			get { return m_evaluator == null ? null : m_evaluator.GetType().Name; }
			set
			{
				m_evaluator = (ITriggeringEventEvaluator)
					OptionConverter.InstantiateByClassName(value, 
					typeof(ITriggeringEventEvaluator),
					m_evaluator);  
			}
		}

		/// <summary>
		/// Returns value of the <b>To</b> option.
		/// </summary>
		public string To 
		{
			get { return m_to; }
			set { m_to = value; }
		}

		/// <summary>
		/// Returns value of the <b>From</b> option.
		/// </summary>
		public string From 
		{
			get { return m_from; }
			set { m_from = value; }
		}

		/// <summary>
		/// Returns value of the <b>Subject</b> option.
		/// </summary>
		public string Subject 
		{
			get { return m_subject; }
			set { m_subject = value; }
		}
  
		/// <summary>
		/// The <b>BufferSize</b> option takes a positive integer
		/// representing the maximum number of logging events to collect in a
		/// cyclic buffer. When the <code>BufferSize</code> is reached,
		/// oldest events are deleted as new events are added to the
		/// buffer. By default the size of the cyclic buffer is 512 events.
		/// </summary>
		public int BufferSize
		{
			get { return m_bufferSize; }
			set
			{
				m_bufferSize = value;
				m_cb.MaxSize = m_bufferSize;
			}
		}
  
		/// <summary>
		/// The <b>SMTPHost</b> option takes a string value which should be a
		/// the host name of the SMTP server that will send the e-mail message.
		/// </summary>
		public string SMTPHost
		{
			get { return m_smtpHost; }
			set { m_smtpHost = value; }
		}
  
		/// <summary>
		/// The <b>LocationInfo</b> option takes a boolean value. By
		/// default, it is set to false which means there will be no effort
		/// to extract the location information related to the event. As a
		/// result, the layout that formats the events as they are sent out
		/// in an e-mail is likely to place the wrong location information
		/// (if present in the format).
		/// 
		/// <p>Location information extraction is comparatively very slow and
		/// should be avoided unless performance is not a concern.</p>
		/// </summary>
		public bool LocationInfo
		{
			get { return m_locationInfo; }
			set { m_locationInfo = value; }
		}
	}

	class DefaultEvaluator : ITriggeringEventEvaluator 
	{
		/// <summary>
		/// Is this <code>event</code> the e-mail triggering event?
		/// </summary>
		/// <param name="loggingEvent">The event to check</param>
		/// <returns>This method returns <code>true</code>, if the event priority
		/// has ERROR priority or higher. Otherwise it returns <code>false</code></returns>
		public bool IsTriggeringEvent(LoggingEvent loggingEvent) 
		{
			return (loggingEvent.Priority >= Priority.ERROR); 
		}
	}
}

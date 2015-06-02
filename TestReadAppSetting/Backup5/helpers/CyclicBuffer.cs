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

using log4net.spi;

namespace log4net.helpers
{
	/// <summary>
	/// 
	/// </summary>
	public class CyclicBuffer
	{
		#region Member Variables
		LoggingEvent[] m_ea;
		int m_first; 
		int m_last; 
		int m_numElems;
		int m_maxSize;
		#endregion

		/// <summary>
		/// Instantiate a new CyclicBuffer of at most <code>maxSize</code> events.
		/// </summary>
		/// <remarks>The <code>maxSize</code> argument must a positive integer.</remarks>
		/// <param name="maxSize">The maximum number of elements in the buffer</param>
		public CyclicBuffer(int maxSize) 
		{
			if(maxSize < 1) 
			{
				throw new System.Exception("The maxSize argument ("+maxSize+") is not a positive integer.");
			}
			m_maxSize = maxSize;
			m_ea = new LoggingEvent[maxSize];
			m_first = 0;
			m_last = 0;
			m_numElems = 0;
		}
    
		/// <summary>
		/// Add an <code>event</code> as the last event in the buffer
		/// </summary>
		/// <param name="loggingEvent">The event to append to the buffer</param>
		public void Append(LoggingEvent loggingEvent) 
		{    
			lock(this)
			{
				m_ea[m_last] = loggingEvent;    
				if(++m_last == m_maxSize)
				{
					m_last = 0;
				}

				if(m_numElems < m_maxSize)
				{
					m_numElems++;
				}
				else if(++m_first == m_maxSize)
				{
					m_first = 0;
				}
			}
		}

		/// <summary>
		/// Get the <i>i</i>th oldest event currently in the buffer
		/// </summary>
		/// <remarks>
		/// Get the <i>i</i>th oldest event currently in the buffer. If
		/// <em>i</em> is outside the range 0 to the number of elements
		/// currently in the buffer, then <code>null</code> is returned.
		/// </remarks>
		public LoggingEvent this[int i] 
		{
			get
			{
				lock(this)
				{
					if(i < 0 || i >= m_numElems)
					{
						return null;
					}

					return m_ea[(m_first + i) % m_maxSize];
				}
			}
		}

		/// <summary>
		/// Get the maximum size of the buffer
		/// </summary>
		public int MaxSize 
		{
			get 
			{ 
				lock(this)
				{
					return m_maxSize; 
				}
			}
			set { Resize(value); }
		}

		/// <summary>
		/// Get the oldest (first) element in the buffer. The oldest element is removed from the buffer.
		/// </summary>
		/// <returns></returns>
		public LoggingEvent PopOldest() 
		{
			lock(this)
			{
				LoggingEvent r = null;
				if(m_numElems > 0) 
				{
					m_numElems--;
					r = m_ea[m_first];
					m_ea[m_first] = null;
					if(++m_first == m_maxSize)
					{
						m_first = 0;
					}
				} 
				return r;
			}
		}

		/// <summary>
		/// Get the number of elements in the buffer. This number is
		/// guaranteed to be in the range 0 to <code>maxSize</code>
		/// (inclusive).
		/// </summary>
		public int Length
		{
			get 
			{ 
				lock(this) 
				{ 
					return m_numElems; 
				}
			}								    
		}

		/// <summary>
		/// Resize the cyclic buffer to <code>newSize</code>.
		/// </summary>
		/// <param name="newSize"></param>
		public void Resize(int newSize) 
		{
			lock(this)
			{
				if(newSize < 0) 
				{
					throw new System.Exception("Negative array size ["+newSize+"] not allowed.");
				}
				if(newSize == m_numElems)
				{
					return; // nothing to do
				}
    
				LoggingEvent[] temp = new  LoggingEvent[newSize];

				int loopLen = newSize < m_numElems ? newSize : m_numElems;
    
				for(int i = 0; i < loopLen; i++) 
				{
					temp[i] = m_ea[m_first];
					m_ea[m_first] = null;
					if(++m_first == m_numElems) 
						m_first = 0;
				}

				m_ea = temp;
				m_first = 0;
				m_numElems = loopLen;
				m_maxSize = newSize;

				if (loopLen == newSize) 
				{
					m_last = 0;
				} 
				else 
				{
					m_last = loopLen;
				}
			}
		}
	}
}

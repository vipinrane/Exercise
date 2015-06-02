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
using System.Diagnostics;

namespace log4net.spi
{
	/// <summary>
	/// The internal representation of caller location information.
	/// </summary>
	public class LocationInfo
	{

		/// <summary>
		/// When location information is not available the constant
		/// <code>NA</code> is returned. Current value of this string
		/// constant is <b>?</b>.
		/// </summary>
		private const string NA = "?";

		private StackFrame m_locationFrame;

		/// <summary>
		/// Instantiate location information based on the current thread
		/// </summary>
		/// <param name="fqnOfCallingClass"></param>
		public LocationInfo(String fqnOfCallingClass) 
		{
			StackTrace st = new StackTrace(true);

			int frameIndex = 0;

			// skip frames not from fqnOfCallingClass
			while (frameIndex < st.FrameCount)
			{
				StackFrame frame = st.GetFrame(frameIndex);
				if (frame.GetMethod().DeclaringType.FullName == fqnOfCallingClass)
				{
					break;
				}
				frameIndex++;
			}

			// skip frames from fqnOfCallingClass
			while (frameIndex < st.FrameCount)
			{
				StackFrame frame = st.GetFrame(frameIndex);
				if (frame.GetMethod().DeclaringType.FullName != fqnOfCallingClass)
				{
					break;
				}
				frameIndex++;
			}

			if (frameIndex < st.FrameCount)
			{
				// now frameIndex is the first 'user' caller frame
				m_locationFrame = st.GetFrame(frameIndex);
			}
			else
			{
				m_locationFrame = null;
			}
		}

		/// <summary>
		/// Return the fully qualified class name of the caller making the logging request.
		/// </summary>
		/// <returns></returns>
		public string ClassName
		{
			get 
			{ 
				return m_locationFrame == null ? NA : m_locationFrame.GetMethod().DeclaringType.FullName;
			}
		}

		/// <summary>
		/// Return the file name of the caller.
		/// </summary>
		/// <returns></returns>
		public string FileName
		{
			get 
			{ 
				return m_locationFrame == null ? NA : m_locationFrame.GetFileName();
			}
		}

		/// <summary>
		/// Returns the line number of the caller.
		/// </summary>
		/// <returns></returns>
		public string LineNumber
		{
			get 
			{ 
				return m_locationFrame == null ? NA : m_locationFrame.GetFileLineNumber().ToString();
			}
		}

		/// <summary>
		/// Returns the method name of the caller.
		/// </summary>
		/// <returns></returns>
		public string MethodName
		{
			get 
			{
				return m_locationFrame == null ? NA : m_locationFrame.GetMethod().Name;
			}
		}

		/// <summary>
		/// All available caller information, in the format
		/// <code>fully.qualified.classname.of.caller.methodName(Filename:line)</code>
		/// </summary>
		/// <returns></returns>
		public string FullInfo
		{
			get 
			{ 
				return m_locationFrame == null ? NA : m_locationFrame.GetMethod().DeclaringType.FullName+'.'+m_locationFrame.GetMethod().Name+'('+m_locationFrame.GetFileName()+':'+m_locationFrame.GetFileLineNumber()+')';
			}
		}

	}
}

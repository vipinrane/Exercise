using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OpenNETCF.Diagnostics
{
	/// <summary>
	/// Use the static methods of this class to output data to a device's debug port. Messages will be sent only in Debug builds, allowing developers to easily compile out messaging.
	/// </summary>
	/// <remarks>
	/// This is the equivalent of the unmanaged DEBUGMSG macro and applies only to generic CE devices.
	/// Most commercial Pocket PC and SmartPhone devices do not expose a debug port.
	/// </remarks>
	public class DebugMessage
	{
		private static string m_indent = "";

		/// <summary>
		/// If <i>condition</i> evaluates to <b>true</b> and the current assembly is a Debug build, <i>message</i> will be output on the device's debug port.
		/// </summary>
		/// <param name="condition">When <b>true</b> output will be sent to the debug port</param>
		/// <param name="message">Text to output</param>
		[Conditional("DEBUG")] public static void Write(bool condition, string message)
		{
			if(condition)
			{
				DebugMsg(m_indent + message);
			}
		}

		/// <summary>
		/// If <i>condition</i> evaluates to <b>true</b> and the current assembly is a Debug build, the result from <i>obj.ToString</i> will be output on the device's debug port.
		/// </summary>
		/// <param name="condition">When <b>true</b> output will be sent to the debug port</param>
		/// <param name="obj">Object to call ToString() on</param>
		[Conditional("DEBUG")] public static void Write(bool condition, object obj)
		{
			if(condition)
			{
				DebugMsg(m_indent + obj.ToString() + "\r\n");
			}
		}

		/// <summary>
		/// If <i>condition</i> evaluates to <b>true</b> and the current assembly is a Debug build, <i>message</i> will be output on the device's debug port followed by a carriage return and new line.
		/// Lines output with <i>WriteLine</i> are also affected by calls to <c>Indent</c> or <c>Unindent</c>.
		/// </summary>
		/// <param name="condition">When <b>true</b> output will be sent to the debug port</param>
		/// <param name="message">Text to output</param>
		[Conditional("DEBUG")] public static void WriteLine(bool condition, string message)
		{
			if(condition)
			{
				DebugMsg(m_indent + message + "\r\n");
			}
		}

		/// <summary>
		/// Increases the indent level used by <c>WriteLine</c> by two spaces
		/// </summary>
		[Conditional("DEBUG")] public static void Indent()
		{
			m_indent+="  ";
		}

		/// <summary>
		/// Decreases the indent level used by <c>WriteLine</c> by two spaces
		/// </summary>
		[Conditional("DEBUG")] public static void Unindent()
		{
			if(m_indent.Length > 0)
				m_indent = m_indent.Substring(0, m_indent.Length - 2);
		}

		[DllImport("coredll.dll", EntryPoint="NKDbgPrintfW")]
		private static extern void DebugMsg(string message);
	}
}

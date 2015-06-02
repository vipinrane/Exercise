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
using System.Collections;

namespace log4net
{
	/// <summary>
	/// Implementation of NDC the nested diagnostic context.
	/// </summary>
	public class NDC
	{
		/// <summary>
		/// The thread local data slot to use for context information
		/// </summary>
		private readonly static LocalDataStoreSlot s_slot = System.Threading.Thread.AllocateDataSlot();

		/// <summary>
		/// Private constructor
		/// </summary>
		private NDC()
		{
		}

		/// <summary>
		/// Clears all the contextual information held on the 
		/// current thread.
		/// </summary>
		public static void Clear() 
		{
			GetStack().Clear();
		}

		/// <summary>
		/// Creates a clone of the stack of context information. This
		/// can be passed to the <see cref="Inherit"/> method to allow
		/// child threads to inherit the context of their parent thread.
		/// </summary>
		/// <returns></returns>
		public static Stack CloneStack() 
		{
			return (Stack)GetStack().Clone();
		}

		/// <summary>
		/// This thread will use the context information from the stack
		/// supplied. This can be used to initialise child threads with
		/// the same contextual information as their parent theasd. These
		/// contexts will <b>NOT</b> be shared. Any further contexts that
		/// are pushed onto the stack will not be visisble to the other.
		/// Call <see cref="CloneStack"/> to obtain a stack to pass to
		/// this method.
		/// </summary>
		/// <param name="stack">The context stack to inherit</param>
		public static void Inherit(Stack stack) 
		{
			System.Threading.Thread.SetData(s_slot, stack);
		}

		/// <summary>
		/// Get the current context information. That is all the messages that
		/// have been pushed on to the context stack.
		/// </summary>
		/// <returns>The current context information</returns>
		internal static string Get() 
		{
			Stack stack = GetStack();
			if (stack.Count > 0)
			{
				return ((DiagnosticContext)(stack.Peek())).FullMessage;
			}
			return null;
		}
  
		/// <summary>
		/// Get the current context depth
		/// </summary>
		/// <returns>the current context depth</returns>
		public static int GetDepth() 
		{
			return GetStack().Count;
		}

		/// <summary>
		/// Get the current context depth
		/// </summary>
		/// <value>the current context depth</value>
		public static int Depth
		{
			get { return GetDepth(); }
		}

		/// <summary>
		/// Remove the top context from the stack
		/// </summary>
		/// <returns>the message in the context that was removed from the top of the stack</returns>
		public static string Pop() 
		{
			Stack stack = GetStack();
			if (stack.Count > 0)
			{
				return ((DiagnosticContext)(stack.Pop())).Message;
			}
			return "";
		}

		/// <summary>
		/// Peek at the message on the top of the context stack
		/// </summary>
		/// <returns>The message on the top of the stack</returns>
		internal static string Peek() 
		{
			Stack stack = GetStack();
			if (stack.Count > 0)
			{
				return ((DiagnosticContext)(stack.Peek())).Message;
			}
			return "";
		}
  
		/// <summary>
		/// Push a new context message
		/// </summary>
		/// <param name="message">The new context message</param>
		public static void Push(string message) 
		{
			Stack stack = GetStack();
			stack.Push(new DiagnosticContext(message, stack.Count>0 ? (DiagnosticContext)stack.Peek() : null));
		}

		/// <summary>
		/// Remove the context information for this thread. It is
		/// not required to call this method.
		/// </summary>
		static public void Remove() 
		{
		}

		/// <summary>
		/// Forces the stack depth to be at most <code>maxDepth</code>.
		/// This may truncate the head of the stack. This only affects the 
		/// stack in the current thread. Also it does not prevent it from
		/// growing, it only sets the maximum depth at the time of the
		/// call. This can be used to return to a known context depth.
		/// </summary>
		/// <param name="maxDepth">The maximum depth of the stack</param>
		static public void SetMaxDepth(int maxDepth) 
		{
			if (maxDepth >= 0)
			{
				Stack stack = GetStack();
				while(stack.Count > maxDepth)
				{
					stack.Pop();
				}
			}
		}

		/// <summary>
		/// Get the stack of context objects on this thread
		/// </summary>
		/// <returns>the stack of context objects</returns>
		static private Stack GetStack()
		{
			Stack stack = (Stack)System.Threading.Thread.GetData(s_slot);
			if (stack == null)
			{
				stack = new Stack();
				System.Threading.Thread.SetData(s_slot, stack);
			}
			return stack;
		}

		/// <summary>
		/// Inner class used to represent a sing context in the stack
		/// </summary>
		internal class DiagnosticContext 
		{
			private string m_fullMessage;
			private string m_message;
    
			/// <summary>
			/// Construct a new context
			/// </summary>
			/// <param name="message">The nessage for this context</param>
			/// <param name="parent">The parent context in the chain</param>
			internal DiagnosticContext(string message, DiagnosticContext parent) 
			{
				m_message = message;
				if(parent != null) 
				{
					m_fullMessage = parent.FullMessage + ' ' + message;
				} 
				else 
				{
					m_fullMessage = message;
				}
			}

			/// <summary>
			/// Get the message
			/// </summary>
			internal string Message
			{
				get { return m_message; }
			}

			/// <summary>
			/// Get the full text of the context down to the root level
			/// </summary>
			internal string FullMessage
			{
				get { return m_fullMessage; }
			}
		}
	}
}

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

namespace log4net.helpers
{
	/// <summary>
	/// An allways empty Enumerator.
	/// </summary>
	public class NullEnumerator : System.Collections.IEnumerator
	{
		/// <summary>
		/// The singleton instance of the null enumerator
		/// </summary>
		private readonly static NullEnumerator s_instance = new NullEnumerator();
  
		/// <summary>
		/// Private constructor to enforce the singleton pattern
		/// </summary>
		private	NullEnumerator() 
		{
		}
  
		/// <summary>
		/// Get the singleton instance of the null enumerator
		/// </summary>
		/// <returns>get the instance of the null enumerator</returns>
		public static NullEnumerator GetInstance() 
		{
			return s_instance;
		}

		/// <summary>
		/// Gets the current object from the enumerator
		/// </summary>
		/// <remarks>
		/// This throws an exception because the NullEnumerator
		/// never has a current value.
		/// </remarks>
		public object Current 
		{
			get	{ throw new InvalidOperationException(); }
		}
  
		/// <summary>
		/// Test if the enumerator can advance, if so advance
		/// </summary>
		/// <remarks>
		/// Always returns false as the NullEnumerator cannot advance
		/// </remarks>
		/// <returns>always false</returns>
		public bool MoveNext()
		{
			return false;
		}
  
		/// <summary>
		/// Reset the enumerator back to the start
		/// </summary>
		public void Reset() 
		{
		}
	}
}

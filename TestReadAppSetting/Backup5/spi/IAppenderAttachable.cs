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

using log4net.Appender;

namespace log4net.spi
{
	/// <summary>
	/// Interface for attaching appenders to objects.
	/// </summary>
	public interface IAppenderAttachable
	{
		/// <summary>
		/// Add an appender.
		/// </summary>
		/// <param name="newAppender"></param>
		void AddAppender(IAppender newAppender);

		/// <summary>
		/// Get all previously added appenders as an Enumeration.
		/// </summary>
		/// <returns></returns>
		System.Collections.IEnumerator GetAllAppenders();

		/// <summary>
		/// Get an appender by name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		IAppender GetAppender(string name);

		/// <summary>
		/// Remove all previously added appenders.
		/// </summary>
		void RemoveAllAppenders();

		/// <summary>
		/// Remove the appender passed as parameter from the list of appenders.
		/// </summary>
		/// <param name="appender"></param>
		void RemoveAppender(IAppender appender);

		/// <summary>
		/// Remove the appender with the name passed as parameter from the list of appenders.  
		/// </summary>
		/// <param name="name"></param>
		void RemoveAppender(string name);   	
	}
}

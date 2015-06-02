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

using System.Configuration;
using System.Xml;

namespace log4net.Config
{
	/// <summary>
	/// Class to register for the log4net section of the configuration file
	/// </summary>
	/// <remarks>
	/// The log4net section of the configuration file needs to have a section
	/// handler registered. This is the section handler used. It simply returns
	/// the XML element that is the root of the section.
	/// </remarks>
	/// <example>
	/// Example of registering the section handler
	/// <code>
	/// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
	/// &lt;configuration&gt;
	///		&lt;configSections&gt;
	///			&lt;section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler,log4net" /&gt;
	///		&lt;/configSections&gt;
	///		&lt;log4net&gt;
	///			log4net configuration XML goes here
	///		&lt;/log4net&gt;
	/// &lt;/configuration&gt;
	/// </code>
	/// </example>
	public class Log4NetConfigurationSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public Log4NetConfigurationSectionHandler()
		{
		}

		/// <summary>
		/// method called to parse the configuration section
		/// </summary>
		/// <param name="parent">ignored</param>
		/// <param name="configContext">ignored</param>
		/// <param name="section">the XML node for the section</param>
		/// <returns>the XML node for the section</returns>
		public object Create(object parent,	object configContext, XmlNode section)
		{
			return section;
		}
	}
}

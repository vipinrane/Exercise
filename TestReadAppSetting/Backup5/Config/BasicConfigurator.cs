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
using log4net.Appender;
using log4net.Layout;
using log4net.helpers;
using log4net.or;

namespace log4net.Config
{
	/// <summary>
	/// Use this class to quickly configure the package.
	/// </summary>
	public class BasicConfigurator
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		protected BasicConfigurator()
		{
		}

		/// <summary>
		/// Used by subclasses to add a renderer to the hierarchy passed as parameter.
		/// </summary>
		/// <remarks>
		/// The <paramref name="renderingClassName"/> must specify a type that implements the <see cref="IObjectRenderer"/> interface/
		/// </remarks>
		/// <param name="hierarchy">the Hierarchy to add the renderer to</param>
		/// <param name="renderedClassName">the type that will be rendered by the renderer</param>
		/// <param name="renderingClassName">the type of the renderer</param>
		protected void AddRenderer(Hierarchy hierarchy, string renderedClassName, string renderingClassName) 
		{
			LogLog.Debug("Rendering class: ["+renderingClassName+"], Rendered class: ["+renderedClassName+"].");
			IObjectRenderer renderer = (IObjectRenderer)OptionConverter.InstantiateByClassName(renderingClassName, typeof(IObjectRenderer), null);
			if(renderer == null) 
			{
				LogLog.Error("Could not instantiate renderer ["+renderingClassName+"].");
				return;
			} 
			else 
			{
				try 
				{
					Type renderedClass = Type.GetType(renderedClassName);
					hierarchy.AddRenderer(renderedClass, renderer);
				} 
				catch(Exception e) 
				{
					LogLog.Error("Could not find class ["+renderedClassName+"].", e);
				}
			}
		}

		/// <summary>
		/// Initialise the log4net system with a default configuration.
		/// </summary>
		/// <remarks>
		/// Initialises the log4net logging system using a <see cref="ConsoleAppender"/>
		/// that will write to <c>Console.Out</c>. The log messages are
		/// formatted using the <see cref="PatternLayout"/> layout object
		/// using the <see cref="PatternLayout.TTCC_CONVERSION_PATTERN"/>
		/// layout style.
		/// </remarks>
		static public void Configure() 
		{
			Category.Root.AddAppender(new ConsoleAppender(new PatternLayout(PatternLayout.TTCC_CONVERSION_PATTERN)));
		}

		/// <summary>
		/// Initialise the log4net system using the specified appender
		/// </summary>
		/// <param name="appender">the appender to use to log all logging events</param>
		static public void Configure(IAppender appender) 
		{
			Category.Root.AddAppender(appender);
		}
	}
}

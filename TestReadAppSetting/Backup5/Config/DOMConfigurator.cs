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

using System.Xml;
using System.Collections;

using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Filter;
using log4net.helpers;
using log4net.spi;

namespace log4net.Config
{
	/// <summary>
	/// Use this class to initialize the log4net environment using a DOM tree.
	/// </summary>
	public class DOMConfigurator : BasicConfigurator
	{
		// String constants used while parsing the XML data
		const string CONFIGURATION_TAG			= "log4net";
		const string RENDERER_TAG				= "renderer";
		const string APPENDER_TAG 				= "appender";
		const string APPENDER_REF_TAG 			= "appender-ref";  
		const string PARAM_TAG    				= "param";
		const string LAYOUT_TAG					= "layout";
		const string CATEGORY					= "category";
		const string CATEGORY_FACTORY_TAG		= "categoryFactory";
		const string NAME_ATTR					= "name";
		const string CLASS_ATTR					= "type";
		const string VALUE_ATTR					= "value";
		const string ROOT_TAG					= "root";
		const string PRIORITY_TAG				= "priority";
		const string FILTER_TAG					= "filter";
		const string ERROR_HANDLER_TAG			= "errorHandler";
		const string REF_ATTR					= "ref";
		const string ADDITIVITY_ATTR			= "additivity";  
		const string DISABLE_OVERRIDE_ATTR		= "disableOverride";
		const string DISABLE_ATTR				= "disable";
		const string CONFIG_DEBUG_ATTR			= "configDebug";
		const string INTERNAL_DEBUG_ATTR		= "debug";
		const string RENDERING_CLASS_ATTR		= "renderingClass";
		const string RENDERED_CLASS_ATTR		= "renderedClass";

		// flag used on the priority element
		const string INHERITED = "inherited";


		/// <summary>
		/// key: appenderName, value: appender
		/// </summary>
		private Hashtable m_appenderBag;

		/// <summary>
		/// No argument constructor.
		/// </summary>
		public DOMConfigurator() 
		{ 
			m_appenderBag = new Hashtable();
		}

		/// <summary>
		/// Used internally to parse appenders by IDREF.
		/// </summary>
		/// <param name="appenderRef">the appender ref element</param>
		/// <returns>the instance of the appender that the ref referes to</returns>
		protected IAppender FindAppenderByReference(XmlElement appenderRef) 
		{    
			string appenderName = appenderRef.GetAttribute(REF_ATTR);

			IAppender appender = (IAppender)m_appenderBag[appenderName];
			if(appender != null) 
			{
				return appender;
			} 
			else 
			{
				// Find the element with that id
				XmlElement element = null;

				if(appenderName != null && appenderName.Length > 0)
				{
					foreach (XmlNode node in appenderRef.OwnerDocument.GetElementsByTagName(APPENDER_TAG))
					{
						if (((XmlElement)node).GetAttribute("name") == appenderName)
						{
							element = (XmlElement)node;
							break;
						}
					}
				}
				if(element == null) 
				{
					LogLog.Error("No appender named ["+appenderName+"] could be found."); 
					return null;
				} 
				else
				{
					appender = ParseAppender(element);
					m_appenderBag[appenderName] = appender;
					return appender;
				}
			} 
		}

		/// <summary>
		/// Used internally to parse an appender element.
		/// </summary>
		/// <param name="appenderElement">the appender element</param>
		/// <returns>the appender instance</returns>
		protected IAppender ParseAppender(XmlElement appenderElement) 
		{
			string className = appenderElement.GetAttribute(CLASS_ATTR);
			LogLog.Debug("Class name: [" + className+']');    
			try 
			{
				IAppender appender = (IAppender)Type.GetType(className).GetConstructor(Type.EmptyTypes).Invoke(null);
				appender.Name = appenderElement.GetAttribute(NAME_ATTR);

				foreach (XmlNode currentNode in appenderElement.ChildNodes)
				{
					/* We're only interested in Elements */
					if (currentNode.NodeType == XmlNodeType.Element) 
					{
						XmlElement currentElement = (XmlElement)currentNode;

						// Parse appender parameters 
						if (currentElement.LocalName == PARAM_TAG)
						{
							SetParameter(currentElement, appender);
						}
							// Set appender layout
						else if (currentElement.LocalName == LAYOUT_TAG)
						{
							appender.Layout = ParseLayout(currentElement);
						}
							// Add filters
						else if (currentElement.LocalName == FILTER_TAG)
						{
							ParseFilters(currentElement, appender);
						}
						else if (currentElement.LocalName == ERROR_HANDLER_TAG)
						{
							ParseErrorHandler(currentElement, appender);
						}
						else if (currentElement.LocalName == APPENDER_REF_TAG)
						{
							string refName = currentElement.GetAttribute(REF_ATTR);
							if(appender is IAppenderAttachable) 
							{
								IAppenderAttachable aa = (IAppenderAttachable)appender;
								LogLog.Debug("Attaching appender named ["+ refName+	"] to appender named ["+ appender.Name+"].");
								aa.AddAppender(FindAppenderByReference(currentElement));
							} 
							else 
							{
								LogLog.Error("Requesting attachment of appender named ["+refName+ "] to appender named ["+ appender.Name+"] which does not implement log4net.spi.IAppenderAttachable.");
							}
						}
					}
				}
				if (appender is IOptionHandler) 
				{
					((IOptionHandler)appender).ActivateOptions();
				}
				return appender;
			}
				/* Yes, it's ugly.  But all of these exceptions point to the same
				   problem: we can't create an Appender */
			catch (Exception oops) 
			{
				LogLog.Error("Could not create an Appender. Reported error follows.", oops);
				return null;
			}
		}

		/// <summary>
		/// Used internally to parse an error handler element.
		/// </summary>
		/// <param name="element">the error hander element</param>
		/// <param name="appender">the appender to set the error handler on</param>
		protected void ParseErrorHandler(XmlElement element, IAppender appender) 
		{
			IErrorHandler eh = (IErrorHandler)Type.GetType(element.GetAttribute(CLASS_ATTR)).GetConstructor(Type.EmptyTypes).Invoke(null);
    
			if(eh != null) 
			{
				foreach (XmlNode currentNode in element.ChildNodes)
				{
					if (currentNode.NodeType == XmlNodeType.Element) 
					{
						XmlElement currentElement = (XmlElement)currentNode;

						if(currentElement.LocalName == PARAM_TAG)
						{
							SetParameter(currentElement, eh);
						}
					}
				}
				if (eh is IOptionHandler) 
				{
					((IOptionHandler)eh).ActivateOptions();
				}
				appender.ErrorHandler = eh;
			}
		}
  
		/// <summary>
		/// Used internally to parse a filter element.
		/// </summary>
		/// <param name="element">the filter element</param>
		/// <param name="appender">the appender to add the filter to</param>
		protected void ParseFilters(XmlElement element, IAppender appender) 
		{
			IFilter filter = (IFilter)Type.GetType(element.GetAttribute(CLASS_ATTR)).GetConstructor(Type.EmptyTypes).Invoke(null);
    
			if(filter != null) 
			{
				foreach (XmlNode currentNode in element.ChildNodes)
				{
					if (currentNode.NodeType == XmlNodeType.Element) 
					{
						XmlElement currentElement = (XmlElement)currentNode;
						if(currentElement.LocalName == PARAM_TAG)
						{
							SetParameter(currentElement, filter);
						}
					}
				}
				((IOptionHandler)filter).ActivateOptions();
				appender.AddFilter(filter);
			}    
		}

		/// <summary>
		/// Used internally to parse an category element.
		/// </summary>
		/// <param name="categoryElement">the category element</param>
		/// <param name="hierarchy">the hierarchy to add the category to</param>
		protected void ParseCategory(XmlElement categoryElement, Hierarchy hierarchy) 
		{
			// Create a new log4net.Category object from the <category> element.
			string catName = categoryElement.GetAttribute(NAME_ATTR);
			string className = categoryElement.GetAttribute(CLASS_ATTR);
			Category cat;    

			if(className.Length <= 0) 
			{
				LogLog.Debug("Retreiving an instance of log4net.Category.");
				cat = hierarchy.GetInstance(catName);
			}
			else 
			{
				LogLog.Debug("Desired category sub-class: ["+className+']');
				try 
				{	 
					cat = (Category)Type.GetType(className).GetMethod("getInstance").Invoke(null, new Object[] { catName });
				} 
				catch (Exception oops) 
				{
					LogLog.Error("Could not retrieve category ["+catName+"]. Reported error follows.", oops);
					return;
				}
			}

			// Setting up a category needs to be an atomic operation, in order
			// to protect potential log operations while category
			// configuration is in progress.
			lock(cat) 
			{
				bool additivity = OptionConverter.ToBoolean(categoryElement.GetAttribute(ADDITIVITY_ATTR), true);
    
				LogLog.Debug("Setting ["+cat.Name+"] additivity to ["+additivity+"].");
				cat.Additivity = additivity;
				ParseChildrenOfCategoryElement(categoryElement, cat, false);
			}
		}

		/// <summary>
		/// Used internally to parse the category factory element.
		/// </summary>
		/// <param name="factoryElement">the factory element</param>
		/// <param name="hierarchy">the hierarchy to set the category factory on</param>
		protected void ParseCategoryFactory(XmlElement factoryElement, Hierarchy hierarchy) 
		{
			string className = factoryElement.GetAttribute(CLASS_ATTR);

			if(className.Length <= 0) 
			{
				LogLog.Error("Category Factory tag " + CLASS_ATTR + " attribute not found.");
				LogLog.Debug("No Category Factory configured.");
			}
			else 
			{
				LogLog.Debug("Desired category factory: ["+className+']');
				ICategoryFactory catFactory = (ICategoryFactory)Type.GetType(className).GetConstructor(Type.EmptyTypes).Invoke(null);

				foreach (XmlNode currentNode in factoryElement.ChildNodes)
				{
					if (currentNode.NodeType == XmlNodeType.Element) 
					{
						XmlElement currentElement = (XmlElement)currentNode;
						if (currentElement.LocalName == PARAM_TAG) 
						{
							SetParameter(currentElement, catFactory);
						}
					}
				}
				if (catFactory is IOptionHandler) 
				{
					((IOptionHandler)catFactory).ActivateOptions();
				}
			}
		}

		/// <summary>
		/// Used internally to parse the roor category element.
		/// </summary>
		/// <param name="rootElement">the root element</param>
		/// <param name="hierarchy">the hierarchy to set the root element on</param>
		protected  void ParseRoot(XmlElement rootElement, Hierarchy hierarchy) 
		{
			Category root = hierarchy.Root;
			// category configuration needs to be atomic
			lock(root) 
			{    
				ParseChildrenOfCategoryElement(rootElement, root, true);
			}
		}

		/// <summary>
		/// Used internally to parse the children of a category element.
		/// </summary>
		/// <param name="catElement">the catefory element</param>
		/// <param name="cat">the category instance</param>
		/// <param name="isRoot">flag to indicate if the category is the root category</param>
		protected void ParseChildrenOfCategoryElement(XmlElement catElement, Category cat, bool isRoot) 
		{
			// Remove all existing appenders from cat. They will be
			// reconstructed if need be.
			cat.RemoveAllAppenders();

			foreach (XmlNode currentNode in catElement.ChildNodes)
			{
				if (currentNode.NodeType == XmlNodeType.Element) 
				{
					XmlElement currentElement = (XmlElement) currentNode;
	
					if (currentElement.LocalName == APPENDER_REF_TAG)
					{
						IAppender appender = FindAppenderByReference(currentElement);
						string refName =  currentElement.GetAttribute(REF_ATTR);
						if(appender != null)
						{
							LogLog.Debug("Adding appender named ["+ refName+ "] to category ["+cat.Name+"].");
						}
						else 
						{
							LogLog.Debug("Appender named ["+ refName + "] not found.");
						}
	    
						cat.AddAppender(appender);
					} 
					else if(currentElement.LocalName == PRIORITY_TAG) 
					{
						ParsePriority(currentElement, cat, isRoot);	
					} 
					else if(currentElement.LocalName == PARAM_TAG)
					{
						SetParameter(currentElement, cat);
					}
				}
			}
			if (cat is IOptionHandler) 
			{
				((IOptionHandler)cat).ActivateOptions();
			}
		}

		/// <summary>
		/// Used internally to parse a layout element.
		/// </summary>
		/// <param name="layout_element">the layout element</param>
		/// <returns>the instance of the layout object</returns>
		protected ILayout ParseLayout(XmlElement layout_element) 
		{
			string className = layout_element.GetAttribute(CLASS_ATTR);
			LogLog.Debug("Parsing layout of class: \""+className+"\"");		 
			try 
			{
				ILayout layout = (ILayout)Type.GetType(className).GetConstructor(Type.EmptyTypes).Invoke(null);
      
				foreach (XmlNode currentNode in layout_element.ChildNodes)
				{
					if (currentNode.NodeType == XmlNodeType.Element) 
					{
						XmlElement currentElement = (XmlElement) currentNode;
						if(currentElement.LocalName == PARAM_TAG)
						{
							SetParameter(currentElement, layout);
						}
					}
				}
      
				if (layout is IOptionHandler) 
				{
					((IOptionHandler)layout).ActivateOptions();
				}
				return layout;
			}
			catch (Exception oops) 
			{
				LogLog.Error("Could not create the Layout. Reported error follows.", oops);
				return null;
			}
		}

		/// <summary>
		/// Used internally to parse an object renderer
		/// </summary>
		/// <param name="element">the renderer element</param>
		/// <param name="hierarchy">the hierarchy to add the renderer to</param>
		protected void ParseRenderer(XmlElement element, Hierarchy hierarchy) 
		{
			string renderingClass = element.GetAttribute(RENDERING_CLASS_ATTR);
			string renderedClass = element.GetAttribute(RENDERED_CLASS_ATTR);
			AddRenderer(hierarchy, renderedClass, renderingClass);
		}

		/// <summary>
		/// Used internally to parse a priority element.
		/// </summary>
		/// <param name="element">the priority element</param>
		/// <param name="cat">the category object to set the priority on</param>
		/// <param name="isRoot">flag to indicate if the category is the root category</param>
		protected void ParsePriority(XmlElement element, Category cat, bool isRoot) 
		{
			string catName = cat.Name;
			if(isRoot) 
			{
				catName = "root";
			}

			string priStr = element.GetAttribute(VALUE_ATTR);
			LogLog.Debug("Priority value for "+catName+" is  ["+priStr+"].");
    
			if(INHERITED == priStr) 
			{
				if(isRoot) 
				{
					LogLog.Error("Root priority cannot be inherited. Ignoring directive.");
				} 
				else 
				{
					cat.Priority = null;
				}
			} 
			else 
			{
				string className = element.GetAttribute(CLASS_ATTR);      
				if(className.Length <= 0) 
				{      
					cat.Priority = Priority.Parse(priStr);
				} 
				else 
				{
					LogLog.Debug("Desired Priority sub-class: ["+className+']');
					try 
					{	 
						Priority pri = (Priority)Type.GetType(className).GetMethod("ToPriority").Invoke(null, new object[] { priStr });
						cat.Priority = pri;
					} 
					catch (Exception oops) 
					{
						LogLog.Error("Could not create priority ["+priStr+"]. Reported error follows.", oops);
						return;
					}
				}
			}
			LogLog.Debug(catName + " priority set to " + cat.Priority);    
		}

		/// <summary>
		/// Internal function to set a param on an object.
		/// </summary>
		/// <remarks>
		/// The param name must correspond to a writable property
		/// on the object. The value of the param is a string,
		/// therefore this function will attempt to set a string
		/// property first. If unable to set a string property it
		/// will inspect the property and its argument type. It will
		/// attempt to call a static method called 'Parse' on the
		/// type of the property. This method will take a single
		/// string argument and return a value that can be used to
		/// set the property.
		/// </remarks>
		/// <param name="elem">the param element</param>
		/// <param name="target">the object to set the param on</param>
		protected void SetParameter(XmlElement elem, object target) 
		{
			string name = elem.GetAttribute(NAME_ATTR);
			string propertyValue = elem.GetAttribute(VALUE_ATTR);

			propertyValue = OptionConverter.ConvertSpecialChars(propertyValue);

			System.Reflection.PropertyInfo prop;
			Type targetType = target.GetType();

			prop = targetType.GetProperty(name, typeof(string));
			if (prop != null)
			{
				prop.SetValue(target, propertyValue, null);
			}
			else
			{
				// Try to find any form of converter
				prop = targetType.GetProperty(name);
				if (prop != null && prop.CanWrite)
				{
					System.Reflection.MethodInfo meth = prop.PropertyType.GetMethod("Parse", new Type[] { typeof(string) });
					if (meth != null)
					{
						object convertedValue = meth.Invoke(null, new object[] { propertyValue } );
						if (convertedValue != null)
						{
							prop.SetValue(target, convertedValue, null);
							return;
						}
					}
				}
				LogLog.Warn("Unable to file property ["+name+"] on object ["+target+"] (with acceptable conversion types)");
			}
		}

		/// <summary>
		/// Configure log4net using a <code>log4net</code> element
		/// </summary>
		/// <param name="element">the element to parse</param>
		static public void Configure(XmlElement element) 
		{
			if (element != null)
			{
				DOMConfigurator configurator = new DOMConfigurator();
				configurator.Parse(element, Category.DefaultHierarchy);
			}
		}

		/// <summary>
		/// Automaticaly configure the log4net system based on the 
		/// application's configuration settings.
		/// </summary>
		/// <remarks>
		/// Each application has a configuration file. This has the
		/// same name as the application with '.config' appended.
		/// This file is XML and calling this function prompts the
		/// configurator to look in that file for a section called
		/// <c>log4net</c> that contains the configuration data.
		/// </remarks>
  		new static public void Configure() 
		{
			Configure((XmlElement)System.Configuration.ConfigurationSettings.GetConfig("log4net"));
		}

		/// <summary>
		/// Used internally to configure the log4net framework by parsing a DOM tree of XML elements.
		/// </summary>
		/// <param name="element">the root element to parse</param>
		/// <param name="hierarchy">the hierarchy to build</param>
		protected void Parse(XmlElement element, Hierarchy hierarchy) 
		{
			if (element == null || hierarchy == null)
			{
				return;
			}

			string rootElementName = element.LocalName;

			if (rootElementName != CONFIGURATION_TAG)
			{
				LogLog.Error("DOM element is - not a <"+CONFIGURATION_TAG+"> element.");
				return;
			}

			string debugAttrib = element.GetAttribute(INTERNAL_DEBUG_ATTR);
      
			LogLog.Debug("debug attribute= \"" + debugAttrib +"\".");

			if(debugAttrib.Length>0 && debugAttrib != "null") 
			{      
				LogLog.SetInternalDebugging(OptionConverter.ToBoolean(debugAttrib, true));
			}
			else 
			{
				LogLog.Debug("Ignoring " + INTERNAL_DEBUG_ATTR + " attribute.");
			}

			string confDebug = element.GetAttribute(CONFIG_DEBUG_ATTR);
			if(confDebug.Length>0 && confDebug != "null")
			{      
				LogLog.Warn("The \""+CONFIG_DEBUG_ATTR+"\" attribute is deprecated.");
				LogLog.Warn("Use the \""+INTERNAL_DEBUG_ATTR+"\" attribute instead.");
				LogLog.SetInternalDebugging(OptionConverter.ToBoolean(confDebug, true));
			}

			string disableOverride = element.GetAttribute(DISABLE_OVERRIDE_ATTR);
			LogLog.Debug("Disable override=\"" + disableOverride +"\".");

			if(disableOverride.Length>0 && disableOverride != "null")
			{
				hierarchy.OverrideAsNeeded(disableOverride);
			}

			string disableStr = element.GetAttribute(DISABLE_ATTR);
			LogLog.Debug("Disable =\"" + disableStr +"\".");
			if(disableStr.Length>0 && disableStr != "null") 
			{
				hierarchy.Disable(disableStr);
			}
    
			/* Building Appender objects, placing them in a local namespace
			   for future reference */

			// First configure each category factory under the root element.
			// Category factories need to be configured before any of
			// categories they support.
			//

			foreach (XmlNode currentNode in element.ChildNodes)
			{
				if (currentNode.NodeType == XmlNodeType.Element) 
				{
					XmlElement currentElement = (XmlElement) currentNode;

					if (currentElement.LocalName == CATEGORY_FACTORY_TAG) 
					{
						ParseCategoryFactory(currentElement, hierarchy);
					}
				}
			}
    
			foreach (XmlNode currentNode in element.ChildNodes)
			{
				if (currentNode.NodeType == XmlNodeType.Element) 
				{
					XmlElement currentElement = (XmlElement) currentNode;

					if (currentElement.LocalName == CATEGORY)
					{
						ParseCategory(currentElement, hierarchy);
					} 
					else if (currentElement.LocalName == ROOT_TAG)
					{
						ParseRoot(currentElement, hierarchy);
					} 
					else if(currentElement.LocalName == RENDERER_TAG)
					{
						ParseRenderer(currentElement, hierarchy);
					}
				}
			}
		}

	}

}


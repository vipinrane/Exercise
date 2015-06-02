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
using System.Text;
using System.Reflection;

namespace log4net.helpers
{
	/// <summary>
	/// A convenience class to convert property values to specific types.
	/// </summary>
	public class OptionConverter
	{
		private const string DELIM_START = "${";
		private const char   DELIM_STOP  = '}';
		private const int DELIM_START_LEN = 2;
		private const int DELIM_STOP_LEN  = 1;

		/// <summary>
		/// Private constructor to prevent instances
		/// </summary>
		private OptionConverter() {}

		/// <summary>
		/// Concatenates two string arrays
		/// </summary>
		/// <param name="l">left array</param>
		/// <param name="r">right array</param>
		/// <returns>array containg left and right arrays</returns>
		public static string[] ConcatanateArrays(string[] l, string[] r) 
		{
			return (string[])ConcatanateArrays(l, r);
		}

		/// <summary>
		/// Concatenates two arrays
		/// </summary>
		/// <param name="l">left array</param>
		/// <param name="r">right array</param>
		/// <returns>array containg left and right arrays</returns>
		public static Array ConcatanateArrays(Array l, Array r) 
		{
			int len = l.Length + r.Length;
			Array a = Array.CreateInstance(l.GetType(), len);

			Array.Copy(l, 0, a, 0, l.Length);
			Array.Copy(r, 0, a, l.Length, r.Length);

			return a;
		}
  
		/// <summary>
		/// Converts string escape chars back to their correct values
		/// </summary>
		/// <param name="s">string to convert</param>
		/// <returns>converted result</returns>
		public static string ConvertSpecialChars(string s) 
		{
			char c;
			int len = s.Length;
			StringBuilder sbuf = new StringBuilder(len);
    
			int i = 0;
			while(i < len) 
			{
				c = s[i++];
				if (c == '\\') 
				{
					c =  s[i++];
					if(c == 'n')      c = '\n';
					else if(c == 'r') c = '\r';
					else if(c == 't') c = '\t';
					else if(c == 'f') c = '\f';
					else if(c == '\b') c = '\b';					
					else if(c == '\"') c = '\"';				
					else if(c == '\'') c = '\'';			
					else if(c == '\\') c = '\\';			
				}
				sbuf.Append(c);      
			}
			return sbuf.ToString();
		}

		/// <summary>
		/// Convert a string to a bool value
		/// </summary>
		/// <remarks>
		/// If <code>value</code> is "true", then <code>true</code> is returned. 
		/// If <code>value</code> is "false", then <code>true</code> is returned. 
		/// Otherwise, <code>default</code> is returned.
		/// </remarks>
		/// <param name="argValue">string to convert</param>
		/// <param name="defaultValue">the default value</param>
		/// <returns>true or false</returns>
		public static bool ToBoolean(string argValue, bool defaultValue) 
		{
			if(argValue == null)
			{
				return defaultValue;
			}
			string trimmedVal = argValue.Trim();
			if(String.Compare("true", trimmedVal, true) == 0) 
			{
				return true;
			}
			if(String.Compare("false", trimmedVal, true) == 0) 
			{
				return false;
			}
			return defaultValue;
		}

		/// <summary>
		/// Convert a string to an integer
		/// </summary>
		/// <param name="argValue">string to convert</param>
		/// <param name="defaultValue">default value</param>
		/// <returns>the int value of the string parsed</returns>
		public static int ToInt(string argValue, int defaultValue) 
		{
			if(argValue != null) 
			{
				string s = argValue.Trim();
				try 
				{
					return int.Parse(s);
				}
				catch (Exception e) 
				{
					LogLog.Error("[" + s + "] is not in proper int form.");
					Console.Error.WriteLine(e.StackTrace);
				}
			}
			return defaultValue;
		}

		/// <summary>
		/// Converts a standard or custom priority level to a Priority object.  
		/// </summary>
		/// <remarks>
		/// If <code>value</code> is of form
		/// "priority#classname", then the specified class' toPriority method
		/// is called to process the specified priority string; if no '#'
		/// character is present, then the default <see cref="log4net.Priority"/>
		/// class is used to process the priority value.  
		/// 
		/// <p>If any error occurs while converting the value to a priority,
		/// the dflt value (which may be null) is returned.</p>
		/// 
		/// <p>Case of value is unimportant for the priority level, but is significant
		/// for any class name part present.</p>
		/// </remarks>
		/// <param name="argValue"></param>
		/// <param name="defaultValue"></param>
		/// <returns>The Priority object</returns>
		public static Priority ToPriority(string argValue, Priority defaultValue) 
		{
			if(argValue == null)
			{
				return defaultValue;
			}

			int hashIndex = argValue.IndexOf('#');
			if (hashIndex == -1) 
			{
				// no class name specified : use standard Priority class
				return Priority.Parse(argValue, defaultValue);
			}

			Priority result = defaultValue;

			string clazz = argValue.Substring(hashIndex+1);
			string priorityName = argValue.Substring(0, hashIndex);

			LogLog.Debug("toPriority" + ":class=[" + clazz + "]" + ":pri=[" + priorityName + "]");

			try 
			{
				// get a ref to the specified class' static method
				// ToPriority(string, log4net.Priority)
				MethodInfo toPriorityMethod = Type.GetType(clazz).GetMethod("Parse", new Type[] { typeof(string), typeof(Priority) } );

				// now call the toPriority method, passing priority string + default
				result = (Priority)toPriorityMethod.Invoke(null, new object[] { priorityName, defaultValue} );
			} 
			catch(Exception e)
			{
				LogLog.Warn("class ["+clazz+"], priority ["+priorityName+"] conversion failed.", e);
			}
			return result;
		}
 
		/// <summary>
		/// Parse a file size into a number
		/// </summary>
		/// <remarks>
		/// Parses a file size of the form: number[KB|MB|GB] into a
		/// long value. It is scaled with the appropriate multiplier.
		/// </remarks>
		/// <param name="argValue">string to parse</param>
		/// <param name="defaultValue">default value</param>
		/// <returns>the value of the string parsed to a long</returns>
		public static long ToFileSize(string argValue, long defaultValue) 
		{
			if(argValue == null)
			{
				return defaultValue;
			}
    
			string s = argValue.Trim().ToUpper();
			long multiplier = 1;
			int index;
    
			if((index = s.IndexOf("KB")) != -1) 
			{      
				multiplier = 1024;
				s = s.Substring(0, index);
			}
			else if((index = s.IndexOf("MB")) != -1) 
			{
				multiplier = 1024*1024;
				s = s.Substring(0, index);
			}
			else if((index = s.IndexOf("GB")) != -1) 
			{
				multiplier = 1024*1024*1024;
				s = s.Substring(0, index);
			}    
			if(s != null) 
			{
				try 
				{
					return long.Parse(s) * multiplier;
				}
				catch (Exception e) 
				{
					LogLog.Error("[" + s + "] is not in proper int form.");
					LogLog.Error("[" + argValue + "] not in expected format.", e);
				}
			}
			return defaultValue;
		}

		/// <summary>
		/// Find the value corresponding to <code>key</code> in 
		/// <code>props</code>. Then perform variable substitution 
		/// on the found value.
		/// </summary>
		/// <param name="key">the key to lookup</param>
		/// <param name="props">the association to use for lookups</param>
		/// <returns>the substituted result</returns>
		public static string FindAndSubst(string key, System.Collections.IDictionary props) 
		{
			string v = props[key] as string;
			if(v == null) 
			{
				return null;      
			}
    
			try 
			{
				return SubstVars(v, props);
			} 
			catch(Exception e) 
			{
				LogLog.Error("Bad option value ["+v+"].", e);
				return v;
			}    
		}

		/// <summary>
		/// Instantiate an object given a class name
		/// </summary>
		/// <remarks>
		/// Instantiate an object given a class name. Check that the
		/// <code>className</code> is a subclass of
		/// <code>superClass</code>. If that test fails or the object could
		/// not be instantiated, then <code>defaultValue</code> is returned.
		/// </remarks>
		/// <param name="className">The fully qualified class name of the object to instantiate</param>
		/// <param name="superClass">The class to which the new object should belong</param>
		/// <param name="defaultValue">The object to return in case of non-fulfillment</param>
		/// <returns></returns>
		public static object InstantiateByClassName(string className, Type superClass, object defaultValue) 
		{
			if(className != null) 
			{
				try 
				{
					Type classObj = Type.GetType(className);
					if(!superClass.IsAssignableFrom(classObj)) 
					{
						LogLog.Error("A \""+className+"\" object is not assignable to a \""+superClass.FullName + "\" variable.");
						return defaultValue;	  
					}
					return classObj.GetConstructor(Type.EmptyTypes).Invoke(null);
				}
				catch (Exception e) 
				{
					LogLog.Error("Could not instantiate class [" + className + "].", e);
				}
			}
			return defaultValue;    
		}

		/// <summary>
		/// Perform variable substitution in string <code>val</code> from the values of keys found in <code>props</code>.
		/// </summary>
		/// <remarks>
		/// <p>The variable substitution delimeters are <b>${</b> and <b>}</b>.</p>
		/// 
		/// <p>For example, if props contains "key=value", then the call</p>
		/// <pre>
		/// String s = OptionConverter.substituteVars("Value of key is ${key}.");
		/// </pre>
		/// 
		/// will set the variable <code>s</code> to "Value of key is value.".
		/// 
		/// <p>If no value could be found for the specified key,
		/// then substitution defaults to the empty string.</p>
		/// 
		/// <p>For example, if system propeties contains no value for the key
		/// "inexistentKey", then the call</p>
		/// 
		/// <pre>
		/// String s = OptionConverter.subsVars("Value of inexistentKey is [${inexistentKey}]");
		/// </pre>
		/// will set <code>s</code> to "Value of inexistentKey is []"     
		/// 
		/// <p>An Exception is thrown if
		/// <code>val</code> contains a start delimeter "${" which is not
		/// balanced by a stop delimeter "}". </p>
		/// </remarks>
		/// <param name="val">The string on which variable substitution is performed</param>
		/// <param name="props">the dictionary to use to lookup variables</param>
		/// <returns>the result of the substitutions</returns>
		public static string SubstVars(string val, System.Collections.IDictionary props) 
		{
			StringBuilder sbuf = new StringBuilder();

			int i = 0;
			int j, k;
    
			while(true) 
			{
				j=val.IndexOf(DELIM_START, i);
				if(j == -1) 
				{
					if(i==0)
					{
						return val;
					}
					else 
					{
						sbuf.Append(val.Substring(i, val.Length));
						return sbuf.ToString();
					}
				}
				else 
				{
					sbuf.Append(val.Substring(i, j));
					k = val.IndexOf(DELIM_STOP, j);
					if(k == -1) 
					{
						throw new Exception('"'+val+"\" has no closing brace. Opening brace at position "+j+'.');
					}
					else 
					{
						j += DELIM_START_LEN;
						string key = val.Substring(j, k);

						string replacement = props[key] as string;

						if(replacement != null) 
						{
							sbuf.Append(replacement);
						}
						i = k + DELIM_STOP_LEN;	    
					}
				}
			}
		}

	}
}

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
using log4net.spi;
using log4net.helpers;

namespace log4net.Layout
{
	/// <summary>
	/// A flexible layout configurable with pattern string.
	/// </summary>
	/// <remarks>
	/// <p>The goal of this class is to <see cref="PatternLayout.Format"/> a 
	/// <see cref="LoggingEvent"/> and return the results as a String. The results
	/// depend on the <em>conversion pattern</em>.</p>
	/// 
	/// <p>The conversion pattern is closely related to the conversion
	/// pattern of the printf function in C. A conversion pattern is
	/// composed of literal text and format control expressions called
	/// <em>conversion specifiers</em>.</p>
	/// 
	/// <p><i>You are free to insert any literal text within the conversion
	/// pattern.</i></p>
	/// 
	/// <p>Each conversion specifier starts with a percent sign (%) and is
	/// followed by optional <em>format modifiers</em> and a <em>conversion
	/// character</em>. The conversion character specifies the type of
	/// data, e.g. category, priority, date, thread name. The format
	/// modifiers control such things as field width, padding, left and
	/// right justification. The following is a simple example.</p>
	/// 
	/// <p>Let the conversion pattern be <b>"%-5p [%t]: %m%n"</b> and assume
	/// that the log4net environment was set to use a PatternLayout. Then the
	/// statements</p>
	/// <pre>
	/// Category root = Category.getRoot();
	/// root.debug("Message 1");
	/// root.warn("Message 2");   
	/// </pre>
	/// would yield the output
	/// <pre>
	/// DEBUG [main]: Message 1
	/// WARN  [main]: Message 2  
	/// </pre>
	/// 
	/// <p>Note that there is no explicit separator between text and
	/// conversion specifiers. The pattern parser knows when it has reached
	/// the end of a conversion specifier when it reads a conversion
	/// character. In the example above the conversion specifier
	/// <b>%-5p</b> means the priority of the logging event should be left
	/// justified to a width of five characters.</p>
	/// 
	/// The recognized conversion characters are
	/// 
	/// <table border="1" CELLPADDING="8">
	/// <th>Conversion Character</th>
	/// <th>Effect</th>
	/// 
	/// <tr>
	/// 	<td align="center"><b>c</b></td>
	/// 
	/// 	<td>Used to output the category of the logging event. The
	/// 	category conversion specifier can be optionally followed by
	/// 	<em>precision specifier</em>, that is a decimal constant in
	/// 	brackets.
	/// 
	/// 	<p>If a precision specifier is given, then only the corresponding
	/// 	number of right most components of the category name will be
	/// 	printed. By default the category name is printed in full.</p>
	/// 
	/// 	<p>For example, for the category name "a.b.c" the pattern
	/// 	<b>%c{2}</b> will output "b.c".</p>
	/// 
	/// 	</td>
	/// </tr>
	/// 
	/// <tr>
	/// 	<td align="center"><b>C</b></td>
	/// 
	/// 	<td>Used to output the fully qualified class name of the caller
	/// 	issuing the logging request. This conversion specifier
	/// 	can be optionally followed by <em>precision specifier</em>, that
	/// 	is a decimal constant in brackets.
	/// 
	/// 	<p>If a precision specifier is given, then only the corresponding
	/// 	number of right most components of the class name will be
	/// 	printed. By default the class name is output in fully qualified form.</p>
	/// 
	/// 	<p>For example, for the class name "org.apache.xyz.SomeClass", the
	/// 	pattern <b>%C{1}</b> will output "SomeClass".</p>
	/// 
	/// 	<p><b>WARNING</b> Generating the caller class information is
	/// 	slow. Thus, it's use should be avoided unless execution speed is
	/// 	not an issue.</p>
	/// 
	/// 	</td>
	/// 	</tr>
	/// 
	/// <tr> <td align="center"><b>d</b></td> 
	/// 
	/// 	<td>Used to output the date of
	/// 	the logging event. The date conversion specifier may be
	/// 	followed by a <em>date format specifier</em> enclosed between
	/// 	braces. For example, <b>%d{HH:mm:ss,SSS}</b> or
	/// 	<b>%d{dd&amp;nbsp;MMM&amp;nbsp;yyyy&amp;nbsp;HH:mm:ss,SSS}</b>.  If no
	/// 	date format specifier is given then ISO8601 format is
	/// 	assumed.
	/// 
	/// 	<p>The date format specifier admits the same syntax as the
	/// 	time pattern string of the java.text.SimpleDateFormat. 
	/// 	Although part of the standard JDK, the performance of 
	/// 	<code>SimpleDateFormat</code> is quite poor.</p>
	/// 
	/// 	<p>For better results it is recommended to use the log4net date
	/// 	formatters. These can be specified using one of the strings
	/// 	"ABSOLUTE", "DATE" and "ISO8601" for specifying <see cref="log4net.helpers.AbsoluteTimeDateFormatter"/>
	/// 	<see cref="log4net.helpers.DateTimeDateFormatter"/> and respectively 
	/// 	<see cref="log4net.helpers.ISO8601DateFormatter"/>. For example, 
	/// 	<b>%d{ISO8601}</b> or <b>%d{ABSOLUTE}</b>.</p>
	/// 
	/// 	<p>These dedicated date formatters perform significantly
	/// 	better than java.text.SimpleDateFormat.</p>
	/// 	</td>
	/// </tr>
	/// 
	/// <tr>
	/// <td align="center"><b>F</b></td>
	/// 
	///		<td>Used to output the file name where the logging request was
	///		issued.
	/// 
	///		<p><b>WARNING</b> Generating caller location information is
	///		extremely slow. It's use should be avoided unless execution speed
	///		is not an issue.</p>
	///		</td>
	/// </tr>
	/// 
	/// <tr>
	/// <td align="center"><b>l</b></td>
	/// 
	/// 	<td>Used to output location information of the caller which generated
	/// 	the logging event. 
	/// 
	/// 	<p>The location information depends on the JVM implementation but
	/// 	usually consists of the fully qualified name of the calling
	/// 	method followed by the callers source the file name and line
	/// 	number between parentheses.</p>
	/// 
	/// 	<p>The location information can be very useful. However, it's
	/// 	generation is <em>extremely</em> slow. It's use should be avoided
	/// 	unless execution speed is not an issue.</p>
	/// 
	/// 	</td>
	/// </tr>
	/// 
	/// <tr>
	///		<td align="center"><b>L</b></td>
	/// 
	///		<td>Used to output the line number from where the logging request
	///		was issued.
	/// 
	///		<p><b>WARNING</b> Generating caller location information is
	///		extremely slow. It's use should be avoided unless execution speed
	///		is not an issue.</p>
	///		</td>
	///		
	/// </tr>
	/// 
	/// 
	/// <tr>
	/// 	<td align="center"><b>m</b></td>
	/// 	<td>Used to output the application supplied message associated with 
	/// 	the logging event.</td>   
	/// </tr>
	/// 
	/// <tr>
	///		<td align="center"><b>M</b></td>
	/// 
	///		<td>Used to output the method name where the logging request was
	///		issued.
	/// 
	///		<p><b>WARNING</b> Generating caller location information is
	///		extremely slow. It's use should be avoided unless execution speed
	///		is not an issue.</p>
	///		</td>
	///		
	/// </tr>
	/// 
	/// <tr>
	/// 	<td align="center"><b>n</b></td>
	/// 
	/// 	<td>Outputs the platform dependent line separator character or
	/// 	characters. 
	/// 
	/// 	<p>This conversion character offers practically the same
	/// 	performance as using non-portable line separator strings such as
	/// 	"\n", or "\r\n". Thus, it is the preferred way of specifying a
	/// 	line separator.</p> 
	/// 	</td>
	/// </tr>
	/// 
	/// <tr>
	/// 	<td align="center"><b>p</b></td>
	/// 	<td>Used to output the priority of the logging event.</td>
	/// </tr>
	/// 
	/// <tr>
	/// 
	/// 	<td align="center"><b>r</b></td>
	/// 
	/// 	<td>Used to output the number of milliseconds elapsed since the start
	/// 	of the application until the creation of the logging event.</td>
	/// </tr>  
	/// 
	/// 
	/// <tr>
	/// 	<td align="center"><b>t</b></td>
	/// 
	/// 	<td>Used to output the name of the thread that generated the
	/// 	logging event.</td>
	/// 
	/// </tr>
	/// 
	/// <tr>
	/// 
	/// 	<td align="center"><b>x</b></td>
	/// 
	/// 	<td>Used to output the NDC (nested diagnostic context) associated
	/// 	with the thread that generated the logging event.
	/// 	</td>     
	/// </tr>
	/// 
	/// <tr>
	/// 
	/// 	<td align="center"><b>%</b></td>
	/// 
	/// 	<td>The sequence %% outputs a single percent sign.
	/// 	</td>     
	/// </tr>
	/// 
	/// </table>
	/// 
	/// <p>By default the relevant information is output as is. However,
	/// with the aid of format modifiers it is possible to change the
	/// minimum field width, the maximum field width and justification.</p>
	/// 
	/// <p>The optional format modifier is placed between the percent sign
	/// and the conversion character.</p>
	/// 
	/// <p>The first optional format modifier is the <em>left justification
	/// flag</em> which is just the minus (-) character. Then comes the
	/// optional <em>minimum field width</em> modifier. This is a decimal
	/// constant that represents the minimum number of characters to
	/// output. If the data item requires fewer characters, it is padded on
	/// either the left or the right until the minimum width is
	/// reached. The default is to pad on the left (right justify) but you
	/// can specify right padding with the left justification flag. The
	/// padding character is space. If the data item is larger than the
	/// minimum field width, the field is expanded to accommodate the
	/// data. The value is never truncated.</p>
	/// 
	/// <p>This behavior can be changed using the <em>maximum field
	/// width</em> modifier which is designated by a period followed by a
	/// decimal constant. If the data item is longer than the maximum
	/// field, then the extra characters are removed from the
	/// <em>beginning</em> of the data item and not from the end. For
	/// example, it the maximum field width is eight and the data item is
	/// ten characters long, then the first two characters of the data item
	/// are dropped. This behavior deviates from the printf function in C
	/// where truncation is done from the end.</p>
	/// 
	/// <p>Below are various format modifier examples for the category
	/// conversion specifier.</p>
	/// 
	/// <table border="1" cellpadding="8">
	/// <th>Format modifier</th>
	/// <th>left justify</th>
	/// <th>minimum width</th>
	/// <th>maximum width</th>
	/// <th>comment</th>
	/// 
	/// <tr>
	/// <td align="center">%20c</td>
	/// <td align="center">false</td>
	/// <td align="center">20</td>
	/// <td align="center">none</td>
	/// 
	/// <td>Left pad with spaces if the category name is less than 20
	/// characters long.</td>
	/// </tr>
	/// 
	/// <tr> <td align="center">%-20c</td> <td align="center">true</td> <td
	/// align="center">20</td> <td align="center">none</td> <td>Right pad with
	/// spaces if the category name is less than 20 characters long.</td></tr>
	/// 
	/// <tr>
	/// <td align="center">%.30c</td>
	/// <td align="center">NA</td>
	/// <td align="center">none</td>
	/// <td align="center">30</td>
	/// 
	/// <td>Truncate from the beginning if the category name is longer than 30
	/// characters.</td>
	/// </tr>
	/// 
	/// <tr>
	/// <td align="center">%20.30c</td>
	/// <td align="center">false</td>
	/// <td align="center">20</td>
	/// <td align="center">30</td>
	/// 
	/// <td>Left pad with spaces if the category name is shorter than 20
	/// characters. However, if category name is longer than 30 characters,
	/// then truncate from the beginning.</td>
	/// </tr>
	/// 
	/// <tr>
	/// <td align="center">%-20.30c</td>
	/// <td align="center">true</td>
	/// <td align="center">20</td>
	/// <td align="center">30</td>
	/// 
	/// <td>Right pad with spaces if the category name is shorter than 20
	/// characters. However, if category name is longer than 30 characters,
	/// then truncate from the beginning.</td>
	/// </tr>
	/// 
	/// </table>
	/// 
	/// <p>Below are some examples of conversion patterns.</p>
	/// 
	/// <dl>
	/// 
	/// <p><dt><b>%r [%t] %-5p %c %x - %m\n</b></dt></p>
	/// <p><dd>This is essentially the TTCC layout.</dd></p>
	/// 
	/// <p><dt><b>%-6r [%15.15t] %-5p %30.30c %x - %m\n</b></dt></p>
	/// 
	/// <p><dd>Similar to the TTCC layout except that the relative time is
	/// right padded if less than 6 digits, thread name is right padded if
	/// less than 15 characters and truncated if longer and the category
	/// name is left padded if shorter than 30 characters and truncated if
	/// longer.</dd></p>
	/// 
	/// </dl>
	/// 
	/// <p>The above text is largely inspired from Peter A. Darnell and
	/// Philip E. Margolis' highly recommended book "C -- a Software
	/// Engineering Approach", ISBN 0-387-97389-3.</p>
	/// </remarks>
	public class PatternLayout : LayoutSkeleton
	{
		/// <summary>
		/// Default pattern string for log output. 
		/// Currently set to the string <b>"%m%n"</b> 
		/// which just prints the application supplied	message. 
		/// </summary>
		public const string DEFAULT_CONVERSION_PATTERN ="%m%n";

		/// <summary>
		/// A conversion pattern equivalent to the TTCCCLayout. Current value is <b>%r [%t] %p %c %x - %m%n</b>.
		/// </summary>
		public const string TTCC_CONVERSION_PATTERN = "%r [%t] %p %c %x - %m%n";

		/// <summary>
		/// Initial buffer size
		/// </summary>
  		protected const int BUF_SIZE = 256;

		/// <summary>
		/// Maximum buffer size before it is recycled
		/// </summary>
		protected const int MAX_CAPACITY = 1024;
  
		/// <summary>
		/// output buffer appended to when Format() is invoked
		/// </summary>
		private StringBuilder m_sbuf = new StringBuilder(BUF_SIZE);
  
		/// <summary>
		/// the pattern
		/// </summary>
		private string m_pattern;
  
		/// <summary>
		/// the head of the pattern converter chain
		/// </summary>
		private PatternConverter m_head;

		/// <summary>
		/// Constructs a PatternLayout using the DEFAULT_LAYOUT_PATTERN
		/// </summary>
		/// <remarks>
		/// The default pattern just produces the application supplied message.
		/// </remarks>
		public PatternLayout() : this(DEFAULT_CONVERSION_PATTERN)
		{
		}

		/// <summary>
		/// Constructs a PatternLayout using the supplied conversion pattern
		/// </summary>
		/// <param name="pattern">the pattern to use</param>
		public PatternLayout(String pattern) 
		{
			m_pattern = pattern;
			m_head = CreatePatternParser((pattern == null) ? DEFAULT_CONVERSION_PATTERN : pattern).Parse();
		}
  
		/// <summary>
		/// The <b>ConversionPattern</b> option. This is the string which
		/// controls formatting and consists of a mix of literal content and
		/// conversion specifiers.
		/// </summary>
		public string ConversionPattern
		{
			get { return m_pattern;	}
			set
			{
				m_pattern = value;
				m_head = CreatePatternParser(m_pattern).Parse();
			}
		}
  
		/// <summary>
		/// Does not do anything as options become effective immediately.
		/// </summary>
		override public void ActivateOptions() 
		{
			// nothing to do.
		}
  
		/// <summary>
		/// The PatternLayout does not handle the exception contained within
		/// LoggingEvents. Thus, it returns <code>true</code>.
		/// </summary>
		override public bool IgnoresException
		{
			get { return true; }
		}

		/// <summary>
		/// Returns PatternParser used to parse the conversion string. Subclasses
		/// may override this to return a subclass of PatternParser which recognize
		/// custom conversion characters.
		/// </summary>
		/// <param name="pattern">the pattern to parse</param>
		/// <returns></returns>
		virtual protected PatternParser CreatePatternParser(string pattern) 
		{
			return new PatternParser(pattern);
		}

		/// <summary>
		/// Produces a formatted string as specified by the conversion pattern.
		/// </summary>
		/// <param name="loggingEvent">the event being logged</param>
		/// <returns>the formatted string</returns>
		override public string Format(LoggingEvent loggingEvent) 
		{
			// Reset working stringbuffer
			if(m_sbuf.Capacity > MAX_CAPACITY) 
			{
				m_sbuf = new StringBuilder(BUF_SIZE);
			} 
			else 
			{
				m_sbuf.Length = 0;
			}
    
			PatternConverter c = m_head;

			// loop through the chain of pattern converters
			while(c != null) 
			{
				c.Format(m_sbuf, loggingEvent);
				c = c.Next;
			}
			return m_sbuf.ToString();
		}	
	}
}

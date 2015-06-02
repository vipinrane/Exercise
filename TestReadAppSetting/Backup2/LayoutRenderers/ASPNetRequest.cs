// 
// Copyright (c) 2004-2006 Jaroslaw Kowalski <jaak@jkowalski.net>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

#if !NETCF

using System;
using System.Text;
using System.Web;

using NLog.Config;

namespace NLog.LayoutRenderers
{
    /// <summary>
    /// ASP.NET Request variable
    /// </summary>
    /// <remarks>
    /// Use this layout renderer to insert the value of the specified parameter of the
    /// ASP.NET Request object.
    /// </remarks>
    /// <example>
    /// <para>Example usage of ${aspnet-request}:</para>
    /// <code lang="NLog Layout Renderer">
    /// ${aspnet-request:item=v}
    /// ${aspnet-request:querystring=v}
    /// ${aspnet-request:form=v}
    /// ${aspnet-request:cookie=v}
    /// ${aspnet-request:serverVariable=v}
    /// </code>
    /// </example>
    [LayoutRenderer("aspnet-request")]
    [NotSupportedRuntime(Framework=RuntimeFramework.DotNetCompactFramework)]
    public class ASPNETRequestValueLayoutRenderer : LayoutRenderer
    {
        private string _queryStringKey;
        private string _formKey;
        private string _cookie;
        private string _item;
        private string _serverVariable;

        /// <summary>
        /// The item name. The QueryString, Form, Cookies, or ServerVariables collection variables having the specified name are rendered.
        /// </summary>
        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }


        /// <summary>
        /// The QueryString variable to be rendered.
        /// </summary>
        public string QueryString
        {
            get { return _queryStringKey; }
            set { _queryStringKey = value; }
        }

        /// <summary>
        /// The form variable to be rendered.
        /// </summary>
        public string Form
        {
            get { return _formKey; }
            set { _formKey = value; }
        }

        /// <summary>
        /// The cookie to be rendered.
        /// </summary>
        public string Cookie
        {
            get { return _cookie; }
            set { _cookie = value; }
        }

        /// <summary>
        /// The ServerVariables item to be rendered.
        /// </summary>
        public string ServerVariable
        {
            get { return _serverVariable; }
            set { _serverVariable = value; }
        }

        /// <summary>
        /// Returns the estimated number of characters that are needed to
        /// hold the rendered value for the specified logging event.
        /// </summary>
        /// <param name="logEvent">Logging event information.</param>
        /// <returns>The number of characters.</returns>
        /// <remarks>
        /// If the exact number is not known or
        /// expensive to calculate this function should return a rough estimate
        /// that's big enough in most cases, but not too big, in order to conserve memory.
        /// </remarks>
        protected internal override int GetEstimatedBufferSize(LogEventInfo logEvent)
        {
            return 64;
        }

        /// <summary>
        /// Renders the specified ASP.NET Request variable and appends it to the specified <see cref="StringBuilder" />.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append the rendered data to.</param>
        /// <param name="logEvent">Logging event.</param>
        protected internal override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
                return ;

			if (context.Request == null)
				return ;
			if (QueryString != null)
            {
                builder.Append(ApplyPadding(context.Request.QueryString[QueryString]));
            }
            else if (Form != null)
            {
                builder.Append(ApplyPadding(context.Request.Form[Form]));
            }
            else if (Cookie != null)
            {
                HttpCookie cookie = context.Request.Cookies[Cookie];

                if (cookie != null)
                    builder.Append(ApplyPadding(cookie.Value));
            }
            else if (ServerVariable != null)
            {
                builder.Append(ApplyPadding(context.Request.ServerVariables[ServerVariable]));
            }
            else if (Item != null)
            {
                builder.Append(ApplyPadding(context.Request[Item]));
            }
        }
    }
}

#endif

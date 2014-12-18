﻿// LICENSE
// -------
// This software was originally authored by Christopher Tacke of OpenNETCF Consulting, LLC
// On March 10, 2009 is was placed in the public domain, meaning that all copyright has been disclaimed.
//
// You may use this code for any purpose, commercial or non-commercial, free or proprietary with no legal 
// obligation to acknowledge the use, copying or modification of the source.
//
// OpenNETCF will maintain an "official" version of this software at www.opennetcf.com and public 
// submissions of changes, fixes or updates are welcomed but not required
//
using System;
using System.Reflection;

#if WINDOWS_PHONE
using TheInvoker = System.Windows.Threading.Dispatcher;
using System.Diagnostics;
#elif IPHONE
using TheInvoker = System.Object;
#elif ANDROID
using TheInvoker = OpenNETCF.UIInvoker;
#else
using TheInvoker = System.Windows.Forms.Control;
#endif

namespace OpenNETCF.IoC
{
    internal class BasicInvoker
    {
        private TheInvoker m_invoker;
        private Delegate m_targetDelegate;
        private MethodInfo m_methodInfo = null;

        public BasicInvoker(TheInvoker invoker)
        {
            m_invoker = invoker;
        }

        public BasicInvoker(TheInvoker invoker, Delegate targetDelegate)
            : this(invoker)
        {
            m_targetDelegate = targetDelegate;
        }

#if IPHONE
        public void Handler(object source, EventArgs args)
        {
        }
#elif WINDOWS_PHONE
        public void Handler(object source, EventArgs args)
        {
            m_targetDelegate.DynamicInvoke(args);
            m_invoker.BeginInvoke(m_targetDelegate, new object[] { source, args });
        }
#else
        public void Handler(object source, EventArgs args)
        {
#if !CF_20
            if (!m_invoker.IsDisposed)
#endif
            {
                try
                {
                    m_invoker.Invoke(m_targetDelegate, new object[] { source, args });
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }
#endif

        public MethodInfo HandlerMethod
        {
            get
            {
                if (m_methodInfo == null)
                {
                    m_methodInfo = this.GetType().GetMethod("Handler", BindingFlags.Public | BindingFlags.Instance);
                }

                return m_methodInfo;
            }
        }
    }
}

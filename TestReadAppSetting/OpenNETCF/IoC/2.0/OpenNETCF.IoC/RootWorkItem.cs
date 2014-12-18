// LICENSE
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
using OpenNETCF.IoC.UI;

#if WINDOWS_PHONE
using TheInvoker = System.Windows.Threading.Dispatcher;
#elif IPHONE
using TheInvoker = System.Object;
#elif ANDROID
using TheInvoker = OpenNETCF.UIInvoker;
#else
using TheInvoker = System.Windows.Forms.Control;
#endif

namespace OpenNETCF.IoC
{
    public static class RootWorkItem
    {
        internal static WorkItem m_workItem;

        static RootWorkItem()
        {
            m_workItem = new WorkItem();
        }

        public static ManagedObjectCollection<WorkItem> WorkItems
        {
            get { return m_workItem.WorkItems; }
        }

        public static ManagedObjectCollection<object> Items 
        {
            get { return m_workItem.Items; }
        }
        
        public static ManagedObjectCollection<ISmartPart> SmartParts
        {
            get { return m_workItem.SmartParts; }
        }

        public static ManagedObjectCollection<IWorkspace> Workspaces
        {
            get { return m_workItem.Workspaces; }
        }

        public static ModuleCollection Modules
        {
            get { return m_workItem.Modules; }
        }

        public static void SetModuleInfoStore(IModuleInfoStore store)
        {
            m_workItem.Services.Add<IModuleInfoStore>(store);
        }

        public static ServiceCollection Services
        {
            get { return m_workItem.Services; }
        }

        public static WorkItem Instance
        {
            get { return m_workItem; }
        }

        public static void RegisterType(Type concreteType, Type registerAs)
        {
            Instance.RegisterType(concreteType, registerAs);
        }

        public static void BeginInvoke(Delegate method)
        {
            var invoker = Items.Get<TheInvoker>(Constants.EventInvokerName);
            invoker.BeginInvoke(method);
        }

        public static void BeginInvoke(Delegate method, params object[] args)
        {
            var invoker = Items.Get<TheInvoker>(Constants.EventInvokerName);
            invoker.BeginInvoke(method, args);
        }
#if !WINDOWS_PHONE
        public static void Invoke(Delegate method)
        {
            var invoker = Items.Get<TheInvoker>(Constants.EventInvokerName);
            invoker.Invoke(method);
        }

        public static void Invoke(Delegate method, params object[] args)
        {
            var invoker = Items.Get<TheInvoker>(Constants.EventInvokerName);
            invoker.Invoke(method, args);
        }
#endif
    }
}

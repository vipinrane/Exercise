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
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC.UI;

using TheInvoker=System.Windows.Forms.Control;

namespace OpenNETCF.IoC
{
    public class RootWorkItem<TBaseSmartPart>
        where TBaseSmartPart : class, ISmartPart
    {
        protected static readonly RootWorkItem<TBaseSmartPart> instance;

        protected WorkItem<TBaseSmartPart> m_workItem;

        static RootWorkItem ( )
        {
            instance = new RootWorkItem<TBaseSmartPart> ( );
        }

        protected RootWorkItem ( )
        {
            m_workItem = new WorkItem<TBaseSmartPart> ( );
        }

        public static RootWorkItem<TBaseSmartPart> Instance { get { return instance; } }

        public ManagedObjectCollection<WorkItem<TBaseSmartPart>, TBaseSmartPart> WorkItems
        {
            get { return m_workItem.WorkItems; }
        }

        public ManagedObjectCollection<object, TBaseSmartPart> Items
        {
            get { return m_workItem.Items; }
        }

        public ManagedObjectCollection<ISmartPart, TBaseSmartPart> SmartParts
        {
            get { return m_workItem.SmartParts; }
        }

        public ManagedObjectCollection<IWorkspace<TBaseSmartPart>, TBaseSmartPart> Workspaces
        {
            get { return m_workItem.Workspaces; }
        }

        public ModuleCollection<TBaseSmartPart> Modules
        {
            get { return m_workItem.Modules; }
        }

        public void SetModuleInfoStore ( IModuleInfoStore store )
        {
            m_workItem.Services.Add<IModuleInfoStore> ( store );
        }

        public ServiceCollection<TBaseSmartPart> Services
        {
            get { return m_workItem.Services; }
        }

        public WorkItem<TBaseSmartPart> RootWorkItemInstance
        {
            get { return m_workItem; }
        }

        public void RegisterType ( Type concreteType, Type registerAs )
        {
            Instance.RegisterType ( concreteType, registerAs );
        }

        public void BeginInvoke ( Delegate method )
        {
            var invoker = Items.Get<TheInvoker> ( IOCConstants.IOCEventInvokerName );
            invoker.BeginInvoke ( method );
        }

        public void BeginInvoke ( Delegate method, params object[ ] args )
        {
            var invoker = Items.Get<TheInvoker> ( IOCConstants.IOCEventInvokerName );
            invoker.BeginInvoke ( method, args );
        }

        public void Invoke ( Delegate method )
        {
            var invoker = Items.Get<TheInvoker> ( IOCConstants.IOCEventInvokerName );
            invoker.Invoke ( method );
        }

        public void Invoke ( Delegate method, params object[ ] args )
        {
            var invoker = Items.Get<TheInvoker> ( IOCConstants.IOCEventInvokerName );
            invoker.Invoke ( method, args );
        }
    }
}

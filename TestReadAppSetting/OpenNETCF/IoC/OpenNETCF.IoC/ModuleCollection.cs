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
using System.Collections;
using System.Diagnostics;
using OpenNETCF.IoC.UI;

namespace OpenNETCF.IoC
{
    public class ModuleCollection<TBaseSmartPart> : IEnumerable<IModuleInfo>
        where TBaseSmartPart : class, ISmartPart
    {
        private WorkItem<TBaseSmartPart> m_root;
        private bool m_storeLoaded = false;

        public event EventHandler<GenericEventArgs<IModuleInfo>> ModuleLoaded;

        internal ModuleCollection ( WorkItem<TBaseSmartPart> root )
        {
            m_root = root;
        }

        private ModuleInfoStoreService<TBaseSmartPart> StoreService
        {
            get { return RootWorkItem<TBaseSmartPart>.Instance.Services.Get<ModuleInfoStoreService<TBaseSmartPart>> ( ); }
        }

        public void LoadModules ( )
        {
            lock ( this )
            {
                if ( m_storeLoaded ) return;

                // see if the service already exists (i.e. a SmartClientApplication already created it)               
                if ( StoreService == null )
                {
                    var infoStore = m_root.Services.Get<IModuleInfoStore> ( );

                    if ( infoStore == null )
                    {
                        infoStore = CreateDefaultInfoStore ( );
                        m_root.Services.Add<IModuleInfoStore> ( infoStore );
                    }

                    RootWorkItem<TBaseSmartPart>.Instance.Services.AddNew<ModuleInfoStoreService<TBaseSmartPart>> ( );
                    StoreService.ModuleLoaded += new EventHandler<GenericEventArgs<IModuleInfo>> ( svc_ModuleLoaded );
                    StoreService.LoadModulesFromStore ( infoStore );

                }

                m_storeLoaded = true;
            }
        }

        void svc_ModuleLoaded ( object sender, GenericEventArgs<IModuleInfo> e )
        {
            ModuleLoaded.Fire ( this, e );
        }

        internal IModuleInfoStore CreateDefaultInfoStore ( )
        {
            return new DefaultModuleInfoStore ( );
        }

        public IEnumerator<IModuleInfo> GetEnumerator ( )
        {
            return StoreService.m_loadedModules.GetEnumerator ( );
        }

        IEnumerator IEnumerable.GetEnumerator ( )
        {
            return GetEnumerator ( );
        }

        public int Count
        {
            get { return StoreService.m_loadedModules.Count; }
        }
    }
}

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
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using OpenNETCF.IoC.UI;

namespace OpenNETCF.IoC
{
    public sealed class ModuleInfoStoreService<TBaseSmartPart>
        where TBaseSmartPart : class, ISmartPart
    {
        public event EventHandler<GenericEventArgs<IModuleInfo>> ModuleLoaded;

        public event EventHandler<EventArgs> ModuleLoadingCompleted;

        internal List<IModuleInfo> m_loadedModules = new List<IModuleInfo> ( );
        private WorkItem<TBaseSmartPart> m_root;

        internal ModuleInfoStoreService ( )
            : this ( RootWorkItem<TBaseSmartPart>.Instance.RootWorkItemInstance )
        {
        }

        internal ModuleInfoStoreService ( WorkItem<TBaseSmartPart> root )
        {
            m_root = root;
        }

        internal void LoadModulesFromStore ( IModuleInfoStore store )
        {
            Validate
                .Begin ( )
                .IsNotNull ( store, "store" )
                .Check ( );

            string xml = store.GetModuleListXml ( );
            if ( xml == null ) return;

            XmlReaderSettings settings = new XmlReaderSettings ( );
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            XElement root = XElement.Parse ( xml );

            string s = "Modules";
            var modules = from n in root.Descendants ( s )
                          select n;

            var assemblies = from n in modules.Descendants ( )
                             where n.Name == "ModuleInfo"
                             select n.Attribute ( "AssemblyFile" ).Value;

            // load each assembly
            LoadAssemblies ( assemblies );

            // now notify all assemblies that all other assemblies are loaded (this is useful when there are module interdependencies
            NotifyAssembliesOfContainerCompletion ( );

            ModuleLoadingCompleted.Fire ( this, EventArgs.Empty );
        }

        private void NotifyAssembliesOfContainerCompletion ( )
        {
            foreach ( var m in m_loadedModules )
            {
                var loadComplete = ( ( ModuleInfo ) m ).Instance.GetType ( ).GetMethod ( "OnContainerLoadComplete", BindingFlags.Public | BindingFlags.Instance );
                if ( loadComplete != null )
                {
                    try
                    {
                        loadComplete.Invoke ( ( ( ModuleInfo ) m ).Instance, null );
                    }
                    catch ( Exception ex )
                    {
                        throw ex.InnerException;
                    }
                }
            }
        }

        private Type FindIModuleType ( Assembly assembly )
        {
            Type imodule;

            // see if we have an explicitly defined entry
            var attrib = ( from a in assembly.GetCustomAttributes ( true )
                           where a is IoCModuleEntryAttribute
                           select a as IoCModuleEntryAttribute ).FirstOrDefault ( );

            if ( attrib != null )
            {
                if ( !attrib.EntryType.Implements<IModule> ( ) )
                {
                    throw new Exception (
                        string.Format ( "IoCModuleEntry.EntryType in assembly '{0}' doesn't derive from IModule",
                        assembly.FullName ) );
                }

                imodule = attrib.EntryType;
            }
            else
            {
                // default to old behavior - loading will be *much* slower under Mono as we have to call GetTypes()
                // under CF and FFX this appears negligible
                try
                {
                    imodule = ( from t in assembly.GetTypes ( )
                                where t.GetInterfaces ( ).Count ( i => i.Equals ( typeof ( IModule ) ) ) > 0
                                select t ).FirstOrDefault ( m => !m.IsAbstract );
                }
                catch ( Exception ex )
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }

            return imodule;
        }

        internal ModuleInfo LoadAssembly ( Assembly assembly )
        {
            var assemblyName = assembly.GetName ( );
            Debug.WriteLine(assemblyName);

            Type imodule = FindIModuleType ( assembly );
            if ( imodule == null ) return null;

            object instance = ObjectFactory<TBaseSmartPart>.CreateObject ( imodule, RootWorkItem<TBaseSmartPart>.Instance.RootWorkItemInstance );

            var info = new ModuleInfo
                {
                    Assembly = assembly,
                    AssemblyFile = assemblyName.CodeBase,
                    Instance = instance
                };

            m_loadedModules.Add ( info );

            var loadMethod = imodule.GetMethod ( "Load", BindingFlags.Public | BindingFlags.Instance );
            if ( loadMethod != null )
            {
                try
                {
                    loadMethod.Invoke ( instance, null );
                }
                catch ( Exception ex )
                {
                    throw ex.InnerException;
                }
            }

            var addServices = imodule.GetMethod ( "AddServices", BindingFlags.Public | BindingFlags.Instance );
            if ( addServices != null )
            {
                try
                {
                    addServices.Invoke ( instance, null );
                }
                catch ( Exception ex )
                {
                    throw ex.InnerException;
                }
            }

            ModuleLoaded.Fire ( this, new GenericEventArgs<IModuleInfo> ( info ) );

            return info;
        }

        private void LoadAssemblies ( IEnumerable<string> assemblyNames )
        {
            Validate
                .Begin ( )
                .IsNotNull ( assemblyNames, "assemblyNames" )
                .Check ( );

            Assembly asm = null;

            foreach ( var s in assemblyNames )
            {
                var tryByPath = true;

                // avoid excepting by default under the Compact Framework
                if ( Environment.OSVersion.Platform != PlatformID.WinCE )
                {
                    try
                    {
                        asm = Assembly.Load ( s );
                        tryByPath = false;
                    }
                    catch ( FileNotFoundException )
                    {
                        // this will try by path below
                    }
                }

                if ( tryByPath )
                {
                    var rootFolder = string.Empty;

                    if ( Environment.OSVersion.Platform == PlatformID.Unix )
                    {
                        rootFolder = Path.Combine ( "/", s );
                    }
                    else
                    {
                        rootFolder = Path.GetDirectoryName ( Assembly.GetExecutingAssembly ( ).GetName ( ).CodeBase );
                        var pathasURI = new Uri ( rootFolder );
                        rootFolder = Path.Combine ( pathasURI.LocalPath, s );
                    }


                    asm = null;

                    var fi = new FileInfo ( rootFolder );

                    if ( fi.Exists )
                    {
                        // local?
                        asm = Assembly.LoadFrom ( fi.FullName );
                    }
                    else if ( File.Exists ( s ) )
                    {
                        // fully qualified path?
                        asm = Assembly.LoadFrom ( s );
                    }
                    else if ( File.Exists ( Path.Combine ( "\\Windows", s ) ) )
                    {
                        // Windows?
                        asm = Assembly.LoadFrom ( Path.Combine ( "\\Windows", s ) );
                    }
                    else
                    {
                        throw new IOException ( string.Format ( "Unable to locate assembly '{0}'", s ) );
                    }
                }

                if ( asm == null ) continue;

                try
                {
                    var info = LoadAssembly ( asm );
                }
                catch ( Exception ex )
                {
                    Trace.WriteLine ( string.Format ( "Exception loading assembly '{0}': {1}", asm.FullName, ex.Message ) );
                }
            }
        }

        public IModuleInfo[ ] LoadedModules
        {
            get { return m_loadedModules.ToArray ( ); }
        }
    }
}

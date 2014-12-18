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

#if WINDOWS_PHONE || ANDROID || CF_20
using Trace = System.Diagnostics.Debug;
#endif

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace OpenNETCF.IoC
{
    public class DefaultModuleInfoStore : IModuleInfoStore
    {
        private string m_catalogFilePath;

        public DefaultModuleInfoStore()
        {
            // use a Uri because the desktop will add in the "file//:" prefix
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = Path.Combine(path, Constants.DefaultProfileCatalogName);
            Uri uri = new Uri(path);
            CatalogFilePath = uri.LocalPath;
        }

        public DefaultModuleInfoStore(string profilePath)
        {
            // use a Uri because the desktop will add in the "file//:" prefix
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = Path.Combine(path, profilePath);
            Uri uri = new Uri(path);
            CatalogFilePath = uri.LocalPath;
        }

        public Assembly[] GetModuleAssemblies()
        {
            return null;
        }

        public string GetModuleListXml()
        {
            try
            {
                if (!File.Exists(m_catalogFilePath))
                {
                    Trace.WriteLine(string.Format("IoC: Catalog file '{0}' not found", m_catalogFilePath), Constants.TraceCategoryName);
                    return null;

                }
                using (TextReader reader = File.OpenText(m_catalogFilePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(string.Format("IoC.DefaultModuleInfoStore: GetModuleListXml exception: '{0}'", ex.Message), Constants.TraceCategoryName);
                return null;
            }
        }

        public string CatalogFilePath
        {
            get { return m_catalogFilePath; }
            set
            {
                Validate
                    .Begin()
                    .IsNotNullOrEmpty(value)
                    .Check();

                m_catalogFilePath = value;
            }
        }
    }
}

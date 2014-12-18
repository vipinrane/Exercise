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
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace OpenNETCF.IoC
{
    public class ResourceModuleInfoStore : IModuleInfoStore
    {
        public string CatalogFilePath { get; set; }
        private Assembly m_assembly;

        public ResourceModuleInfoStore ( )
            : this ( Assembly.GetCallingAssembly ( ), null )
        {
        }

        public ResourceModuleInfoStore ( Assembly resourceAssembly )
            : this ( resourceAssembly, null )
        {
        }

        public ResourceModuleInfoStore ( Assembly resourceAssembly, string resourcePath )
        {
            m_assembly = resourceAssembly ?? Assembly.GetCallingAssembly ( );
            CatalogFilePath = resourcePath ?? ( m_assembly.GetName ( ).Name + "." + IOCConstants.DefaultProfileCatalogName );
        }

        public string GetModuleListXml ( )
        {
            var resourceName = ParseResourceName ( );

            var names = m_assembly.GetManifestResourceNames ( );
            try
            {
                using ( var stream = m_assembly.GetManifestResourceStream ( CatalogFilePath ) )
                {
                    if ( stream == null ) return null;

                    using ( var reader = new StreamReader ( stream ) )
                    {
                        return reader.ReadToEnd ( );
                    }
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine("Cannot find resource catalog\r\n" + ex.Message);
                return null;
            }
        }

        private string ParseResourceName ( )
        {
            return CatalogFilePath.Replace ( "[EmbeddedResource]", string.Empty );
        }
    }
}

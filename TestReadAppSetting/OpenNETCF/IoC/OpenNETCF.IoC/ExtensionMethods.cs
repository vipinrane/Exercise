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

namespace OpenNETCF.IoC
{
    public static class ExtensionMethods
    {
        public static TWorkItem GetOrAdd<TWorkItem, TBaseSmartPart> ( this ManagedObjectCollection<WorkItem<TBaseSmartPart>, TBaseSmartPart> collection, string id )
            where TWorkItem : class
            where TBaseSmartPart : class, ISmartPart
        {
            if ( collection.Contains ( id ) )
            {
                return collection.Get<TWorkItem> ( id );
            }

            return collection.AddNew<TWorkItem> ( id );
        }

        /// <summary>
        /// Gets an existing service of a given type if it already exists in the collection.  If it does not exist, it creates and adds a new instance.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetOrAdd<TService, TBaseSmartPart> ( this ServiceCollection<TBaseSmartPart> collection )
            where TService : class
            where TBaseSmartPart : class, ISmartPart
        {
            var service = collection.Get<TService> ( );
            if ( service != null ) return service;
            return collection.AddNew<TService> ( );
        }
    }
}

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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenNETCF.IoC.UI;

namespace OpenNETCF.IoC
{
    public abstract class WorkItemController<TBaseSmartPart> : IWorkItemController
        where TBaseSmartPart : class, ISmartPart
    {
        /// <summary>
        /// Gets or sets the work item.
        /// </summary>
        /// <value>The work item.</value>
        [ServiceDependency]
        public WorkItem<TBaseSmartPart> WorkItem { get; set; }

        [EventPublication ( "topic://ICM/ICMErrorOrWarning", PublicationScope.Global )]
        public event EventHandler<GenericEventArgs<object>> ICMErrorOrWarning;

        public virtual void Run ( )
        {
        }

        protected void RaiseICMErrorOrWarning ( object state )
        {
            if ( ICMErrorOrWarning != null ) ICMErrorOrWarning ( this, new GenericEventArgs<object> ( state ) );
        }
    }
}

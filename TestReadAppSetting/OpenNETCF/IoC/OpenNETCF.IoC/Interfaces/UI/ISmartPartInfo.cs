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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.IoC.UI
{
    public interface ISmartPartInfo
    {
        /// <summary>
        /// Description of this SmartPart.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Title of this SmartPart.
        /// </summary>
        string Title { get; set; }
    }
}
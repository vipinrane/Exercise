using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.IoC.UI
{
    public interface IService<TBaseSmartPart>
        where TBaseSmartPart : ISmartPart
    {
        IWorkspace<TBaseSmartPart> Workspace { get; }

        void Init ( );
        void Start ( );
    }
}

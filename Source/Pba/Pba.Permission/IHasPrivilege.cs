using System;
using System.Collections.Generic;
using System.Text;

namespace Pba.Permission {
    public interface IHasPrivileges {

        bool HasPrivilege( IPrivilege privilege );

        bool HasPrivilege( IPrivilege[] privileges );

        bool HasPrivilege( string privilege );

        IPrivilege[] GetPrivileges();

    }
}

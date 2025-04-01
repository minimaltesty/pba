//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace SIT.Components.Security {
    public interface IHasPrivileges {

        bool HasPrivilege( IPrivilege privilege );

        bool HasPrivilege( IPrivilege[] privileges );

        bool HasPrivilege( string privilege );

        IPrivilege[] GetPrivileges();

    }
}

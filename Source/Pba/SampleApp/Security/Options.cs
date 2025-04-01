//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using SIT.Components.Security.Permission.CAS;
using System.Security.Permissions;
namespace SIT.Components.Security {
    public class Options {

        private int m_MaxLoginAttempts;

        public int MaxLoginAttempts {
            get { return m_MaxLoginAttempts; }

            [PrivilegePermission( SecurityAction.Demand, Privilege="ChangeSecurityOptions")]
            set { m_MaxLoginAttempts = value; }
        }

    }
}

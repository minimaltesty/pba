//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Security;
using System.Security.Permissions;
using System.Reflection;

namespace SIT.Components.Security.Permission.CAS {

    [AttributeUsageAttribute( AttributeTargets.All, AllowMultiple = true )]
    public class PrivilegePermissionAttribute : CodeAccessSecurityAttribute {
        
        private bool unrestricted = false;
        private string m_Privilege;

        public new bool Unrestricted {
            get { return unrestricted; }
            set { unrestricted = value; }
        }

        public string Privilege {
            get { return m_Privilege; }
            set { m_Privilege = value; }
        }

        public PrivilegePermissionAttribute( SecurityAction action )
            : base( action ) {
            
            
        }
        public override IPermission CreatePermission() {

            if( Unrestricted ) {
                throw new ArgumentException( "Unrestricted permissions not allowed in identity permissions." );

            } else {
                PrivilegePermission pp = new PrivilegePermission( m_Privilege );
                pp.Demand();
                return new PrivilegePermission( m_Privilege );
            }
        }

    }
}
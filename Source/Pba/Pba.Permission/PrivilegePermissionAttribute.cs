using System;
using System.Security;
using System.Security.Permissions;
using System.Reflection;

namespace Pba.Permission {

    [AttributeUsageAttribute( AttributeTargets.All, AllowMultiple = true )]
    public class PrivilegePermissionAttribute : CodeAccessSecurityAttribute {

        private readonly string _privilegeId;
        public string PrivilegeId { get { return _privilegeId; } }

        public PrivilegePermissionAttribute( SecurityAction action, string privilegeId )
            : base( action ) {
                _privilegeId = privilegeId;

        }
        public override IPermission CreatePermission() {
            if ( base.Unrestricted ) {
                throw new ArgumentException( "Unrestricted permissions not allowed in identity permissions." );

            } else {
                PrivilegePermission pp = new PrivilegePermission( _privilegeId );
                pp.Demand();
                return new PrivilegePermission( _privilegeId );

            }
        }

    }
}

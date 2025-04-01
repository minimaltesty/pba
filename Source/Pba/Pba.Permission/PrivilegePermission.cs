//
// Implementation based on the example of MSDN
// Link: http://msdn.microsoft.com/de-de/library/vstudio/system.security.permissions.iunrestrictedpermission(v=vs.110).aspx
// >>> Update it to implementation: http://msdn.microsoft.com/de-de/library/vstudio/system.security.codeaccesspermission(v=vs.110).aspx
//
//
using System;
using System.Security;
using System.Security.Permissions;
using System.Reflection;

namespace Pba.Permission {

    // Derive from CodeAccessPermission to gain implementations of the following
    // sealed IStackWalk methods: Assert, Demand, Deny, and PermitOnly.
    // Implement the following abstract IPermission methods: Copy, Intersect, and IsSubSetOf.
    // Implementing the Union method of the IPermission class is optional.
    // Implement the following abstract ISecurityEncodable methods: FromXml and ToXml.
    // Making the class 'sealed' is optional.
    public sealed class PrivilegePermission : 
        CodeAccessPermission, 
        IPermission, 
        IUnrestrictedPermission, 
        ISecurityEncodable, 
        ICloneable 
    {

        #region Members

        private bool _specifiedAsUnrestricted = false;
        private string _privilegeId = string.Empty;

        #endregion

        #region Constructor, Destructor

        // This constructor creates and initializes a permission with generic access.
        public PrivilegePermission( PermissionState state ) {
            _specifiedAsUnrestricted = (state == PermissionState.Unrestricted);

        }

        // This constructor creates and initializes a permission with specific access.        
        public PrivilegePermission( string privilege ) : this( PermissionState.None )  {
            _privilegeId = privilege;

        }

        #endregion

        // For debugging, return the state of this object as XML.
        public override String ToString() { return ToXml().ToString(); }



        // Private method to cast (if possible) an IPermission to the type.
        private PrivilegePermission VerifyTypeMatch( IPermission target ) {

            if ( target == null )
                return null;

            if ( GetType() != target.GetType() )
                throw new ArgumentException(
                    String.Format( "target must be of the {0} type",
                    GetType().FullName )
                    );
            return (PrivilegePermission)target;
        }

        // This is the Private Clone helper method. 
        private PrivilegePermission Clone( Boolean specifiedAsUnrestricted, string privilegeId ) {
            PrivilegePermission privperm = (PrivilegePermission)Clone();
            privperm._specifiedAsUnrestricted = specifiedAsUnrestricted;
            privperm._privilegeId = _privilegeId;
            return privperm;
        }

        #region IPermission Members
        // Return a new object that contains the intersection of 'this' and 'target'.
        public override IPermission Intersect( IPermission target ) {
            return null;
        }

        // Called by the Demand method: returns true if 'this' is a subset of 'target'.
        public override Boolean IsSubsetOf( IPermission target ) {

            IHasPrivileges privilegesObject;

            if ( target == null ) {
                if ( System.Threading.Thread.CurrentPrincipal is IHasPrivileges ) {
                    privilegesObject = System.Threading.Thread.CurrentPrincipal as IHasPrivileges;
                    if ( !privilegesObject.HasPrivilege( _privilegeId ) )
                        throw new SecurityException( "No rights!" );
                    else
                        return true;
                } else
                    throw new SecurityException( "No rights!" );
            }
            PrivilegePermission privPerm = VerifyTypeMatch( target );

            if ( privPerm == null )
                throw new SecurityException( "No rights!" );

            return privPerm._privilegeId == _privilegeId;
        }



        // Return a new object that matches 'this' object's permissions.
        public sealed override IPermission Copy() {
            return (IPermission)Clone();
        }

        // Return a new object that contains the union of 'this' and 'target'.
        // Note: You do not have to implement this method. If you do not, the version
        // in CodeAccessPermission does this:
        //    1. If target is not null, a NotSupportedException is thrown.
        //    2. If target is null, then Copy is called and the new object is returned.
        public override IPermission Union( IPermission target ) {
            return Copy();
        }
        #endregion

        #region ISecurityEncodable Members
        // Populate the permission's fields from XML.
        public override void FromXml( SecurityElement e ) {
            _specifiedAsUnrestricted = false;
            _privilegeId = string.Empty;

            // If XML indicates an unrestricted permission, make this permission unrestricted.
            string s = e.Attributes[ "Unrestricted" ].ToString();
            if ( s != null ) {
                _specifiedAsUnrestricted = Convert.ToBoolean( s );
                if ( _specifiedAsUnrestricted )
                    _privilegeId = string.Empty;
            }

            // If XML indicates a restricted permission, parse the flags.
            if ( !_specifiedAsUnrestricted ) {
                s = e.Attributes[ "PrivilegeId" ].ToString();
                if ( s != null ) {
                    _privilegeId = s;
                }
            }
        }

        // Produce XML from the permission's fields.
        public override SecurityElement ToXml() {
            // These first three lines create an element with the required format.
            SecurityElement e = new SecurityElement( "IPermission" );
            // Replace the double quotation marks () with single quotation marks ()
            // to remain XML compliant when the culture is not neutral.
            e.AddAttribute( "class", GetType().AssemblyQualifiedName.Replace( '\"', '\'' ) );
            e.AddAttribute( "version", "1" );

            if ( !_specifiedAsUnrestricted )
                e.AddAttribute( "PrivilegeId", _privilegeId );
            else
                e.AddAttribute( "Unrestricted", "true" );
            return e;
        }
        #endregion

        #region IUnrestrictedPermission Members

        // Returns true if permission is effectively unrestricted.
        /// <summary>
        /// Returns a value that indicates if the 
        /// </summary>
        /// <returns></returns>
        public bool IsUnrestricted() {
            // This means that the object is unrestricted at runtime.
            //TODO: consider returning false, because unrestrictd makes no sense? >> consider Admin-Accounts?
            return _specifiedAsUnrestricted;
        }
        #endregion

        #region ICloneable Members

        // Return a copy of the permission.
        public Object Clone() { return MemberwiseClone(); }

        #endregion
    }
}


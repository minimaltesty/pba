//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Security;
using System.Security.Permissions;
using System.Reflection;


namespace SIT.Components.Security.Permission.CAS {


    // Derive from CodeAccessPermission to gain implementations of the following
    // sealed IStackWalk methods: Assert, Demand, Deny, and PermitOnly.
    // Implement the following abstract IPermission methods: Copy, Intersect, and IsSubSetOf.
    // Implementing the Union method of the IPermission class is optional.
    // Implement the following abstract ISecurityEncodable methods: FromXml and ToXml.
    // Making the class 'sealed' is optional.

    public sealed class PrivilegePermission : CodeAccessPermission, IPermission, IUnrestrictedPermission, ISecurityEncodable, ICloneable {

        #region Members

        private Boolean m_specifiedAsUnrestricted = false;
        private string m_Privilege = string.Empty;

        #endregion

        #region Constructor, Destructor

        // This constructor creates and initializes a permission with generic access.
        public PrivilegePermission( PermissionState state ) {
            m_specifiedAsUnrestricted = ( state == PermissionState.Unrestricted );
        }

        // This constructor creates and initializes a permission with specific access.        
        public PrivilegePermission( string privilege ) {
            m_specifiedAsUnrestricted = false;
            m_Privilege = privilege;
        }

        #endregion

        // For debugging, return the state of this object as XML.
        public override String ToString() { return ToXml().ToString(); }

        

        // Private method to cast (if possible) an IPermission to the type.
        private PrivilegePermission VerifyTypeMatch( IPermission target ) {

            if( target == null )
                return null;

            if( GetType() != target.GetType() )
                throw new ArgumentException( 
                    String.Format( "target must be of the {0} type",
                    GetType().FullName ) 
                    );
            return (PrivilegePermission)target;
        }

        // This is the Private Clone helper method. 
        private PrivilegePermission Clone( Boolean specifiedAsUnrestricted, string privilege ) {
            PrivilegePermission privperm = (PrivilegePermission)Clone();
            privperm.m_specifiedAsUnrestricted = specifiedAsUnrestricted;
            privperm.m_Privilege = m_Privilege;
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

            if( target == null ) {
                if( System.Threading.Thread.CurrentPrincipal is IHasPrivileges ) {
                    privilegesObject = System.Threading.Thread.CurrentPrincipal as IHasPrivileges;
                    if( !privilegesObject.HasPrivilege( m_Privilege ) )
                        throw new SecurityException( "No rights!" );
                    else
                        return true;
                } else
                    throw new SecurityException( "No rights!" );
            }
            PrivilegePermission privPerm = VerifyTypeMatch( target );

            if( privPerm == null )
                throw new SecurityException( "No rights!" );

            return privPerm.m_Privilege == m_Privilege;
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
            m_specifiedAsUnrestricted = false;
            m_Privilege = string.Empty;

            // If XML indicates an unrestricted permission, make this permission unrestricted.
            string s = e.Attributes[ "Unrestricted" ].ToString();
            if( s != null ) {
                m_specifiedAsUnrestricted = Convert.ToBoolean( s );
                if( m_specifiedAsUnrestricted )
                    m_Privilege = string.Empty;
            }

            // If XML indicates a restricted permission, parse the flags.
            if( !m_specifiedAsUnrestricted ) {
                s = e.Attributes[ "Privilege" ].ToString();
                if( s != null ) {
                    m_Privilege = s;
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

            if( !m_specifiedAsUnrestricted )
                e.AddAttribute( "Privilege", m_Privilege );
            else
                e.AddAttribute( "Unrestricted", "true" );
            return e;
        }
        #endregion

        #region IUnrestrictedPermission Members
        // Returns true if permission is effectively unrestricted.
        public Boolean IsUnrestricted() {
            // This means that the object is unrestricted at runtime.
            return m_specifiedAsUnrestricted;
        }
        #endregion

        #region ICloneable Members

        // Return a copy of the permission.
        public Object Clone() { return MemberwiseClone(); }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Pba.Permission {
    public interface IPrivilegedPrincipal : IPrincipal, IHasPrivileges {
    }


    //////////////////////////////////////////////////////////
    //                                                      //
    // The following classes are example implementations    //
    //                                                      //
    //////////////////////////////////////////////////////////

    // .Net 2.0 > Base: IPrincipal
    // .Net 4.5 > Base: ClaimsPrincipal
    public class PrivilegedGenericPrincipal : GenericPrincipal, IPrivilegedPrincipal {


        public PrivilegedGenericPrincipal( IIdentity identity, string[] roles )
            : base( identity, roles ) {

        }

        #region IHasPrivileges Member

        public bool HasPrivilege( IPrivilege privilege ) {
            throw new NotImplementedException();
        }

        public bool HasPrivilege( IPrivilege[] privileges ) {
            throw new NotImplementedException();
        }

        public bool HasPrivilege( string privilege ) {
            throw new NotImplementedException();
        }

        public IPrivilege[] GetPrivileges() {
            throw new NotImplementedException();
        }

        #endregion
    }

    // .Net 2.0 > Base: IPrincipal
    // .Net 4.5 > Base: ClaimsPrincipal
    public class PrivilegedWindowsPrincipal : WindowsPrincipal, IPrivilegedPrincipal {

        public PrivilegedWindowsPrincipal( WindowsIdentity identity, string[] roles )
            : base( identity ) {

        }

        #region IHasPrivileges Member

        public bool HasPrivilege( IPrivilege privilege ) {
            throw new NotImplementedException();
        }

        public bool HasPrivilege( IPrivilege[] privileges ) {
            throw new NotImplementedException();
        }

        public bool HasPrivilege( string privilege ) {
            throw new NotImplementedException();
        }

        public IPrivilege[] GetPrivileges() {
            throw new NotImplementedException();
        }

        #endregion
    }

    // .Net 2.0 > Base: IPrincipal
    //public class PrivilegedPassportPrincipal : System.Web.Security.PassportPrincipal, IPrivilegedPrincipal
    
    // .Net 2.0 > Base: IPrincipal
    // .Net 4.5 > Base: ClaimsPrincipal
    //public class PrivilegedPassportPrincipal : System.Web.Security.RolePrincipal, IPrivilegedPrincipal

    // .Net 4.5 > Base: IPrincipal
    //public class PrivilegedClaimsPrincipal : System.Security.Claims.ClaimsPrincipal, IPrivilegedPrincipal

}

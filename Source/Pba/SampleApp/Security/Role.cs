//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SIT.Components.Data;

namespace SIT.Components.Security {
    public class Role : IHasPrivileges {

        #region Members

        private int m_ID;
        private string m_Name;
        private int[] m_UserIDs;

        #endregion

        #region Constructor, Destructor

        public Role() {

        }

        #endregion

        #region Properties

        public int ID {
            get { return m_ID; }
        }

        public string Name {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int[] UserIDs {
            get { return m_UserIDs; }
        }

        #endregion

        #region IHasPrivileges Members

        public bool HasPrivilege( IPrivilege privilege ) {
            throw new Exception( "The method or operation is not implemented." );
        }

        public bool HasPrivilege( IPrivilege[] privileges ) {
            throw new Exception( "The method or operation is not implemented." );
        }

        public bool HasPrivilege( string Privilege ) {
            throw new Exception( "The method or operation is not implemented." );
        }

        public IPrivilege[] GetPrivileges() {
            return null;
        }

        #endregion

        #region Database

        public void Save() {
        }

        public void Load( IDataReader dr ) {
        }

        public void Load( int ID ) {
        }

        public void Load( string name ) {
           
            
        }

        #endregion

        #region Security

        private static string CreateGroupHash( int id, string name ) {
            return Functions.CreateHash( id.ToString() + name );
        }

        #endregion
    }

    
}

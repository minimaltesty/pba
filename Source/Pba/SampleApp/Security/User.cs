//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using SIT.Components.Data;
using System.Data;

namespace SIT.Components.Security {
    public class User : IPrincipal, IHasPrivileges {

        #region Members

        private int m_ID;
        private string m_Name;
        private IIdentity m_Identity;
        public string TempPriv;
        private DateTime m_LastLoginAttempt;
        private int m_LoginAttemptCount;
        private int[] m_UserGroupIDs;

        #endregion

        #region Constructor, Destructor

        public User() {
        }

        #endregion

        #region Properties

        public int ID {
            get { return m_ID; }
        }

        public string Name {
            get { return m_Name; }
            set { 
                m_Name = value;
                m_Identity = new GenericIdentity( m_Name );
            }
        }

        public int[] UserGroupIDs {
            get { return m_UserGroupIDs; }
        }

        #endregion

        #region IPrincipal Members

        public IIdentity Identity {
            get { return m_Identity; }
        }

        public bool IsInRole( string role ) {
            throw new Exception( "The method or operation is not implemented." );
        }

        #endregion

        #region IHasPrivileges Members

        public bool HasPrivilege( IPrivilege privilege ) {
            throw new NotImplementedException( "The method or operation is not implemented." );
        }

        public bool HasPrivilege( IPrivilege[] privileges ) {
            throw new NotImplementedException( "The method or operation is not implemented." );
        }

        public bool HasPrivilege( string privilege ) {
            return privilege == TempPriv;
        }

        public IPrivilege[] GetPrivileges() {
            return null;
        }

        #endregion

        #region Database




        public static void Login( string name, string password ) {

            User user;
            int userid = 0;
            try {
                user = new User();

                user = Authenticate( name, password );

                System.Threading.Thread.CurrentPrincipal = user;
            }catch( UserLoginException ex ) {
                if( ex.UserID > 0 )
                    IncLoginAttempts( CreatePasswordHash( ex.UserID, name, password ) );
                throw;
            } finally {
                user = null;
                GC.Collect();
            }

            

        }

        public static User Authenticate( string username, string password ) {
            IDataReader dr = null;

            try {
                return Authenticate( username, password, out dr );
            } finally {
                if( dr != null )
                    dr.Dispose();
            }
        }
        
        internal static User Authenticate( string username, string password, out IDataReader dr ) {

            VerifyPassword( username, password, out dr );
            VerifyLoginAttempts( dr );

            User user = new User();
            user.Load( dr );
            return user;

        }


        public static void CreateUser() {
        }

        public static User Load( string username, string password ) {
            return null;
        }

        internal void Load( IDataReader dr ) {

            m_ID = (int)dr[ "ID" ];
            m_Identity = new GenericIdentity( dr[ "USERNAME" ].ToString() );
            

        }

        internal void LoadGroups( IDataReader dr ) {



        }

        public void Save( User user ) {
        }

        #endregion

        #region Security

        public static void ResetLoginAttempts( User user ) {
        }

        public static void IncLoginAttempts( string hash ) {
        }

        public static void SetLastLoginAttemptTime( string hash ) {
        }

        public static void SetLastLoginTime( User user ) {
        }

        public static void LockUser( string hash ) {
        }

        public static void UnlockUser( User user ) {
        }

        public static string CreatePasswordHash( int id, string username, string password ) {

            string uk = CreateUserKey( id, username );

            return Functions.CreateHash( uk + password );
        }

        public static string CreateUserKey( int id, string username ) {
            return id.ToString() + username;
        }

        public static void VerifyPassword( string username, string password ) {
            
            IDataReader dr = null;

            try {
                VerifyPassword( username, password, out dr );
            } finally {
                dr.Dispose();
            }
        }

        private static void VerifyPassword( string username, string password, out IDataReader dr ) {

            int id = 0;
            string db_username = "";
            string db_pwhash = "";
            string pwhash = "";

            try {
                dr = StoredProcedure.ExecuteReader( Main.Database, "USER_BY_NAME", "@Name=" + username );

                db_username = dr[ "USERNAME" ].ToString();

                if( !db_username.Equals( username ) )
                    throw new System.Security.SecurityException( "Usernames do not match!" );

                id = (int)dr[ "ID" ];
                db_pwhash = dr[ "PASSWORD" ].ToString();

                pwhash = CreatePasswordHash( id, username, password );
                if( !pwhash.Equals( db_pwhash ) )
                    throw new System.Security.SecurityException( "Password hashs do not match!" );
            } finally {
                id = 0;
                db_username = null;
                db_pwhash = null;
                pwhash = null;
            }

        }

        internal static void VerifyLoginAttempts( IDataReader dr ) {
            throw new Exception( "The method or operation is not implemented." );
        }


        #endregion
    }
}

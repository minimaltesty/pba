//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Data;
using System.Reflection;
using System.Collections;

namespace SIT.Components.Data {
    public class DBConnection {

        private static DBConnection _mainConnection;

        public static DBConnection MainConnection {
            get { return _mainConnection; }
            set { _mainConnection = value; }
        }

        private IDbConnection m_Connection;
        private string m_Password;

        public IDbConnection Connection {
            get { return m_Connection; }
            set { m_Connection = value; }
        }

        public string Password {
            get { return m_Password; }
            set { m_Password = value; }
        }

        public IDbConnection Duplicate() {

            return Duplicate( m_Connection, m_Password );

        }

        public static IDbConnection Duplicate( IDbConnection dbcon ) {
            return Duplicate( dbcon, true );
        }

        public static IDbConnection Duplicate( IDbConnection dbcon, bool autoOpen ) {
            IDbConnection newdbcon;

            newdbcon = Activator.CreateInstance( dbcon.GetType() ) as IDbConnection;
            newdbcon.ConnectionString = dbcon.ConnectionString;

            if( autoOpen )
                newdbcon.Open();

            return newdbcon;

        }

        public static IDbConnection Duplicate( IDbConnection dbcon, string password ) {

            IDbConnection newdbcon;
            
            newdbcon = Activator.CreateInstance( dbcon.GetType()) as IDbConnection;
            newdbcon.ConnectionString = dbcon.ConnectionString;
            if( password != null )
                if( string.IsNullOrEmpty( password ) )
                    newdbcon.ConnectionString += "Password = " + password + ";";
            newdbcon.Open();
            return newdbcon;

        }

        public StoredProc CreateStoredProc( string procedureName, bool duplicateConnection ) {
            if( duplicateConnection)
                return CreateStoredProc( _mainConnection.Duplicate(), procedureName );    
            else
                return CreateStoredProc( m_Connection, procedureName );
        }

        public StoredProc CreateStoredProc( IDbTransaction transaction, string procedureName  ) {
            return StoredProcFactory.CreateInstance( transaction, procedureName );
        }

        public StoredProc CreateStoredProc( IDbConnection connection, string procedureName  ) {
            return StoredProcFactory.CreateInstance( connection, procedureName );
        }


        public IDbTransaction BeginTransaction() {
            return m_Connection.BeginTransaction();
        }

    }
}

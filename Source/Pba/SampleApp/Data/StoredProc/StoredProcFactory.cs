//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Data;
using System.Data.SqlClient;
//using Oracle.DataAccess.Client;
//using MySql.Data.MySqlClient;

namespace SIT.Components.Data {
    public static class StoredProcFactory {

        public static StoredProc CreateInstance( IDbConnection connection, string procedureName ) {
            return Factory.CreateStoredProc( connection, procedureName );
            
            //if( connection is SqlConnection )
            //    return new SqlStoredProc( connection as SqlConnection, procedureName );

            //if( connection is OracleConnection )
            //    return new OracleStoredProc( connection as OracleConnection, procedureName );
            
            //if( connection is MySqlConnection )
            //    return new MySQLStoredProc( connection as MySqlConnection, procedureName, transaction as MySqlTransaction );

            //return null;

        }

        public static StoredProc CreateInstance( IDbTransaction transaction, string procedureName ) {
            return Factory.CreateStoredProc( transaction.Connection, procedureName );

            //if( transaction.Connection is SqlConnection )
            //    return new SqlStoredProc( transaction as SqlTransaction, procedureName );

            //if( transaction.Connection is OracleConnection )
            //    return new OracleStoredProc( transaction as OracleTransaction, procedureName );

            //if( connection is MySqlConnection )
            //    return new MySQLStoredProc( connection as MySqlConnection, procedureName, transaction as MySqlTransaction );

            //return null;

        }

        public static StoredProc CreateInstance( DBConnection connection, string procedureName ) {
            return Factory.CreateStoredProc( connection, procedureName );
            //return CreateInstance( DBConnection.MainConnection.Connection, procedureName );
        }

    }
}

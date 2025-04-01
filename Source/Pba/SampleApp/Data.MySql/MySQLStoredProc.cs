//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SIT.Components.Data {
    public class MySQLStoredProc : StoredProc {

        public MySQLStoredProc( MySqlConnection connection, string procedureName )
            : base( connection, procedureName ) {
        }

        public MySQLStoredProc( MySqlConnection connection, string procedureName, MySqlTransaction transaction )
            : base( transaction, procedureName ) {
        }

        protected override IDataParameter ConvertParameter( IDataParameter param ) {
            throw new Exception( "The method or operation is not implemented." );
        }
    }
}

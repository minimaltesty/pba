//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Data;
using System.Data.Common;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using SIT.Components.Data;

namespace SIT.Components.Data {
    public class OracleStoredProc : StoredProc {

        public OracleStoredProc( OracleConnection connection, string procedureName )
            : base( connection, procedureName ) {
        }

        public OracleStoredProc( OracleTransaction transaction, string procedureName  )
            : base( transaction.Connection, procedureName ) {
        }

        public new OracleConnection Connection { get { return base.Connection as OracleConnection; } }

        public override IDataReader ExecuteReader( CommandBehavior behavior ) {
            
            OracleCommand cmd;
            OracleCommand derivedCmd;
            IDataReader retval;
            //OracleParameter refCursor;
            OracleParameter cmdCopy;

            cmd = this.CreateCommand();

            derivedCmd = cmd.Clone() as OracleCommand;
            OracleCommandBuilder.DeriveParameters( derivedCmd );

            foreach( OracleParameter param in derivedCmd.Parameters ) {

            //    if( param.OracleDbType == OracleDbType.RefCursor )
            //        refCursor = param;

                if( param.Direction == ParameterDirection.Output ) {
                    cmdCopy = cmd.CreateParameter();
                    cmdCopy.Direction = param.Direction;
                    //cmdCopy.DbType = param.DbType;
                    cmdCopy.OracleDbType = param.OracleDbType;
                    cmdCopy.ParameterName = param.ParameterName;
                    cmdCopy.Value = param.Value;
                    cmd.Parameters.Add( cmdCopy );

                }

            }

            retval = cmd.ExecuteReader( behavior );

            cmd.Dispose();
            cmd = null;

            return retval;
        }


        

        protected new OracleCommand CreateCommand() {

            OracleCommand cmd;
            
            cmd = base.CreateCommand() as OracleCommand;

            for( int idx = 0; idx < m_InParameters.Count; idx++ )
                cmd.Parameters.Add( m_InParameters[ idx ] );

            return cmd;

        }

        protected override IDataParameter ConvertParameter( IDataParameter param ) {

            OracleParameter specParam;

            specParam = param as OracleParameter;

            switch( specParam.OracleDbType ) {
                case OracleDbType.Date:

                    DateTime dt;
                    OracleDate odt;

                    if( specParam.Value is DateTime )
                        dt = (DateTime)specParam.Value;
                    else
                        dt = DateTime.Parse( specParam.Value.ToString() );

                    dt = (DateTime)specParam.Value;
                    odt = new OracleDate( dt );
                    specParam.Value = odt;

                    return specParam;
                    
            }

            if( specParam.OracleDbType == OracleDbType.Decimal && specParam.Value is string ) {

                switch( specParam.Value.ToString().ToUpper() ) {
                    case "TRUE":
                        specParam.Value = -1;
                        return specParam;
                    case "FALSE":
                        specParam.Value = 0;
                        return specParam;
                }

            }

            return specParam;

        }

        

    }
}

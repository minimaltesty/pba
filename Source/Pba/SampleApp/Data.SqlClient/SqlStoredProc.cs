//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using SIT.Components.Data;

namespace SIT.Components.Data.SqlClient {
    public class SqlStoredProc : StoredProc {

        public static bool AutoPrepend_AT_Sign = true;

        private static bool m_AutoDuplicateConnection = false;
        private static bool m_IgnoreCloseConnectionBehaviour = true;
        private static CommandBehavior m_DefaultCommandBehavior = CommandBehavior.CloseConnection;

        public static bool AutoDuplicateConnection {
            get { return m_AutoDuplicateConnection; }
            set { m_AutoDuplicateConnection = value; }
        }

        public static bool IgnoreCloseConnectionBehaviour {
            get { return m_IgnoreCloseConnectionBehaviour; }
            set { m_IgnoreCloseConnectionBehaviour = value; }
        }


        public SqlStoredProc( SqlConnection connection, string procedureName )
            : base( connection, procedureName ) {

            if( m_AutoDuplicateConnection )
                m_Connection = DBConnection.Duplicate( connection );

        }

        public SqlStoredProc( SqlTransaction transaction, string procedureName )
            : this( transaction.Connection, procedureName ) {
            m_Transaction = transaction;
        }

        protected override IDataParameter ConvertParameter( IDataParameter param ) {
            
            param.ParameterName = "@" + param.ParameterName;
            return param;
        }

        public override IDataReader ExecuteReader() {
            return ExecuteReader( m_DefaultCommandBehavior );
        }

        public override IDataReader ExecuteReader( CommandBehavior behavior ) {
            if ( m_IgnoreCloseConnectionBehaviour )
                behavior = CommandBehavior.Default;
            return base.ExecuteReader( behavior );
        }

        public IAsyncResult BeginExecuteReader( AsyncCallback callback ) {
            return BeginExecuteReader( callback, CommandBehavior.Default );
        }

        public IAsyncResult BeginExecuteReader( AsyncCallback callback, CommandBehavior behavior ) {
            SqlCommand cmd;
            IAsyncResult retval;

            if ( m_IgnoreCloseConnectionBehaviour )
                behavior = CommandBehavior.Default;

            cmd = this.CreateCommand() as SqlCommand;
            retval = cmd.BeginExecuteReader( callback, cmd, behavior );
            
            return retval;

        }

        public SqlDataReader EndExecuteReader( IAsyncResult result ) {
            return ( result.AsyncState as SqlCommand ).EndExecuteReader( result );
        }


        //public override IDataParameter AddParameter( string name, object value, ParameterDirection direction, DbType type ) {
        //    if ( AutoPrepend_AT_Sign )
        //        name = "@" + name;
        //    return base.AddParameter( name, value, direction, type );
        //}

        protected override IDbCommand CreateCommand() {

            SqlCommand cmd;
            SqlCommand cmdClone;
            bool paramExists;
            SqlParameter newParam;

            cmd = base.CreateCommand() as SqlCommand;

            cmdClone = cmd.Clone();

            SqlCommandBuilder.DeriveParameters( cmdClone );

            foreach( SqlParameter param in cmdClone.Parameters ) {

                if ( param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput ) {
                    paramExists = false;
                    foreach ( IDataParameter param2 in m_OutParameters ) {
                        if ( param2.ParameterName == param.ParameterName ) {
                            paramExists = true;
                            break;
                        }
                    }

                    if ( !paramExists ) {
                        newParam = cmd.CreateParameter();
                        newParam.DbType = param.DbType;
                        newParam.Direction = ParameterDirection.Output;
                        //if ( SqlStoredProc.AutoPrepend_AT_Sign )
                        //    newParam.ParameterName = param.ParameterName.Substring( 1 );
                        //else
                            newParam.ParameterName = param.ParameterName;
                        newParam.SourceColumn = param.SourceColumn;
                        newParam.SourceVersion = param.SourceVersion;
                        newParam.Value = param.Value;
                        newParam.Size = param.Size;

                        m_OutParameters.Add( newParam );

                        ( cmd as IDbCommand ).Parameters.Add( newParam );
                    }


                }
                if( param.Direction == ParameterDirection.Input ) {
                    paramExists = false;
                    foreach( IDataParameter param2 in m_InParameters ) {
                        if( param2.ParameterName == param.ParameterName ) {
                            param2.DbType=param.DbType;
                            break;
                        }
                    }

                }
            }

            return cmd;


        }

        public override object ExecuteScalar() {
            IDbCommand cmd;
            object retval=null;

            cmd = this.CreateCommand();

            retval = cmd.ExecuteScalar();

            if ( m_OutParameters.Count == 1 ) {
                retval = m_OutParameters[ 0 ].Value;
            }

            cmd.Dispose();
            cmd = null;

            return retval;
        }


    }
}

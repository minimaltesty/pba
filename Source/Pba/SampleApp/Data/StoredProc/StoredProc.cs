//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace SIT.Components.Data {
    public abstract class StoredProc : IDisposable{

        protected string m_Name;
        protected IDbConnection m_Connection;
        protected IDbTransaction m_Transaction;
        protected DataParameterCollection m_InParameters;
        protected DataParameterCollection m_OutParameters;
        private IDbCommand m_DummyCommand;

        public StoredProc( IDbConnection dbConnection, string procedureName ) {
            m_Connection = dbConnection;
            m_Name = procedureName;
            m_DummyCommand = m_Connection.CreateCommand();
            m_InParameters = new DataParameterCollection();
            m_OutParameters = new DataParameterCollection();
        }

        public StoredProc( IDbTransaction transaction, string procedureName )
            : this( transaction.Connection, procedureName ) {
            m_Transaction = transaction;
        }

        public IDbConnection Connection { get { return m_Connection; } }
        public IDbTransaction Transaction { get { return m_Transaction; } }
        public DataParameterCollection InParameters { get { return m_InParameters; } }
        public DataParameterCollection OutParameters { get { return m_OutParameters; } }

        protected virtual IDbCommand CreateCommand() {

            IDbCommand cmd;
            int idx;

            cmd = m_Connection.CreateCommand();
            
            cmd.CommandText = m_Name;
            cmd.CommandType = CommandType.StoredProcedure;

            for( idx = 0; idx < m_InParameters.Count; idx++ )
                cmd.Parameters.Add( ConvertParameter( m_InParameters[ idx ] ) );

            for( idx = 0; idx < m_OutParameters.Count; idx++ )
                cmd.Parameters.Add( ConvertParameter( m_OutParameters[ idx ] ) );



            cmd.Transaction = m_Transaction;

            Trace.TraceInformation( "SP: " + m_Name );

            return cmd;

        }

        public virtual void ExecuteNonQuery() {

            IDbCommand cmd;

            cmd = this.CreateCommand();

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;

        }

        public virtual object ExecuteScalar() {

            IDbCommand cmd;
            object retval;

            cmd = this.CreateCommand();

            retval = cmd.ExecuteScalar();
            
            cmd.Dispose();
            cmd = null;

            return retval;

        }

        public virtual IDataReader ExecuteReader() {
            return this.ExecuteReader( CommandBehavior.Default );
        }

        public virtual IDataReader ExecuteReader( CommandBehavior behavior ) {

            IDbCommand cmd;
            IDataReader retval;

            cmd = this.CreateCommand();
            retval = cmd.ExecuteReader( behavior );

            cmd.Dispose();
            cmd = null;

            return retval;
        }


        protected abstract IDataParameter ConvertParameter( IDataParameter param );

        public virtual IDataParameter AddParameter( string name, object value ) {
            return AddParameter( name, value, ParameterDirection.Input );
        }

        public virtual IDataParameter AddParameter( string name, object value, ParameterDirection direction ) {
            return AddParameter( name, value, direction, DbType.AnsiString );
        }

        public virtual IDataParameter AddParameter( string name, object value, ParameterDirection direction, DbType type ) {

            IDataParameter newParam;
            
            newParam = m_DummyCommand.CreateParameter();
            newParam.DbType = type;
            newParam.ParameterName = name;
            newParam.Value = value;
            newParam.Direction = direction;

            switch( direction ) {
                case ParameterDirection.Input:
                    m_InParameters.Add( newParam );
                    break;
                case ParameterDirection.Output:
                    m_OutParameters.Add( newParam );
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            return newParam;

        }


        #region IDisposable Member

        public void Dispose() {
            
            if( m_DummyCommand != null )
                m_DummyCommand.Dispose();

        }

        #endregion
    }
}

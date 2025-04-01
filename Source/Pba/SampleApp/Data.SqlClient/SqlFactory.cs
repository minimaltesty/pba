using System;
using System.Data;
using System.Data.SqlClient;
using SIT.Components.Data;

namespace SIT.Components.Data.SqlClient {

    [ConnectionType("System.Data.SqlClient.SqlConnection")]
    public class SqlFactory : IFactory {
        #region IFactory Member

        public IDbCommand CreateCommand() {
            return new SqlCommand();
        }

        public StoredProc CreateStoredProc( IDbConnection connection, string procedureName ) {
            return new SqlStoredProc( connection as SqlConnection, procedureName );
        }

        public StoredProc CreateStoredProc( IDbTransaction transaction, string procedureName ) {
            return new SqlStoredProc( transaction as SqlTransaction, procedureName );
        }

        public IDbDataAdapter CreateDataAdapter() {
            return new SqlDataAdapter();
        }

        public IDbConnection CreateConnection() {
            return new SqlConnection();
        }

        public IDbDataParameter CreateDataParameter() {
            return new SqlParameter();
        }

        public IDbUpdater CreateUpdater() {
            SqlDbUpdater retval = new SqlDbUpdater();
            retval.BackupCreator = this.CreateBackupCreator() as SqlBackupCreator;
            return retval;
        }

        public IBackupCreator CreateBackupCreator() {
            return new SqlBackupCreator();
        }

        public IBackupRecovery CreateBackupRecovery() {
            return new SqlBackupRecovery();
        }

        #endregion
    }
}

using System;
using System.Data;

namespace SIT.Components.Data
{
    public interface IFactory {

        IDbCommand CreateCommand();
        StoredProc CreateStoredProc( IDbConnection dbConnection, string procedureName );
        StoredProc CreateStoredProc( IDbTransaction transaction, string procedureName );
        IDbDataAdapter CreateDataAdapter();
        IDbConnection CreateConnection();
        IDbDataParameter CreateDataParameter();
        IDbUpdater CreateUpdater();
        IBackupCreator CreateBackupCreator();
        IBackupRecovery CreateBackupRecovery();

    }
}

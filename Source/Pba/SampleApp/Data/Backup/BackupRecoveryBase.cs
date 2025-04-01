using System;
using System.Collections.Generic;

namespace SIT.Components.Data {
    public abstract class BackupRecoveryBase : IBackupRecovery {

        string _connectionString;

        public BackupRecoveryBase() {
        }

        public BackupRecoveryBase( string connectionString ) : this() {
            _connectionString=connectionString;
        }

        public string ConnectionString {
            get { return _connectionString; }
            set { _connectionString=value; }
        }

        public abstract List<IBackupInfo> GetBackupInfos();

    }
}

using System;

namespace SIT.Components.Data {
    public abstract class BackupCreatorBase : IBackupCreator {
        #region IBackupCreator Member

        protected string _serverName;

        public string Servername {
            get { return _serverName; }
            set { _serverName = value; }
        }

        protected string _databaseName;

        public string Databasename {
            get { return _databaseName; }
            set { _databaseName = value; }
        }

        public abstract void CreateFullBackup();

        #endregion
    }
}

using System;

namespace SIT.Components.Data {
    public interface IBackupCreator {

        string Servername {
            get;
            set;
        }

        string Databasename {
            get;
            set;
        }

        void CreateFullBackup();

    }
}

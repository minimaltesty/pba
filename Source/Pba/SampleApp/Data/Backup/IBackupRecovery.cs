using System.Collections.Generic;

namespace SIT.Components.Data {
    public interface IBackupRecovery {

        string ConnectionString {
            get;
            set;
        }

        List<IBackupInfo> GetBackupInfos();

    }
}

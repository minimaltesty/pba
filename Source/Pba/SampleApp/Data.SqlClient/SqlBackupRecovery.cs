using System;
using System.Collections.Generic;
using Smo=Microsoft.SqlServer.Management.Smo;
using SIT.Components.Data;

namespace SIT.Components.Data.SqlClient {
    public class SqlBackupRecovery : BackupRecoveryBase {

        public override List<IBackupInfo> GetBackupInfos() {
            return null;
        }

    }
}

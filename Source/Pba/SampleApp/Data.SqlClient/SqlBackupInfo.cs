using System;
using SIT.Components.Data;

namespace SIT.Components.Data.SqlClient {
    public class SqlBackupInfo : BackupInfoBase  {

        public SqlBackupInfo( string name, DateTime date )
            : base( name, date ) {
        }

    }
}

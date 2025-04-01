using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using SIT.Components.Data;

namespace SIT.Components.Data.SqlClient {
    public class SqlDbUpdater : DbUpdaterBase{

        public new SqlConnection DbConnection {
            get { return _dbConnection as SqlConnection; }
            set { 
                _dbConnection = value;
                _dbName = ( _dbConnection as SqlConnection ).Database;
            }
        }

        public new SqlBackupCreator BackupCreator {
            get { return _backupCreator as SqlBackupCreator; }
            set { _backupCreator = value; }
        }

        public new string DbName {
            get { return DbConnection.Database; }
        }

        protected override void InternalUpdate() {
            Trace.TraceInformation( "Running database update ..." );
            string serverName = this.DbConnection.DataSource;
            string databaseName = this.DbConnection.Database;
            OnUpdateProgress(0, "Getting script files");
            string[] fileNames = base.GetScriptFiles();
            if (fileNames.Length == 0) {
                OnUpdateProgress(0, "No Script files found");
                return;
            }
            OnUpdateProgress(0, string.Format("Found {0} script files", fileNames.Length));
            OnUpdateProgress(0, "Combining file contents");
            string completeScript;
            StringBuilder sb = new StringBuilder();
            int fileCounter = 0;
            for ( int i = 0; i < fileNames.Length; i++ ) {
                Trace.TraceInformation( "Loading script file \"{0}\" ...", fileNames[ i ] );
                sb.AppendLine( System.IO.File.ReadAllText( fileNames[ i ] ) );
                sb.AppendLine( "GO" );
                OnUpdateProgress(++fileCounter * 100 / fileNames.Length, string.Empty );

            }
            completeScript=sb.ToString();
            OnUpdateProgress(0, "Running script");
            Server s=new Server( serverName);
            Database db = s.Databases[ databaseName ];
            try {
                Trace.TraceInformation( "Executing loaded scripts ..." );
                db.ExecuteNonQuery( completeScript, ExecutionTypes.ParseOnly );
                db.ExecuteNonQuery( completeScript, ExecutionTypes.Default );
                OnUpdateProgress(0, "Update succeeded");
                Trace.TraceInformation( "Database update succeeded." );

            } catch ( Microsoft.SqlServer.Management.Common.SqlServerManagementException ex ) {
                OnUpdateProgress(0, string.Format("Update failed: {0}", ex.Message));
                Console.WriteLine( "ERROR" );
                Trace.TraceInformation( "Database update failed! Errors:{0}", SIT.Components.Common.ExceptionExtractor.Extract(ex).ToString() );
                throw;

            }
        }
    }
}

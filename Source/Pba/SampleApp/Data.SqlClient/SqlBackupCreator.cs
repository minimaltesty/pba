using System;
using System.Diagnostics;
using SIT.Components.Data;
using Smo = Microsoft.SqlServer.Management.Smo;

namespace SIT.Components.Data.SqlClient {
    public class SqlBackupCreator : BackupCreatorBase {

        public override void CreateFullBackup() {
            Trace.TraceInformation( string.Format( "Creating backup (Server = \"{0}\"; Database = \"{1}\")", _serverName,_databaseName ));

            string timeStampStr = DateTime.Now.Year.ToString( "0000" ) +
                DateTime.Now.Month.ToString( "00" ) +
                DateTime.Now.Day.ToString( "00" ) +
                DateTime.Now.Hour.ToString( "00" ) +
                DateTime.Now.Minute.ToString( "00" ) +
                DateTime.Now.Second.ToString( "00" );

            Smo.Server srv = new Smo.Server( _serverName );
            Smo.Database db = srv.Databases[ _databaseName ];
            Smo.RecoveryModel recoveryMod = db.RecoveryModel;
            Smo.Backup bu = new Smo.Backup();
            
            bu.Action = Smo.BackupActionType.Database;
            bu.BackupSetDescription = "Full backup of Personalverwaltung";
            bu.BackupSetName = "Personalverwaltung Backup";
            bu.Database = db.Name;

            Smo.BackupDeviceItem buDev = new Smo.BackupDeviceItem( "Personalverwlatung_Full_" + timeStampStr, Smo.DeviceType.File );
            bu.Devices.Add( buDev );
            bu.Incremental = false;
            bu.ExpirationDate = DateTime.Now.AddMonths( 6 );
            bu.LogTruncation = Smo.BackupTruncateLogType.NoTruncate;
            bu.SqlBackup( srv );

            bu = null;
            db = null;
            srv = null;

            Trace.TraceInformation( string.Format( "Backup created (BackupSetName = \"{0}\"; BackupSetDescription = \"{1}\"; BackupDeciveItemName = \"{2}\")", bu.BackupSetName, bu.BackupSetDescription, buDev.Name ) );

        }

    }
}

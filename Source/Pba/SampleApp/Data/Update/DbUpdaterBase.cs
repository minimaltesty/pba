using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SIT.Components.Data {
    public abstract class DbUpdaterBase : IDbUpdater{

        protected delegate void UpdateDelegate();
        public event EventHandler<UpdateFinishedEventArgs> UpdateFinished;
        public event EventHandler<UpdateStartedEventArgs> UpdateStarted;
        public event EventHandler<UpdateProgressEventArgs> UpdateProgress;

        protected IDbConnection _dbConnection;
        protected string _scriptPath;
        protected string _customerName;
        protected string _dbName;
        protected string _sourceDbVersion;
        protected string _destinationDbVersion;
        protected IBackupCreator _backupCreator;

        public DbUpdaterBase() {
        }

        public DbUpdaterBase( IDbConnection dbConnection )
            : this() {
            _dbConnection = dbConnection;
        }

        public IDbConnection DbConnection {
            get { return _dbConnection; }
            set { _dbConnection = value; }
        }

        public string CustomerName {
            get { return _customerName; }
            set { _customerName = value; }
        }

        public string CatalogName {
            get { return _dbName; }
            set { _dbName = value; }
        }

        public string ScriptPath {
            get { return _scriptPath; }
            set { _scriptPath = value; }
        }

        public string SourceDbVersion {
            get { return _sourceDbVersion; }
            set { _sourceDbVersion = value; }
        }

        public string DestinationDbVersion {
            get { return _destinationDbVersion; }
            set { _destinationDbVersion = value; }
        }

        public IBackupCreator BackupCreator {
            get { return _backupCreator; }
            set { _backupCreator = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update() {
            OnUpdateStarted();
            if( _backupCreator != null ) {
                OnUpdateProgress( 0, "Datenbanksicherung erstellen" );
                _backupCreator.CreateFullBackup();
            }

            try {
                InternalUpdate();
            } catch( DBUpdaterException ex ) { 
            }
            OnUpdateFinished();
        }

        protected void OnUpdateStarted() {
            if (UpdateStarted != null)
                UpdateStarted(this, new UpdateStartedEventArgs(_sourceDbVersion, _destinationDbVersion));
        }

        protected void OnUpdateFinished()
        {
            if (UpdateFinished != null)
                UpdateFinished(this, new UpdateFinishedEventArgs(_sourceDbVersion, _destinationDbVersion));
        }

        protected void OnUpdateProgress( int percentage, string message )
        {
            if (UpdateProgress != null)
                UpdateProgress(this, new UpdateProgressEventArgs(_sourceDbVersion, _destinationDbVersion, percentage,message ));
        }


        protected abstract void InternalUpdate();

        public IAsyncResult BeginUpdate() {
            UpdateDelegate del = new UpdateDelegate( this.Update );
            return del.BeginInvoke( new AsyncCallback( this.EndUpdate ), del );
        }

        public void EndUpdate(IAsyncResult ar) {
            ( ar.AsyncState as UpdateDelegate ).EndInvoke( ar );
            ar = null;
        }

        public bool CheckForUpdates() {
            if ( _sourceDbVersion.Equals( _destinationDbVersion ) ) return false;

            return false;
        }

        protected string[] GetScriptFiles() {
            List<string> retval = new List<string>();
            DirectoryInfo di = new DirectoryInfo( _scriptPath );
            DirectoryInfo[] sdi = di.GetDirectories();
            DirectoryInfo currendSubDir;
            FileInfo[] scriptFiles;
            UpdateVersionHistory vh = UpdateVersionHistory.CreateFromXmlFile( _scriptPath + @"\" + UpdateVersionHistory.XML_FILENAME );
            vh = vh.GetSpan( _customerName, _dbName, _sourceDbVersion, _destinationDbVersion );

            foreach ( UpdateVersion uv in vh ) {
                for ( int i = 0; i < sdi.Length; i++ ) {
                    currendSubDir = sdi[ i ];
                    if ( uv.Version.Equals( currendSubDir.Name ) ) {
                        scriptFiles = currendSubDir.GetFiles();
                        for ( int k = 0; k < scriptFiles.Length; k++ )
                            retval.Add( scriptFiles[ k ].FullName );
                    }
                }
            }

            scriptFiles = null;
            currendSubDir = null;
            sdi = null;
            di = null;

            return retval.ToArray();
        }
    }
}

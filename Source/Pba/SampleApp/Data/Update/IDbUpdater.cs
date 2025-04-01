using System;
using System.Data;

namespace SIT.Components.Data {
    public interface IDbUpdater {

        event EventHandler<UpdateFinishedEventArgs> UpdateFinished;
        event EventHandler<UpdateStartedEventArgs> UpdateStarted;
        event EventHandler<UpdateProgressEventArgs> UpdateProgress;

        string CustomerName {
            get;
            set;
        }

        string CatalogName {
            get;
            set;
        }

        IDbConnection DbConnection {
            get;
            set;
        }

        string ScriptPath {
            get;
            set;
        }

        string SourceDbVersion {
            get;
            set;
        }

        string DestinationDbVersion {
            get;
            set;
        }

        IBackupCreator BackupCreator {
            get;
            set;
        }

        void Update();
        IAsyncResult BeginUpdate();
        void EndUpdate( IAsyncResult ar );
        bool CheckForUpdates();

    }

    public class UpdateStartedEventArgs : EventArgs
    {

        readonly string _fromVersion;
        readonly string _toVersion;

        public UpdateStartedEventArgs(string fromVersion, string toVersion)
            : base()
        {
            _fromVersion = fromVersion;
            _toVersion = toVersion;
        }

        public string FromVersion { get { return _fromVersion; } }
        public string ToVersion { get { return _toVersion; } }

    }

    public class UpdateFinishedEventArgs : EventArgs
    {

        readonly string _fromVersion;
        readonly string _toVersion;

        public UpdateFinishedEventArgs(string fromVersion, string toVersion)
            : base()
        {
            _fromVersion = fromVersion;
            _toVersion = toVersion;
        }

        public string FromVersion { get { return _fromVersion; } }
        public string ToVersion { get { return _toVersion; } }

    }


    public class UpdateProgressEventArgs : EventArgs
    {

        readonly string _fromVersion;
        readonly string _toVersion;
        readonly int _percentage;
        readonly string _message;

        public UpdateProgressEventArgs(string fromVersion, string toVersion, int percentage, string message)
            : base()
        {
            _fromVersion = fromVersion;
            _toVersion = toVersion;
            _percentage = percentage;
            _message = message;
        }

        public string FromVersion { get { return _fromVersion; } }
        public string ToVersion { get { return _toVersion; } }
        public int Percentage { get { return _percentage; } }
        public string Message { get { return _message; } }

    }

}

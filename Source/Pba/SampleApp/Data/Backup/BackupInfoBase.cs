using System;

namespace SIT.Components.Data {
    public abstract class BackupInfoBase : IBackupInfo {
        #region IBackupInfo Member

        string _name;
        DateTime _date;

        public BackupInfoBase( string name, DateTime date ) {
            _name = name;
            _date=date;
        }

        public string Name { get { return _name; } }
        public DateTime Date { get { return _date; } }

        #endregion
    }
}

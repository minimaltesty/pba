using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SIT.Components.Data {

    public class UpdateVersion  {

        string _version;
        string _previousVersion;
        string _customerName;
        string _dbName;

        public UpdateVersion() {
        }

        public string Version {
            get { return _version; }
            set { _version = value; }
        }

        public string PreviousVersion {
            get { return _previousVersion; }
            set { _previousVersion = value; }
        }

        public string CustomerName {
            get { return _customerName; }
            set { _customerName = value; }
        }

        public string DbName {
            get { return _dbName; }
            set { _dbName = value; }
        }

        public bool Equals( string customerName, string dbName ) {
            return _customerName.Equals( customerName ) && _dbName.Equals( dbName );
        }

    }

    public class UpdateVersionHistory : List<UpdateVersion> {

        public const string XML_FILENAME = "DBUpdateHistory.xml";

        public UpdateVersionHistory GetSpan( string customerName, string dbName, string fromVersion, string toVersion ) {
            UpdateVersionHistory retval = new UpdateVersionHistory();
            UpdateVersion firstVersion = null;
            UpdateVersion currentVersion = null;

            //find first Version
            foreach ( UpdateVersion uv in this )
                if ( string.IsNullOrEmpty( uv.PreviousVersion ) )
                    if ( uv.Equals( customerName, dbName ) ) {
                        firstVersion = uv;
                        break;

                    }

            if ( firstVersion != null ) {
                currentVersion = firstVersion;
                while ( currentVersion != null ) {
                    retval.Add( currentVersion );
                    currentVersion = FindByPreviousVersion( customerName, dbName, currentVersion.Version );
                    if( currentVersion == null )
                        break;
                    if ( currentVersion.PreviousVersion.Equals( toVersion ) ) break;

                }
            }

            if ( retval.Count > 0 )
                retval.RemoveAt( 0 );

            return retval;
        }

        private UpdateVersion FindByVersion( string customerName, string dbName, string version ) {
            foreach ( UpdateVersion uv in this )
                if ( uv.Version.Equals( version ) )
                    if ( uv.Equals( customerName, dbName ) )
                        return uv;

            return null;
        }

        private UpdateVersion FindByPreviousVersion( string customerName, string dbName, string version ) {
            foreach ( UpdateVersion uv in this )
                if ( uv.PreviousVersion.Equals( version ) )
                    if ( uv.Equals( customerName, dbName ) )
                        return uv;

            return null;
            
        }

        public void WriteXml( string fileName ) {
            using ( FileStream fs = new FileStream( fileName, System.IO.FileMode.Create ) ) {
                XmlSerializer ser = new XmlSerializer( this.GetType() );
                ser.Serialize( fs, this );
                fs.Flush();
                ser = null;
            }
        }

        public static UpdateVersionHistory CreateFromXmlFile( string fileName ) {
            UpdateVersionHistory retval = new UpdateVersionHistory();

            using ( FileStream fs = new FileStream( fileName, FileMode.Open ) ) {
                XmlSerializer ser = new XmlSerializer( typeof(UpdateVersionHistory) );
                retval = ser.Deserialize( fs ) as UpdateVersionHistory;
                fs.Close();
            }

            return retval;
        }

    }
}

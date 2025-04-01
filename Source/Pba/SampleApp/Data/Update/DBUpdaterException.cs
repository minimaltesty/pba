using System;

namespace SIT.Components.Data {
    public class DBUpdaterException : ApplicationException {

        public DBUpdaterException( string message ) : base( message ) { }
        public DBUpdaterException( string message, Exception innerException ) : base( message, innerException ) { }

    }
}

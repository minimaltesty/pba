//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace SIT.Components.Security {
    class UserLoginException : System.Security.SecurityException {

        private int m_UserID;

        public UserLoginException( int userID, string message ) : base( message ) {
            m_UserID = userID;
        }

        public UserLoginException( int userID, string message, Exception inner )
            : base( message, inner ) {
            m_UserID = userID;
        }

        public int UserID {
            get { return m_UserID; }
        }


    }
}

//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Data;

namespace SIT.Components.Security {

    public static class Main {

        private static Options m_Options;

        private static SIT.Components.Data.DBConnection m_Database;

        public static SIT.Components.Data.DBConnection Database {
            get { return m_Database; }
            set { m_Database = value; }
        }

        public static Options SecurityOptions {
            get { return m_Options; }
        }


    }
}

//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIT.Components.Data;

namespace SIT.Components.Security {
    public class Privilege : IPrivilege {

        #region Members

        private int m_ID;
        private string m_Name;

        #endregion

        #region Properties

        #region IPrivilege Members

        public int ID {
            get { return m_ID; }
        }

        public string Name {
            get { return m_Name; }
            set { m_Name = value; }
        }

        #endregion

        #endregion

        #region Database

        public static Privilege Create( string name ) {

            object retval;
            string hashstr = "";
            Privilege newpriv;

            newpriv = new Privilege();
            retval = StoredProcedure.EcexuteScalar(
                Main.Database,
                "SEC_PRIVILEGE_ADD",
                "@Name=" + name + "," +
                "@hash=" + hashstr
                );
            newpriv.m_ID = (int)retval;
            newpriv.m_Name = name;
            return newpriv;

        }

        public static void Delete( string name ) {

            StoredProcedure.EcexuteNonQuery(
                Main.Database,
                "SEC_PRIVILEGE_DELETE_BY_NAME",
                "@Name=" + name
            );

        }

        public static void Delete( int id ) {

            StoredProcedure.EcexuteNonQuery(
                Main.Database,
                "SEC_PRIVILEGE_DELETE_BY_ID",
                "@ID=" + id.ToString()
            );

        }


        private void Load( IDataReader dr ) {

            string hashstr;

            if( dr != null ) {

                dr.Read();
                m_ID = (int)dr[ "ID" ];
                m_Name = dr[ "Name" ].ToString();
                hashstr = dr[ "Hash" ].ToString();

            }
        }

        public void Load( int id ) {
            IDataReader dr;

            dr = StoredProcedure.ExecuteReader(
                Main.Database,
                "SEC_PRIVILEGE_BY_ID",
                "@ID=" + id.ToString()
                );

            Load( dr );

            dr.Close();
            dr.Dispose();
        }

        public void Load( string name ) {
            IDataReader dr;

            dr = StoredProcedure.ExecuteReader(
                Main.Database,
                "SEC_PRIVILEGE_BY_NAME",
                "@Name=" + name
                );

            Load( dr );

            dr.Close();
            dr.Dispose();
        }

        #endregion
    }
}

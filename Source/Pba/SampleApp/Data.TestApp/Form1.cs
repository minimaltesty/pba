using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using Oracle.DataAccess.Client;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using SIT.Components.MySql.Data.MySqlClient;

using SIT.Components.Data;


namespace SIT.Components.Data.TestApp {
    public partial class Form1 : Form {

        private System.Data.Common.DbConnection m_DBCon;

        public Form1() {
            InitializeComponent();
        }

        private void OpenDB() {

            m_DBCon = new System.Data.SqlClient.SqlConnection();
            m_DBCon.ConnectionString =
                "server=WSAP\\SQLEXPRESS;" +
                "database=TEST_AT;" +
                "Integrated Security=true";
            m_DBCon.Open();

        }

        private void CloseDB() {
            m_DBCon.Close();
            m_DBCon.Dispose();
        }

        private void button1_Click( object sender, EventArgs e ) {
            OpenDB();

            Data.DBConnection db = new DBConnection();
            db.Connection = m_DBCon;

            

            CloseDB();
        }

        private void button2_Click( object sender, EventArgs e ) {
            DSPerson a;

            

        }

        private void Form1_Load( object sender, EventArgs e ) {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "sIT_TESTDataSet.View_1". Sie können sie bei Bedarf verschieben oder entfernen.
            //this.view_1TableAdapter.Fill( this.sIT_TESTDataSet.View_1 );
            // TODO: Diese Codezeile lädt Daten in die Tabelle "sIT_TESTDataSet.View_1". Sie können sie bei Bedarf verschieben oder entfernen.
            //this.view_1TableAdapter.Fill( this.sIT_TESTDataSet.View_1 );
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dSPerson.PERSON". Sie können sie bei Bedarf verschieben oder entfernen.
  

        }

        private void button3_Click( object sender, EventArgs e ) {


            StoredProc sp1;
            IDataReader dr1;
            StoredProc sp2;
            IDataReader dr2;

            #region Oracle

            //OracleConnection connOracle;
            //connOracle = new OracleConnection();
            //connOracle.ConnectionString = "Data Source=oraclesrv;Persist Security Info=True;User ID=test;Password=test";
            //connOracle.Open();

            //sp1 = StoredProcFactory.CreateInstance( connOracle, "PERSON_ALL" );
            //dr1 = sp1.ExecuteReader( CommandBehavior.CloseConnection );

            //sp2 = StoredProcFactory.CreateInstance( connOracle, "PERSON_ALL" );
            //dr2 = sp2.ExecuteReader( CommandBehavior.CloseConnection );

            //sp2.Dispose();
            //dr2.Dispose();

            //dr1.Dispose();
            //sp1.Dispose();

            #endregion

            #region SQL

            SqlConnection connSQL;
            connSQL = new SqlConnection();
            connSQL.ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=SIT_TEST;Integrated Security=SSPI;";
            connSQL.Open();

            sp1 = StoredProcFactory.CreateInstance( connSQL, "PERSON_ALL" );
            dr1 = sp1.ExecuteReader( CommandBehavior.CloseConnection );

            sp2 = StoredProcFactory.CreateInstance( connSQL, "PERSON_ALL" );
            dr2 = sp2.ExecuteReader( CommandBehavior.CloseConnection );

            sp2 = null;
            dr2.Dispose();

            dr1.Dispose();
            sp1 = null;

            #endregion

            //#region MySQL

            //MySqlConnection connMySQL;
            //connMySQL = new MySqlConnection();
            //connMySQL.ConnectionString = @"Data Source = oraclesrv;Database=sit_test;User ID=situser;Password=;";
            //connMySQL.Open();

            //sp1 = StoredProcFactory.CreateInstance( connMySQL, "person_all" );
            //dr1 = sp1.ExecuteReader( CommandBehavior.CloseConnection );

            //sp2 = StoredProcFactory.CreateInstance( connMySQL, "PERSON_ALL" );
            //dr2 = sp2.ExecuteReader( CommandBehavior.CloseConnection );

            //sp2 = null;
            //dr2.Dispose();

            //dr1.Dispose();
            //sp1 = null;

            //#endregion










        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SIT.Components.Security;
using SIT.Components.Security.Permission.CAS;

namespace SIT.Components.Security.Permission.TestApp {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click( object sender, EventArgs e ) {

            User u = new User();
            u.Name = "TestUser";
            u.TempPriv = "HALzLO";
            System.Threading.Thread.CurrentPrincipal = u;


            test();
        }

        [PrivilegePermission( System.Security.Permissions.SecurityAction.Demand, Privilege="HALLO")] 
        private void test() {


            PrivilegePermission pp = new PrivilegePermission( "HALLO" );
            pp.Demand();
            double futureValue = 0;
            double payment;

            

        }
    }
}
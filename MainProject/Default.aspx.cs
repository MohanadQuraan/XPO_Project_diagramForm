using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XPO_Project;
using XPO_Project.Database;

namespace MainProject {
    public partial class _Default : System.Web.UI.Page {

        Session session1;
        protected void Page_Load(object sender, EventArgs e) {

            const string connectionString = "Data Source = mohanad; Initial Catalog = RegisterationVersion2.4; Integrated Security = True; Encrypt = False;";
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);

            session1 = new Session();


            XPCollection<SETUP_MTS_QUESTION> Questions = new XPCollection<SETUP_MTS_QUESTION>(session1);
            Questions.Load();




            //SETUP_MTS_QUESTION question = new SETUP_MTS_QUESTION(session1);
            //question=(SETUP_MTS_QUESTION)session1.GetObjectByKey(typeof(SETUP_MTS_QUESTION), "hi");
            //question.LOVType = "testo2";
            //session1.Save(question);

            
        }
    }
}
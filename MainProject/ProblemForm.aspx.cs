using DevExpress.Web;
using DevExpress.Web.Internal;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XPO_Project.Database;

namespace MainProject
{
    public partial class ProblemForm : System.Web.UI.Page
    {


        Session session1;
        XPCollection<SETUP_MTS_PROPLEM> Problems;

        protected void Page_Load(object sender, EventArgs e)
        {


            const string connectionString = "Data Source = mohanad; Initial Catalog = RegisterationVersion2.4; Integrated Security = True; Encrypt = False;";
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            session1 = new Session();

            BindGridViews();

            if (!IsPostBack)
            {
                HiddenOnUpdate.Value = "false";
            }
        }

        protected void cp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] arguments = e.Parameter.Split('|');
            string command = arguments[0];
            if (command == "OnCustomDeleteButtonClick")
            {
                string ProblemId = arguments[1];

                var ProblemToRemove = (SETUP_MTS_PROPLEM)session1.GetObjectByKey(typeof(SETUP_MTS_PROPLEM), ProblemId);

                if (ProblemToRemove != null)
                {
                    XPCollection<SETUP_MTS_GROUP> groups = new XPCollection<SETUP_MTS_GROUP>(session1);
                    XPCollection<SETUP_MTS_GROUP_QUESTION> groupsQuestions = new XPCollection<SETUP_MTS_GROUP_QUESTION>(session1);

                    foreach (SETUP_MTS_GROUP g in groups.Where(x => x.ProblemID == ProblemToRemove.ID))
                    {

                        foreach (SETUP_MTS_GROUP_QUESTION gq in groupsQuestions.Where(x => x.GroupID == g.ID).ToList())
                        {
                            gq.DELETED_BY = "mohanad";
                            gq.DELETE_DATETIME = DateTime.Now;
                           // session1.Delete(gq);
                        }

                        g.DELETED_BY = "mohanad";
                        g.DELETE_DATETIME=DateTime.Now;
                        //session1.Delete(g);
                    }
                    ProblemToRemove.DELETED_BY = "mohanad";
                    ProblemToRemove.DELETE_DATETIME= DateTime.Now;
                    //session1.Delete(ProblemToRemove);
                    session1.Save(groups);
                    session1.Save(groupsQuestions);
                    session1.Save(ProblemToRemove);
                    BindGridViews();

                }
                resetFields();

            }

            else if (command == "RowDoubleClicked")
            {
                string ProblemId = arguments[1];
                OpenProblemPage(ProblemId);

            }
            else if (command == "OnCustomEnterButtonClick")
            {
                string ProblemId = arguments[1];
                ASPxWebControl.RedirectOnCallback("~/ProblemGroupForm.aspx?Id=" + ProblemId);

            }




            switch (e.Parameter)
            {
                case "ButtonSave_clicked":
                    if (HiddenOnUpdate.Value == "false")
                    {
                        InsertNewProblem();

                    }
                    else
                    {
                        var problemToUpdate = (SETUP_MTS_PROPLEM)session1.GetObjectByKey(typeof(SETUP_MTS_PROPLEM), hiddenProblemId.Value.ToString());

                        problemToUpdate.ProblemDescription = textProblemDescribtion.Text;
                        problemToUpdate.ProblemName = textProblemName.Text;
                        problemToUpdate.UPDATED_BY = "admin2";
                        problemToUpdate.UPDATE_DATETIME = DateTime.Now;
                        session1.Save(problemToUpdate);
                    }

                    BindGridViews();


                    break;

                case "ButtonReset_clicked":
                    resetFields();
                    break;

            }

        }
        protected void resetFields()
        {
            HiddenOnUpdate.Value = "false";
            textProblemDescribtion.Value = "";
            textProblemName.Value = "";
            gridProblems.Selection.UnselectAll();
            gridProblems.FocusedRowIndex = -1;
        }


        protected void BindGridViews()
        {
            Problems= new XPCollection<SETUP_MTS_PROPLEM>(session1);
            Problems.Load();

            gridProblems.DataSource = Problems.Where(p=>string.IsNullOrEmpty(p.DELETED_BY));
            gridProblems.DataBind();


        }

        protected void InsertNewProblem()
        {
            int newId;
            if (Problems != null && Problems.Count != 0)
            {


                string id = Problems.LastOrDefault().ID;



                int index = 0;
                while (index < id.Length && !char.IsDigit(id[index]))
                {
                    index++;
                }
                id = id.Substring(index);
                newId = int.Parse(id) + 1;
            }
            else
            {
                newId = 1;
            }
            SETUP_MTS_PROPLEM problem = new SETUP_MTS_PROPLEM(session1)
            {
                ID = "p" + newId,
                ProblemName = textProblemName.Text,
                ProblemDescription = textProblemDescribtion.Text,
                CREATED_BY = "Admin",
                CREATION_DATETIME = DateTime.Now,

            };
            session1.Save(problem);
            BindGridViews();





        }

        protected void OpenProblemPage(string id)
        {
            SETUP_MTS_PROPLEM problem = Problems.FirstOrDefault(x => x.ID == id);
            textProblemName.Value = problem.ProblemName;
            textProblemDescribtion.Value = problem.ProblemDescription;
            hiddenProblemId.Value = problem.ID;
            HiddenOnUpdate.Value = "true";
        }

    }
}
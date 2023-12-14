using DevExpress.Web;
using DevExpress.Xpo;
using DevExpress.XtraEditors.Filtering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XPO_Project.Database;

namespace MainProject
{
    public partial class ProblemGroupForm : System.Web.UI.Page
    {

        Session session1;
        XPCollection<SETUP_MTS_GROUP> groups;

        XPCollection<SETUP_MTS_GROUP> groupsToBindToTable;

        string ProblemIdFromRequest;
        protected void Page_Load(object sender, EventArgs e)
        {
            const string connectionString = "Data Source = mohanad; Initial Catalog = RegisterationVersion2.4; Integrated Security = True; Encrypt = False;";
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            session1 = new Session();

            ProblemIdFromRequest = Request.QueryString["Id"];

            if (!IsPostBack)
            {
                HiddenOnUpdate.Value = "false";

            }
            BindGridviews();
        }

        protected void cp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] arguments = e.Parameter.Split('|');
            string command = arguments[0];

            if (command == "OnCustomDeleteButtonClick")
            {
                string GroupId = arguments[1];
                var GroupToRemove = (SETUP_MTS_GROUP)session1.GetObjectByKey(typeof(SETUP_MTS_GROUP), GroupId);

                if (GroupToRemove != null)
                {
                    XPCollection<SETUP_MTS_GROUP_QUESTION> groupQuestions = new XPCollection<SETUP_MTS_GROUP_QUESTION>(session1);


                    foreach (SETUP_MTS_GROUP_QUESTION gq in groupQuestions.Where(x => x.GroupID == GroupToRemove.ID).ToList())
                    {
                        gq.DELETED_BY = "mohanad";
                        gq.DELETE_DATETIME = DateTime.Now;
                    }
                    GroupToRemove.DELETED_BY = "mohanad";
                    GroupToRemove.DELETE_DATETIME = DateTime.Now;

                    session1.Save(groupQuestions);
                    session1.Save(GroupToRemove);

                    BindGridviews();
                }
                resetFields();

            }
            else if (command == "OnCustomEnterButtonClick")
            {
                string GroupId = arguments[1];
                ASPxWebControl.RedirectOnCallback("~/QuestionGroupForm.aspx?Id=" + GroupId);
            }

            else if (command == "RowDoubleClicked")
            {
                string ProblemGroupId = arguments[1];
                OpenProblemGroupPage(ProblemGroupId);
            }

            switch (e.Parameter)
            {
                case "ButtonSave_clicked":

                    if (HiddenOnUpdate.Value == "false")
                    {
                        InsertNewGroup();
                    }

                    else
                    {
                        var problemGroupToUpdate = (SETUP_MTS_GROUP)session1.GetObjectByKey(typeof(SETUP_MTS_GROUP), hiddenProblemGroupId.Value.ToString());

                        int? prevOrder = problemGroupToUpdate.GroupOrder;
                        int? spinReading = int.Parse(spinGroupOrder.Text);

                        problemGroupToUpdate.GroupName = textGroupName.Text;
                        problemGroupToUpdate.GroupDescription = textGroupDescription.Text;
                        problemGroupToUpdate.GroupOrder = int.Parse(spinGroupOrder.Text);
                        problemGroupToUpdate.GroupYesResult = selectGroupYesResult.SelectedItem != null ? int.Parse(selectGroupYesResult.SelectedItem.Value.ToString()) : 0;
                        problemGroupToUpdate.GroupNoResult = selectGroupNoResult.SelectedItem != null ? int.Parse(selectGroupNoResult.SelectedItem.Value.ToString()) : 0;
                        problemGroupToUpdate.UPDATED_BY = "admin2";
                        problemGroupToUpdate.UPDATE_DATETIME = DateTime.Now;

                        foreach (SETUP_MTS_GROUP g in groups.Where(x => x.ProblemID == problemGroupToUpdate.ProblemID && x.GroupOrder >= problemGroupToUpdate.GroupOrder && x != problemGroupToUpdate &&
                        prevOrder != spinReading).ToList())
                        {
                            g.GroupOrder = g.GroupOrder + 1;
                        }
                        session1.Save(groups);

                    }

                    BindGridviews();

                    break;

                case "buttonBack_clicked":

                    ASPxWebControl.RedirectOnCallback("~/ProblemForm.aspx");

                    break;


                case "ButtonReset_clicked":
                    resetFields();

                    break;
            }
        }
        protected void resetFields()
        {
            HiddenOnUpdate.Value = "false";
            textGroupName.Value = "";
            textGroupDescription.Value = "";
            spinGroupOrder.Value = 1;
            selectGroupYesResult.Value = null;
            selectGroupNoResult.Value = null;
            gridProblemGroups.Selection.UnselectAll();
            gridProblemGroups.FocusedRowIndex = -1;
        }


        protected void BindGridviews()
        {
            groups = new XPCollection<SETUP_MTS_GROUP>(session1);
            groups.Load();

            gridProblemGroups.DataSource = groups.Where(g => g.ProblemID == ProblemIdFromRequest && string.IsNullOrEmpty(g.DELETED_BY)).ToList();
            gridProblemGroups.DataBind();
        }
        protected void InsertNewGroup()
        {
            int newId;
            if (groups != null && groups.Count != 0)
            {
                string id = groups.LastOrDefault().ID;

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
            SETUP_MTS_GROUP group = new SETUP_MTS_GROUP(session1)
            {
                ID = "g" + newId,
                ProblemID = ProblemIdFromRequest,
                GroupName = textGroupName.Text,
                GroupDescription = textGroupDescription.Text,
                GroupOrder = int.Parse(spinGroupOrder.Text),
                GroupYesResult = selectGroupYesResult.SelectedItem != null ? int.Parse(selectGroupYesResult.SelectedItem.Value.ToString()) : 0,
                GroupNoResult = selectGroupNoResult.SelectedItem != null ? int.Parse(selectGroupNoResult.SelectedItem.Value.ToString()) : 0,
                CREATED_BY = "Admin",
                CREATION_DATETIME = DateTime.Now,

            };

            foreach (SETUP_MTS_GROUP g in groups.Where(x => x.ProblemID == group.ProblemID && x.GroupOrder >= group.GroupOrder))
            {

                g.GroupOrder = g.GroupOrder + 1;
            }

            session1.Save(groups);
            session1.Save(group);
            BindGridviews();
        }
        protected void OpenProblemGroupPage(string id)
        {
            SETUP_MTS_GROUP problemGroup = groups.FirstOrDefault(x => x.ID == id);

            textGroupName.Value = problemGroup.GroupName;
            textGroupDescription.Value = problemGroup.GroupDescription;
            spinGroupOrder.Value = problemGroup.GroupOrder;
            selectGroupYesResult.Value = problemGroup.GroupYesResult.ToString();
            selectGroupNoResult.Value = problemGroup.GroupNoResult.ToString();
            HiddenOnUpdate.Value = "true";
            hiddenProblemGroupId.Value = problemGroup.ID;

        }

    }
}
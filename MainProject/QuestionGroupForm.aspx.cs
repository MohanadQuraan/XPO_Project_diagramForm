using DevExpress.Web;
using DevExpress.Xpo;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XPO_Project.Database;
using XPO_Project;
namespace MainProject
{
    public partial class QuestionGroupForm : System.Web.UI.Page
    {
        Session session1;

        XPCollection<SETUP_MTS_GROUP_QUESTION> groupQuestions;

        List<SETUP_MTS_GROUP_QUESTION> groupQuestionsToBindToTable;


        string GroupIdFromRequest;
        protected void Page_Load(object sender, EventArgs e)
        {
            const string connectionString = "Data Source = mohanad; Initial Catalog = RegisterationVersion2.4; Integrated Security = True; Encrypt = False;";
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            session1 = new Session();

            GroupIdFromRequest = Request.QueryString["Id"];


            if (!IsPostBack)
            {
                HiddenOnUpdate.Value = "false";

            }
            bindGridviews();

        }

        protected void cp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {

            string[] arguments = e.Parameter.Split('|');
            string command = arguments[0];
            if (command == "OnCustomDeleteButtonClick")
            {
                string GroupQuestionId = arguments[1];

                var GroupQuestionToRemove = (SETUP_MTS_GROUP_QUESTION)session1.GetObjectByKey(typeof(SETUP_MTS_GROUP_QUESTION), GroupQuestionId);

                if (GroupQuestionToRemove != null)
                {
                    GroupQuestionToRemove.DELETED_BY = "mohanad";
                    GroupQuestionToRemove.DELETE_DATETIME = DateTime.Now;
                    session1.Save(GroupQuestionToRemove);

                    bindGridviews();

                }
                resetFields();

            }

            else if (command == "RowDoubleClicked")
            {
                string QuestionGroupId = arguments[1];
                OpenQuestionGroupPage(QuestionGroupId);

            }

            switch (e.Parameter)
            {
                case "ButtonSave_clicked":

                    if (HiddenOnUpdate.Value == "false")
                    {
                        InsertNewGroupQuestion();

                    }
                    else
                    {
                        var questionGroupToUpdate = (SETUP_MTS_GROUP_QUESTION)session1.GetObjectByKey(typeof(SETUP_MTS_GROUP_QUESTION), hiddenQuestionGroupId.Value.ToString());

                        int? prevOrder = questionGroupToUpdate.QuestionOrder;
                        int? spinReading = int.Parse(spinQuestionGroupOrder.Text);
                        questionGroupToUpdate.QuestionOrder = int.Parse(spinQuestionGroupOrder.Text);
                        questionGroupToUpdate.QuestionID = selectQuestions.SelectedItem != null ? selectQuestions.SelectedItem.Value.ToString() : "";
                        questionGroupToUpdate.UPDATED_BY = "admin2";
                        questionGroupToUpdate.UPDATE_DATETIME = DateTime.Now;


                        foreach (SETUP_MTS_GROUP_QUESTION q in groupQuestions.Where(x => x.GroupID == questionGroupToUpdate.GroupID && x.QuestionOrder >= questionGroupToUpdate.QuestionOrder &&
                       prevOrder != spinReading))
                        {
                            q.QuestionOrder = q.QuestionOrder + 1;
                        }



                        session1.Save(groupQuestions);
                    }
                    bindGridviews();

                    break;

                case "buttonBack_clicked":
                    var group = (SETUP_MTS_GROUP)session1.GetObjectByKey(typeof(SETUP_MTS_GROUP), GroupIdFromRequest);

                    if (group != null)
                    {
                        ASPxWebControl.RedirectOnCallback("~/ProblemGroupForm.aspx?Id=" + group.ProblemID);

                    }
                    else
                    {
                        ASPxWebControl.RedirectOnCallback("~/ProblemGroupForm.aspx");

                    }


                    break;


                case "ButtonReset_clicked":
                    resetFields();
                    break;

            }

        }
        protected void resetFields()
        {
            HiddenOnUpdate.Value = "false";
            spinQuestionGroupOrder.Value = 1;
            selectQuestions.Value = null;
            gridGroupQuestions.Selection.UnselectAll();
            gridGroupQuestions.FocusedRowIndex = -1;
        }
        protected void OpenQuestionGroupPage(string id)
        {
            SETUP_MTS_GROUP_QUESTION questionGroup = groupQuestions.FirstOrDefault(x => x.ID == id);

            spinQuestionGroupOrder.Value = questionGroup.QuestionOrder;
            selectQuestions.Value = questionGroup.QuestionID.ToString();
            HiddenOnUpdate.Value = "true";
            hiddenQuestionGroupId.Value = questionGroup.ID;

        }
        protected void bindGridviews()
        {
            XPCollection<SETUP_MTS_QUESTION> Questions = new XPCollection<SETUP_MTS_QUESTION>(session1);

            groupQuestions = new XPCollection<SETUP_MTS_GROUP_QUESTION>(session1);

            var results = SprocHelper.ExecGetGroupQuestionsIntoObjects(session1);

            results = results.Where(gq => gq.GroupID == GroupIdFromRequest && string.IsNullOrEmpty(gq.DELETED_BY)).ToList();
            gridGroupQuestions.DataSource = results;
            gridGroupQuestions.DataBind();

            selectQuestions.DataSource = Questions.Select(x => x.ID).ToList();
            selectQuestions.DataBind();
        }

        protected void InsertNewGroupQuestion()
        {
            int newId;
            if (groupQuestions != null && groupQuestions.Count != 0)
            {
                string id = groupQuestions.LastOrDefault().ID;
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
            SETUP_MTS_GROUP_QUESTION groupQuestion = new SETUP_MTS_GROUP_QUESTION(session1)
            {
                ID = "qg" + newId,
                GroupID = GroupIdFromRequest,
                QuestionOrder = int.Parse(spinQuestionGroupOrder.Text),
                QuestionID = selectQuestions.SelectedItem != null ? selectQuestions.SelectedItem.Value.ToString() : "",
                CREATED_BY = "admin",
                CREATION_DATETIME = DateTime.Now
            };

            foreach (SETUP_MTS_GROUP_QUESTION q in groupQuestions.Where(x => x.GroupID == groupQuestion.GroupID && x.QuestionOrder >= groupQuestion.QuestionOrder))
            {
                q.QuestionOrder = q.QuestionOrder + 1;
            }
            session1.Save(groupQuestions);
            session1.Save(groupQuestion);
            bindGridviews();

        }

    }
}
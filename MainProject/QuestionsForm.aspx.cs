
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XPO_Project.Database;

namespace MainProject
{

    public partial class QuestionsForm : System.Web.UI.Page
    {

        Session session1;
        XPCollection<SETUP_MTS_QUESTION> Questions;

        protected void Page_Load(object sender, EventArgs e)
        {

            const string connectionString = "Data Source = mohanad; Initial Catalog = RegisterationVersion2.4; Integrated Security = True; Encrypt = False;";
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            session1 = new Session();

            BindGridviews();
            if (!IsPostBack)
            {
                HiddenOnUpdate.Value = "false";
            }
        }

        protected void cp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] arguments = e.Parameter.Split('|');
            string command = arguments[0];
            if (command == "OnCustomButtonClick")
            {
                string QuestionId = arguments[1];
                var QuestionToRemove = (SETUP_MTS_QUESTION)session1.GetObjectByKey(typeof(SETUP_MTS_QUESTION), QuestionId); 
                if (QuestionToRemove != null)
                {
                    QuestionToRemove.DELETED_BY = "mohanad";
                    QuestionToRemove.DELETE_DATETIME = DateTime.Now;

                    session1.Save(QuestionToRemove);
                    BindGridviews();


                }
                ResetFields();

            }

            else if (command == "RowDoubleClicked")
            {
                string QuestionId = arguments[1];
                OpenQuestionPage(QuestionId);

            }


            switch (e.Parameter)
            {
                case "ButtonSave_clicked":
                    if (HiddenOnUpdate.Value == "false")
                    {
                        InsertNewQuestion();
                    }
                    else
                    {
                        SETUP_MTS_QUESTION questionToUpdate = (SETUP_MTS_QUESTION)session1.GetObjectByKey(typeof(SETUP_MTS_QUESTION), hiddenQuestionId.Value.ToString());

                        questionToUpdate.QuestionText = textQuestionText.Text;
                        questionToUpdate.LOVType = textLOVType.Text;
                        questionToUpdate.AnswerObject = selectAnswerObject.SelectedItem != null ? int.Parse(selectAnswerObject.SelectedItem.Value.ToString()) : 0;
                        questionToUpdate.AnswerAttribute = selectAnswerAttribute.SelectedItem != null ? int.Parse(selectAnswerAttribute.SelectedItem.Value.ToString()) : 0;
                        questionToUpdate.IsFreeText = checkIsFreeText.Checked;
                        questionToUpdate.AnswerYesValueCriteria = textAnswerYesValueCriteria.Text;
                        questionToUpdate.UPDATED_BY = "admin2";
                        questionToUpdate.UPDATE_DATETIME = DateTime.Now;
                        session1.Save(questionToUpdate);

                    }
                    BindGridviews();


                    break;


                case "ButtonReset_clicked":
                   
                    ResetFields();
                    break;




            }
        }

        protected void ResetFields()
        {
            HiddenOnUpdate.Value = "false";
            textQuestionText.Value = "";
            textLOVType.Value = "";
            selectAnswerObject.Value = null;
            selectAnswerAttribute.Value = null;
            checkIsFreeText.Checked = false;
            textAnswerYesValueCriteria.Value = "";
            gridQuestions.Selection.UnselectAll();
            gridQuestions.FocusedRowIndex = -1;
        }
        protected void BindGridviews()
        {
            Questions = new XPCollection<SETUP_MTS_QUESTION>(session1);
            Questions.Load();

            gridQuestions.DataSource = Questions.Where(q=>string.IsNullOrEmpty(q.DELETED_BY));
            gridQuestions.DataBind();

        }

        protected void OpenQuestionPage(string id)
        {
            SETUP_MTS_QUESTION question = (SETUP_MTS_QUESTION)session1.GetObjectByKey(typeof(SETUP_MTS_QUESTION), id);

            textQuestionText.Value = question.QuestionText;
            textLOVType.Value = question.LOVType;
            selectAnswerObject.Value = question.AnswerObject.ToString();
            selectAnswerAttribute.Value = question.AnswerAttribute.ToString();
            checkIsFreeText.Value = question.IsFreeText;
            textAnswerYesValueCriteria.Value = question.AnswerYesValueCriteria;
            HiddenOnUpdate.Value = "true";
            hiddenQuestionId.Value = question.ID;
        }

        protected void InsertNewQuestion()
        {
            int newId;
            if (Questions != null && Questions.Count != 0)
            {

                string id = Questions.LastOrDefault().ID;
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
            SETUP_MTS_QUESTION question = new SETUP_MTS_QUESTION(session1)
            {
                ID = "q" + newId,
                QuestionText = textQuestionText.Text,
                LOVType = textLOVType.Text,
                AnswerObject = selectAnswerObject.SelectedItem != null ? int.Parse(selectAnswerObject.SelectedItem.Value.ToString()) : 0,
                AnswerAttribute = selectAnswerAttribute.SelectedItem != null ? int.Parse(selectAnswerAttribute.SelectedItem.Value.ToString()) : 0,
                IsFreeText = checkIsFreeText.Checked,
                AnswerYesValueCriteria = textAnswerYesValueCriteria.Text,
                CREATED_BY = "mohanad",
                CREATION_DATETIME = DateTime.Now

            };
            session1.Save(question);
            BindGridviews();


        }

    }
}
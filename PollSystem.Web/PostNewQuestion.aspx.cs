using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PollSystem.Web.DataTransferObjects;
using PollSystem.OpenAccess.Identity.DalHelpers;

namespace PollSystem.Web
{
    public partial class PostNewQuestion : BasePage
    {
        private readonly string ANSWERS_TOKEN_IN_VIEW_STATE = "GridViewAnswers";

        private List<AnswerByPostQuestionDto> Answers;
        protected void Page_Prerender(object sender, EventArgs e)
        {
            this.RedirectUnauthorizedUsers();
            this.Answers = new List<AnswerByPostQuestionDto>();

            if (!this.IsPostBack)
            {
                this.Answers.Add(new AnswerByPostQuestionDto() { Text = string.Empty });
            }

            if (ViewState[ANSWERS_TOKEN_IN_VIEW_STATE] != null)
            {
                var answers = (List<AnswerByPostQuestionDto>)ViewState[ANSWERS_TOKEN_IN_VIEW_STATE];
                this.Answers = answers;
            }

            ViewState[ANSWERS_TOKEN_IN_VIEW_STATE] = this.Answers;
            this.GridViewAnswers.DataSource = this.Answers;
            this.GridViewAnswers.DataBind();
        }

        protected void ButtonAddNewRow_Click(object sender, EventArgs e)
        {
            var answers = GetAnswerDTOsFromViewState();

            answers.Add(new AnswerByPostQuestionDto() { Text = string.Empty });
            ViewState[ANSWERS_TOKEN_IN_VIEW_STATE] = answers;
        }

        protected void ButtonSubmitQuestion_Click(object sender, EventArgs e)
        {
            ThrowExceptionIf(this.User == null, new ArgumentNullException("Unknown user"));

            var questionQueryText = this.TextBoxQueryText.Text;

            var questionAnswers = this.GetAnswerDTOsFromViewState();
            var validQuestionTexts = questionAnswers.Where(x => !string.IsNullOrEmpty(x.Text)).Select(x=>x.Text).ToArray();

            var questionBuilder = new QuestionBuilder();
            var question = questionBuilder.WithQueryText(questionQueryText)
                .AddMultipleAnswers(validQuestionTexts)
                .WithUser(this.CurrentUser)
                .BuildQuestion();

            this.DbContext.Add(question);
            this.DbContext.SaveChanges();

            string newlyCreateQuestionPageUrl = string.Format("~/ShowVotingResults?questionId={0}", question.Id);
            Response.Redirect(newlyCreateQuestionPageUrl);
        }

        protected void GridViewAnswers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var indexToBeRemoved = Convert.ToInt32(e.CommandArgument);
            var currectlyAvaiableAnsweers = GetAnswerDTOsFromViewState();
            var actualAnswers = currectlyAvaiableAnsweers.SkipElementAtIndexes(indexToBeRemoved).ToList();
            ViewState[ANSWERS_TOKEN_IN_VIEW_STATE] = actualAnswers;
        }

        private IList<AnswerByPostQuestionDto> GetAnswerDTOsFromViewState()
        {
            var answers = new List<AnswerByPostQuestionDto>();

            foreach (GridViewRow row in this.GridViewAnswers.Rows)
            {
                var textBoxAnswerText = row.FindControl("TextBoxAnswerText") as TextBox;
                if (textBoxAnswerText != null)
                {
                    var currectAnswer = new AnswerByPostQuestionDto();
                    currectAnswer.Text = textBoxAnswerText.Text;
                    answers.Add(currectAnswer);
                }
            }

            return answers;
        }
    }
}
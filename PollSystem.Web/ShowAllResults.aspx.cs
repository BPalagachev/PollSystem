using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;

namespace PollSystem.Web
{
    public partial class ShowAllResults : BasePage
    {
        private const int pageSize = 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            RedirectUnauthorizedUsers();

            this.RepeaterAnswers.DataSource = null;
            this.RepeaterAnswers.DataBind();
        }

        protected void Page_Prerender(object sender, EventArgs e)
       {
           this.GridViewQuestions.PageSize = pageSize;
           
            if (!this.IsPostBack)
           {
               this.GridViewQuestions_PageIndexChanging(this, new GridViewPageEventArgs(0));  
           }
        }

        protected void GridViewQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string questionId = this.GridViewQuestions.SelectedDataKey.Value.ToString();
            var answers = this.DbContext.Answers.Where(x => x.QuestionId == questionId).ToList();

            this.LabelQuestion.Text = this.DbContext.Questions.First(x => x.Id == questionId).QueryText;
            this.RepeaterAnswers.DataSource = answers;
            this.RepeaterAnswers.DataBind();
        }

        protected void GridViewQuestions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var currentPage = e.NewPageIndex;
            var questions = this.DbContext
                .Questions
                .Include(x => x.PostedBy)
                .Skip(pageSize*currentPage)
                .Take(pageSize * currentPage + pageSize)
                .ToList();
            this.GridViewQuestions.DataSource = questions;

            this.GridViewQuestions.PageIndex = e.NewPageIndex;
            this.GridViewQuestions.VirtualItemCount = this.DbContext.Questions.Count();

            this.DataBind();
        }
    }
}
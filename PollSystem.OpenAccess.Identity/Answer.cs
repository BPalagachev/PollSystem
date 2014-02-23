using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PollSystem.OpenAccess.Identity
{
    public class Answer
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string AnswerText { get; set; }

        public string QuestionId { get; set; }

        public Question Question { get; set; }

        public int Votes { get; set; }
    }
}

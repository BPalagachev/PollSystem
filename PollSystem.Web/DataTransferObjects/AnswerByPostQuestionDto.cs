using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollSystem.Web.DataTransferObjects
{
    [Serializable]
    public class AnswerByPostQuestionDto
    {
        public string Text { get; set; }
    }
}
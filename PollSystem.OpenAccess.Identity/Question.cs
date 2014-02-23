using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollSystem.OpenAccess.Identity
{
    public class Question
    {
        public Question()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Answers = new List<Answer>();
            this.UsersThatVoted = new List<IdentityUser>();
        }

        public string Id { get; set; }

        public string QueryText { get; set; }

        public string PostedById { get; set; }

        public IdentityUser PostedBy { get; set; }

        public virtual IList<IdentityUser> UsersThatVoted { get; set; }

        public virtual IList<Answer> Answers { get; set; }

    }
}

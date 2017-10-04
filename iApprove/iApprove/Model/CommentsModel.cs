using System;
namespace iApprove.Model
{
    public class CommentsModel
    {
        public CommentsModel()
        {
        }

        public string CommentsText
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string Approver
        {
            get;
            set;
        }

        public DateTime CommentedDate
        {
            get;
            set;
        }
    }
}

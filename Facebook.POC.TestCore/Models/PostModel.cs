using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.POC.TestCore.Models
{
    public class PostModel
    {
        public int PostNumber { get; set; }
        public string Postmessage { get; set; }

        public PostModel(int postNumber, string postmessage)
        {
            this.PostNumber = postNumber;
            this.Postmessage = postmessage;
        }
    }
}

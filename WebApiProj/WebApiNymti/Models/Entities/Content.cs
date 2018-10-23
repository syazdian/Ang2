using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNymti.Models.Entities
{
    public class Content
    {
        private int contentId;
        [Key]
        public int ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }

        private int groupsId;
        public int GroupsId
        {
            get { return groupsId; }
            set { groupsId = value; }
        }

        private string contentText;

        public string ContentText
        {
            get { return contentText; }
            set { contentText = value; }
        }



    }
}

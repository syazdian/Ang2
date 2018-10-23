using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNymti.Models.Entities
{
    public class Groups
    {
        private int groupsId;
        [Key]
        public int GroupsId
        {
            get { return groupsId; }
            set { groupsId = value; }
        }

        private string groupsTitle;

        [Required]
        public string GroupsTitle
        {
            get { return groupsTitle; }
            set { groupsTitle = value; }
        }

        public List<Content> Contents { get; set; }

    }
}

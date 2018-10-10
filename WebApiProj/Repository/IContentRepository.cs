using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNymti.Models.Entities;

namespace WebApiNymti.Repository
{
   public interface IContentRepository
    {

        IEnumerable<Groups> GetAllGroups();
        IEnumerable<Content> GetAllContent();
        IEnumerable<Content> GetContentByGroupId(int id);
    }
}

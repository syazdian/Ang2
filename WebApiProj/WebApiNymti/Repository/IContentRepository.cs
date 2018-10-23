using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNymti.Models.Entities;

namespace WebApiNymti.Repository
{
   public interface IContentRepository
    {

        IQueryable<Groups> GetAllGroups();
        IQueryable<Content> GetAllContent();
        IQueryable<Content> GetContentByGroupId(int id);
    }
}

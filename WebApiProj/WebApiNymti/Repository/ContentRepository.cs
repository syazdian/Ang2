using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNymti.Data;
using WebApiNymti.Models.Entities;

namespace WebApiNymti.Repository
{
    public class ContentRepository : IContentRepository
    {
        private IApplicationDbContext db;
        public ContentRepository(IApplicationDbContext _db)
        {
            db = _db;
        }

        public IQueryable<Content> GetAllContent()
        {
            return db.Contents;
        }

        public IQueryable<Groups> GetAllGroups()
        {
            return db.Groups;
        }

        public IQueryable<Content> GetContentByGroupId(int groupId)
        {
            return db.Contents.Where(c => c.GroupsId == groupId);
        }
    }
}

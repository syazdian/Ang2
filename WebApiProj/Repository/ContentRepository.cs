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
        private ApplicationDbContext db;
        public ContentRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IEnumerable<Content> GetAllContent()
        {
            return db.Contents.ToList();
        }

        public IEnumerable<Groups> GetAllGroups()
        {
            return db.Groups.ToList();
        }

        public IEnumerable<Content> GetContentByGroupId(int groupId)
        {
            return db.Contents.Where(c => c.GroupsId == groupId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNymti.Models.Entities;

namespace WebApiNymti.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Groups> Groups { get; set; }
        DbSet<WebApiNymti.Models.Entities.Content> Contents { get; set; }
    }
}

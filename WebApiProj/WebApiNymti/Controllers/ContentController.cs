using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNymti.Models.Entities;
using WebApiNymti.Repository;

namespace WebApiNymti.Controllers
{
     
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowSpecificOrigin")]
    public class ContentController : ControllerBase
    {
        IContentRepository db;
        public ContentController(IContentRepository _db)
        {
            this.db = _db;
        }

        [HttpGet]
        [Route("GetAllGroups")]
        public IActionResult GetAllGroups()
        {
            var groups = db.GetAllGroups().ToList();

            //if (groups==null)
            //        return null; 


            return Ok(groups);

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllContent()
        {
            var contents = db.GetAllContent().ToList();


            return Ok(contents);

        }
       
        [HttpGet]
        [Route("GetByGroupId/{id}")]
        public IActionResult GetByGroupId(int id = 1)
        {
            var contents = db.GetContentByGroupId(id).ToList();

            return Ok(contents);

        }


        


    }
}

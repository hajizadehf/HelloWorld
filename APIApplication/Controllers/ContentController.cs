using Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Framework;

namespace APIApplication.Controllers
{
    public class ContentController : ApiController
    {
       
        public List<Content> GetAllContents()
        {
            var contents = DBHandler.getcontents();
            return DBHandler.getcontents();
        }
        public IHttpActionResult GetContent(int id)
        {
            var content = DBHandler.getcontents().FirstOrDefault((p) => p.id == id);
            if (content == null)
            {
                return NotFound();
            }
            return Ok(content);
        }
    }
}

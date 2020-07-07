using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FakeAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private static LogHelper _loggerAPI = new LogHelper(LogHelper.LogName.APILogger);
        private static SqlHelper _sqlAdapter = new SqlHelper();

        [ApiVersion("1.0")]
        [HttpGet, Route("v{Version:apiVersion}/posts")]
        public IActionResult GetAllPosts()
        {
            DataTable dataTable = _sqlAdapter.ExecuteDataTable("select * from Post");
            return Ok();
        }

        [ApiVersion("1.0")]
        [HttpPost, Route("v{Version:apiVersion}/posts/{id}")]
        public IActionResult GetPost([FromBody] object body)
        {
            return Ok();
        }
    }
}

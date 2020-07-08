﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FakeAPI.Helpers;
using FakeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private static LogHelper _loggerAPI = new LogHelper(LogHelper.LogName.APILogger);

        public class UserInfo
        {
            public string Name { get; set; }
            public string LoginTime { get; set; }
        }

        [ApiVersion("1.0")]
        [HttpGet, Route("v{Version:apiVersion}/get")]
        public ActionResult<UserInfo> Get()
        {
            return new UserInfo()
            {
                Name = "Marcus",
                LoginTime = DateTime.Now.ToString()
            };
        }

        [ApiVersion("1.0")]
        [HttpGet, Route("v{Version:apiVersion}/posts")]
        public APIResult GetAllPosts()
        {
            var result = new APIResult();
            try
            {
                result.Data = PostModel.GetPosts();
                result.IsSucceed = true;
            }
            catch (Exception e)
            {
                PostModel.ExceptionHandler(_loggerAPI, result, e.Message);
            }
            return result;
        }

        [ApiVersion("1.0")]
        [HttpGet, Route("v{Version:apiVersion}/posts/{id}")]
        public APIResult GetPost(int id)
        {
            var result = new APIResult();
            try
            {
                result.Data = PostModel.GetPostById(id);
                result.IsSucceed = true;
            }
            catch (Exception e)
            {
                PostModel.ExceptionHandler(_loggerAPI, result, e.Message);
            }
            return result;
        }

        [ApiVersion("1.0")]
        [HttpPost, Route("v{Version:apiVersion}/posts")]
        public APIResult CreatePost([FromBody] Post reqBody)
        {
            var result = new APIResult();
            try
            {
                result.Data = PostModel.CreatePost(reqBody);
                result.IsSucceed = true;
            }
            catch (Exception e)
            {
                PostModel.ExceptionHandler(_loggerAPI, result, e.Message);
            }
            return result;
        }

        [ApiVersion("1.0")]
        [HttpPut, Route("v{Version:apiVersion}/posts/{id}")]
        public APIResult UpdatePost([FromBody] object body)
        {
            var result = new APIResult();
            return result;
        }

        [ApiVersion("1.0")]
        [HttpDelete, Route("v{Version:apiVersion}/posts/{id}")]
        public APIResult DeletePost([FromBody] object body)
        {
            var result = new APIResult();
            return result;
        }
    }
}

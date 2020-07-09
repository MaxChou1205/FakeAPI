using System;
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

        /// <summary>
        /// 取得所有Post
        /// </summary>
        /// <returns>回傳所有Post</returns>
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

        /// <summary>
        /// 取得特定Post
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <returns>回傳特定Post</returns>
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
                PostModel.CreatePost(reqBody);
                result.Data = PostModel.GetPostById(reqBody.Id);
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
        public APIResult UpdatePost([FromBody] Post reqBody, int id)
        {
            var result = new APIResult();
            try
            {
                PostModel.UpdatePost(id, reqBody);
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
        [HttpDelete, Route("v{Version:apiVersion}/posts/{id}")]
        public APIResult DeletePost(int id)
        {
            var result = new APIResult();
            try
            {
                PostModel.DeletePost(id);
                result.IsSucceed = true;
            }
            catch (Exception e)
            {
                PostModel.ExceptionHandler(_loggerAPI, result, e.Message);
            }
            return result;
        }
    }
}

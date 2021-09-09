﻿using DMS.Auth;
using DMS.Auth.Tickets;
using DMS.Redis;
using DMS.Sample31.Contracts;
using DMSN.Common.BaseResult;
using DMSN.Common.Helper;
using DMSN.Common.JsonHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Sample31.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IUserAuth userAuth;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="userAuth"></param>

        public ValuesController(IProductService productService, IUserAuth userAuth)
        {
            this.productService = productService;
            this.userAuth = userAuth;
        }

        #region redis
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestRedis0")]
        public async Task TestRedis0(string msg = "我应该在0库")
        {
            await productService.TestRedis0(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestRedis1")]
        public async Task TestRedis1(string msg = "我应该在1库")
        {
            await productService.TestRedis1(msg);
        }
        #endregion

        #region Subscribe&Publish
        /// <summary>
        /// 订阅
        /// </summary>
        /// <returns></returns>
        [HttpGet("Subscribe")]
        public void Subscribe()
        {
            productService.Subscribe();
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <returns></returns>
        [HttpGet("Publish")]
        public void Publish()
        {
            productService.Publish();
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <returns></returns>
        [HttpGet("RedisPublish")]
        public void RedisPublish()
        {
            productService.RedisPublish();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckAuth")]
        [TypeFilter(typeof(CheckLoginAttribute))]
        public async Task<ResponseResult> CheckAuth()
        {
            var ip = IPHelper.GetCurrentIp();
            var (loginFlag, result) = await userAuth.ChenkLoginAsync();
            if (!loginFlag)
            {
                return result;
            }
            var id = userAuth.ID;
            var name = userAuth.Name;
            return new ResponseResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [HttpGet("GetProduct")]
        public async Task<ResponseResult<UserEntity>> GetProductAsync(long productID)
        {
            var ip = IPHelper.GetCurrentIp();
            return await productService.GetProductAsync(productID);
        }

    }
}

using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabratorController : ControllerBase
    {
        private readonly ICollabarateRL iCollabarateBL;

        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;

        private readonly FundooContext fundooContext;

        public CollabratorController(ICollabarateRL iCollabarateBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iCollabarateBL=iCollabarateBL;
            this.memoryCache=memoryCache;
            this.distributedCache=distributedCache;
            this.fundooContext=fundooContext;
        }


        [Authorize]
        [HttpPost]
        [Route("Addemail")]
        public IActionResult CreateNotes(string Email,long noteId)
        {
            try
            {
                //var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iCollabarateBL.AddCollabrate(Email, noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Created notes successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Created notes unsuccessful"
                    });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        [Authorize]
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult retrieveCollaborate(long noteid)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iCollabarateBL.retrieveCollaborate(noteid,userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Retrieving email successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Retrieving  email unsuccessful"
                    });
                }
            }
            catch (System.Exception) { throw; }
        }

        [Authorize]
        [HttpGet]
        [Route("Delete")]

        public IActionResult DeleteCollabarator(long Collabratorid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iCollabarateBL.DeleteCollabarator(Collabratorid);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Delete Successful" });
                }
                else
                {
                    return BadRequest(new {success = false, message="Delete Unsuccessful"});
                }
            }
            catch(System.Exception) { throw; }
        }


        [Authorize]
        [HttpGet]
        [Route("redis")]

        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "CollabratorList";
            string serializedCollabratorList;
            var CollabratorList = new List<CollabratorEntity>();
            var redisCollabratorList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabratorList != null)
            {
                serializedCollabratorList = Encoding.UTF8.GetString(redisCollabratorList);
                CollabratorList = JsonConvert.DeserializeObject<List<CollabratorEntity>>(serializedCollabratorList);
            }
            else
            {
                CollabratorList = fundooContext.CollabratorTable.ToList();
                serializedCollabratorList = JsonConvert.SerializeObject(CollabratorList);
                redisCollabratorList = Encoding.UTF8.GetBytes(serializedCollabratorList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabratorList, options);
            }
            return Ok(CollabratorList);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
    public class LabelController : ControllerBase
    {
        private readonly ILabelRL iLabelBL;

        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;

        private readonly ILogger<LabelController> _logger;

        private readonly FundooContext fundooContext;
        public LabelController(ILabelRL iLabelBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundoocontext, ILogger<LabelController> logger)
        {
            this.iLabelBL=iLabelBL;
            this.memoryCache=memoryCache;
            this.distributedCache=distributedCache;
            this.fundooContext=fundoocontext;
            _logger=logger;
        }

        [Authorize]
        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(long noteId, string labelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iLabelBL.AddLabel(noteId,userId,labelName);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Created label successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Created label unsuccessful"
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
        public IActionResult RetrieveLabel(long labelId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iLabelBL.RetrieveLabel(labelId );
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

        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                var result = iLabelBL.DeleteLabel(labelId);

                if(result != null)
                {
                    return Ok(new { success = true, message = "Label Deleted Succesfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label deleted Unsuccessful" });
                }
            }
            catch(System.Exception) { throw; }
        }

        [Authorize]
        [HttpPut]
        [Route("Edit")]

        public IActionResult EditLabel(long noteId,long labelId, string labelName)
        {
            try
            {
                var result = iLabelBL.EditLabel(noteId,labelId, labelName);

                if( result != null)
                {
                    return Ok(new { success = true, message = "Label Updated Successful", data = result }); 
                }
                else
                {
                    return Ok(new { success = false, message = "Label Updated Unsuccessful" });
                }
            }
            catch(System.Exception ) { throw; }
        }

        [Authorize]
        [HttpGet]
        [Route("redis")]

        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = fundooContext.LabelTable.ToList();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }


    }
}

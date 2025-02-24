﻿using CommonLayer.Model;
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
    public class NotesController : ControllerBase
    {
        private readonly INotesRL iNotesBL;
        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;

        private readonly FundooContext fundooContext;

        private readonly ILogger<NotesController> _logger;

        public NotesController(INotesRL iNotesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundoocontext, ILogger<NotesController> logger)
        {
            this.iNotesBL=iNotesBL;
            this.memoryCache=memoryCache;
            this.distributedCache=distributedCache;
            this.fundooContext=fundoocontext;
            _logger=logger;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateNotes")]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Email").Value);
                var result = iNotesBL.createNotes(notesModel, userId);
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
        public IActionResult retrieveNotes(long noteid)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                
                var result = iNotesBL.retrieveNotes(userId,noteid);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Retrieving notes successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Retrieving unsuccessful"
                    });
                }
            }
            catch (System.Exception){throw;}
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteNotesId(long noteid)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iNotesBL.DeleteNotesId(noteid);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Deleting notes successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Deleting unsuccessful"
                    });
                }
            }
            catch (System.Exception) { throw; }
        }


        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNote(long noteId, NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.UpdateNote(userId, noteId, notesModel);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Updating data Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Updating data UnSuccessful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Pin")]

        public IActionResult PinNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x=>x.Type == "UserId").Value);
                var result = iNotesBL.PinNotes(noteId, userId);

                if(result!=null)
                {
                    return Ok(new { success = true, message = "Pinned Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Pinned Unsuccesful" });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Archieve")]

        public IActionResult Archieve(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iNotesBL.Archieve(noteId, userId);

                if (result!=null)
                {
                    return Ok(new { success = true, message = "Archieved Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Archieved Unsuccesful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Trash")]

        public IActionResult Trash(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iNotesBL.Trash(noteId, userId);

                if (result!=null)
                {
                    return Ok(new { success = true, message = "Trashed Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Trashed Unsuccesful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [Authorize]
        [HttpPut]
        [Route("BgColor")]
        public IActionResult BgColor(long noteId,string backgroundColor, NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.BgColor(userId, noteId,backgroundColor, notesModel);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Bg color data Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Bg color data UnSuccessful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [Authorize]
        [HttpPut]
        [Route("Imageupload")]

        public IActionResult ImageUploadNotes(IFormFile image, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x=>x.Type == "UserId").Value);
                var result = iNotesBL.ImageUploadNotes(image, noteId, userId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Image Uploaded Succesfully" });
                }
                else 
                {
                    return BadRequest(new { success = false, message = "Image Uploaded Unsuccessfully" });
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("redis")]

        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}

using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using System;
using System.Linq;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabratorController : ControllerBase
    {
        private readonly ICollabarateRL iCollabarateBL;
        public CollabratorController(ICollabarateRL iCollabarateBL)
        {
            this.iCollabarateBL=iCollabarateBL;
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
                var result = iCollabarateBL.DeleteCollabarator(Collabratorid,userId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Delete Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Delete Unsuccessful" });
                }
            }
            catch(System.Exception) { throw; }
        }

    }
}

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
    public class NotesController : ControllerBase
    {
        private readonly INotesRL iNotesBL;
        public NotesController(INotesRL iNotesBL)
        {
            this.iNotesBL=iNotesBL;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateNotes")]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
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




    }
}

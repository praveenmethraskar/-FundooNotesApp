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
    public class LabelController : ControllerBase
    {
        private readonly ILabelRL iLabelBL;
        public LabelController(ILabelRL iLabelBL)
        {
            this.iLabelBL=iLabelBL;
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


    }
}

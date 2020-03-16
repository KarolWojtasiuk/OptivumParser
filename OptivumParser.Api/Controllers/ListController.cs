using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OptivumParser.Api.Controllers
{
    /// <summary>
    /// Operations about lesson plan list.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase
    {
        /// <summary>
        /// Gets a identifier of given class.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <param name="className">Name of the class for which the identifier is to be obtained.</param>
        /// <response code="400">If the required parameter is null</response>
        /// <response code="406">If the class with the given name doesn't exist.</response>
        [HttpGet("[action]")]
        public ActionResult Class([FromQuery][Required]string planUrl, [FromQuery][Required]string className)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(ListParser.GetClass(provider, className));
                }
                catch (InvalidNameException e)
                {
                    return StatusCode(406, e.Message);
                }
                catch (UriFormatException e)
                {
                    return StatusCode(406, $"Error during parsing url: '{e.Message}'");
                }
            }
        }

        /// <summary>
        /// Gets a dictionary of class names and identifiers.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <response code="400">If the required parameter is null</response>
        [HttpGet("[action]")]
        public ActionResult Classes([FromQuery][Required]string planUrl)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(ListParser.GetClasses(provider));
                }
                catch (InvalidNameException e)
                {
                    return StatusCode(406, e.Message);
                }
                catch (UriFormatException e)
                {
                    return StatusCode(406, $"Error during parsing url: '{e.Message}'");
                }
            }
        }

        /// <summary>
        /// Gets a identifier of given teacher.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <param name="teacherName">Name of the teacher for which the identifier is to be obtained.</param>
        /// <response code="400">If the required parameter is null</response>
        /// <response code="406">If the teacher with the given name doesn't exist.</response>
        [HttpGet("[action]")]
        public ActionResult Teacher([FromQuery][Required]string planUrl, [FromQuery][Required]string teacherName)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(ListParser.GetTeacher(provider, teacherName));
                }
                catch (InvalidNameException e)
                {
                    return StatusCode(406, e.Message);
                }
                catch (UriFormatException e)
                {
                    return StatusCode(406, $"Error during parsing url: '{e.Message}'");
                }
            }
        }

        /// <summary>
        /// Gets a dictionary of teacher names and identifiers.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <response code="400">If the required parameter is null</response>
        [HttpGet("[action]")]
        public ActionResult Teachers([FromQuery][Required]string planUrl)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(ListParser.GetTeachers(provider));
                }
                catch (InvalidNameException e)
                {
                    return StatusCode(406, e.Message);
                }
                catch (UriFormatException e)
                {
                    return StatusCode(406, $"Error during parsing url: '{e.Message}'");
                }
            }
        }

        /// <summary>
        /// Gets a identifier of given room.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <param name="roomName">Name of the room for which the identifier is to be obtained.</param>
        /// <response code="400">If the required parameter is null</response>
        /// <response code="406">If the room with the given name doesn't exist.</response>
        [HttpGet("[action]")]
        public ActionResult Room([FromQuery][Required]string planUrl, [FromQuery][Required]string roomName)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(ListParser.GetRoom(provider, roomName));
                }
                catch (InvalidNameException e)
                {
                    return StatusCode(406, e.Message);
                }
                catch (UriFormatException e)
                {
                    return StatusCode(406, $"Error during parsing url: '{e.Message}'");
                }
            }
        }

        /// <summary>
        /// Gets a dictionary of room names and identifiers.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <response code="400">If the required parameter is null</response>
        [HttpGet("[action]")]
        public ActionResult Rooms([FromQuery][Required]string planUrl)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(ListParser.GetRooms(provider));
                }
                catch (InvalidNameException e)
                {
                    return StatusCode(406, e.Message);
                }
                catch (UriFormatException e)
                {
                    return StatusCode(406, $"Error during parsing url: '{e.Message}'");
                }
            }
        }

        /// <summary>
        /// Gets a dictionaries of everything names and identifiers.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <response code="400">If the required parameter is null</response>
        [HttpGet("[action]")]
        public ActionResult All([FromQuery][Required]string planUrl)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(ListParser.GetAll(provider));
                }
                catch (InvalidNameException e)
                {
                    return StatusCode(406, e.Message);
                }
                catch (UriFormatException e)
                {
                    return StatusCode(406, $"Error during parsing url: '{e.Message}'");
                }
            }
        }
    }
}

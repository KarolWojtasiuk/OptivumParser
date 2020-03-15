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
        /// Gets a dictionary of class names and identifiers.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <response code="400">If the required parameter is null</response>
        [HttpGet("classes")]
        public ActionResult Classes([FromQuery][Required]string planUrl)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                var provider = new PlanProvider(planUrl);
                return Ok(ListParser.GetClasses(provider));
            }
        }

        /// <summary>
        /// Gets a dictionary of teacher names and identifiers.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <response code="400">If the required parameter is null</response>
        [HttpGet("teachers")]
        public ActionResult Teachers([FromQuery][Required]string planUrl)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                var provider = new PlanProvider(planUrl);
                return Ok(ListParser.GetTeachers(provider));
            }
        }

        /// <summary>
        /// Gets a dictionary of room names and identifiers.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <response code="400">If the required parameter is null</response>
        [HttpGet("rooms")]
        public ActionResult Rooms([FromQuery][Required]string planUrl)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                var provider = new PlanProvider(planUrl);
                return Ok(ListParser.GetRooms(provider));
            }
        }

        /// <summary>
        /// Gets a dictionaries of everything names and identifiers.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <response code="400">If the required parameter is null</response>
        [HttpGet("all")]
        public ActionResult All([FromQuery][Required]string planUrl)
        {
            if (String.IsNullOrWhiteSpace(planUrl))
            {
                return BadRequest();
            }
            else
            {
                var provider = new PlanProvider(planUrl);
                return Ok(ListParser.GetAll(provider));
            }
        }
    }
}

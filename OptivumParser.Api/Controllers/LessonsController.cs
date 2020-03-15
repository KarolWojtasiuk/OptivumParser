using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OptivumParser.Api.Controllers
{
    /// <summary>
    /// Operations about lessons.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LessonsController : ControllerBase
    {
        /// <summary>
        /// Gets a list of lessons for the class.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <param name="classId">Id of the class for which the lessons are to be obtained.</param>
        /// <response code="400">If the required parameter is null</response>
        /// <response code="406">If an error occurs while downloading the plan. The most common cause is a bad id.</response>
        [HttpGet("[action]")]
        public ActionResult ForClass([FromQuery][Required]string planUrl, [FromQuery][Required]string classId)
        {
            if (String.IsNullOrWhiteSpace(planUrl) || String.IsNullOrWhiteSpace(classId))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(LessonParser.GetLessonsForClass(provider, classId));
                }
                catch (System.Net.WebException e)
                {
                    return StatusCode(406, $"Error during plan download: '{e.Message}'");
                }
            }
        }

        /// <summary>
        /// Gets a list of lessons for the teacher.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <param name="teacherId">Id of the teacher for which the lessons are to be obtained.</param>
        /// <response code="400">If the required parameter is null</response>
        /// <response code="406">If an error occurs while downloading the plan. The most common cause is a bad id.</response>
        [HttpGet("[action]")]
        public ActionResult ForTeacher([FromQuery][Required]string planUrl, [FromQuery][Required]string teacherId)
        {
            if (String.IsNullOrWhiteSpace(planUrl) || String.IsNullOrWhiteSpace(teacherId))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(LessonParser.GetLessonsForTeacher(provider, teacherId));
                }
                catch (System.Net.WebException e)
                {
                    return StatusCode(406, $"Error during plan download: '{e.Message}'");
                }
            }
        }

        /// <summary>
        /// Gets a list of lessons for the room.
        /// </summary>
        /// <param name="planUrl">Url to the lesson plan main page.</param>
        /// <param name="roomId">Id of the room for which the lessons are to be obtained.</param>
        /// <response code="400">If the required parameter is null</response>
        /// <response code="406">If an error occurs while downloading the plan. The most common cause is a bad id.</response>
        [HttpGet("[action]")]
        public ActionResult ForRoom([FromQuery][Required]string planUrl, [FromQuery][Required]string roomId)
        {
            if (String.IsNullOrWhiteSpace(planUrl) || String.IsNullOrWhiteSpace(roomId))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var provider = new PlanProvider(planUrl);
                    return Ok(LessonParser.GetLessonsForRoom(provider, roomId));
                }
                catch (System.Net.WebException e)
                {
                    return StatusCode(406, $"Error during plan download: '{e.Message}'");
                }
            }
        }
    }
}
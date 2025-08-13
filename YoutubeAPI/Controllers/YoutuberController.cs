using Microsoft.AspNetCore.Mvc;
using YoutubeAPI.Models.DTOs;
using YoutubeAPI.Services.Interfaces;
using YoutubeAPI.Validators;

namespace YoutubeAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ValidationFilter))]
    public class YoutuberController : ControllerBase
    {
        private readonly IYoutuberService _youtuberService;

        public YoutuberController(IYoutuberService youtuberService)
        {
            _youtuberService = youtuberService;
        }

        [HttpGet]
        public async Task<ActionResult<List<YoutuberReadDTO>>> GetYoutubers()
        {
            try
            {
                var youtubers = await _youtuberService.GetAllYoutubersAsync();
                return Ok(youtubers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving YouTubers.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<YoutuberReadDTO>> GetYoutuber(int id)
        {
            try
            {
                var youtuber = await _youtuberService.GetYoutuberByIdAsync(id);
                if (youtuber == null)
                    return NotFound(new { message = $"YouTuber with ID {id} not found." });

                return Ok(youtuber);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the YouTuber.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<YoutuberReadDTO>> CreateYoutuber(YoutuberCreateDTO youtuberCreateDTO)
        {
            try
            {
                var youtuber = await _youtuberService.CreateYoutuberAsync(youtuberCreateDTO);
                return CreatedAtAction(nameof(GetYoutuber), new { id = youtuber.Id }, youtuber);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the YouTuber.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateYoutuber(int id, YoutuberUpdateDTO updateYoutuberDTO)
        {
            try
            {
                var success = await _youtuberService.UpdateYoutuberAsync(id, updateYoutuberDTO);
                if (!success)
                    return NotFound(new { message = $"YouTuber with ID {id} not found." });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the YouTuber.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYoutuber(int id, YoutuberDeleteDTO deleteYoutuberDto)
        {
            try
            {
                var success = await _youtuberService.DeleteYoutuberAsync(id, deleteYoutuberDto);
                if (!success)
                    return NotFound(new { message = $"YouTuber with ID {id} not found." });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the YouTuber.", error = ex.Message });
            }
        }

        [HttpGet("{id}/exists")]
        public async Task<ActionResult<bool>> YoutuberExists(int id)
        {
            try
            {
                var exists = await _youtuberService.YoutuberExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while checking YouTuber existence.", error = ex.Message });
            }
        }

        [HttpGet("{id}/can-delete")]
        public async Task<ActionResult<bool>> CanDeleteYoutuber(int id)
        {
            try
            {
                var canDelete = await _youtuberService.CanDeleteYoutuberAsync(id);
                return Ok(new { canDelete, message = canDelete ? "YouTuber can be deleted." : "YouTuber has active videos and cannot be deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while checking if YouTuber can be deleted.", error = ex.Message });
            }
        }

        [HttpGet("admin/all")]
        public async Task<ActionResult<List<YoutuberAdminReadDTO>>> GetAllYoutubersAdmin()
        {
            var youtubers = await _youtuberService.GetAllYoutubersAdminAsync();
            return Ok(youtubers);
        }

        [HttpGet("admin/{id}")]
        public async Task<ActionResult<YoutuberAdminReadDTO>> GetYoutuberAdmin(int id)
        {
            var youtuber = await _youtuberService.GetYoutuberByIdAdminAsync(id);
            if (youtuber == null)
                return NotFound();
            return Ok(youtuber);
        }

        [HttpPost("admin/{id}/restore")]
        public async Task<IActionResult> RestoreYoutuber(int id)
        {
            var success = await _youtuberService.RestoreYoutuberAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("admin/{id}/hard-delete")]
        public async Task<IActionResult> HardDeleteYoutuber(int id)
        {
            try
            {
                var success = await _youtuberService.HardDeleteYoutuberAsync(id);
                if (!success)
                    return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
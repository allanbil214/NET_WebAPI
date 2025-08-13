using Microsoft.AspNetCore.Mvc;
using YoutubeAPI.Models.DTOs;
using YoutubeAPI.Services.Interfaces;
using YoutubeAPI.Validators;

namespace YoutubeAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ValidationFilter))]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoReadDTO>>> GetVideos()
        {
            try
            {
                var videos = await _videoService.GetAllVideosAsync();
                return Ok(videos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving videos.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoReadDTO>> GetVideo(int id)
        {
            try
            {
                var video = await _videoService.GetVideoByIdAsync(id);
                if (video == null)
                    return NotFound(new { message = $"Video with ID {id} not found." });

                return Ok(video);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the video.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<VideoReadDTO>> CreateVideo(VideoCreateDTO videoCreateDTO)
        {
            try
            {
                var video = await _videoService.CreateVideoAsync(videoCreateDTO);
                return CreatedAtAction(nameof(GetVideo), new { id = video.Id }, video);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the video.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideo(int id, VideoUpdateDTO updateVideoDTO)
        {
            try
            {
                var success = await _videoService.UpdateVideoAsync(id, updateVideoDTO);
                if (!success)
                    return NotFound(new { message = $"Video with ID {id} not found." });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the video.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id, VideoDeleteDTO deleteVideoDto)
        {
            try
            {
                var success = await _videoService.DeleteVideoAsync(id, deleteVideoDto);
                if (!success)
                    return NotFound(new { message = $"Video with ID {id} not found." });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the video.", error = ex.Message });
            }
        }

        [HttpGet("youtuber/{youtuberId}")]
        public async Task<ActionResult<List<VideoReadDTO>>> GetVideoByYoutuber(int youtuberId)
        {
            try
            {
                var videos = await _videoService.GetVideosByYoutuberAsync(youtuberId);
                return Ok(videos);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving videos for the YouTuber.", error = ex.Message });
            }
        }

        [HttpGet("{id}/exists")]
        public async Task<ActionResult<bool>> VideoExists(int id)
        {
            try
            {
                var exists = await _videoService.VideoExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while checking video existence.", error = ex.Message });
            }
        }

        [HttpGet("admin/all")]
        public async Task<ActionResult<List<VideoAdminReadDTO>>> GetAllVideosAdmin()
        {
            var videos = await _videoService.GetAllVideosAdminAsync();
            return Ok(videos);
        }

        [HttpGet("admin/{id}")]
        public async Task<ActionResult<VideoAdminReadDTO>> GetVideoAdmin(int id)
        {
            var video = await _videoService.GetVideoByIdAdminAsync(id);
            if (video == null)
                return NotFound();
            return Ok(video);
        }

        [HttpPost("admin/{id}/restore")]
        public async Task<IActionResult> RestoreVideo(int id)
        {
            var success = await _videoService.RestoreVideoAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("admin/{id}/hard-delete")]
        public async Task<IActionResult> HardDeleteVideo(int id)
        {
            var success = await _videoService.HardDeleteVideoAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
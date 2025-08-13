using AutoMapper;
using YoutubeAPI.Models.DTOs;
using YoutubeAPI.Models.Entities;
using YoutubeAPI.Repositories.Interfaces;
using YoutubeAPI.Services.Interfaces;

namespace YoutubeAPI.Services.Implementations
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IYoutuberRepository _youtuberRepository;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, IYoutuberRepository youtuberRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _youtuberRepository = youtuberRepository;
            _mapper = mapper;
        }

        public async Task<List<VideoReadDTO>> GetAllVideosAsync()
        {
            var videos = await _videoRepository.GetAllAsync();
            return _mapper.Map<List<VideoReadDTO>>(videos);
        }

        public async Task<VideoReadDTO?> GetVideoByIdAsync(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if (video == null)
                return null;

            return _mapper.Map<VideoReadDTO>(video);
        }

        public async Task<VideoReadDTO> CreateVideoAsync(VideoCreateDTO videoCreateDTO)
        {
            var youtuberExists = await _youtuberRepository.YoutuberExistsAsync(videoCreateDTO.YoutuberID);
            if (!youtuberExists)
            {
                throw new ArgumentException($"YouTuber with ID {videoCreateDTO.YoutuberID} does not exist.");
            }

            var allVideos = await _videoRepository.GetAllAsync();
            var urlExists = allVideos.Any(v => v.Url.Equals(videoCreateDTO.Url, StringComparison.OrdinalIgnoreCase));
            if (urlExists)
            {
                throw new InvalidOperationException("A video with this URL already exists.");
            }

            var video = _mapper.Map<Video>(videoCreateDTO);
            video.PublishedAt = DateTime.UtcNow;

            var createdVideo = await _videoRepository.AddAsync(video);
            return _mapper.Map<VideoReadDTO>(createdVideo);
        }

        public async Task<bool> UpdateVideoAsync(int id, VideoUpdateDTO videoUpdateDTO)
        {
            var existingVideo = await _videoRepository.GetByIdAsync(id);
            if (existingVideo == null)
                return false;

            if (videoUpdateDTO.YoutuberID.HasValue)
            {
                var youtuberExists = await _youtuberRepository.YoutuberExistsAsync(videoUpdateDTO.YoutuberID.Value);
                if (!youtuberExists)
                {
                    throw new ArgumentException($"YouTuber with ID {videoUpdateDTO.YoutuberID} does not exist.");
                }
            }

            if (!string.IsNullOrEmpty(videoUpdateDTO.Url) && 
                !existingVideo.Url.Equals(videoUpdateDTO.Url, StringComparison.OrdinalIgnoreCase))
            {
                var allVideos = await _videoRepository.GetAllAsync();
                var urlExists = allVideos.Any(v => v.Id != id && 
                    v.Url.Equals(videoUpdateDTO.Url, StringComparison.OrdinalIgnoreCase));
                if (urlExists)
                {
                    throw new InvalidOperationException("A video with this URL already exists.");
                }
            }

            if (videoUpdateDTO.ViewCount.HasValue && videoUpdateDTO.LikeCount.HasValue)
            {
                var viewCount = videoUpdateDTO.ViewCount.Value;
                var likeCount = videoUpdateDTO.LikeCount.Value;
                
                if (likeCount > viewCount)
                {
                    throw new InvalidOperationException("Like count cannot exceed view count.");
                }
            }

            _mapper.Map(videoUpdateDTO, existingVideo);
            await _videoRepository.EditAsync(existingVideo);
            return true;
        }

        public async Task<bool> DeleteVideoAsync(int id, VideoDeleteDTO videoDeleteDTO)
        {
            var existingVideo = await _videoRepository.GetByIdAsync(id);
            if (existingVideo == null)
                return false;

            await _videoRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<VideoReadDTO>> GetVideosByYoutuberAsync(int youtuberId)
        {
            var youtuberExists = await _youtuberRepository.YoutuberExistsAsync(youtuberId);
            if (!youtuberExists)
            {
                throw new ArgumentException($"YouTuber with ID {youtuberId} does not exist.");
            }

            var videos = await _videoRepository.GetVideoByYoutuberAsync(youtuberId);
            return _mapper.Map<List<VideoReadDTO>>(videos);
        }

        public async Task<bool> VideoExistsAsync(int id)
        {
            return await _videoRepository.VideoExistsAsync(id);
        }

        public async Task<List<VideoAdminReadDTO>> GetAllVideosAdminAsync()
        {
            var videos = await _videoRepository.GetAllAsync();
            var allVideos = await _videoRepository.GetAllIncludingDeletedAsync();
            return _mapper.Map<List<VideoAdminReadDTO>>(allVideos);
        }

        public async Task<VideoAdminReadDTO?> GetVideoByIdAdminAsync(int id)
        {
            var video = await _videoRepository.GetByIdIncludingDeletedAsync(id);
            if (video == null)
                return null;

            return _mapper.Map<VideoAdminReadDTO>(video);
        }

        public async Task<bool> RestoreVideoAsync(int id)
        {
            var video = await _videoRepository.GetByIdIncludingDeletedAsync(id);
            if (video == null || !video.IsDeleted)
                return false;

            await _videoRepository.RestoreAsync(id);
            return true;
        }

        public async Task<bool> HardDeleteVideoAsync(int id)
        {
            var video = await _videoRepository.GetByIdIncludingDeletedAsync(id);
            if (video == null)
                return false;

            await _videoRepository.HardDeleteAsync(id);
            return true;
        }
    }
}
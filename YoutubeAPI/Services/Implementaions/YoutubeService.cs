using AutoMapper;
using YoutubeAPI.Models.DTOs;
using YoutubeAPI.Models.Entities;
using YoutubeAPI.Repositories.Interfaces;
using YoutubeAPI.Services.Interfaces;

namespace YoutubeAPI.Services.Implementations
{
    public class YoutuberService : IYoutuberService
    {
        private readonly IYoutuberRepository _youtuberRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public YoutuberService(IYoutuberRepository youtuberRepository, IVideoRepository videoRepository, IMapper mapper)
        {
            _youtuberRepository = youtuberRepository;
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<List<YoutuberReadDTO>> GetAllYoutubersAsync()
        {
            var youtubers = await _youtuberRepository.GetAllAsync();
            return _mapper.Map<List<YoutuberReadDTO>>(youtubers);
        }

        public async Task<YoutuberReadDTO?> GetYoutuberByIdAsync(int id)
        {
            var youtuber = await _youtuberRepository.GetByIdAsync(id);
            if (youtuber == null)
                return null;

            return _mapper.Map<YoutuberReadDTO>(youtuber);
        }

        public async Task<YoutuberReadDTO> CreateYoutuberAsync(YoutuberCreateDTO youtuberCreateDTO)
        {
            var allYoutubers = await _youtuberRepository.GetAllAsync();
            var emailExists = allYoutubers.Any(y => y.Email.Equals(youtuberCreateDTO.Email, StringComparison.OrdinalIgnoreCase));
            if (emailExists)
            {
                throw new InvalidOperationException("A YouTuber with this email already exists.");
            }

            var channelNameExists = allYoutubers.Any(y => y.ChannelName.Equals(youtuberCreateDTO.ChannelName, StringComparison.OrdinalIgnoreCase));
            if (channelNameExists)
            {
                throw new InvalidOperationException("A YouTuber with this channel name already exists.");
            }

            if (youtuberCreateDTO.Subscriber > 1000000 && string.IsNullOrEmpty(youtuberCreateDTO.Name))
            {
                throw new InvalidOperationException("New channels with high subscriber counts require verification.");
            }

            var youtuber = _mapper.Map<Youtuber>(youtuberCreateDTO);
            var createdYoutuber = await _youtuberRepository.AddAsync(youtuber);
            return _mapper.Map<YoutuberReadDTO>(createdYoutuber);
        }

        public async Task<bool> UpdateYoutuberAsync(int id, YoutuberUpdateDTO youtuberUpdateDTO)
        {
            var existingYoutuber = await _youtuberRepository.GetByIdAsync(id);
            if (existingYoutuber == null)
                return false;

            if (!string.IsNullOrEmpty(youtuberUpdateDTO.Email) && 
                !existingYoutuber.Email.Equals(youtuberUpdateDTO.Email, StringComparison.OrdinalIgnoreCase))
            {
                var allYoutubers = await _youtuberRepository.GetAllAsync();
                var emailExists = allYoutubers.Any(y => y.Id != id && 
                    y.Email.Equals(youtuberUpdateDTO.Email, StringComparison.OrdinalIgnoreCase));
                if (emailExists)
                {
                    throw new InvalidOperationException("A YouTuber with this email already exists.");
                }
            }

            if (!string.IsNullOrEmpty(youtuberUpdateDTO.ChannelName) && 
                !existingYoutuber.ChannelName.Equals(youtuberUpdateDTO.ChannelName, StringComparison.OrdinalIgnoreCase))
            {
                var allYoutubers = await _youtuberRepository.GetAllAsync();
                var channelNameExists = allYoutubers.Any(y => y.Id != id && 
                    y.ChannelName.Equals(youtuberUpdateDTO.ChannelName, StringComparison.OrdinalIgnoreCase));
                if (channelNameExists)
                {
                    throw new InvalidOperationException("A YouTuber with this channel name already exists.");
                }
            }

            if (youtuberUpdateDTO.Subscriber.HasValue)
            {
                var newCount = youtuberUpdateDTO.Subscriber.Value;
                var currentCount = existingYoutuber.Subscriber;
                
                if (newCount < currentCount * 0.9)
                {
                    throw new InvalidOperationException("Subscriber count decrease of more than 10% requires additional verification.");
                }
            }

            _mapper.Map(youtuberUpdateDTO, existingYoutuber);
            await _youtuberRepository.EditAsync(existingYoutuber);
            return true;
        }

        public async Task<bool> DeleteYoutuberAsync(int id, YoutuberDeleteDTO youtuberDeleteDTO)
        {
            var existingYoutuber = await _youtuberRepository.GetByIdAsync(id);
            if (existingYoutuber == null)
                return false;

            var canDelete = await CanDeleteYoutuberAsync(id);
            if (!canDelete)
            {
                throw new InvalidOperationException("Cannot delete YouTuber with active videos. Please delete or reassign videos first.");
            }

            await _youtuberRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> YoutuberExistsAsync(int id)
        {
            return await _youtuberRepository.YoutuberExistsAsync(id);
        }

        public async Task<bool> CanDeleteYoutuberAsync(int id)
        {
            var videos = await _videoRepository.GetVideoByYoutuberAsync(id);
            return !videos.Any();
        }

        public async Task<List<YoutuberAdminReadDTO>> GetAllYoutubersAdminAsync()
        {
            var youtubers = await _youtuberRepository.GetAllIncludingDeletedAsync();
            return _mapper.Map<List<YoutuberAdminReadDTO>>(youtubers);
        }

        public async Task<YoutuberAdminReadDTO?> GetYoutuberByIdAdminAsync(int id)
        {
            var youtuber = await _youtuberRepository.GetByIdIncludingDeletedAsync(id);
            if (youtuber == null)
                return null;

            return _mapper.Map<YoutuberAdminReadDTO>(youtuber);
        }

        public async Task<bool> RestoreYoutuberAsync(int id)
        {
            var youtuber = await _youtuberRepository.GetByIdIncludingDeletedAsync(id);
            if (youtuber == null || !youtuber.IsDeleted)
                return false;

            await _youtuberRepository.RestoreAsync(id);
            return true;
        }

        public async Task<bool> HardDeleteYoutuberAsync(int id)
        {
            var youtuber = await _youtuberRepository.GetByIdIncludingDeletedAsync(id);
            if (youtuber == null)
                return false;

            var videos = await _videoRepository.GetVideoByYoutuberAsync(id);
            if (videos.Any())
            {
                throw new InvalidOperationException("Cannot permanently delete YouTuber with videos. Delete videos first.");
            }

            await _youtuberRepository.HardDeleteAsync(id);
            return true;
        }
    }
}
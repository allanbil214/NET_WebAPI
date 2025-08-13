using AutoMapper;
using YoutubeAPI.Models.DTOs;
using YoutubeAPI.Models.Entities;


namespace YoutubeAPI.Mapping
{
    public class VideoMappingProfile : Profile
    {
        public VideoMappingProfile()
        {
            CreateMap<Video, VideoReadDTO>();
            CreateMap<Video, VideoAdminReadDTO>();

            CreateMap<VideoCreateDTO, Video>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Youtuber, opt => opt.Ignore());

            CreateMap<VideoUpdateDTO, Video>()
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
                .ForMember(dest => dest.Url, opt => opt.Condition(src => src.Url != null))
                .ForMember(dest => dest.ViewCount, opt => opt.Condition(src => src.ViewCount.HasValue))
                .ForMember(dest => dest.LikeCount, opt => opt.Condition(src => src.LikeCount.HasValue))
                .ForMember(dest => dest.DislikeCount, opt => opt.Condition(src => src.DislikeCount.HasValue))
                .ForMember(dest => dest.YoutuberID, opt => opt.Condition(src => src.YoutuberID.HasValue))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.PublishedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Youtuber, opt => opt.Ignore());

            CreateMap<VideoDeleteDTO, Video>()
                .ForMember(dest => dest.Title, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opt => opt.Ignore())
                .ForMember(dest => dest.ViewCount, opt => opt.Ignore())
                .ForMember(dest => dest.LikeCount, opt => opt.Ignore())
                .ForMember(dest => dest.DislikeCount, opt => opt.Ignore())
                .ForMember(dest => dest.PublishedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.IsDeleted ? DateTime.UtcNow : (DateTime?)null))
                .ForMember(dest => dest.YoutuberID, opt => opt.Ignore())
                .ForMember(dest => dest.Youtuber, opt => opt.Ignore());
        }
    }
}
using AutoMapper;
using YoutubeAPI.Models.DTOs;
using YoutubeAPI.Models.Entities;

namespace YoutubeAPI.Mappings
{
    public class YoutuberMappingProfile : Profile
    {
        public YoutuberMappingProfile()
        {
            CreateMap<Youtuber, YoutuberReadDTO>();
            CreateMap<Youtuber, YoutuberAdminReadDTO>();

            CreateMap<YoutuberCreateDTO, Youtuber>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Videos, opt => opt.Ignore());

            CreateMap<YoutuberUpdateDTO, Youtuber>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
                .ForMember(dest => dest.ChannelName, opt => opt.Condition(src => src.ChannelName != null))
                .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))
                .ForMember(dest => dest.Subscriber, opt => opt.Condition(src => src.Subscriber.HasValue))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Videos, opt => opt.Ignore());

            CreateMap<YoutuberDeleteDTO, Youtuber>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.ChannelName, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Subscriber, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.IsDeleted ? DateTime.UtcNow : (DateTime?)null))
                .ForMember(dest => dest.Videos, opt => opt.Ignore());
        }
    }
}
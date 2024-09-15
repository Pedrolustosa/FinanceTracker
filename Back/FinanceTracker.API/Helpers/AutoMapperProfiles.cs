using AutoMapper;
using FinanceTracker.API.DTOs;
using FinanceTracker.API.Entities;
using FinanceTracker.API.Extensions;

namespace FinanceTracker.API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
            .ForMember(x => x.PhotoUrl, o => 
                       o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));
        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto, AppUser>();
        CreateMap<RegisterDto, AppUser>();
        CreateMap<string, DateOnly>().ConvertUsing(s => DateOnly.Parse(s));
        CreateMap<Message, MessageDto>().ForMember(d => d.SenderPhotoUrl, o =>
                                       o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain)!.Url))
                                       .ForMember(d => d.RecipientPhotoUrl, o =>
                                       o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain)!.Url));
    }
}

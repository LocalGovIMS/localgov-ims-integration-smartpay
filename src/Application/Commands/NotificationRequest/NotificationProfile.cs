using Application.Clients.LocalGovImsPaymentApi;
using Application.Models;
using AutoMapper;

namespace Application.Commands
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationModel>()
                .ForMember(destination => destination.Live, options => options.MapFrom(source => source.Live == "Live"));
        }
    }
}

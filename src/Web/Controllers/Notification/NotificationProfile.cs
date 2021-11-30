using Application.Models;
using AutoMapper;

namespace Web.Controllers
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationModel, Notification>();
        }
    }
}

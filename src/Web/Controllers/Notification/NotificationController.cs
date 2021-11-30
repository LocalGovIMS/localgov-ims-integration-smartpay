using Application.Commands;
using Application.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IMapper _mapper;

        public NotificationController(
            ILogger<NotificationController> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<StatusCodeResult> Post(NotificationModel model)
        {
            try
            {
                var result = await Mediator.Send(new NotificationRequestCommand() { Notification = _mapper.Map<Notification>(model) });

                return new StatusCodeResult((int)result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to process notification");

                return BadRequest();
            }
        }
    }
}

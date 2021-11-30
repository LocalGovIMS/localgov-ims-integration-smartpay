using Application.Clients.LocalGovImsPaymentApi;
using Application.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class NotificationRequestCommand : IRequest<HttpStatusCode>
    {
        public Notification Notification { get; set; }
        public string AuthorisationHeader { get; set; }
    }

    public class NotificationRequestCommandHandler : IRequestHandler<NotificationRequestCommand, HttpStatusCode>
    {
        private readonly ILogger<NotificationRequestCommandHandler> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILocalGovImsPaymentApiClient _localGovImsPaymentApiClient;

        public NotificationRequestCommandHandler(
            ILogger<NotificationRequestCommandHandler> logger,
            IConfiguration configuration,
            IMapper mapper,
            ILocalGovImsPaymentApiClient localGovImsPaymentApiClient)
        {
            _logger = logger;
            _configuration = configuration;
            _localGovImsPaymentApiClient = localGovImsPaymentApiClient;
            _mapper = mapper;
        }

        public async Task<HttpStatusCode> Handle(NotificationRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AuthenticateNotificationRequest(request);

                return await _localGovImsPaymentApiClient.Notify(_mapper.Map<NotificationModel>(request.Notification));
            }
            catch (NotificationAuthenticationException ex)
            {
                return ex.StatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to process notification");

                return HttpStatusCode.BadRequest;
            }
        }

        private void AuthenticateNotificationRequest(NotificationRequestCommand request)
        {
            string authorisationHeader = request.AuthorisationHeader;

            if (string.IsNullOrEmpty(authorisationHeader))
            {
                throw new NotificationAuthenticationException("Unauthorized", HttpStatusCode.Unauthorized);
            }

            string encodedAuthorisationCredentials = authorisationHeader.Split(' ')[1];
            string decodedAuthorisationCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedAuthorisationCredentials));

            var requestUser = decodedAuthorisationCredentials.Split(':')[0];
            var requestPassword = decodedAuthorisationCredentials.Split(':')[1];
            var notificationUser = _configuration.GetValue<string>("SmartPay:NotificationUser");
            var notificationPassword = _configuration.GetValue<string>("SmartPay:NotificationPassword");

            if (!notificationUser.Equals(requestUser) || !notificationPassword.Equals(requestPassword))
            {
                throw new NotificationAuthenticationException("Forbidden", HttpStatusCode.Forbidden);
            }
        }
    }
}

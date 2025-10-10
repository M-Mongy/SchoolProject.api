using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Command.Models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.Emails.Command.Handler
{
    public class EmailsCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IEmailsService _emailsService;
        IStringLocalizer<SharedResource> _stringLocalizer;
        public EmailsCommandHandler(IStringLocalizer<SharedResource> stringLocalizer,
            IEmailsService emailsService) : base(stringLocalizer)
        {
            _emailsService = emailsService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailsService.SendEmail(request.Email, request.Message);
            if(response == "success")
            {
                return Success<string>("");
            }
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
        }
    }
}

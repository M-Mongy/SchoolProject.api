using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Emails.Command.Models;
using SchoolProject.Core.SharedResources;

namespace SchoolProject.Core.Features.Emails.Command.Validators
{
    public class SendEmailValidator:AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion
        #region Constructors
        public SendEmailValidator(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Message)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        #endregion
    }
}

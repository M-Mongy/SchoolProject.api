using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Absract;
using SchoolProject.Service.Implemntation;

namespace SchoolProject.Core.Features.student.Commands.validatiors
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IstudentService _service;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public AddStudentValidator(IstudentService service, IDepartmentService departmentService,
            IStringLocalizer<SharedResource> localizer)
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
            _service = service;
            _departmentService = departmentService;
            _localizer = localizer;
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.NameAr)
             .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
             .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
             .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.address)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.Department_id)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.NameAr)
           .MustAsync(async (Key, CancellationToken) => !await _service.IsNameExsit(Key))
          .WithMessage(_localizer[SharedResourcesKeys.IsExist]);
            RuleFor(x => x.NameEn)
               .MustAsync(async (Key, CancellationToken) => !await _service.IsNameExsit(Key))
               .WithMessage(_localizer[SharedResourcesKeys.IsExist]);


            RuleFor(x => x.Department_id)
       .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentIdExist(Key))
       .WithMessage(_localizer[SharedResourcesKeys.IsExist]);
        }
        #endregion


    }
}

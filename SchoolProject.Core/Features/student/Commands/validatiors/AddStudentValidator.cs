using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.student.Commands.validatiors
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IstudentService _service;
        public AddStudentValidator(IstudentService service)
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
            _service = service;
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name Must not Be Empty")
                .NotNull().WithMessage("Name Must not be Null")
                .MaximumLength(10).WithMessage("MAX Length is 10");

            RuleFor(x => x.address)
                .NotEmpty().WithMessage("{PropertyName} Must not Be Empty")
                .NotNull().WithMessage("{PropertyValue} Must not Be Null")
                .MaximumLength(10).WithMessage("{PropertyName} Length is 10");
        }

        public void ApplyCustomValidationsRules() 
        {
            RuleFor(x => x.name).MustAsync(async (Key, CancellationToken) => await _service.IsNameExsit(Key))
                .WithMessage("Name is Exist");
        }
        #endregion


    }
}

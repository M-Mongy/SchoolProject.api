using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.student.Commands.validatiors
{
    public class EditStudentValidator: AbstractValidator<EditStudentCommand>
    {

        private readonly IstudentService _service;

        public EditStudentValidator(IstudentService service)
        {
            _service = service;  
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name Must not Be Empty")
                .MaximumLength(10).WithMessage("MAX Length is 10");

            RuleFor(x => x.address)
                .NotEmpty().WithMessage("{PropertyName} Must not Be Empty")
                .MaximumLength(10).WithMessage("{PropertyName} Length is 10");
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.name)
                .MustAsync(async (model, name, cancellationToken) =>
                    !await _service.IsNameExsitExclusive(name, model.id))
                .WithMessage("Name is Exist");
        }

    }
}

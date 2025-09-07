using FluentValidation;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.student.Commands.validatiors
{
    public class DeleteStudentValidator : AbstractValidator<DeleteStudentCommand>
    {
        private readonly IstudentService _service;

        public DeleteStudentValidator(IstudentService service)
        {
            _service = service;

            // Rule: الـ Id لازم يكون أكبر من 0
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Student Id must be greater than 0.");

            // Rule: لازم الطالب يكون موجود فعلاً (تتحقق بس لو Id > 0)
            RuleFor(x => x.Id)
                .MustAsync(async (id, cancellation) =>
                    await _service.GetStudentByIdAsync(id) != null)
                .WithMessage("Student with this Id does not exist.")
                .When(x => x.Id > 0);
        }
    }
}

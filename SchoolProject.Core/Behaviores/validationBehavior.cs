using FluentValidation;
using MediatR;
using SchoolProject.Core.bases; // <-- ده المكان اللي فيه Response<T>

namespace SchoolProject.api.Behaviores
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response<string>, new() // لازم Response<T> يكون له constructor
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count > 0)
                {
                    // نجمع كل الأخطاء ونبعتها في الـ Response
                    var response = new TResponse
                    {
                        Succeeded = false,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "Validation errors occurred.",
                        Errors = failures.Select(f => f.PropertyName + ": " + f.ErrorMessage).ToList()
                    };

                    return response;
                }
            }

            return await next();
        }
    }
}

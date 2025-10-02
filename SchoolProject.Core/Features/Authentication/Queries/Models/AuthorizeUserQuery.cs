using MediatR;
using SchoolProject.Core.bases;

namespace SchoolProject.Core.Features.Authentication.Queries.Models
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }
    }
}
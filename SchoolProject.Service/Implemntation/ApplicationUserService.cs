using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Absract;

namespace SchoolProject.Service.Implemntation
{
    public class ApplicationUserService : IApplicationUserService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailsService _emailsService;
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        #endregion

        #region Constructors
        public ApplicationUserService(UserManager<User> userManager,
                                      IHttpContextAccessor httpContextAccessor,
                                      IEmailsService emailsService,
                                      ApplicationDBContext applicationDBContext,
                                      IUrlHelperFactory urlHelperFactory,
                                      IActionContextAccessor actionContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _applicationDBContext = applicationDBContext;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }
        #endregion

        #region Handle Functions
        public async Task<string> AddUserAsync(User user, string password)
        {
            var trans = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                // If Email is Exist
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                // Email is Exist
                if (existUser != null) return "EmailIsExist";

                // If username is Exist
                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                // Username is Exist
                if (userByUserName != null) return "UserNameIsExist";

                // Create
                var createResult = await _userManager.CreateAsync(user, password);
                // Failed
                if (!createResult.Succeeded)
                    return string.Join(",", createResult.Errors.Select(x => x.Description).ToList());

                await _userManager.AddToRoleAsync(user, "User");

                // Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var resquestAccessor = _httpContextAccessor.HttpContext.Request;

                // Create IUrlHelper from factory
                var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

                var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host +
                    urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });

                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Confirm Email</a>";

                // Send email
                await _emailsService.SendEmail(user.Email, message, "Confirm Email");

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
        #endregion
    }
}
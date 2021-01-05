using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using placementDepartmentBLL;
using placementDepartmentCOMMON;

namespace placementDepartmentWebAPI.util
{
    public class authFilter : Attribute, IAuthenticationFilter‏
    {
        public bool AllowMultiple => false;

        public static List<string> ManagerOnly = new List<string>()
        {
           "User/", "Branch/","City/","Language/","Expertise/","ExprtWithSubj/","Subject/"
        };

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string action = context.Request.RequestUri.Segments.LastOrDefault();
            string apiController = context.Request.RequestUri.Segments[context.Request.RequestUri.Segments.Count() - 2];
            if (apiController == "User/" && action == "Validate" //login
             || apiController == "Email/" //graduate request 
             || apiController == "Graduate/" && action == "UploadCVFile")//can be graduate request  
                return;
            string token = context.Request.Headers.Authorization.ToString();
            token = token.Substring(7);
            var valid = Token.ValidateCurrentToken(token);
            if (valid != null)
            {
                if( ManagerOnly.Contains(apiController) && action != "GetAll" && action != "ChangePass")
                {
                    UserDto user = UserBLManager.UserDtoById(Int32.Parse(valid.Identity.Name));
                    if (user.Permission.Id != 1)
                        throw new HttpResponseException(HttpStatusCode.Forbidden);
                }

                //save user
                context.Principal = valid;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using placementDepartmentBLL;
using placementDepartmentCOMMON;
using placementDepartmentWebAPI.util;

namespace placementDepartmentWebAPI.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [Route("GetAll")]
        public List<UserDto> Get()
     {
            return UserBLManager.UserDtoList();
        }

        [Route("GetById")]
        public UserDto Get(int id)
        {
            return UserBLManager.UserDtoById(id);
        }

        [Route("Validate")]
        [HttpGet]
        public UserDto Get(string email, string password)
        {
            try
            {
                var userDto = UserBLManager.ValidateUser(email, password);
                userDto.token = Token.GenerateToken(userDto.Id.ToString());
                return userDto;
            }
            catch (UnauthorizedAccessException)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        [Route("Save")]
        public void Post(string password ,[FromBody]UserDto userDto)
        {
            userDto.password = password;
            userDto.isInitialPassword = true;
            UserBLManager.NewUserDto(userDto);
        }

        [Route("ChangePass")]
        public void Put(int id, bool isInit, string password)
        {
            UserBLManager.ChangePassword(id, isInit, password);
        }

        [Route("Edit")]
        public void Put([FromBody]UserDto userDto)
        {
            UserBLManager.UserDtoEditing(userDto);

        }

        [Route("Delete")]
        public void Delete(int id)
        {
            UserBLManager.DeleteUserDto(id);
        }
    }
}

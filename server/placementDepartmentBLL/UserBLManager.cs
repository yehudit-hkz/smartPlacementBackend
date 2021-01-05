using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using placementDepartmentDAL;
using placementDepartmentCOMMON;

namespace placementDepartmentBLL
{
    public static class UserBLManager
    {
        public static List<UserDto> UserDtoList()
        {
            return UserManager.UserList();
        }
        public static UserDto UserDtoById(int Id)
        {
            return UserManager.UserById(Id);
        }
        public static UserDto ValidateUser(string email, string password)
        {
            var user = UserManager.UserByEmail(email);
            if (user == null)
                throw new UnauthorizedAccessException();
            if (!BCrypt.Net.BCrypt.Verify(password, user.password))
                throw new UnauthorizedAccessException();
            return user;
        }
        public static void UserDtoEditing(UserDto userDto)
        {
            UserManager.UserEditing(userDto);
        }
        public static void ChangePassword(int id,bool isInit, string password)
        {
            password = BCrypt.Net.BCrypt.HashPassword(password);
            UserManager.ChangePassword(id, isInit, password);
        }

        public static void NewUserDto(UserDto userDto)
        {
            userDto.password = BCrypt.Net.BCrypt.HashPassword(userDto.password);
            UserManager.NewUser(userDto);
        }
        public static void DeleteUserDto(int id)
        {
            UserManager.DeleteUser(id);
        }
    }
}

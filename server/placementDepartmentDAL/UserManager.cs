using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using placementDepartmentCOMMON;


namespace placementDepartmentDAL
{
    public static class UserManager
    {
        public static List<UserDto> UserList()
        {
            List<UserDto> usersDto;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                usersDto = placementDepartmentDB.User
                    .ProjectTo<UserDto>(AutoMapperConfiguration.config)
                    .ToList();
                return usersDto;
            }
        }
        public static UserDto UserById(int Id)
        {
            User user;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                user = placementDepartmentDB.User.Find(Id);
                return AutoMapperConfiguration.mapper.Map<UserDto>(user);
            }
        }

        public static UserDto UserByEmail(string email)
        {
            User user;
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                user = placementDepartmentDB.User
                    .Where(u => u.email == email && u.isActive == true)
                    .FirstOrDefault();
                return AutoMapperConfiguration.mapper.Map<UserDto>(user);
            }
        }
        public static void NewUser(UserDto userDto)
        {
            User user = AutoMapperConfiguration.mapper.Map<User>(userDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                    placementDepartmentDB.User.Add(user);
                    placementDepartmentDB.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null)
                    {
                        if (sqlException.Number == 2627)
                        {
                            throw new Exception("Duplicate");
                        }
                    }
                }

            }
        }
        public static void UserEditing(UserDto userDto)
        {
            User user = AutoMapperConfiguration.mapper.Map<User>(userDto);
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                    placementDepartmentDB.User.Attach(user);
                    placementDepartmentDB.Entry(user).Property(x => x.name).IsModified = true;
                    placementDepartmentDB.Entry(user).Property(x => x.email).IsModified = true;
                    placementDepartmentDB.Entry(user).Property(x => x.permissionId).IsModified = true;
                    placementDepartmentDB.Entry(user).Property(x => x.isActive).IsModified = true;

                    placementDepartmentDB.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null)
                    {
                        if (sqlException.Number == 2627)
                        {
                            throw new Exception("Duplicate");
                        }
                    }
                }
            }
        }
        public static void ChangePassword(int id, bool isInit, string hashPassword)
        {
            User user = new User()
            {
                Id = id,
                isInitialPassword = isInit,
                password = hashPassword
            };
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                placementDepartmentDB.Configuration.ValidateOnSaveEnabled = false;
                placementDepartmentDB.User.Attach(user);
                placementDepartmentDB.Entry(user).Property(x => x.isInitialPassword).IsModified = true;
                placementDepartmentDB.Entry(user).Property(x => x.password).IsModified = true;
                placementDepartmentDB.SaveChanges();
            }
        }
        public static void DeleteUser(int id)
        {
            using (placementDepartmentDBEntities placementDepartmentDB = new placementDepartmentDBEntities())
            {
                try
                {
                    User user = placementDepartmentDB.User.Find(id);
                    placementDepartmentDB.User.Remove(user);
                    placementDepartmentDB.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null)
                    {
                        if (sqlException.Number == 547)
                        {
                            throw new DbUpdateException("547");
                        }
                    }
                }
            }
        }
    }
}

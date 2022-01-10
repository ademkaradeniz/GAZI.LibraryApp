using GAZI.LibraryApp.Data.Entities;
using GAZI.LibraryApp.Data.Repositories.RolesRepositories;
using GAZI.LibraryApp.Data.Repositories.UsersRepositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Business.UsersAppService
{
    public class UsersApp : IUsersApp
    {
        [Inject]
        public IRolesRepository rolesRepository { private get; set; }

        [Inject]
        public IUsersRepository usersRepository { private get; set; }

        public UsersApp(IRolesRepository _rolesRepository)
        {
            rolesRepository = _rolesRepository;
        }

        #region Roles Service

        public List<Roles> GetAllRoles()
        {
            var result = rolesRepository.GetAll();
            return result;
        }

        public Roles GetRole(int? Id)
        {
            var result = rolesRepository.FindID(Id);
            return result;
        }

        public bool CreateOrUpdateRole(Roles roles)
        {

            if (roles != null)
            {
                if (roles.ID != default(int))
                {
                    var updateroles = rolesRepository.FindID(roles.ID);
                    if (updateroles == null)
                    {
                        return false;
                    }
                    updateroles.Name = roles.Name;
                    updateroles.Status = 1;
                    updateroles.CreateOrModifyDate = DateTime.Now;
                    rolesRepository.Update(updateroles);
                    return true;

                }
                else
                {

                    if (rolesRepository.FindName(roles.Name) == true)
                    {
                        return false;
                    }
                    var entityroles = roles;
                    entityroles.Status = 1;
                    entityroles.CreateOrModifyDate = DateTime.Now;
                    rolesRepository.Add(entityroles);
                    return true;

                }
            }
            else
            {
                return false;
            }


        }

        public bool DeleteRole(int Id)
        {
            //Daha sonradan ortak tablolardan silinecek.
            if (Id != default(int))
            {
                var entityRole = rolesRepository.FindID(Id);
                if (entityRole == null)
                {
                    return false;
                }
                entityRole.Status = 0;
                entityRole.CreateOrModifyDate = DateTime.Now;
                rolesRepository.Delete(entityRole);
                return true;
            }
            else
            {
                //Id değeri boş gönderilemez!
                return false;
            }
        }

        #endregion

        #region Users Service

        public List<Users> GetAllUsers()
        {
            var result = usersRepository.GetAll();
            return result;
        }

        public Users GetUser(int? Id)
        {
            var result = usersRepository.FindID(Id);
            return result;
        }

        public Users GetUsersEmail(string email)
        {
            var result = usersRepository.FindEmail(email);
            return result;
        }

        public Users GetUserNamePassword(string username, string password)
        {
            var result = usersRepository.FindUserNameAndPassword(username, password);
            return result;
        }

        public List<ViewUsers> GetAllViewUsers()
        {
            var result = usersRepository.GetAllView();
            return result;
        }

        public bool CreateOrUpdateUser(Users users)
        {

            if (users != null)
            {
                if (users.ID != default(int))
                {
                    var updateusers = usersRepository.FindID(users.ID);
                    if (updateusers == null)
                    {
                        return false;
                    }
                    updateusers.RoleID = users.RoleID;
                    updateusers.Name = users.Name;
                    updateusers.SurName = users.SurName;
                    updateusers.UserName = users.UserName;
                    updateusers.Email = users.Email;
                    updateusers.Password = users.Password;
                    updateusers.AuthCode = users.AuthCode;
                    updateusers.Telephone = users.Telephone;
                    updateusers.Status = 1;
                    updateusers.CreateOrModifyDate = DateTime.Now;
                    usersRepository.Update(updateusers);
                    return true;

                }
                else
                {

                    if (usersRepository.FindUserNameOrEmail(users.UserName,users.Email) == true)
                    {
                        return false;
                    }
                    var entityusers = users;
                    entityusers.Status = 1;
                    entityusers.CreateOrModifyDate = DateTime.Now;
                    usersRepository.Add(entityusers);
                    return true;

                }
            }
            else
            {
                return false;
            }


        }

        public bool CreateLoginUser(Users users)
        {

            if (users != null)
            {
                if (rolesRepository.FindName("Kullanıcı") != false)
                {
                    if (usersRepository.FindUserNameOrEmail(users.UserName, users.Email) == true)
                    {
                        return false;
                    }

                    Roles role = new Roles();
                    role.Name = "Kullanıcı";
                    rolesRepository.Add(role);
                    users.RoleID = rolesRepository.GetID("Kullanıcı");

                    var entityusers = users;
                    entityusers.Status = 1;
                    entityusers.CreateOrModifyDate = DateTime.Now;
                    usersRepository.Add(entityusers);
                    return true;
                }
                else
                {
                    if (usersRepository.FindUserNameOrEmail(users.UserName, users.Email) == true)
                    {
                        return false;
                    }
                    var entityusers = users;
                    entityusers.Status = 1;
                    entityusers.CreateOrModifyDate = DateTime.Now;
                    usersRepository.Add(entityusers);
                    return true;
                }
            }
            else
            {
                return false;
            }


        }

        public bool UpdateUser(Users users)
        {

            if (users != null)
            {
                if (users.ID != default(int))
                {
                    var updateusers = usersRepository.FindID(users.ID);
                    if (updateusers == null)
                    {
                        return false;
                    }
                    updateusers.RoleID = users.RoleID;
                    updateusers.Name = users.Name;
                    updateusers.SurName = users.SurName;
                    updateusers.UserName = users.UserName;
                    updateusers.Email = users.Email;
                    updateusers.Password = users.Password;
                    updateusers.AuthCode = users.AuthCode;
                    updateusers.Telephone = users.Telephone;
                    updateusers.Status = 1;
                    updateusers.CreateOrModifyDate = DateTime.Now;
                    usersRepository.Update(updateusers);
                    return true;

                }
                else
                {

                    return false;

                }
            }
            else
            {
                return false;
            }


        }

        public bool DeleteUser(int Id)
        {
            //Daha sonradan ortak tablolardan silinecek.
            if (Id != default(int))
            {
                var entityUser = usersRepository.FindID(Id);
                if (entityUser == null)
                {
                    return false;
                }
                entityUser.Status = 0;
                entityUser.CreateOrModifyDate = DateTime.Now;
                usersRepository.Delete(entityUser);
                return true;
            }
            else
            {
                //Id değeri boş gönderilemez!
                return false;
            }
        }

        #endregion
    }
}

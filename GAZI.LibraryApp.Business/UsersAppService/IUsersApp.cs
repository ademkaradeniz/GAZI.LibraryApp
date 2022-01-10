using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GAZI.LibraryApp.Business.UsersAppService
{
    public interface IUsersApp
    {
        #region Roles Service

        /// <summary>
        /// Tüm rolleri getirir.
        /// </summary>
        /// <returns></returns>
        List<Roles> GetAllRoles();
        
        /// <summary>
        /// Id'si gönderilen role bilgisini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Roles GetRole(int? Id);

        /// <summary>
        /// Yeni rol ekler veya var olanı günceller.
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        bool CreateOrUpdateRole(Roles roles);

        /// <summary>
        /// Id'si gönderilen yazarı siler.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteRole(int Id);

        #endregion

        #region Users Service

        /// <summary>
        /// Tüm kullanıcıları getirir.
        /// </summary>
        /// <returns></returns>
        List<Users> GetAllUsers();

        /// <summary>
        /// Id'si gönderilen kullanıcı bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Users GetUser(int? Id);

        /// <summary>
        /// Email'i gönderilen kullanıcının bilgilerini getirir.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Users GetUsersEmail(string email);

        /// <summary>
        /// Username ve Password ile kullanıcı bilgilerini doğrular.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Users GetUserNamePassword(string username, string password);

        /// <summary>
        /// Tüm kullanıcıları role ismi ile birlikte getirir.
        /// </summary>
        /// <returns></returns>
        List<ViewUsers> GetAllViewUsers();

        /// <summary>
        /// Yeni kullanıcı ekler veya var olanı günceller.
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        bool CreateOrUpdateUser(Users users);

        /// <summary>
        /// Login ekranından gelen sadece kullanıcı kaydı yapar.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        bool CreateLoginUser(Users users);

        /// <summary>
        /// Var olan kullanıcıyı günceller
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        bool UpdateUser(Users users);

        /// <summary>
        /// Id'si gönderilen kullanıcıyı siler.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteUser(int Id);

        #endregion
    }
}

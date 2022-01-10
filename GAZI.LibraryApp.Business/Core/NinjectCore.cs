using GAZI.LibraryApp.Business.BooksAppService;
using GAZI.LibraryApp.Business.BorrowBooksAppService;
using GAZI.LibraryApp.Business.Core;
using GAZI.LibraryApp.Business.MailAppService;
using GAZI.LibraryApp.Business.UsersAppService;
using GAZI.LibraryApp.Data.Repositories;
using GAZI.LibraryApp.Data.Repositories.AuthorsRepositories;
using GAZI.LibraryApp.Data.Repositories.BooksRepositories;
using GAZI.LibraryApp.Data.Repositories.BorrowBooksRepositories;
using GAZI.LibraryApp.Data.Repositories.DatesRepositories;
using GAZI.LibraryApp.Data.Repositories.PublishersRepositories;
using GAZI.LibraryApp.Data.Repositories.RolesRepositories;
using GAZI.LibraryApp.Data.Repositories.TypesRepositories;
using GAZI.LibraryApp.Data.Repositories.UsersRepositories;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(NinjectCore), "Start")]
namespace GAZI.LibraryApp.Business.Core
{
    public static class NinjectCore
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {

            bootstrapper.Initialize(CreateKernel);

        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            //kernel.Bind(typeof(IApplicationCore)).To(typeof(ApplicationCore));
            // Generic Bind işlemi bulunanacak !!!!!!!!!!!!!!!!!!!!!!!!!!

            #region Kernel.Bindler

            //Books Repository and App Injection
            kernel.Bind<IAuthorsRepository>().To<AuthorsRepository>();
            kernel.Bind<IPublishersRepository>().To<PublishersRepository>();
            kernel.Bind<ITypesRepository>().To<TypesRepository>();
            kernel.Bind<IBooksRepository>().To<BooksRepository>();
            kernel.Bind<IBooksApp>().To<BooksApp>();

            //Borrow Books Repository and App Injection
            kernel.Bind<IBorrowBooksRepository>().To<BorrowBooksRepository>();
            kernel.Bind<IDatesRepository>().To<DatesRepository>();
            kernel.Bind<IBorrowBooksApp>().To<BorrowBooksApp>();

            //Users Repository and App Injection
            kernel.Bind<IUsersRepository>().To<UsersRepository>();
            kernel.Bind<IRolesRepository>().To<RolesRepository>();
            kernel.Bind<IUsersApp>().To<UsersApp>();

            //Mail App Injection
            kernel.Bind<IMailApp>().To<MailApp>();
            #endregion
        }

    }
}

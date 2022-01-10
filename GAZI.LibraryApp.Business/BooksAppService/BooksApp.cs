using GAZI.LibraryApp.Data.Entities;
using GAZI.LibraryApp.Data.Repositories.AuthorsRepositories;
using GAZI.LibraryApp.Data.Repositories.BooksRepositories;
using GAZI.LibraryApp.Data.Repositories.PublishersRepositories;
using GAZI.LibraryApp.Data.Repositories.TypesRepositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Business.BooksAppService
{
    public class BooksApp : IBooksApp
    {
        [Inject]
        public IBooksRepository booksRepository { private get; set; }
        [Inject]
        public IAuthorsRepository authorsRepository { private get; set; }
        [Inject]
        public IPublishersRepository publishersRepository { private get; set; }
        [Inject]
        public ITypesRepository typesRepository { private get; set; }
        
        public BooksApp(IBooksRepository _booksRepository,
            IAuthorsRepository _authorsRepository,
            IPublishersRepository _publishersRepository,
            ITypesRepository _typesRepository)
        {
            booksRepository = _booksRepository;
            authorsRepository = _authorsRepository;
            publishersRepository = _publishersRepository;
            typesRepository = _typesRepository;
        }

        #region Authors Service

        public List<Authors> GetAllAuthors()
        {
            var result = authorsRepository.GetAll();
            return result;
        }

        public bool CreateOrUpdateAuthor(Authors authors)
        {

            if (authors != null)
            {
                if (authors.ID != default(int))
                {
                    var updateauthors = authorsRepository.FindID(authors.ID);
                    if (updateauthors == null)
                    {
                        return false;
                    }
                    updateauthors.Name = authors.Name;
                    updateauthors.Status = 1;
                    updateauthors.CreateOrModifyDate = DateTime.Now;
                    authorsRepository.Update(updateauthors);
                    return true;

                }
                else
                {

                    if (authorsRepository.FindName(authors.Name) == true)
                    {
                        //Eğitmen daha önceden eklenmiş!
                        return false;
                    }
                    var entityauthors = authors;
                    entityauthors.Status = 1;
                    entityauthors.CreateOrModifyDate = DateTime.Now;
                    authorsRepository.Add(entityauthors);
                    //Yeni Eğitmen oluşturuldu.
                    return true;

                }
            }
            else
            {
                //Eğitmen Datası boş!
                return false;
            }


        }

        public bool DeleteAuthor(int Id)
        {
            //Daha sonradan ortak tablolardan silinecek.
            if (Id != default(int))
            {
                var entityAuthor = authorsRepository.FindID(Id);
                if (entityAuthor == null)
                {
                    //ID'li silinecek eğitmen bulunamadı!
                    return false;
                }
                entityAuthor.Status = 0;
                entityAuthor.CreateOrModifyDate = DateTime.Now;
                authorsRepository.Delete(entityAuthor);
                //Id'li eğitmen Silindi.
                return true;
            }
            else
            {
                //Id değeri boş gönderilemez!
                return false;
            }
        }

        #endregion

        #region Publishers Service

        public List<Publishers> GetAllPublishers()
        {
            var result = publishersRepository.GetAll();
            return result;
        }

        public bool CreateOrUpdatePublisher(Publishers publishers)
        {

            if (publishers != null)
            {
                if (publishers.ID != default(int))
                {
                    var updatepublishers = publishersRepository.FindID(publishers.ID);
                    if (updatepublishers == null)
                    {
                        return false;
                    }
                    updatepublishers.Name = publishers.Name;
                    updatepublishers.Status = 1;
                    updatepublishers.CreateOrModifyDate = DateTime.Now;
                    publishersRepository.Update(updatepublishers);
                    return true;

                }
                else
                {
                    if (publishersRepository.FindName(publishers.Name) == true)
                    {
                        //Yayınevi daha önceden eklenmiş!
                        return false;
                    }
                    var entitypublisher = publishers;
                    entitypublisher.Status = 1;
                    entitypublisher.CreateOrModifyDate = DateTime.Now;
                    publishersRepository.Add(entitypublisher);
                    //Yeni Yayınevi oluşturuldu.
                    return true;
                }
            }
            else
            {
                //Yayınevi Datası boş!
                return false;
            }


        }

        public bool DeletePublisher(int Id)
        {
            //Daha sonradan ortak tablolardan silinecek.
            if (Id != default(int))
            {
                var entityPublisher = publishersRepository.FindID(Id);
                if (entityPublisher == null)
                {
                    //ID'li silinecek yayınevi bulunamadı!
                    return false;
                }
                entityPublisher.Status = 0;
                entityPublisher.CreateOrModifyDate = DateTime.Now;
                publishersRepository.Delete(entityPublisher);
                //Id'li yayınevi Silindi.
                return true;
            }
            else
            {
                //Id değeri boş gönderilemez!
                return false;
            }
        }

        #endregion

        #region Types Service

        public List<Types> GetAllTypes()
        {
            var result = typesRepository.GetAll();
            return result;
        }

        public bool CreateOrUpdateType(Types types)
        {

            if (types != null)
            {
                if (types.ID != default(int))
                {
                    var updatetypes = typesRepository.FindID(types.ID);
                    if (updatetypes == null)
                    {
                        return false;
                    }
                    updatetypes.Name = types.Name;
                    updatetypes.Status = 1;
                    updatetypes.CreateOrModifyDate = DateTime.Now;
                    typesRepository.Update(updatetypes);
                    return true;

                }
                else
                {
                    if (typesRepository.FindName(types.Name) == true)
                    {
                        return false;
                    }
                    var entitytype = types;
                    entitytype.Status = 1;
                    entitytype.CreateOrModifyDate = DateTime.Now;
                    typesRepository.Add(entitytype);
                    return true;
                }
            }
            else
            {
                return false;
            }


        }

        public bool DeleteType(int Id)
        {
            //Daha sonradan ortak tablolardan silinecek.
            if (Id != default(int))
            {
                var entityTypes = typesRepository.FindID(Id);
                if (entityTypes == null)
                {
                    return false;
                }
                entityTypes.Status = 0;
                entityTypes.CreateOrModifyDate = DateTime.Now;
                typesRepository.Delete(entityTypes);
                return true;
            }
            else
            {
                //Id değeri boş gönderilemez!
                return false;
            }
        }

        #endregion

        #region Books Service

        public List<Books> GetAllBooks()
        {
            var result = booksRepository.GetAll();
            return result;
        }
        public List<ViewBooks> GetAllLibraryBooks()
        {
            var result = booksRepository.GetAllLibraryBooks();
            return result;
        }

        public List<ViewBooks> GetAllNotLibraryBooks()
        {
            var result = booksRepository.GetAllNotLibraryBooks();
            return result;
        }

        public List<ViewBooks> GetAllViewBooks()
        {
            var result = booksRepository.GetAllView();
            return result;
        }

        public bool CreateOrUpdateBook(Books books)
        {

            if (books != null)
            {
                if (books.ID != default(int))
                {
                    var updatebooks = booksRepository.FindID(books.ID);
                    if (updatebooks == null)
                    {
                        return false;
                    }
                    updatebooks.Name = books.Name;
                    updatebooks.AuthorID = books.AuthorID;
                    updatebooks.PublisherID = books.PublisherID;
                    updatebooks.TypeID = books.TypeID;
                    updatebooks.NumberOfPage = books.NumberOfPage;
                    updatebooks.YearOfPublic = books.YearOfPublic;
                    updatebooks.Image = books.Image;
                    updatebooks.Status = 1;
                    updatebooks.CreateOrModifyDate = DateTime.Now;
                    booksRepository.Update(updatebooks);
                    return true;

                }
                else
                {
                    var entitybook = books;
                    entitybook.Status = 1;
                    entitybook.CreateOrModifyDate = DateTime.Now;
                    booksRepository.Add(entitybook);
                    return true;
                }
            }
            else
            {
                return false;
            }


        }

        public bool DeleteBook(int Id)
        {
            //Daha sonradan ortak tablolardan silinecek.
            if (Id != default(int))
            {
                var entityBooks = booksRepository.FindID(Id);
                if (entityBooks == null)
                {
                    return false;
                }
                entityBooks.Status = 0;
                entityBooks.CreateOrModifyDate = DateTime.Now;
                booksRepository.Delete(entityBooks);
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

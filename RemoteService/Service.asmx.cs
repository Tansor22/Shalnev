using Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RemoteService
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        private BooksLogic booksLogic;
        private AuditoriesLogic auditoriesLogic;
        private AuthorsLogic authorsLogic;
        private CategoriesLogic categoriesLogic;
        private PublishersLogic publishersLogic;
        public Service()
        {
            string cns = ConfigurationManager.ConnectionStrings["BooksConnectionString"].ConnectionString;
            Helper.console(cns);
            AbstractConnection c = ConnectionFactory.getConnection(DataProvider.SqlServer, cns);

            booksLogic = new BooksLogic(c);
            auditoriesLogic = new AuditoriesLogic(c);
            authorsLogic = new AuthorsLogic(c);
            categoriesLogic = new CategoriesLogic(c);
            publishersLogic = new PublishersLogic(c);
        }
        private BooksDataSet wrapWithDataSet(DataRow row) {
            BooksDataSet output = new BooksDataSet();
            output.Tables[row.Table.TableName].ImportRow(row);
            return output;
        }
        // START BOOKS
        [WebMethod]
        public BooksDataSet GetAllBooks()
        {
            return booksLogic.getBooks();
        }
        [WebMethod]
        public BooksDataSet GetAllBooksDirectlyFromDB()
        {
            return booksLogic.getBooksDirectlyFromDb();
        }
        [WebMethod]
        public BooksDataSet NewBooksRow()
        {
            return wrapWithDataSet(booksLogic.NewBooksRow());  
        }
        [WebMethod]
        public bool DeleteBookWithID(int id)
        {
            return booksLogic.DeleteBookWithID(id);
        }
        [WebMethod]
        public BooksDataSet getBookByID(int id)
        {
            return wrapWithDataSet(booksLogic.getBookByID(id));
        }
        [WebMethod]
        public bool AddBook(BooksDataSet row)
        {
            return booksLogic.AddBook(row.Books[0]);
        }
        [WebMethod]
        public bool UpdateBook(int id, BooksDataSet row)
        {
            return booksLogic.UpdateBook(id, row.Books[0]);
        }
        [WebMethod]
        public bool UpdateBooksTable()
        {
            return booksLogic.UpdateDBWithCache();
        }

        // END BOOKS

        // START AUTHORS
        [WebMethod]
        public BooksDataSet GetAllAuthors()
        {
            return authorsLogic.getAuthors();
        }
        [WebMethod]
        public BooksDataSet GetAllAuthorsDirectlyFromDB()
        {
            return authorsLogic.getAuthorsDirectlyFromDb();
        }
        [WebMethod]
        public BooksDataSet NewAuthorsRow()
        {
            return wrapWithDataSet( authorsLogic.NewAuthorsRow());
        }
        [WebMethod]
        public bool DeleteAuthorWithID(int id)
        {
            return authorsLogic.DeleteAuthorWithID(id);
        }
        [WebMethod]
        public BooksDataSet getAuthorByID(int id)
        {
            return wrapWithDataSet( authorsLogic.getAuthorByID(id));
        }
        [WebMethod]
        public bool UpdateAuthor(int id, BooksDataSet row)
        {
            return authorsLogic.UpdateAuthor(id, row.Authors[0]);
        }
        [WebMethod]
        public bool UpdateAuthorsTable()
        {
            return authorsLogic.UpdateDBWithCache();
        }
        // END AUTHORS

        // START PUBLISHERS
        [WebMethod]
        public BooksDataSet GetAllPublishers()
        {
            return publishersLogic.getPublishers();
        }
        [WebMethod]
        public BooksDataSet GetAllPublishersDirectlyFromDB()
        {
            return publishersLogic.getPublishersDirectlyFromDb();
        }
        [WebMethod]
        public BooksDataSet NewPublishersRow()
        {
            return wrapWithDataSet( publishersLogic.NewPublishersRow());
        }
        [WebMethod]
        public bool DeletePublisherWithID(int id)
        {
            return publishersLogic.DeletePublisherWithID(id);
        }
        [WebMethod]
        public BooksDataSet getPublisherByID(int id)
        {
            return wrapWithDataSet( publishersLogic.getPublisherByID(id));
        }
        [WebMethod]
        public bool UpdatePublisher(int id, BooksDataSet row)
        {
            return publishersLogic.UpdatePublisher(id, row.Publishers[0]);
        }
        [WebMethod]
        public bool UpdatePublishersTable()
        {
            return publishersLogic.UpdateDBWithCache();
        }
        // END PUBLISHERS

        // START AUDITORIES
        [WebMethod]
        public BooksDataSet GetAllAuditories()
        {
            return auditoriesLogic.GetAuditories();
        }
        [WebMethod]
        public BooksDataSet GetAllAuditorsDirectlyFromDB()
        {
            return auditoriesLogic.GetAuditoriesDirectlyFromDb();
        }
        [WebMethod]
        public BooksDataSet NewAuditoriesRow()
        {
            return wrapWithDataSet( auditoriesLogic.NewAuditoriesRow());
        }
        [WebMethod]
        public bool DeleteAuditoryWithID(int id)
        {
            return auditoriesLogic.DeleteAuditoryWithID(id);
        }
        [WebMethod]
        public BooksDataSet getAuditoryById(int id)
        {
            return wrapWithDataSet( auditoriesLogic.getAuditoryByID(id));
        }
        [WebMethod]
        public bool UpdateAuditory(int id, BooksDataSet row)
        {
            return auditoriesLogic.UpdateAuditory(id, row.Auditories[0]);
        }
        [WebMethod]
        public bool UpdateAuditoriesTable()
        {
            return auditoriesLogic.UpdateDBWithCache();
        }
        // END AUDITORIES

        // START CATEGORIES
        [WebMethod]
        public BooksDataSet GetAllCategories()
        {
            return categoriesLogic.GetCategories();
        }
        [WebMethod]
        public BooksDataSet GetAllCategoriesDirectlyFromDB()
        {
            return categoriesLogic.GetCategoriesDirectlyFromDb();
        }
        [WebMethod]
        public BooksDataSet NewCategoriesRow()
        {
            return wrapWithDataSet( categoriesLogic.NewCategoriesRow());
        }
        [WebMethod]
        public bool DeleteCategoryWithID(int id)
        {
            return categoriesLogic.DeleteCategoryWithID(id);
        }
        [WebMethod]
        public BooksDataSet getCategoryById(int id)
        {
            return wrapWithDataSet(categoriesLogic.getCategoryByID(id));
        }
        [WebMethod]
        public bool UpdateCategory(int id, BooksDataSet row)
        {
            return categoriesLogic.UpdateCategory(id, row.Categories[0]);
        }
        [WebMethod]
        public bool UpdateCategoriesTable()
        {
            return categoriesLogic.UpdateDBWithCache();
        }
        // END CATEGORIES

        //OTHER
        [WebMethod]
        public bool UpdateWithCache(BooksDataSet cache)
        {
            return categoriesLogic.UpdateDBWithCache(cache);
        }
    }
}

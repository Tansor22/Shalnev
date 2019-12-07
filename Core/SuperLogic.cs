using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class SuperLogic
    {
        private AbstractConnection _c;
        public AbstractConnection Connection
        {
            get
            {
                return _c;
            }
        }

        private BooksLogic _booksLogic;
        public BooksLogic Books {
            get
            {
                if (_booksLogic == null)
                    _booksLogic = new BooksLogic(Connection);
                return _booksLogic;
            }
        }
        private AuthorsLogic _authorsLogic;
        public AuthorsLogic Authors
        {
            get
            {
                if (_authorsLogic == null)
                    _authorsLogic = new AuthorsLogic(Connection);
                return _authorsLogic;
            }
        }
        private CategoriesLogic _categoriesLogic;
        public CategoriesLogic Categories
        {
            get
            {
                if (_categoriesLogic == null)
                    _categoriesLogic = new CategoriesLogic(Connection);
                return _categoriesLogic;
            }
        }
        private AuditoriesLogic _auditoriesLogic;
        public AuditoriesLogic Auditories
        {
            get
            {
                if (_auditoriesLogic == null)
                    _auditoriesLogic = new AuditoriesLogic(Connection);
                return _auditoriesLogic;
            }
        }
        private PublishersLogic _publishersLogic;
        public PublishersLogic Publishers
        {
            get
            {
                if (_publishersLogic == null)
                    _publishersLogic = new PublishersLogic(Connection);
                return _publishersLogic;
            }
        }
    }
}

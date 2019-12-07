using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;
using Labi.RemoteServiceReference;

namespace Labi
{
    public partial class MainForm : Form
    {
        private ServiceSoapClient cli = new ServiceSoapClient();
        BooksDataSet booksCache;
        BooksDataSet authorsCache;
        BooksDataSet auditoriesCache;
        BooksDataSet publishersCache;
        BooksDataSet categoriesCache;

        BooksDataSet allTablesCache;

        public MainForm()  
        {            
            InitializeComponent();
          
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            booksCache = cli.GetAllBooks();
            authorsCache = cli.GetAllAuthors();
            auditoriesCache = cli.GetAllAuditories();
            publishersCache = cli.GetAllPublishers();
            categoriesCache = cli.GetAllCategories();
            //Merging
            allTablesCache = new BooksDataSet();
            allTablesCache.Merge(booksCache);
            allTablesCache.Merge(authorsCache);
            allTablesCache.Merge(auditoriesCache);
            allTablesCache.Merge(publishersCache);
            allTablesCache.Merge(categoriesCache);

            BooksGridView.DataSource = allTablesCache.Books;
            foreach (DataGridViewColumn column in BooksGridView.Columns) {
                if (column.Name == "title") {
                    column.HeaderText = "Название";
                }
                else if (column.Name == "price")
                {
                    column.HeaderText = "Цена";
                } else { column.Visible = false; }
            }
            BooksBindingSource.DataSource = BooksGridView.DataSource;
            
            BooksGridView.ReadOnly = true;
            BooksBindingNavigator.BindingSource = BooksBindingSource;


            CategoriesComboBox.DataSource = allTablesCache.Categories;
            CategoriesComboBox.DisplayMember = "name";
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

            // DEPRECATED! cli.UpdateWithCache(allTablesCache);
        }
    }
}

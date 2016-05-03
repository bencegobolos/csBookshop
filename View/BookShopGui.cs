using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BookShop.Controller;
using BookShop.Model;
using BookShop.View.Dialogs;

namespace BookShop.View
{
    /// <summary>
    /// A BookShop alkalmaz�s f� ablaka.
    /// <para>
    /// A "partial" kulcssz� azt jelzi, hogy az oszt�ly k�dja t�bb f�jlban is szerepel.
    /// </para>
    /// </summary>
    public partial class BookShopGui : Form
    {
        #region mez�k
        private BookShopController m_controller;
        #endregion


        #region tulajdons�gok
        /// <summary>
        /// Visszaadja vagy be�ll�tja a <see cref="BookShopController"/> objektumot.
        /// </summary>
        public BookShopController Controller
        {
            get { return m_controller; }
            set { m_controller = value; }
        }
        #endregion


        #region konstruktor
        /// <summary>
        /// L�trehozza a <see cref="BookShopController"/> oszt�ly egy �j p�ld�ny�t.
        /// </summary>
        public BookShopGui()
        {
            InitializeComponent();
        }
        #endregion


        #region esem�nykezel�k
        /// <summary>
        /// A <see cref="newCustomerToolStripMenuItem"/> men�pont kiv�laszt�sakor
        /// l�trehozza �s megjelen�ti a <see cref="AddCustomerDialog"/> dial�gus
        /// ablakot �j �gyf�l l�trehoz�s�ra.
        /// </summary>
        /// <param name="sender">Az esem�ny k�ld�je.</param>
        /// <param name="e">Az esem�ny argumentumai.</param>
        private void NewCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* �gyf�l hozz�ad�s�ra szolg�l� dial�gus ablak p�ld�nyos�t�sa �s megjelen�t�se.
             * "ShowDialog()" h�v�sakor tegy�k a dial�gusablakot "using" blokkba hogy
             * biztos�tsuk az er�forr�sok felszabad�t�s�t, mert ebben az eseben nem
             * h�v�dik meg automatikusan a "Dispose()" met�dus a dial�gusablak bez�r�sakor.
             * */
            using (AddCustomerDialog dialog = new AddCustomerDialog(this))
            {
                dialog.ShowDialog();
            }
        }


        /// <summary>
        /// A <see cref="buyBookToolStripMenuItem"/> men�pont kiv�laszt�sakor
        /// l�trehozza �s megjelen�ti a <see cref="BuyBookDialog"/> dial�gus
        /// ablakot k�nyv v�s�rl�s�ra.
        /// </summary>
        /// <param name="sender">Az esem�ny k�ld�je.</param>
        /// <param name="e">Az esem�ny argumentumai.</param>
        private void BuyBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BuyBookDialog dialog = new BuyBookDialog(this))
            {
                dialog.ShowDialog();
            }
        }


        /// <summary>
        /// A  <see cref="listCustomersToolStripMenuItem"/> men�pont kiv�laszt�sakor
        /// lekr�dezi a v�s�rl�k list�j�t, �s megjelen�ti a <see cref="booksDataGridView"/>
        /// t�bl�zatban.
        /// </summary>
        /// <param name="sender">Az esem�ny k�ld�je.</param>
        /// <param name="e">Az esem�ny argumentumai.</param>
        private void listAvailableBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<Book> books = m_controller.GetAvailableBookList();

            // ki�rjuk konzolra
            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }

            // konvert�ljuk a forr�st a grid-nek megfelel�re
            if (!(books is IList<Book>))
            {
                // A "List<T>" oszt�ly megval�s�tja az "IList<T>" interface-t.
                books = new List<Book>(books);
            }

            // megjelen�t�s
            booksDataGridView.DataSource = null;
            booksDataGridView.DataSource = books;
            booksDataGridView.Visible = true;
        }


        /// <summary>
        /// Az <see cref="exitToolStripMenuItem"/> men�pont kiv�laszt�sakor kil�p�nk
        /// az alkalmaz�sb�l.
        /// </summary>
        /// <param name="sender">Az esem�ny k�ld�je.</param>
        /// <param name="e">Az esem�ny argumentumai.</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Az alkalmaz�s bez�r�sa.
            Application.Exit();
        }
        #endregion

        private void addCdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AddCdDialog dialog = new AddCdDialog(this))
            {
                dialog.ShowDialog();
            }
        }

        private void listCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listCdsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<Cd> cds = m_controller.GetAvailableCdList();

            // ki�rjuk konzolra
            foreach (Cd cd in cds)
            {
                Console.WriteLine(cd);
            }

            // konvert�ljuk a forr�st a grid-nek megfelel�re
            if (!(cds is IList<Cd>))
            {
                // A "List<T>" oszt�ly megval�s�tja az "IList<T>" interface-t.
                cds = new List<Cd>(cds);
            }

            // megjelen�t�s
            booksDataGridView.DataSource = null;
            booksDataGridView.DataSource = cds;
            booksDataGridView.Visible = true;
        }
    }
}

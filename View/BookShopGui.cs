using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BookShop.Controller;
using BookShop.Model;
using BookShop.View.Dialogs;

namespace BookShop.View
{
    /// <summary>
    /// A BookShop alkalmazás fõ ablaka.
    /// <para>
    /// A "partial" kulcsszó azt jelzi, hogy az osztály kódja több fájlban is szerepel.
    /// </para>
    /// </summary>
    public partial class BookShopGui : Form
    {
        #region mezõk
        private BookShopController m_controller;
        #endregion


        #region tulajdonságok
        /// <summary>
        /// Visszaadja vagy beállítja a <see cref="BookShopController"/> objektumot.
        /// </summary>
        public BookShopController Controller
        {
            get { return m_controller; }
            set { m_controller = value; }
        }
        #endregion


        #region konstruktor
        /// <summary>
        /// Létrehozza a <see cref="BookShopController"/> osztály egy új példányát.
        /// </summary>
        public BookShopGui()
        {
            InitializeComponent();
        }
        #endregion


        #region eseménykezelõk
        /// <summary>
        /// A <see cref="newCustomerToolStripMenuItem"/> menüpont kiválasztásakor
        /// létrehozza és megjeleníti a <see cref="AddCustomerDialog"/> dialógus
        /// ablakot új ügyfél létrehozására.
        /// </summary>
        /// <param name="sender">Az esemény küldöje.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void NewCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Ügyfél hozzáadására szolgáló dialógus ablak példányosítása és megjelenítése.
             * "ShowDialog()" hívásakor tegyük a dialógusablakot "using" blokkba hogy
             * biztosítsuk az erõforrások felszabadítását, mert ebben az eseben nem
             * hívódik meg automatikusan a "Dispose()" metódus a dialógusablak bezárásakor.
             * */
            using (AddCustomerDialog dialog = new AddCustomerDialog(this))
            {
                dialog.ShowDialog();
            }
        }


        /// <summary>
        /// A <see cref="buyBookToolStripMenuItem"/> menüpont kiválasztásakor
        /// létrehozza és megjeleníti a <see cref="BuyBookDialog"/> dialógus
        /// ablakot könyv vásárlására.
        /// </summary>
        /// <param name="sender">Az esemény küldöje.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void BuyBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BuyBookDialog dialog = new BuyBookDialog(this))
            {
                dialog.ShowDialog();
            }
        }


        /// <summary>
        /// A  <see cref="listCustomersToolStripMenuItem"/> menüpont kiválasztásakor
        /// lekrédezi a vásárlók listáját, és megjeleníti a <see cref="booksDataGridView"/>
        /// táblázatban.
        /// </summary>
        /// <param name="sender">Az esemény küldöje.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void listAvailableBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<Book> books = m_controller.GetAvailableBookList();

            // kiírjuk konzolra
            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }

            // konvertáljuk a forrást a grid-nek megfelelõre
            if (!(books is IList<Book>))
            {
                // A "List<T>" osztály megvalósítja az "IList<T>" interface-t.
                books = new List<Book>(books);
            }

            // megjelenítés
            booksDataGridView.DataSource = null;
            booksDataGridView.DataSource = books;
            booksDataGridView.Visible = true;
        }


        /// <summary>
        /// Az <see cref="exitToolStripMenuItem"/> menûpont kiválasztásakor kilépünk
        /// az alkalmazásból.
        /// </summary>
        /// <param name="sender">Az esemény küldõje.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Az alkalmazás bezárása.
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

            // kiírjuk konzolra
            foreach (Cd cd in cds)
            {
                Console.WriteLine(cd);
            }

            // konvertáljuk a forrást a grid-nek megfelelõre
            if (!(cds is IList<Cd>))
            {
                // A "List<T>" osztály megvalósítja az "IList<T>" interface-t.
                cds = new List<Cd>(cds);
            }

            // megjelenítés
            booksDataGridView.DataSource = null;
            booksDataGridView.DataSource = cds;
            booksDataGridView.Visible = true;
        }
    }
}

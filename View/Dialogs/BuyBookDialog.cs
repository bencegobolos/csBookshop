using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BookShop.Model;
using BookShop.Resources;

namespace BookShop.View.Dialogs
{
    /// <summary>
    /// Az osztály az új könyv vásárlását valósítja meg.
    /// </summary>
    public partial class BuyBookDialog : Form
    {
        #region konstansok
        // kategóriák a Category lenyíló mezõ feltöltéséhez
        private readonly String[] categories = {
            StringConstants.Category_Drama,
            StringConstants.Category_Romantic,
            StringConstants.Category_Scifi,
            StringConstants.Category_Horror,
            StringConstants.Category_Fantasy,
            StringConstants.Category_Historic,
            StringConstants.Category_Scientific,
            StringConstants.Category_Classic,
            StringConstants.Category_Children
        };
        #endregion

        #region mezõk
        private BookShopGui gui;
        #endregion

        #region konstruktorok
        public BuyBookDialog(BookShopGui gui)
        {
            this.gui = gui;

            InitializeComponent();

            // lásd: AddCustomerDialog hasonló sorának kommentje
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // feltöltjük az elõbb definiált értékekkel a lenyíló mezõt
            categoryComboBox.Items.AddRange(categories);

            // Az AddCustomerDialog esetén számot írtunk ide, ez kevésbé
            // érzékeny a hibákra. Ha megváltozik a kívánt elem sorszáma,
            // akkor itt nem kell hozzáigazítani.
            categoryComboBox.SelectedItem = StringConstants.Category_Scientific;
        }
        #endregion

        #region eseménykezelõk
        private void buttonOk_Click(object sender, EventArgs e)
        {
            /* Az AddCustomerDialog-ban fordítva van az if két ága. Elõbb van a
             * jelentésében pozitív, majd a negatív ág.
             * Így viszont:
             *   - Jobban látszik, hogy azonnal vége a folyamatnak validációs
             *     probléma esetén.
             *   - Lapossabb a vezérlési hierarchia, kevesebb az egymásba ágyazás.
             * */
            if (!ValidateFields())
            {
                MessageBox.Show(
                    this,
                    StringConstants.AddBookDialog_ErrorMessage_Validation_Message,
                    StringConstants.AddBookDialog_ErrorMessage_Validation_Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            /* C# 3.0 újdonság: object initializer
             * Vesd össze hasonló AddCustomerDialog részlettel!
             * Kompaktabb, mert nem kell leírni a változónevet minden egyes
             * property beállításához.
             * Default konstruktor hívásához a '()' sem kell.
             * */

            Book book = new Book
            {
                Author = authorTextBox.Text,
                Title = titleTextBox.Text,
                Year = (int)yearNumericUpDown.Value,
                Category = (string)categoryComboBox.SelectedItem,
                Price = Convert.ToDouble(priceTextBox.Text),
                Pieces = (int)pieceNumericUpDown.Value
            };

            bool isBookBought = gui.Controller.BuyBook(book);

            /* Itt is a jelentésében negatív if ágat vesszük elõre.
             * */

            if (!isBookBought)
            {
                MessageBox.Show(
                    this,
                    StringConstants.AddBookDialog_ErrorMessage_BuyBook_Message,
                    StringConstants.AddBookDialog_ErrorMessage_BuyBook_Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Close nem szükséges, lásd AddCustomerDialog.
            this.DialogResult = DialogResult.OK;
            //this.Close();
        }
        #endregion


        #region validáció
        /// <summary>
        /// Validálja a beviteli mezõket.
        /// </summary>
        /// <returns>Igaz, ha a bevitt értékek érvényesek, egyébként hamis.</returns>
        private bool ValidateFields()
        {
            /* Minden validációs szabályt külön vizsgálunk.
             * (Jelen egyszerû helyzetben lehetne egyben.)
             * Tfh. van egy specifikációnk. Lásd lent "Spec vN, x.y.z".
             * */

            // Spec v10, 2.2.2: szerzõ ellenõrzése
            if (string.IsNullOrEmpty(authorTextBox.Text))
            {
                return false;
            }

            // Spec v10, 2.2.3: cím ellenõrzése
            if (string.IsNullOrEmpty(titleTextBox.Text))
            {
                return false;
            }

            // Validáljuk, hogy az ár valóban double-e.
            //   - Pl. magyar lokál esetén a '2.34' nem megfelelõ, mivel magyarban
            //     a tizedeselválasztó a vesszõ
            //   - A Parse-szal ellentétben a TryParse-hoz nem kell try-catch,
            //   - bool-t ad vissza
            //   - Az out-tal kell megmondani, hogy ez egy kimeneti paraméter
            //     mind függvénydefinícióban, mind híváskor ki kell rakni
            //       - nincs semmi -> bementi paraméter
            //       - ref -> be- és kimeneti paraméter
            //       - out -> kimeneti paraméter
            //     Lásd: érték típusok érték szerint másolódnak
            //     Az out azért kell, hogy visszakapjuk az értéket
            //     Kvázi C++ referencia szerinti paraméterátadás (double& price).
            double price;

            if (!double.TryParse(priceTextBox.Text, out price))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
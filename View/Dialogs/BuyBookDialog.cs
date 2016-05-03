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
    /// Az oszt�ly az �j k�nyv v�s�rl�s�t val�s�tja meg.
    /// </summary>
    public partial class BuyBookDialog : Form
    {
        #region konstansok
        // kateg�ri�k a Category leny�l� mez� felt�lt�s�hez
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

        #region mez�k
        private BookShopGui gui;
        #endregion

        #region konstruktorok
        public BuyBookDialog(BookShopGui gui)
        {
            this.gui = gui;

            InitializeComponent();

            // l�sd: AddCustomerDialog hasonl� sor�nak kommentje
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // felt�ltj�k az el�bb defini�lt �rt�kekkel a leny�l� mez�t
            categoryComboBox.Items.AddRange(categories);

            // Az AddCustomerDialog eset�n sz�mot �rtunk ide, ez kev�sb�
            // �rz�keny a hib�kra. Ha megv�ltozik a k�v�nt elem sorsz�ma,
            // akkor itt nem kell hozz�igaz�tani.
            categoryComboBox.SelectedItem = StringConstants.Category_Scientific;
        }
        #endregion

        #region esem�nykezel�k
        private void buttonOk_Click(object sender, EventArgs e)
        {
            /* Az AddCustomerDialog-ban ford�tva van az if k�t �ga. El�bb van a
             * jelent�s�ben pozit�v, majd a negat�v �g.
             * �gy viszont:
             *   - Jobban l�tszik, hogy azonnal v�ge a folyamatnak valid�ci�s
             *     probl�ma eset�n.
             *   - Lapossabb a vez�rl�si hierarchia, kevesebb az egym�sba �gyaz�s.
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

            /* C# 3.0 �jdons�g: object initializer
             * Vesd �ssze hasonl� AddCustomerDialog r�szlettel!
             * Kompaktabb, mert nem kell le�rni a v�ltoz�nevet minden egyes
             * property be�ll�t�s�hoz.
             * Default konstruktor h�v�s�hoz a '()' sem kell.
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

            /* Itt is a jelent�s�ben negat�v if �gat vessz�k el�re.
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

            // Close nem sz�ks�ges, l�sd AddCustomerDialog.
            this.DialogResult = DialogResult.OK;
            //this.Close();
        }
        #endregion


        #region valid�ci�
        /// <summary>
        /// Valid�lja a beviteli mez�ket.
        /// </summary>
        /// <returns>Igaz, ha a bevitt �rt�kek �rv�nyesek, egy�bk�nt hamis.</returns>
        private bool ValidateFields()
        {
            /* Minden valid�ci�s szab�lyt k�l�n vizsg�lunk.
             * (Jelen egyszer� helyzetben lehetne egyben.)
             * Tfh. van egy specifik�ci�nk. L�sd lent "Spec vN, x.y.z".
             * */

            // Spec v10, 2.2.2: szerz� ellen�rz�se
            if (string.IsNullOrEmpty(authorTextBox.Text))
            {
                return false;
            }

            // Spec v10, 2.2.3: c�m ellen�rz�se
            if (string.IsNullOrEmpty(titleTextBox.Text))
            {
                return false;
            }

            // Valid�ljuk, hogy az �r val�ban double-e.
            //   - Pl. magyar lok�l eset�n a '2.34' nem megfelel�, mivel magyarban
            //     a tizedeselv�laszt� a vessz�
            //   - A Parse-szal ellent�tben a TryParse-hoz nem kell try-catch,
            //   - bool-t ad vissza
            //   - Az out-tal kell megmondani, hogy ez egy kimeneti param�ter
            //     mind f�ggv�nydefin�ci�ban, mind h�v�skor ki kell rakni
            //       - nincs semmi -> bementi param�ter
            //       - ref -> be- �s kimeneti param�ter
            //       - out -> kimeneti param�ter
            //     L�sd: �rt�k t�pusok �rt�k szerint m�sol�dnak
            //     Az out az�rt kell, hogy visszakapjuk az �rt�ket
            //     Kv�zi C++ referencia szerinti param�ter�tad�s (double& price).
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
using System;
using System.Windows.Forms;

using BookShop.Model;
using BookShop.Resources;

namespace BookShop.View.Dialogs
{
    /// <summary>
    /// Az osztály az új vásárló hozzáadását valósítja meg.
    /// </summary>
    public partial class AddCustomerDialog : Form
    {
        #region mezõk
        /* A fõ GUI-nk hozzárendelése a dialógusablakhoz, abban definiáltuk a Controller property-t
        /* és hogy ezt ne kelljen minden ablakban megtenni ezért felhasználjuk azt.
         * */
        private BookShopGui m_bookShopGui;
        #endregion


        #region konstruktorok
        /// <summary>
        /// Létrehozza az <see cref="AddCustomerDialog"/> osztály egy új példányát.
        /// </summary>
        /// <param name="bookShopGui">A <see cref="BookShopGui"/> osztály egy példánya.</param>
        public AddCustomerDialog(BookShopGui bookShopGui)
        {
            InitializeComponent();

            this.m_bookShopGui = bookShopGui;

            /*
             * Ezt a designerben szokás állítani, most azért tettük ide, hogy
             * jól látható legyen, hogy beállítása fontos
             * Feladata: csak kiválasztani lehet, újat beírni nem
             * Feladat: megnézni, hogy hogyan kell beállítani designerbõl
             * */
            qualificationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // A végzettségeket tartalmazó lenyíló menü elemeinek feltöltése.
            qualificationComboBox.Items.AddRange(new string[] {
                StringConstants.Qualification_University,
                StringConstants.Qualification_College,
                StringConstants.Qualification_HighSchool,
                StringConstants.Qualification_ElementarySchool
            });

            /* Az alapértelmezetten kiválasztott elem indexének megadása.
             * FONTOS: az indexelés 0-tól kezdõdik!
             * */
            qualificationComboBox.SelectedIndex = 2;
        }
        #endregion


        #region eseménykezelõk
        /// <summary>
        ///  A <see cref="okButton"/> gomb kiválasztásakor menti az új vásárlót.
        /// </summary>
        /// <param name="sender">Az esemény küldõje.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                // Új ügyfél objektum példányosítása, tulajdonságainak beállítása.
                Customer customer = new Customer();
                customer.Name = nameTextBox.Text;
                customer.Age = (int)ageNumericUpDown.Value;
                customer.Female = femaleRadioButton.Checked;
                customer.Grantee = granteeCheckBox.Checked;

                /* FONTOS:
                 * Helytelen lenne:
                 * customer.Qualification = qualificationComboBox.SelectedItem.ToString();
                 * Ha a kód megváltozik és nem string-eket teszünk a combo boxba,
                 * hanem mondjuk Qualification model osztályokat (adatbázisból id + név),
                 * akkor a ToString lefut, csak az érték nem a kívánt Qualification
                 * objektum lesz, hanem "BookShop.Model.Qualification".
                 * Megoldás: castolni kell a szükséges típusra, ekkor Qualification-t
                 * nem fog tudni string-re castolni, egyértelmû lesz a hiba
                 * */
                customer.Qualification = (string)qualificationComboBox.SelectedItem;

                // Megkísérli elmenteni az új vásárlót.
                bool isNewCustomerSaved = m_bookShopGui.Controller.NewCustomer(customer);

                // Sikeresség ellenõrzése.
                if (isNewCustomerSaved)
                {
                    /* Sikeres mentés esetén beállítjuk a dialógusablak eredményét
                     * "DialogResult.OK"-ra, és bezárjuk az ablakot.
                     * FONTOS: a dialógusablak "DialogResult" tulajdonságát
                     * beállítva a dialógusablak bezáródik, nincs szükség a
                     * "Close()" metódus meghívására.
                     * */
                    this.DialogResult = DialogResult.OK;
                    //this.Close();
                }
                else
                {
                    // Sikertelen mentés esetén hibaüzenetet írunk ki.
                    MessageBox.Show(
                        StringConstants.AddCustomerDialog_ErrorMessage_CustomerAlreadyExists_Message,
                        StringConstants.AddCustomerDialog_ErrorMessage_CustomerAlreadyExists_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                /* A "MessageBox" osztály egyszerû üzenetablakok megjelenítését
                 * teszi lehetõvé.
                 * Fõbb fajtái: Exclamation, Error, Question, Warning, Information.
                 * */
                MessageBox.Show(
                    StringConstants.AddCustomerDialog_ErrorMessage_NameFieldEmpty_Message,
                    StringConstants.AddCustomerDialog_ErrorMessage_NameFieldEmpty_Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }


        /// <summary>
        /// Validálja a beviteli mezõket.
        /// </summary>
        /// <returns>Igaz, ha a bevitt értékek érvényesek, egyébként hamis.</returns>
        private bool ValidateFields()
        {
            bool rvIsValid = false;

            if (!string.IsNullOrEmpty(nameTextBox.Text))
            {
                rvIsValid = true;
            }

            return rvIsValid;
        }
        #endregion
    }
}

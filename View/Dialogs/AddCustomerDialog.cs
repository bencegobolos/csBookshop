using System;
using System.Windows.Forms;

using BookShop.Model;
using BookShop.Resources;

namespace BookShop.View.Dialogs
{
    /// <summary>
    /// Az oszt�ly az �j v�s�rl� hozz�ad�s�t val�s�tja meg.
    /// </summary>
    public partial class AddCustomerDialog : Form
    {
        #region mez�k
        /* A f� GUI-nk hozz�rendel�se a dial�gusablakhoz, abban defini�ltuk a Controller property-t
        /* �s hogy ezt ne kelljen minden ablakban megtenni ez�rt felhaszn�ljuk azt.
         * */
        private BookShopGui m_bookShopGui;
        #endregion


        #region konstruktorok
        /// <summary>
        /// L�trehozza az <see cref="AddCustomerDialog"/> oszt�ly egy �j p�ld�ny�t.
        /// </summary>
        /// <param name="bookShopGui">A <see cref="BookShopGui"/> oszt�ly egy p�ld�nya.</param>
        public AddCustomerDialog(BookShopGui bookShopGui)
        {
            InitializeComponent();

            this.m_bookShopGui = bookShopGui;

            /*
             * Ezt a designerben szok�s �ll�tani, most az�rt tett�k ide, hogy
             * j�l l�that� legyen, hogy be�ll�t�sa fontos
             * Feladata: csak kiv�lasztani lehet, �jat be�rni nem
             * Feladat: megn�zni, hogy hogyan kell be�ll�tani designerb�l
             * */
            qualificationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // A v�gzetts�geket tartalmaz� leny�l� men� elemeinek felt�lt�se.
            qualificationComboBox.Items.AddRange(new string[] {
                StringConstants.Qualification_University,
                StringConstants.Qualification_College,
                StringConstants.Qualification_HighSchool,
                StringConstants.Qualification_ElementarySchool
            });

            /* Az alap�rtelmezetten kiv�lasztott elem index�nek megad�sa.
             * FONTOS: az indexel�s 0-t�l kezd�dik!
             * */
            qualificationComboBox.SelectedIndex = 2;
        }
        #endregion


        #region esem�nykezel�k
        /// <summary>
        ///  A <see cref="okButton"/> gomb kiv�laszt�sakor menti az �j v�s�rl�t.
        /// </summary>
        /// <param name="sender">Az esem�ny k�ld�je.</param>
        /// <param name="e">Az esem�ny argumentumai.</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                // �j �gyf�l objektum p�ld�nyos�t�sa, tulajdons�gainak be�ll�t�sa.
                Customer customer = new Customer();
                customer.Name = nameTextBox.Text;
                customer.Age = (int)ageNumericUpDown.Value;
                customer.Female = femaleRadioButton.Checked;
                customer.Grantee = granteeCheckBox.Checked;

                /* FONTOS:
                 * Helytelen lenne:
                 * customer.Qualification = qualificationComboBox.SelectedItem.ToString();
                 * Ha a k�d megv�ltozik �s nem string-eket tesz�nk a combo boxba,
                 * hanem mondjuk Qualification model oszt�lyokat (adatb�zisb�l id + n�v),
                 * akkor a ToString lefut, csak az �rt�k nem a k�v�nt Qualification
                 * objektum lesz, hanem "BookShop.Model.Qualification".
                 * Megold�s: castolni kell a sz�ks�ges t�pusra, ekkor Qualification-t
                 * nem fog tudni string-re castolni, egy�rtelm� lesz a hiba
                 * */
                customer.Qualification = (string)qualificationComboBox.SelectedItem;

                // Megk�s�rli elmenteni az �j v�s�rl�t.
                bool isNewCustomerSaved = m_bookShopGui.Controller.NewCustomer(customer);

                // Sikeress�g ellen�rz�se.
                if (isNewCustomerSaved)
                {
                    /* Sikeres ment�s eset�n be�ll�tjuk a dial�gusablak eredm�ny�t
                     * "DialogResult.OK"-ra, �s bez�rjuk az ablakot.
                     * FONTOS: a dial�gusablak "DialogResult" tulajdons�g�t
                     * be�ll�tva a dial�gusablak bez�r�dik, nincs sz�ks�g a
                     * "Close()" met�dus megh�v�s�ra.
                     * */
                    this.DialogResult = DialogResult.OK;
                    //this.Close();
                }
                else
                {
                    // Sikertelen ment�s eset�n hiba�zenetet �runk ki.
                    MessageBox.Show(
                        StringConstants.AddCustomerDialog_ErrorMessage_CustomerAlreadyExists_Message,
                        StringConstants.AddCustomerDialog_ErrorMessage_CustomerAlreadyExists_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                /* A "MessageBox" oszt�ly egyszer� �zenetablakok megjelen�t�s�t
                 * teszi lehet�v�.
                 * F�bb fajt�i: Exclamation, Error, Question, Warning, Information.
                 * */
                MessageBox.Show(
                    StringConstants.AddCustomerDialog_ErrorMessage_NameFieldEmpty_Message,
                    StringConstants.AddCustomerDialog_ErrorMessage_NameFieldEmpty_Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }


        /// <summary>
        /// Valid�lja a beviteli mez�ket.
        /// </summary>
        /// <returns>Igaz, ha a bevitt �rt�kek �rv�nyesek, egy�bk�nt hamis.</returns>
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

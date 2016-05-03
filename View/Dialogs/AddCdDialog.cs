using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BookShop.Model;

namespace BookShop.View.Dialogs
{
    public partial class AddCdDialog : Form
    {
        private BookShopGui m_bookShopGui;

        public AddCdDialog(BookShopGui bookShopGui)
        {
            InitializeComponent();

            this.m_bookShopGui = bookShopGui;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                // Új ügyfél objektum példányosítása, tulajdonságainak beállítása.
                Cd cd = new Cd();
                cd.Artist = artistBox.Text;
                cd.Title = titleBox.Text;
                cd.Price = Convert.ToInt32(priceBox.Text);
                cd.Year = Convert.ToInt32(yearBox.Text);
                cd.Selection = selectionRadio.Checked;

                // Megkísérli elmenteni az új vásárlót.
                bool isNewCdSaved = m_bookShopGui.Controller.BuyCd(cd);

                // Sikeresség ellenőrzése.
                if (isNewCdSaved)
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
                        "Hey user!",
                        "Something went wrong!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                /* A "MessageBox" osztály egyszerű üzenetablakok megjelenítését
                 * teszi lehetővé.
                 * Főbb fajtái: Exclamation, Error, Question, Warning, Information.
                 * */
                MessageBox.Show(
                    "Artist and Title should not be empty!",
                    "Hey user!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private bool ValidateFields()
        {
            bool rvIsValid = false;

            if (!string.IsNullOrEmpty(artistBox.Text) ||
                !string.IsNullOrEmpty(titleBox.Text))
            {
                rvIsValid = true;
            }

            return rvIsValid;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}

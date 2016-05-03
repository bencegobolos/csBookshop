using System;
using System.Windows.Forms;

using BookShop.Controller;
using BookShop.Dal;
using BookShop.Model;
using BookShop.View;

namespace BookShop
{
    // Ez az oszt�ly automatikusan gener�l�dik ha Windows Application t�pus�
    // projektet hozunk l�tre a VS-ben. Feladata az alkalmaz�s elind�t�sa.
    static class Program
    {
        /// <summary>
        /// Az alkalmaz�s f� bel�p�si pontja.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enged�lyezi az oper�ci�s rendszer �ltal haszn�lt st�lus haszn�lat�t az alkalmaz�sban.
            Application.EnableVisualStyles();

            // Be�ll�tja az alap�rtelmezett sz�veg renderel�st.
            Application.SetCompatibleTextRenderingDefault(false);

            // L�trehozza a r�tegek p�ld�nyait �s be�ll�tja a sz�ks�ges referenci�kat.
            //IBookShopDao bookShopDAO = new BookShopDaoMem();
            IBookShopDao bookShopDAO = new BookShopDaoDb();
            BookShopGui bookShopGui = new BookShopGui();
            BookShopController bookShopController = new BookShopController();

            bookShopGui.Controller = bookShopController;
            bookShopController.BookShopDao = bookShopDAO;

            /* Elind�tja az alkalmaz�st �s az �gynevezett
             * "application message loop"-ot, ami a felhaszn�l�i akci�k
             * kezel�s��rt (eg�r-, billenty� esem�nyek) felel�s.
             * L�trehozza a BookShopGui oszt�ly egy p�ld�ny�t, mint az
             * alkalmaz�s f�ablaka, �s fel�ratkozik a "Closed' esem�nyre. Ha az
             * alkalmaz�s f�ablaka bez�r�dik, le�ll�tja az
             * "application message loop"-ot �s felszabad�tja az er�forr�sokat.
             * */
            Application.Run(bookShopGui);
        }
    }
}
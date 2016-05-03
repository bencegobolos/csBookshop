using System;
using System.Windows.Forms;

using BookShop.Controller;
using BookShop.Dal;
using BookShop.Model;
using BookShop.View;

namespace BookShop
{
    // Ez az osztály automatikusan generálódik ha Windows Application típusú
    // projektet hozunk létre a VS-ben. Feladata az alkalmazás elindítása.
    static class Program
    {
        /// <summary>
        /// Az alkalmazás fõ belépési pontja.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Engedélyezi az operációs rendszer által használt stílus használatát az alkalmazásban.
            Application.EnableVisualStyles();

            // Beállítja az alapértelmezett szöveg renderelést.
            Application.SetCompatibleTextRenderingDefault(false);

            // Létrehozza a rétegek példányait és beállítja a szükséges referenciákat.
            //IBookShopDao bookShopDAO = new BookShopDaoMem();
            IBookShopDao bookShopDAO = new BookShopDaoDb();
            BookShopGui bookShopGui = new BookShopGui();
            BookShopController bookShopController = new BookShopController();

            bookShopGui.Controller = bookShopController;
            bookShopController.BookShopDao = bookShopDAO;

            /* Elindítja az alkalmazást és az úgynevezett
             * "application message loop"-ot, ami a felhasználói akciók
             * kezeléséért (egér-, billentyû események) felelõs.
             * Létrehozza a BookShopGui osztály egy példányát, mint az
             * alkalmazás fõablaka, és felíratkozik a "Closed' eseményre. Ha az
             * alkalmazás fõablaka bezáródik, leállítja az
             * "application message loop"-ot és felszabadítja az erõforrásokat.
             * */
            Application.Run(bookShopGui);
        }
    }
}
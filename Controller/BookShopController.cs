using System.Collections.Generic;

using BookShop.Dal;
using BookShop.Model;

namespace BookShop.Controller
{
    /// <summary>
    /// Ez az osztály vezérli az egész programot, valamint a view és model csomagokat
    /// köti össze. Itt található az üzleti logika (business logic) is.
    /// </summary>
    public class BookShopController
    {
        #region mezõk
        private IBookShopDao m_dao;
        #endregion


        #region tulajdonságok
        /// <summary>
        /// Visszaadja vagy beállítja a <see cref="BookShopDao"/> objektumot.
        /// </summary>
        public IBookShopDao BookShopDao
        {
            get { return m_dao; }
            set { m_dao = value; }
        }
        #endregion


        #region customer metódusok
        /// <summary>
        /// Hozzáad egy <see cref="Customer"/> objektumot az adattárhoz.
        /// </summary>
        /// <param name="customer">A tárolandó <see cref="Customer"/>.</param>
        /// <returns>Igaz, ha sikeresen tárolva, egyébként hamis.</returns>
        public bool NewCustomer(Customer customer)
        {
            if (customer.Age < 14)
            {
                customer.Student = true;
            }
            else if (customer.Age > 62)
            {
                customer.Rented = true;
            }

            return m_dao.AddCustomer(customer);
        }


        /// <summary>
        /// Visszaadja a tárolt <see cref="Customer"/> példányokat.
        /// </summary>
        /// <returns>A tárolt <see cref="Customer"/> példányok kollekciója.</returns>
        public IEnumerable<Customer> GetCustomerList()
        {
            return m_dao.GetCustomers();
        }
        #endregion


        #region book metódusok
        /// <summary>
        /// Új könyv vásárlása.
        /// </summary>
        /// <param name="book">A megvásárlandó könyv.</param>
        /// <returns>Igaz, ha sikeresen megvásárolva, egyébként hamis.</returns>
        public bool BuyBook(Book book)
        {
            // 1900 elõtti könyveket antiknak állítjuk be
            book.Ancient = book.Year < 1900;

            return m_dao.AddOrUpdateBook(book);
        }

        // ZSOLT
        // nem kell a listája: Azon könyvek listájának lekérése -> Azon könyvek lekérése és GetAvailableBookList -> ListAvailableBooks (lásd fent dao vs. controller)

        /// <summary>
        /// Azon könyvek listájának lekérése amelybõl van eladható készlet. Csak azokat adja
        /// vissza ahol a készlet (Piece) legalább 1.
        /// </summary>
        /// <returns>eladható könyvek listája</returns>
        public IEnumerable<Book> GetAvailableBookList()
        {
            // csinálunk egy új kollekciót amibe belegyûjtjük a raktáron lévõ könyveket
            List<Book> availableBooks = new List<Book>();

            // végigmegyünk az összes könyvön
            foreach (Book book in m_dao.GetBooks())
            {
                // csak azok érdekelnek minket amibõl van készlet
                if (book.Pieces > 0)
                {
                    availableBooks.Add(book);
                }
            }

            return availableBooks;
        }
        #endregion
    }
}

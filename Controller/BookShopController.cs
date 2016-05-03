using System.Collections.Generic;

using BookShop.Dal;
using BookShop.Model;

namespace BookShop.Controller
{
    /// <summary>
    /// Ez az oszt�ly vez�rli az eg�sz programot, valamint a view �s model csomagokat
    /// k�ti �ssze. Itt tal�lhat� az �zleti logika (business logic) is.
    /// </summary>
    public class BookShopController
    {
        #region mez�k
        private IBookShopDao m_dao;
        #endregion


        #region tulajdons�gok
        /// <summary>
        /// Visszaadja vagy be�ll�tja a <see cref="BookShopDao"/> objektumot.
        /// </summary>
        public IBookShopDao BookShopDao
        {
            get { return m_dao; }
            set { m_dao = value; }
        }
        #endregion


        #region customer met�dusok
        /// <summary>
        /// Hozz�ad egy <see cref="Customer"/> objektumot az adatt�rhoz.
        /// </summary>
        /// <param name="customer">A t�roland� <see cref="Customer"/>.</param>
        /// <returns>Igaz, ha sikeresen t�rolva, egy�bk�nt hamis.</returns>
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
        /// Visszaadja a t�rolt <see cref="Customer"/> p�ld�nyokat.
        /// </summary>
        /// <returns>A t�rolt <see cref="Customer"/> p�ld�nyok kollekci�ja.</returns>
        public IEnumerable<Customer> GetCustomerList()
        {
            return m_dao.GetCustomers();
        }
        #endregion


        #region book met�dusok
        /// <summary>
        /// �j k�nyv v�s�rl�sa.
        /// </summary>
        /// <param name="book">A megv�s�rland� k�nyv.</param>
        /// <returns>Igaz, ha sikeresen megv�s�rolva, egy�bk�nt hamis.</returns>
        public bool BuyBook(Book book)
        {
            // 1900 el�tti k�nyveket antiknak �ll�tjuk be
            book.Ancient = book.Year < 1900;

            return m_dao.AddOrUpdateBook(book);
        }

        // ZSOLT
        // nem kell a list�ja: Azon k�nyvek list�j�nak lek�r�se -> Azon k�nyvek lek�r�se �s GetAvailableBookList -> ListAvailableBooks (l�sd fent dao vs. controller)

        /// <summary>
        /// Azon k�nyvek list�j�nak lek�r�se amelyb�l van eladhat� k�szlet. Csak azokat adja
        /// vissza ahol a k�szlet (Piece) legal�bb 1.
        /// </summary>
        /// <returns>eladhat� k�nyvek list�ja</returns>
        public IEnumerable<Book> GetAvailableBookList()
        {
            // csin�lunk egy �j kollekci�t amibe belegy�jtj�k a rakt�ron l�v� k�nyveket
            List<Book> availableBooks = new List<Book>();

            // v�gigmegy�nk az �sszes k�nyv�n
            foreach (Book book in m_dao.GetBooks())
            {
                // csak azok �rdekelnek minket amib�l van k�szlet
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

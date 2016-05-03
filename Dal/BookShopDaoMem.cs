using System.Collections.Generic;
using BookShop.Model;

namespace BookShop.Dal
{
    /// <summary>
    /// Ez az osztály az adatelérést szolgálja. Mivel nincs mögötte tényleges
    /// perzisztens tároló (pl. adatbázis), ezért csak memóriába tárolja az
    /// adatokat.
    /// </summary>
    public class BookShopDaoMem : IBookShopDao
    {
        #region mezõk
        private IList<Customer> m_customers = new List<Customer>();
        private IList<Book> m_books = new List<Book>();
        #endregion


        #region customer mûveletek
        /// <summary>
        /// Hozzáad egy <see cref="Customer"/> objektumot az adattárhoz.
        /// </summary>
        /// <param name="customer">A tárolandó <see cref="Customer"/>.</param>
        /// <returns>Igaz, ha sikeresen tárolva, egyébként hamis.</returns>
        public bool AddCustomer(Customer customer)
        {
            bool rvSucceed = false;

            /* Ha van már ilyen nevû ügyfél a memóriában, akkor nem tesszük bele.
             * Persze éles használat esetén nem a nevével szoktunk hivatkozni egy ügyfélre, hanem például
             * a személyi számával, de illusztráció céljából ez most nekünk megfelel.
             */
            if (CheckCustomerNameUnique(customer))
            {
                m_customers.Add(customer);

                rvSucceed = true;
            }

            return rvSucceed;
        }


        /* Megj.: az egyenlõség operátor C++ örökség alapján felül van definiálva a
         * "string" típusnál ezért (JAVA-bal ellentétben) érték és nem referencia szerint
         * hasonlítja össze az ezen típusú objektumokat.
         * */
        private bool CheckCustomerNameUnique(Customer newCustomer)
        {
            bool rvIsValid = true;

            foreach (Customer customer in m_customers)
            {
                /* Habár a "string" típus referencia típus, az "==" operátora felülírásra
                 * került a keretrendszerben, hogy a "String.Equals(String a, String b)"
                 * legyen meghívva.
                 * */
                if (customer.Name == newCustomer.Name)
                {
                    rvIsValid = false;
                    break;
                }
            }

            return rvIsValid;
        }


        /// <summary>
        /// Visszaadja a tárolt <see cref="Customer"/> példányokat.
        /// </summary>
        /// <returns>A tárolt <see cref="Customer"/> példányok kollekciója.</returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return m_customers;
        }
        #endregion


        #region book mûveletek
        /// <summary>
        /// Egy könyv hozzáadása az adattárhoz vagy frissítése az adattárban.
        /// táblához.
        /// </summary>
        /// <remarks>
        /// Ha már létezik a könyv, akkor eggyel növeli a <see cref="Book.Pieces"/>
        /// értékét, egyébként rögzíti a <see cref="Book"/> példányt.
        /// </remarks>
        /// <param name="book">A tárolandó vagy frissítendõ <see cref="Book"/>.</param>
        /// <returns>Igaz, ha sikeres a tárolás, egyébként hamis.</returns>
        public bool AddOrUpdateBook(Book book)
        {
            // megnézzük, hogy a könyv tárolt-e már
            Book storedBook = FindBookByAuthorTitleYear(book);

            if (storedBook != null)
            {
                // a könyv már létezik, növeljük a darabszámot
                storedBook.Pieces += book.Pieces;
            }
            else
            {
                // a könyv még nincs az adattárban, eltároljuk
                m_books.Add(book);
            }

            return true;
        }


        /// <summary>
        /// Megnézi hogy az adott könyv szerepel-e már az adattárban (szerzõ,
        /// cím és kiadási év alapján.
        /// </summary>
        /// <param name="book">A megkeresendõ <see cref="Book"/>.</param>
        /// <returns>
        /// Az adattárban lévõ <see cref="Book"/>, ha már létezik, egyébként null.
        /// </returns>
        private Book FindBookByAuthorTitleYear(Book book)
        {
            Book rvFound = null;

            foreach (Book storedBook in m_books)
            {
                if (storedBook.Author == book.Author &&
                    storedBook.Title == book.Title &&
                    storedBook.Year == book.Year)
                {
                    rvFound = storedBook;
                    break;
                }
            }

            return rvFound;
        }


        /// <summary>
        /// Visszaadja a tárolt <see cref="Book"/> példányokat
        /// </summary>
        /// <returns>A tárolt <see cref="Book"/> példányok.</returns>
        public IEnumerable<Book> GetBooks()
        {
            return m_books;
        }
        #endregion
    }
}

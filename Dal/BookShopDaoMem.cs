using System.Collections.Generic;
using BookShop.Model;

namespace BookShop.Dal
{
    /// <summary>
    /// Ez az oszt�ly az adatel�r�st szolg�lja. Mivel nincs m�g�tte t�nyleges
    /// perzisztens t�rol� (pl. adatb�zis), ez�rt csak mem�ri�ba t�rolja az
    /// adatokat.
    /// </summary>
    public class BookShopDaoMem : IBookShopDao
    {
        #region mez�k
        private IList<Customer> m_customers = new List<Customer>();
        private IList<Book> m_books = new List<Book>();
        #endregion


        #region customer m�veletek
        /// <summary>
        /// Hozz�ad egy <see cref="Customer"/> objektumot az adatt�rhoz.
        /// </summary>
        /// <param name="customer">A t�roland� <see cref="Customer"/>.</param>
        /// <returns>Igaz, ha sikeresen t�rolva, egy�bk�nt hamis.</returns>
        public bool AddCustomer(Customer customer)
        {
            bool rvSucceed = false;

            /* Ha van m�r ilyen nev� �gyf�l a mem�ri�ban, akkor nem tessz�k bele.
             * Persze �les haszn�lat eset�n nem a nev�vel szoktunk hivatkozni egy �gyf�lre, hanem p�ld�ul
             * a szem�lyi sz�m�val, de illusztr�ci� c�lj�b�l ez most nek�nk megfelel.
             */
            if (CheckCustomerNameUnique(customer))
            {
                m_customers.Add(customer);

                rvSucceed = true;
            }

            return rvSucceed;
        }


        /* Megj.: az egyenl�s�g oper�tor C++ �r�ks�g alapj�n fel�l van defini�lva a
         * "string" t�pusn�l ez�rt (JAVA-bal ellent�tben) �rt�k �s nem referencia szerint
         * hasonl�tja �ssze az ezen t�pus� objektumokat.
         * */
        private bool CheckCustomerNameUnique(Customer newCustomer)
        {
            bool rvIsValid = true;

            foreach (Customer customer in m_customers)
            {
                /* Hab�r a "string" t�pus referencia t�pus, az "==" oper�tora fel�l�r�sra
                 * ker�lt a keretrendszerben, hogy a "String.Equals(String a, String b)"
                 * legyen megh�vva.
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
        /// Visszaadja a t�rolt <see cref="Customer"/> p�ld�nyokat.
        /// </summary>
        /// <returns>A t�rolt <see cref="Customer"/> p�ld�nyok kollekci�ja.</returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return m_customers;
        }
        #endregion


        #region book m�veletek
        /// <summary>
        /// Egy k�nyv hozz�ad�sa az adatt�rhoz vagy friss�t�se az adatt�rban.
        /// t�bl�hoz.
        /// </summary>
        /// <remarks>
        /// Ha m�r l�tezik a k�nyv, akkor eggyel n�veli a <see cref="Book.Pieces"/>
        /// �rt�k�t, egy�bk�nt r�gz�ti a <see cref="Book"/> p�ld�nyt.
        /// </remarks>
        /// <param name="book">A t�roland� vagy friss�tend� <see cref="Book"/>.</param>
        /// <returns>Igaz, ha sikeres a t�rol�s, egy�bk�nt hamis.</returns>
        public bool AddOrUpdateBook(Book book)
        {
            // megn�zz�k, hogy a k�nyv t�rolt-e m�r
            Book storedBook = FindBookByAuthorTitleYear(book);

            if (storedBook != null)
            {
                // a k�nyv m�r l�tezik, n�velj�k a darabsz�mot
                storedBook.Pieces += book.Pieces;
            }
            else
            {
                // a k�nyv m�g nincs az adatt�rban, elt�roljuk
                m_books.Add(book);
            }

            return true;
        }


        /// <summary>
        /// Megn�zi hogy az adott k�nyv szerepel-e m�r az adatt�rban (szerz�,
        /// c�m �s kiad�si �v alapj�n.
        /// </summary>
        /// <param name="book">A megkeresend� <see cref="Book"/>.</param>
        /// <returns>
        /// Az adatt�rban l�v� <see cref="Book"/>, ha m�r l�tezik, egy�bk�nt null.
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
        /// Visszaadja a t�rolt <see cref="Book"/> p�ld�nyokat
        /// </summary>
        /// <returns>A t�rolt <see cref="Book"/> p�ld�nyok.</returns>
        public IEnumerable<Book> GetBooks()
        {
            return m_books;
        }
        #endregion
    }
}

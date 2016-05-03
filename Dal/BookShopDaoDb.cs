using System.Collections.Generic;
using System.Data.SQLite;
using BookShop.Model;
using System.Data;

namespace BookShop.Dal
{
    /// <summary>
    /// Ez az oszt�ly az SQLite adatb�zisb�l t�rt�n� adatel�r�st szolg�lja.
    /// </summary>
    public class BookShopDaoDb : IBookShopDao
    {
        #region statikus tagok
        /// <summary>
        /// Az adatb�zis SQLite specifikus connection stringje.
        /// </summary>
        private static readonly string s_connectionString = @"Data Source=..\..\..\..\..\kulso\db\bookshop.db;";
        #endregion


        #region customer m�veletek
        /// <summary>
        /// Hozz�ad egy <see cref="Customer"/> objektumot az adatt�rhoz.
        /// </summary>
        /// <param name="customer">A t�roland� <see cref="Customer"/>.</param>
        /// <returns>Igaz, ha sikeresen t�rolva, egy�bk�nt hamis.</returns>
        public bool AddCustomer(Customer customer)
        {
            bool rvSucceeded = false;

            // TODO implement�land�!

            return rvSucceeded;
        }


        /// <summary>
        /// Visszaadja a t�rolt <see cref="Customer"/> p�ld�nyokat.
        /// </summary>
        /// <returns>A t�rolt <see cref="Customer"/> p�ld�nyok kollekci�ja.</returns>
        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> rvCustomers = null;

            // TODO implement�land�!
            rvCustomers = new List<Customer>();

            return rvCustomers;
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
            bool rvSucceeded = false;

            using (SQLiteConnection conn = new SQLiteConnection(s_connectionString))
            {
                /* FONTOS
                 * M�r itt megnyitjuk a kapcsolatot, mert azt �jrahaszn�ljuk
                 * (�tadjuk) a FindBookByAuthorTitleYear-nak, hogy ne kelljen
                 * feleslegesen k�t kapcsolatot nyitni.
                 *   - Ha ezt k�s�bb tenn�nk, akkor a h�vott f�ggv�nybe is
                 *     kellene Open, illetve a k�s�bbre tett Open exception-t
                 *     dobna, mert m�r nyitva van a kapcsolat
                 *   - Ha csak a FindBookByAuthorTitleYear-ba tenn�nk Open-t,
                 *     akkor a f�ggv�ny esetleges t�bbi h�v�ja sem nyithatna.
                 *     Ha azok m�s met�dust is h�vnak, akkor azok sem
                 *     nyithatn�nak, de sz�ks�g�k lenne r�, nem tudn�nk
                 *     �r�ny�tani, hogy mely f�ggv�ny nyissa pontosan.
                 * Ez�rt mindig az a j�, ha az nyitja a kapcsolatot, aki l�trehozza.
                 * */
                conn.Open();

                // megn�zz�k, hogy a k�nyv t�rolt-e m�r
                Book storedBook = FindBookByAuthorTitleYear(conn, book);

                SQLiteCommand command = conn.CreateCommand();

                if (storedBook != null)
                {
                    // a k�nyv m�r l�tezik, n�velj�k a darabsz�mot
                    // a k�nyv m�r l�tezik update SQL parancsot kell haszn�lni
                    command.CommandText =
                        "UPDATE Book " +
                        "SET pieces = pieces + @pieces " +
                        "WHERE id=@id";

                    /* �rt�ket adunk a param�tereknek.
                     * Ehhez el�bb l�rehozunk egy �j param�tert (Add) �s megadjuk a
                     * nev�t �s SQL t�pus�t. A n�v azonos az el�bb defini�lt
                     * SQL parancsban �rt nevekkel.
                     * A keletkezett (SQLite)Parameter objektum Value property-nek
                     * �rt�k�l adjuk a param�ter �rt�k�t.
                     * */
                    command.Parameters.Add("id", DbType.Int32).Value = storedBook.Id;
                    command.Parameters.Add("pieces", DbType.Int32).Value = book.Pieces;
                }
                else
                {
                    // a k�nyv m�g nincs az adatt�rban, elt�roljuk
                    // a k�nyv m�g nincs az adatb�zisban, insert SQL parancs kell
                    command.CommandText =
                        "INSERT INTO Book " +
                        "  (author, title, year, category, price, pieces, ancient) " +
                        "VALUES " +
                        "  (@author, @title, @year, @category, @price, @pieces, @ancient)";

                    command.Parameters.Add("author", DbType.String).Value = book.Author;
                    command.Parameters.Add("title", DbType.String).Value = book.Title;
                    command.Parameters.Add("year", DbType.Int32).Value = book.Year;
                    command.Parameters.Add("category", DbType.String).Value = book.Category;
                    command.Parameters.Add("price", DbType.Double).Value = book.Price;
                    command.Parameters.Add("pieces", DbType.Int32).Value = book.Pieces;
                    command.Parameters.Add("ancient", DbType.Boolean).Value = book.Ancient;
                }

                /* az ExecuteNonQuery() met�dus v�grehajtja a parancsot, olyan
                 * parancsok eset�n haszn�ljuk ahol nem lek�rdez�nk �rt�keket
                 * (nem SELECT).
                 * A visszat�r�si �rt�ke a parancs �ltal �rintett sorok sz�ma.
                 * Ha ez nem 1, akkor valami gond van, mivel mi pont 1 rekordot
                 * szeretn�nk besz�rni/m�dos�tani.
                 * */
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows == 1)
                {
                    rvSucceeded = true;
                }
            }

            return rvSucceeded;
        }


        /// <summary>
        /// Megn�zi hogy az adott k�nyv szerepel-e m�r az adatt�rban szerz�, c�m
        /// �s kiad�si �v alapj�n.
        /// </summary>
        /// <param name="connection">A haszn�land� kapcsolat.</param>
        /// <param name="book">A megkeresend� <see cref="Book"/>.</param>
        /// <returns>
        /// Az adatt�rban l�v� <see cref="Book"/>, ha m�r l�tezik, egy�bk�nt null.
        /// </returns>
        private Book FindBookByAuthorTitleYear(SQLiteConnection connection, Book book)
        {
            Book rvFound = null;

            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT * " +
                    "FROM Book " +
                    "WHERE author = @author AND title = @title AND year = @year";

                command.Parameters.Add("author", DbType.String).Value = book.Author;
                command.Parameters.Add("title", DbType.String).Value = book.Title;
                command.Parameters.Add("year", DbType.Int32).Value = book.Year;

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    List<Book> books = ReadBooksFromReader(reader);

                    if (books.Count > 0)
                    {
                        rvFound = books[0];
                    }
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
            List<Book> rvBooks = null;

            /*
             * A using kulcssz� a nem menedzselt er�forr�sok kezel�s�t seg�ti
             * a blokkb�l kil�pve garant�lja hogy a param�ternek megadott er�forr�s
             * (itt a 'conn') le lesz z�rva (conn.Close()) ak�r sikeresen lefutottak
             * a m�veletek, ak�r hiba (exception) miatt l�p ki a blokkb�l.
             *
             * Nem menedzselt er�forr�sok .NET alatt (�s Javaban is!):
             * adatb�ziskapcsolat, stream, f�jlok, h�l�zati kapcsolatok, ...
             *
             * Egym�s ut�n lehet tenni a using-okat, nem kell be�gyazni.
             * */

            /* L�trehozunk egy parancs objektumot, ami SQL parancsok v�grehajt�s�ra
             * lesz alkalmas, ezt mindig egy adott connection-h�z k�tj�k
             * */
            using (SQLiteConnection conn = new SQLiteConnection(s_connectionString))
            using (SQLiteCommand command = conn.CreateCommand())
            {
                /* CommandText-be �ll�tand� be a futtattand� SQL parancs.
                 * Param�teres SQL parancsot adunk, a param�tereket n�vvel
                 * ell�tva kell defini�lni .NET-ben.
                 * */
                command.CommandText =
                    "SELECT * " +
                    "FROM Book";

                /* Ellent�tben Java-val a connection l�trehoz�sa nem jelent
                 * automatikus csatlakoz�st az adatb�zishoz. A t�nyleges
                 * m�veletv�gz�s el�tt meg kell nyitni a kapcsolatot.
                 * FONTOS a v�g�n le k�ne z�rni is, ezt a 'using' blokk megoldja.
                 * */
                conn.Open();

                /* A parancsot lefuttatjuk. Az ExecuteReader() met�dust akkor
                 * haszn�ljuk, ha sorokat tartalmaz� eredm�nyt v�runk.
                 * */
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    /* SQLiteDataReader-t adunk �t, a met�dus �ltal�nos
                     * IDbReader-t v�r, �gy az nem f�gg a konkr�t DB-t�l
                     * */
                    rvBooks = ReadBooksFromReader(reader);
                }
            }

            return rvBooks;
        }


        /// <summary>
        /// Kiolvassa a <see cref="Book"/> p�ld�nyokat egy
        /// <see cref="IDataReader"/> p�ld�nyb�l.
        /// </summary>
        /// <param name="reader">Az olvasand� <see cref="IDataReader"/>.</param>
        /// <returns>Az olvasott <see cref="Book"/> p�ld�nyok.</returns>
        private List<Book> ReadBooksFromReader(IDataReader reader)
        {
            List<Book> rvBooks = new List<Book>();

            /* Bej�rjuk az eredm�nyhalmazt. Hasonl�, mint JDBC eset�n,
             * az utols� sor eset�n false a Read() visszat�r�si �rt�ke
             * */
            while (reader.Read())
            {
                Book book = new Book
                {
                    /* FONTOS
                    /* Vesz�lyes, mi van, ha az adatb�zisban a mez� t�pusa
                     * float-ra v�ltozik?
                     * */
                    //Id = Convert.ToInt32(reader["id"]);
                    Id = reader.GetInt32(reader.GetOrdinal("id")),

                    /* FONTOS
                     * Vesz�lyes, mi van, ha az adatb�zisban a t�pusa nem
                     * sz�vegre v�ltozik?
                     * */
                    //Author = reader["author"].ToString(),
                    Author = reader.GetString(reader.GetOrdinal("author")),

                    Title = reader.GetString(reader.GetOrdinal("title")),
                    Year = reader.GetInt32(reader.GetOrdinal("year")),
                    Category = reader.GetString(reader.GetOrdinal("category")),
                    Price = reader.GetDouble(reader.GetOrdinal("price")),
                    Pieces = reader.GetInt32(reader.GetOrdinal("pieces")),
                    Ancient = reader.GetBoolean(reader.GetOrdinal("ancient"))
                };

                rvBooks.Add(book);
            }

            return rvBooks;
        }
        #endregion
    }
}

using System.Collections.Generic;
using System.Data.SQLite;
using BookShop.Model;
using System.Data;

namespace BookShop.Dal
{
    /// <summary>
    /// Ez az osztály az SQLite adatbázisból történõ adatelérést szolgálja.
    /// </summary>
    public class BookShopDaoDb : IBookShopDao
    {
        #region statikus tagok
        /// <summary>
        /// Az adatbázis SQLite specifikus connection stringje.
        /// </summary>
        private static readonly string s_connectionString = @"Data Source=..\..\..\..\..\kulso\db\bookshop.db;";
        #endregion


        #region customer mûveletek
        /// <summary>
        /// Hozzáad egy <see cref="Customer"/> objektumot az adattárhoz.
        /// </summary>
        /// <param name="customer">A tárolandó <see cref="Customer"/>.</param>
        /// <returns>Igaz, ha sikeresen tárolva, egyébként hamis.</returns>
        public bool AddCustomer(Customer customer)
        {
            bool rvSucceeded = false;

            // TODO implementálandó!

            return rvSucceeded;
        }


        /// <summary>
        /// Visszaadja a tárolt <see cref="Customer"/> példányokat.
        /// </summary>
        /// <returns>A tárolt <see cref="Customer"/> példányok kollekciója.</returns>
        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> rvCustomers = null;

            // TODO implementálandó!
            rvCustomers = new List<Customer>();

            return rvCustomers;
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
            bool rvSucceeded = false;

            using (SQLiteConnection conn = new SQLiteConnection(s_connectionString))
            {
                /* FONTOS
                 * Már itt megnyitjuk a kapcsolatot, mert azt újrahasználjuk
                 * (átadjuk) a FindBookByAuthorTitleYear-nak, hogy ne kelljen
                 * feleslegesen két kapcsolatot nyitni.
                 *   - Ha ezt késõbb tennénk, akkor a hívott függvénybe is
                 *     kellene Open, illetve a késõbbre tett Open exception-t
                 *     dobna, mert már nyitva van a kapcsolat
                 *   - Ha csak a FindBookByAuthorTitleYear-ba tennénk Open-t,
                 *     akkor a függvény esetleges többi hívója sem nyithatna.
                 *     Ha azok más metódust is hívnak, akkor azok sem
                 *     nyithatnának, de szükségük lenne rá, nem tudnánk
                 *     írányítani, hogy mely függvény nyissa pontosan.
                 * Ezért mindig az a jó, ha az nyitja a kapcsolatot, aki létrehozza.
                 * */
                conn.Open();

                // megnézzük, hogy a könyv tárolt-e már
                Book storedBook = FindBookByAuthorTitleYear(conn, book);

                SQLiteCommand command = conn.CreateCommand();

                if (storedBook != null)
                {
                    // a könyv már létezik, növeljük a darabszámot
                    // a könyv már létezik update SQL parancsot kell használni
                    command.CommandText =
                        "UPDATE Book " +
                        "SET pieces = pieces + @pieces " +
                        "WHERE id=@id";

                    /* Értéket adunk a paramétereknek.
                     * Ehhez elõbb lérehozunk egy új paramétert (Add) és megadjuk a
                     * nevét és SQL típusát. A név azonos az elõbb definiált
                     * SQL parancsban írt nevekkel.
                     * A keletkezett (SQLite)Parameter objektum Value property-nek
                     * értékül adjuk a paraméter értékét.
                     * */
                    command.Parameters.Add("id", DbType.Int32).Value = storedBook.Id;
                    command.Parameters.Add("pieces", DbType.Int32).Value = book.Pieces;
                }
                else
                {
                    // a könyv még nincs az adattárban, eltároljuk
                    // a könyv még nincs az adatbázisban, insert SQL parancs kell
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

                /* az ExecuteNonQuery() metódus végrehajtja a parancsot, olyan
                 * parancsok esetén használjuk ahol nem lekérdezünk értékeket
                 * (nem SELECT).
                 * A visszatérési értéke a parancs által érintett sorok száma.
                 * Ha ez nem 1, akkor valami gond van, mivel mi pont 1 rekordot
                 * szeretnénk beszúrni/módosítani.
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
        /// Megnézi hogy az adott könyv szerepel-e már az adattárban szerzõ, cím
        /// és kiadási év alapján.
        /// </summary>
        /// <param name="connection">A használandó kapcsolat.</param>
        /// <param name="book">A megkeresendõ <see cref="Book"/>.</param>
        /// <returns>
        /// Az adattárban lévõ <see cref="Book"/>, ha már létezik, egyébként null.
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
        /// Visszaadja a tárolt <see cref="Book"/> példányokat
        /// </summary>
        /// <returns>A tárolt <see cref="Book"/> példányok.</returns>
        public IEnumerable<Book> GetBooks()
        {
            List<Book> rvBooks = null;

            /*
             * A using kulcsszó a nem menedzselt erõforrások kezelését segíti
             * a blokkból kilépve garantálja hogy a paraméternek megadott erõforrás
             * (itt a 'conn') le lesz zárva (conn.Close()) akár sikeresen lefutottak
             * a mûveletek, akár hiba (exception) miatt lép ki a blokkból.
             *
             * Nem menedzselt erõforrások .NET alatt (és Javaban is!):
             * adatbáziskapcsolat, stream, fájlok, hálózati kapcsolatok, ...
             *
             * Egymás után lehet tenni a using-okat, nem kell beágyazni.
             * */

            /* Létrehozunk egy parancs objektumot, ami SQL parancsok végrehajtására
             * lesz alkalmas, ezt mindig egy adott connection-höz kötjük
             * */
            using (SQLiteConnection conn = new SQLiteConnection(s_connectionString))
            using (SQLiteCommand command = conn.CreateCommand())
            {
                /* CommandText-be állítandó be a futtattandó SQL parancs.
                 * Paraméteres SQL parancsot adunk, a paramétereket névvel
                 * ellátva kell definiálni .NET-ben.
                 * */
                command.CommandText =
                    "SELECT * " +
                    "FROM Book";

                /* Ellentétben Java-val a connection létrehozása nem jelent
                 * automatikus csatlakozást az adatbázishoz. A tényleges
                 * mûveletvégzés elõtt meg kell nyitni a kapcsolatot.
                 * FONTOS a végén le kéne zárni is, ezt a 'using' blokk megoldja.
                 * */
                conn.Open();

                /* A parancsot lefuttatjuk. Az ExecuteReader() metódust akkor
                 * használjuk, ha sorokat tartalmazó eredményt várunk.
                 * */
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    /* SQLiteDataReader-t adunk át, a metódus általános
                     * IDbReader-t vár, így az nem függ a konkrét DB-tõl
                     * */
                    rvBooks = ReadBooksFromReader(reader);
                }
            }

            return rvBooks;
        }


        /// <summary>
        /// Kiolvassa a <see cref="Book"/> példányokat egy
        /// <see cref="IDataReader"/> példányból.
        /// </summary>
        /// <param name="reader">Az olvasandó <see cref="IDataReader"/>.</param>
        /// <returns>Az olvasott <see cref="Book"/> példányok.</returns>
        private List<Book> ReadBooksFromReader(IDataReader reader)
        {
            List<Book> rvBooks = new List<Book>();

            /* Bejárjuk az eredményhalmazt. Hasonló, mint JDBC esetén,
             * az utolsó sor esetén false a Read() visszatérési értéke
             * */
            while (reader.Read())
            {
                Book book = new Book
                {
                    /* FONTOS
                    /* Veszélyes, mi van, ha az adatbázisban a mezõ típusa
                     * float-ra változik?
                     * */
                    //Id = Convert.ToInt32(reader["id"]);
                    Id = reader.GetInt32(reader.GetOrdinal("id")),

                    /* FONTOS
                     * Veszélyes, mi van, ha az adatbázisban a típusa nem
                     * szövegre változik?
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

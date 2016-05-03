using System.Collections.Generic;
using BookShop.Model;

namespace BookShop.Dal
{
    /// <summary>
    /// Az interfész a BookShop app adatelérési retegét reprezentálja.
    /// </summary>
    /// <remarks>
    /// .NET/C#-ban konvenció szerint az interfészek neve I-vel kezdődik.
    /// Illetve max a 2 betűs rövidítéseket írjuk full capitallal.
    /// </remarks>
    public interface IBookShopDao
    {
        #region customer műveletek
        /// <summary>
        /// Hozzáad egy <see cref="Customer"/> objektumot az adattárhoz.
        /// </summary>
        /// <param name="customer">A tárolandó <see cref="Customer"/>.</param>
        /// <returns>Igaz, ha sikeresen tárolva, egyébként hamis.</returns>
        bool AddCustomer(Customer customer);


        /// <summary>
        /// Visszaadja a tárolt <see cref="Customer"/> példányokat.
        /// </summary>
        /// <returns>A tárolt <see cref="Customer"/> példányok kollekciója.</returns>
        IEnumerable<Customer> GetCustomers();
        #endregion


        #region book műveletek
        /// <summary>
        /// Egy könyv hozzáadása az adattárhoz vagy frissítése az adattárban.
        /// táblához.
        /// </summary>
        /// <remarks>
        /// Ha már létezik a könyv, akkor eggyel növeli a <see cref="Book.Pieces"/>
        /// értékét, egyébként rögzíti a <see cref="Book"/> példányt.
        /// </remarks>
        /// <param name="book">A tárolandó vagy frissítendő <see cref="Book"/>.</param>
        /// <returns>Igaz, ha sikeres a tárolás, egyébként hamis.</returns>
        bool AddOrUpdateBook(Book book);


        /// <summary>
        /// Visszaadja a tárolt <see cref="Book"/> példányokat
        /// </summary>
        /// <returns>A tárolt <see cref="Book"/> példányok.</returns>
        IEnumerable<Book> GetBooks();
        #endregion
    }
}

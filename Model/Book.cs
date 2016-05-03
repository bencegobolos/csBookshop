using System;

namespace BookShop.Model
{
    public class Book
    {
        #region konstansok
        private const string TOSTRING_FORMAT_STRING =
            "{0} - {1} - {2} - {3} - Price: {5} - Pieces: {4} - Ancient: {6}";
        #endregion


        #region tulajdonságok
        /// <summary>
        /// Beállítja vagy visszaadja a könyv rekord id-ját.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Beállítja vagy visszaadja a könyv szerzõjét.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Beállítja vagy visszaadja a könyv címét.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Beállítja vagy visszaadja a könyv kiadási évét.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Beállítja vagy visszaadja a könyv kategóriáját.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Beállítja vagy visszaadja a könyv árát.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Beállítja vagy visszaadja a könyv darabszámát.
        /// </summary>
        public int Pieces { get; set; }

        /// <summary>
        /// Beállítja vagy visszaadja, hogy a könyv ósdi-e.
        /// </summary>
        public bool Ancient { get; set; }
        #endregion


        /* Szokás szerint kiírjuk, hogy melyik osztály tagjának felülbírálása történik.
         * A VS absztrakt osztály tagjainak generálása esetén tesz így.
         * * */
        #region Object felüldefiniálások
        /// <summary>
        /// Megadja az osztály szöveges reprezentációját.
        /// </summary>
        /// <returns>Az osztály szöveges reprezentációja.</returns>
        public override String ToString()
        {
            return string.Format(
                TOSTRING_FORMAT_STRING,
                Author, Title, Year, Category, Pieces, Price, Ancient);
        }
        #endregion
    }
}

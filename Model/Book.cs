using System;

namespace BookShop.Model
{
    public class Book
    {
        #region konstansok
        private const string TOSTRING_FORMAT_STRING =
            "{0} - {1} - {2} - {3} - Price: {5} - Pieces: {4} - Ancient: {6}";
        #endregion


        #region tulajdons�gok
        /// <summary>
        /// Be�ll�tja vagy visszaadja a k�nyv rekord id-j�t.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Be�ll�tja vagy visszaadja a k�nyv szerz�j�t.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Be�ll�tja vagy visszaadja a k�nyv c�m�t.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Be�ll�tja vagy visszaadja a k�nyv kiad�si �v�t.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Be�ll�tja vagy visszaadja a k�nyv kateg�ri�j�t.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Be�ll�tja vagy visszaadja a k�nyv �r�t.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Be�ll�tja vagy visszaadja a k�nyv darabsz�m�t.
        /// </summary>
        public int Pieces { get; set; }

        /// <summary>
        /// Be�ll�tja vagy visszaadja, hogy a k�nyv �sdi-e.
        /// </summary>
        public bool Ancient { get; set; }
        #endregion


        /* Szok�s szerint ki�rjuk, hogy melyik oszt�ly tagj�nak fel�lb�r�l�sa t�rt�nik.
         * A VS absztrakt oszt�ly tagjainak gener�l�sa eset�n tesz �gy.
         * * */
        #region Object fel�ldefini�l�sok
        /// <summary>
        /// Megadja az oszt�ly sz�veges reprezent�ci�j�t.
        /// </summary>
        /// <returns>Az oszt�ly sz�veges reprezent�ci�ja.</returns>
        public override String ToString()
        {
            return string.Format(
                TOSTRING_FORMAT_STRING,
                Author, Title, Year, Category, Pieces, Price, Ancient);
        }
        #endregion
    }
}

using System;

namespace BookShop.Model
{
    public class Cd
    {
        #region konstansok
        private const string TOSTRING_FORMAT_STRING =
            "{0} - {1} - {2} - {3} - Price: {5} - Pieces: {4}";
        #endregion


        #region tulajdonságok
        public int Id { get; set; }

        public string Artist { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public int Year { get; set; }

        public bool Hit { get; set; }

        public bool Selection { get; set; }
        #endregion


        #region Object felüldefiniálások
        public override String ToString()
        {
            return string.Format(
                TOSTRING_FORMAT_STRING,
                Artist, Title, Price, Year, Hit, Selection);
        }
        #endregion
    }
}
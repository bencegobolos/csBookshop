using System;

namespace BookShop.Model
{
    /// <summary>
    /// Az osztály egy vásárlót/ügyfelet ír le.
    /// <para>
    /// Hasonlóan a Java bean osztályoknál itt is minden adattag privát és van hozzá
    /// getter/setter. C#-ban van nyelvi szinten erre külön támogatás az ún. property-k,
    /// amelyek általában azonos nevûek mint az adattagok csak nagy kezdõbetûvel.
    /// </para>
    /// </summary>
    public class Customer
    {
        #region konstansok
        private const string TOSTRING_FORMAT_STRING =
            "{0} - {1} - Female: {2} - Student: {3} - Rendted: {4} - Grantee: {5} - {6}";
        #endregion


        #region mezõk
        private string m_name;
        #endregion


        #region tulajdonságok
        /// <summary>
        /// Beállítja vagy visszaadja a vásárló rekord id-ját.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Beállítja vagy visszaadja a vásárló nevét.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }


        /// <summary>
        /// Beállítja vagy visszaadja a vásárló korát.
        /// <para>
        /// C# 3.0-tól (VS 2008) lehetõség van ún. "Auto-implemented properties"
        /// használatára. Ebben az esetben nincs szükség adattag explicit definiálására,
        /// a szükséges adattag fordításkor létrejön.
        /// Fontos: habár a láthatóság továbbra is szabadon állítható, a "get" és a "set"
        /// mûvelet nem bõvíthetõ.
        /// </para>
        /// </summary>
        public int Age { get; set; }


        /// <summary>
        /// Beállítja vagy visszaadja, hogy a vásárló nõnemû-e.
        /// </summary>
        public bool Female { get; set; }


        /// <summary>
        /// Beállítja vagy visszadja, hogy a vásárló nyugdíjas-e.
        /// </summary>
        public bool Rented { get; set; }


        /// <summary>
        /// Beállítja vagy visszaadja, hogy a vásárló tanuló-e.
        /// </summary>
        public bool Student { get; set; }


        /// <summary>
        /// Beállítja vagy visszaadja, hogy a vásárló kedvezményezett-e.
        /// </summary>
        public bool Grantee { get; set; }


        /// <summary>
        /// Beállítja vagy visszaadja a vásárló végzettségét.
        /// </summary>
        public string Qualification { get; set; }
        #endregion


        #region Object felüldefiniálások
        /// <summary>
        /// Megadja az osztály szöveges reprezentációját.
        /// </summary>
        /// <returns>Az osztály szöveges reprezentációja.</returns>
        public override string ToString()
        {
            return string.Format(
                TOSTRING_FORMAT_STRING,
                Name, Age, Female, Student,Rented, Grantee, Qualification);
        }
        #endregion
    }
}

using System;

namespace BookShop.Model
{
    /// <summary>
    /// Az oszt�ly egy v�s�rl�t/�gyfelet �r le.
    /// <para>
    /// Hasonl�an a Java bean oszt�lyokn�l itt is minden adattag priv�t �s van hozz�
    /// getter/setter. C#-ban van nyelvi szinten erre k�l�n t�mogat�s az �n. property-k,
    /// amelyek �ltal�ban azonos nev�ek mint az adattagok csak nagy kezd�bet�vel.
    /// </para>
    /// </summary>
    public class Customer
    {
        #region konstansok
        private const string TOSTRING_FORMAT_STRING =
            "{0} - {1} - Female: {2} - Student: {3} - Rendted: {4} - Grantee: {5} - {6}";
        #endregion


        #region mez�k
        private string m_name;
        #endregion


        #region tulajdons�gok
        /// <summary>
        /// Be�ll�tja vagy visszaadja a v�s�rl� rekord id-j�t.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Be�ll�tja vagy visszaadja a v�s�rl� nev�t.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }


        /// <summary>
        /// Be�ll�tja vagy visszaadja a v�s�rl� kor�t.
        /// <para>
        /// C# 3.0-t�l (VS 2008) lehet�s�g van �n. "Auto-implemented properties"
        /// haszn�lat�ra. Ebben az esetben nincs sz�ks�g adattag explicit defini�l�s�ra,
        /// a sz�ks�ges adattag ford�t�skor l�trej�n.
        /// Fontos: hab�r a l�that�s�g tov�bbra is szabadon �ll�that�, a "get" �s a "set"
        /// m�velet nem b�v�thet�.
        /// </para>
        /// </summary>
        public int Age { get; set; }


        /// <summary>
        /// Be�ll�tja vagy visszaadja, hogy a v�s�rl� n�nem�-e.
        /// </summary>
        public bool Female { get; set; }


        /// <summary>
        /// Be�ll�tja vagy visszadja, hogy a v�s�rl� nyugd�jas-e.
        /// </summary>
        public bool Rented { get; set; }


        /// <summary>
        /// Be�ll�tja vagy visszaadja, hogy a v�s�rl� tanul�-e.
        /// </summary>
        public bool Student { get; set; }


        /// <summary>
        /// Be�ll�tja vagy visszaadja, hogy a v�s�rl� kedvezm�nyezett-e.
        /// </summary>
        public bool Grantee { get; set; }


        /// <summary>
        /// Be�ll�tja vagy visszaadja a v�s�rl� v�gzetts�g�t.
        /// </summary>
        public string Qualification { get; set; }
        #endregion


        #region Object fel�ldefini�l�sok
        /// <summary>
        /// Megadja az oszt�ly sz�veges reprezent�ci�j�t.
        /// </summary>
        /// <returns>Az oszt�ly sz�veges reprezent�ci�ja.</returns>
        public override string ToString()
        {
            return string.Format(
                TOSTRING_FORMAT_STRING,
                Name, Age, Female, Student,Rented, Grantee, Qualification);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty_podstawy
{
    class Program
    {
        public class Karty 
        {
           public string[] figura;
           public string[] kolor;
           public string[,] talia;
           public string[] hierarchiaUkladow;
           public Karty() //////////////Konstruktor tworzący talie kart//////////////
            {
                figura = new string[13] { "2","3","4","5","6","7","8","9","10","J","Q","K","A" }; 
                kolor = new string[4] { "Kier", "Trefl", "Karo", "Pik"};
                talia = new string[kolor.Length, figura.Length];
                hierarchiaUkladow = new string[10] {"Kicker", "Para", "Dwie pary", "Trójka", "Street", "Kolor", "Full", "Kareta", "Mały Poker", "Królewski Poker"};               
                for (int i = 0; i <= kolor.Length - 1; i++)
                    for (int j = 0; j <= figura.Length - 1; j++)
                        talia[i, j] = figura[j] + " " + kolor[i];
                
           }
           public void Fan() //////////////Metoda pomocnicza do wyświetlenia talii kart//////////////
            {
                Console.WriteLine("Robie Fan'a z kart\n"); 
                for (int i = 0; i <= kolor.Length - 1; i++)
                {
                    if (i > 0)
                        Console.WriteLine("\n");
                    for (int j = 0; j <= figura.Length - 1; j++)
                    {
                        Console.WriteLine("[{0},{1}] = {2}", i, j, talia[i, j]);
                    }
                }
            }
            public string[] Rozdaj()
            {
                var rand = new Random();
                string[] reka = new string[5];
                int losI; // Wylosowana współrzędna i
                int losJ; // Wylosowana współrzędna j
                //Console.WriteLine("\nRozdano: ");
                for (int i = 0; i<5; i++)
                {
                    losI = rand.Next(4);
                    losJ = rand.Next(13);
                    reka[i] = talia[losI, losJ];
                    talia[losI, losJ] = "Null";
                    while(reka[i] == "Null")
                    {
                        losI = rand.Next(4);
                        losJ = rand.Next(13);
                        reka[i] = talia[losI, losJ];
                        talia[losI, losJ] = "Null";
                    }
                    Console.WriteLine(reka[i]);
                }
                return reka;
            }
            public int[] Sprawdz(string[] reka){
                //////////////Sortowanie tablicy z figurami z reka//////////////
                int[] rekaIdSort = new int[5]; //////////////Tablica posortowanych indeksow figur z ręki//////////////                     
                string substr;               
                for (int i = 0; i < 5; i++)               
                {
                    for(int j = 0; j < 13; j++)
                    {
                        
                        if(reka[i].Substring(0,1) == "1")
                        {
                            substr = reka[i].Substring(0, 2);
                        }                                                  
                        else
                        {
                            substr = reka[i].Substring(0, 1);
                        }
                        if(figura[j] == substr)
                        {
                            rekaIdSort[i] = j;
                        }
                    }
                }               
                //////////////Sortowanie tablicy z kolorami ręki//////////////
                int[] rekaKolorSort = new int[5];// { 0, 0, 0, 0, 0 }; //////////////Posortowana tablica z kolorami ręki//////////////
                for (int i = 0; i < 5; i++)
                {
                    switch(reka[i].Substring(reka[i].Length - 1, 1))
                    {
                        case "r":
                            rekaKolorSort[i] = 0;
                            break;
                        case "l":
                            rekaKolorSort[i] = 1;
                            break;
                        case "o":
                            rekaKolorSort[i] = 2;
                            break;
                        case "k":
                            rekaKolorSort[i] = 3;
                            break;
                    }
                }                
                Array.Sort(rekaKolorSort);
                Array.Sort(rekaIdSort);
                int[] ukladTab = new int[4]; 
                ukladTab[0] = 0;
                ukladTab[1] = rekaIdSort[4];
                ukladTab[2] = rekaIdSort[3];
                ukladTab[3] = rekaIdSort[2];                
                //////////////Analizowanie ułożenia kart i przypisanie im odpowiedniej nazwy ukladu//////////////
                int licznikPowtorzen = 0;
                int licznikKolejnosci = 0;
                if (rekaKolorSort[0] == rekaKolorSort[4]) //////////////Sprawdzanie koloru; Na początku, bo to najprostsza instrukcja//////////////
                {
                    ukladTab[0] = 5;
                    ukladTab[1] = rekaIdSort[4];
                    ukladTab[2] = rekaKolorSort[0];
                }
                for (int i = 1; i < 5; i++)
                {
                    if (rekaIdSort[i] == rekaIdSort[i - 1])
                    {
                        licznikPowtorzen++;
                        if (licznikPowtorzen == 1) //////////////Odnajdywanie 1 pary//////////////
                        {
                            ukladTab[0] = 1;
                            ukladTab[1] = rekaIdSort[i];
                            ukladTab[2] = rekaIdSort[4];
                            if (rekaIdSort[i] == rekaIdSort[4])
                            {
                                ukladTab[0] = 1;
                                ukladTab[1] = rekaIdSort[i];
                                ukladTab[2] = rekaIdSort[2];
                            }
                        }
                        else if (licznikPowtorzen == 2)
                        {
                            if (rekaIdSort[i - 1] == rekaIdSort[i - 2])//////////////Odnajdywanie trójki//////////////
                            {
                                ukladTab[0] = 3;
                                ukladTab[1] = rekaIdSort[i];
                                ukladTab[2] = rekaIdSort[4];
                                if (rekaIdSort[i] == rekaIdSort[4])
                                {
                                    ukladTab[0] = 1;
                                    ukladTab[1] = rekaIdSort[i];
                                    ukladTab[2] = rekaIdSort[1];
                                }
                            }
                            else //////////////Odnajdywanie 2 par//////////////
                            {
                                if ((rekaIdSort[i] == rekaIdSort[4]) && (rekaIdSort[2] != rekaIdSort[1]))
                                {
                                    ukladTab[0] = 2;
                                    ukladTab[1] = rekaIdSort[i];
                                    ukladTab[2] = rekaIdSort[1];
                                    ukladTab[3] = rekaIdSort[2];
                                }
                                else if ((rekaIdSort[i] != rekaIdSort[4]) && (rekaIdSort[2] != rekaIdSort[1]))
                                {
                                    ukladTab[0] = 2;
                                    ukladTab[1] = rekaIdSort[i];
                                    ukladTab[2] = rekaIdSort[1];
                                    ukladTab[3] = rekaIdSort[4];
                                }
                                else
                                {
                                    ukladTab[0] = 2;
                                    ukladTab[1] = rekaIdSort[i];
                                    ukladTab[2] = rekaIdSort[1];
                                    ukladTab[3] = rekaIdSort[0];
                                }
                            }
                        }
                        else if (licznikPowtorzen == 3)
                        {
                            if ((rekaIdSort[i - 1] == rekaIdSort[i - 2]) && (rekaIdSort[i - 2]) == rekaIdSort[i - 3]) //////////////Odnajdywanie karety//////////////
                            {
                                ukladTab[0] = 7;
                                ukladTab[1] = rekaIdSort[i-3];
                                ukladTab[2] = rekaIdSort[4];
                                if (rekaIdSort[i] == rekaIdSort[4])
                                {
                                    ukladTab[0] = 7;
                                    ukladTab[1] = rekaIdSort[i-3];
                                    ukladTab[2] = rekaIdSort[0];
                                }
                            }
                            else //////////////Odnajdywanie Fulla//////////////
                            {
                                if (rekaIdSort[1] != rekaIdSort[2])
                                {
                                    ukladTab[0] = 6;
                                    ukladTab[1] = rekaIdSort[2];
                                    ukladTab[2] = rekaIdSort[1];
                                }
                                else
                                {
                                    ukladTab[0] = 6;
                                    ukladTab[1] = rekaIdSort[1];
                                    ukladTab[2] = rekaIdSort[3];
                                }
                            }
                        }
                    }
                    else if ((rekaIdSort[i] - rekaIdSort[i - 1]) == 1) //////////////Analiza czy karty są w kolejności//////////////
                    {
                        licznikKolejnosci++;
                        if ((licznikKolejnosci == 4) || ((((licznikKolejnosci == 3) && (rekaIdSort[4] == 12)) && rekaIdSort[0] == 0) && rekaIdSort[3] != 11))
                        {
                            if (rekaKolorSort[0] == rekaKolorSort[4]) //////////////Warunek do pokerów//////////////
                            {
                                ukladTab[0] = 8;
                                ukladTab[1] = rekaIdSort[0];
                                ukladTab[2] = rekaIdSort[4];
                                if (rekaIdSort[0] == 0 && rekaIdSort[4] == 12)
                                {
                                    ukladTab[0] = 8;
                                    ukladTab[1] = rekaIdSort[4];
                                    ukladTab[2] = rekaIdSort[3];
                                }
                                else if (rekaIdSort[0] == 8 && rekaIdSort[4] == 12)
                                {
                                    ukladTab[0] = 9;
                                }
                            }
                            else //////////////Street//////////////
                            {
                                ukladTab[0] = 4;
                                ukladTab[1] = rekaIdSort[0];
                                ukladTab[2] = rekaIdSort[4];
                                if (rekaIdSort[0] == 0 && rekaIdSort[4] == 12)
                                {
                                    ukladTab[0] = 4;
                                    ukladTab[1] = rekaIdSort[4];
                                    ukladTab[2] = rekaIdSort[3];
                                }
                            }
                        }
                    } 
                }
                switch (ukladTab[0])
                {
                    case 0:
                        Console.WriteLine("{0} {1}", hierarchiaUkladow[ukladTab[0]], figura[ukladTab[1]]);
                        break;
                    case 2:
                        Console.WriteLine("{0}, {1} i {2}, z kickerem {3}", hierarchiaUkladow[ukladTab[0]], figura[ukladTab[1]], figura[ukladTab[2]], figura[ukladTab[3]]);
                        break;
                    case 5:
                        Console.WriteLine("{0} {1}, z kickerem {2}", hierarchiaUkladow[ukladTab[0]], figura[ukladTab[2]], figura[ukladTab[1]]);
                        break;
                    case 1:
                    case 3:
                    case 7:
                        Console.WriteLine("{0} {1}, z kickerem {2}", hierarchiaUkladow[ukladTab[0]], figura[ukladTab[1]], figura[ukladTab[2]]);
                        break;
                    case 6:
                        Console.WriteLine("{0} {1} na {2}", hierarchiaUkladow[ukladTab[0]], figura[ukladTab[1]], figura[ukladTab[2]]);
                        break;
                    case 4:
                    case 8:
                        Console.WriteLine("{0} od {1} do {2}", hierarchiaUkladow[ukladTab[0]], figura[ukladTab[1]], figura[ukladTab[2]]);
                        break;
                    case 9:
                        Console.WriteLine("{0}, Gratulacje!!!", hierarchiaUkladow[ukladTab[0]]);
                        break;
                }
                return ukladTab;
            }
        };
        static void Main()
        {                               
            Karty poker = new Karty();
            Console.Write("Ile rąk rozdać? (2-5): ");
            int ilosc = int.Parse(Console.ReadLine());
            int[,] wynikTab = new int[ilosc, 4]; //[ID Reki, ID z tablicy ukladTab] np[2,0] wyświetli ID układu trzeciej ręki 
            int[] uklady = new int[ilosc];
            for (int i=0; i < ilosc; i++)
            {
                Console.WriteLine("\nRęka {0}: ", i + 1);
                int[] wynik = poker.Sprawdz(poker.Rozdaj());                
                for (int j = 0; j < wynik.Length; j++)
                {
                    wynikTab[i, j] = wynik[j];
                }
                uklady[i] = wynik[0]; // Indeksy numerów układów; indeks danego numeru to indeks ręki
            }
            /// I STOPIEN - Uklad się nie powtarza ///
            int licznik = 0;           
            for (int i=0; i < ilosc; i++)
            {
                if(uklady.Max() == uklady[i])
                {                    
                    licznik++;
                }                
            };
            if (licznik == 1) // Najprostsza opcja, największy układ występuje raz i łatwo go wyłonić
            {
                Console.WriteLine("\nWygrywa ręka: {0}!",Array.IndexOf(uklady , uklady.Max())+1);
            }
            else
            {
                int j = 0;
                while (j != 3)
                {                    
                    int[] indeksyNajwiekszych = new int[licznik];
                    for (int i = 0; i < licznik; i++)
                    {
                        if (i == 0)
                        {
                            indeksyNajwiekszych[i] = Array.IndexOf(uklady, uklady.Max());
                        }
                        else if (i > 0)
                        {
                            indeksyNajwiekszych[i] = Array.IndexOf(uklady, uklady.Max(), indeksyNajwiekszych[i - 1] + 1);
                        }
                    }
                    int[] kickery = new int[licznik];
                    for (int i = 0; i < licznik; i++)
                    {
                        kickery[i] = wynikTab[indeksyNajwiekszych[i], 1 + j]; // Tablica przechowująca indeksy kickerów najwyzszych ukladów
                    }
                    int licznikKickerow = 0;
                    for (int i = 0; i < licznik; i++)
                    {
                        if (kickery[i] == kickery.Max())
                        {
                            licznikKickerow++;
                        }
                    }
                    if (licznikKickerow == 1)
                    {
                        Console.WriteLine("\nWygrywa ręka: {0}!", indeksyNajwiekszych[Array.IndexOf(kickery, kickery.Max())] + 1);
                        j = 3;
                        ////////////// TESTY////////////////
                        Console.WriteLine("\n" + poker.hierarchiaUkladow[uklady.Max()] + ", powtarza się " + licznik + " razy");
                        Console.WriteLine("I są na rękach: ");
                        for (int i = 0; i < licznik; i++)
                        {
                            Console.WriteLine(indeksyNajwiekszych[i] + 1 + " " + poker.figura[kickery[i]]);
                        }
                        //////////////////////////////////////
                    }
                    else
                    {
                        j++;
                    }
                };
            }
            /*                                                                                                             Tu komentarz
            /// II STOPIEN - Uklad się powtarza i I znacząca sie nie powtarza ///
            else if(licznik > 1) 
            {
                int[] indeksyNajwiekszych = new int[licznik];
                for(int i = 0; i < licznik; i++)
                {
                    if (i == 0)
                    {
                        indeksyNajwiekszych[i] = Array.IndexOf(uklady, uklady.Max());
                    }
                    else if(i > 0)
                    {
                        indeksyNajwiekszych[i] = Array.IndexOf(uklady, uklady.Max(),indeksyNajwiekszych[i-1]+1);
                    }
                }
                int[] kickery = new int[licznik];
                for(int i = 0; i < licznik; i++)
                {
                    kickery[i] = wynikTab[indeksyNajwiekszych[i], 1]; // Tablica przechowująca indeksy kickerów najwyzszych ukladów
                }
                int licznikKickerow = 0;
                for(int i = 0; i< licznik; i++)
                {
                    if(kickery[i] == kickery.Max())
                    {
                        licznikKickerow++;
                    }
                }
                if(licznikKickerow == 1)
                {
                    Console.WriteLine("\nWygrywa ręka: {0}!",indeksyNajwiekszych[Array.IndexOf(kickery, kickery.Max())] + 1);
                }
                /// III STOPIEN - Uklad się powtarza i I znacząca się powtarza, ale II znacząca się nie powtarza ///
                else if(licznikKickerow > 1)
                {
                    Console.WriteLine("XDXDXD");
                }
                ////////////// TESTY////////////////
                
                Console.WriteLine("\n" + poker.hierarchiaUkladow[uklady.Max()] + ", powtarza się " + licznik + " razy");
                Console.WriteLine("I są na rękach: ");
                for (int i = 0; i < licznik; i++)
                {
                    Console.WriteLine(indeksyNajwiekszych[i] + 1 + " " + poker.figura[kickery[i]]);                    
                } 
                
                //////////////////////////////////////
            }                                                                                                             Aż do tąd */
            Console.WriteLine("\nNaciśnij dowolny przycisk aby zakończyć");
            Console.Read();           
        }
    }
}


//ukladTab INFO
//[ID Ukladu, ID Kickera] Dla braku układu
//[ID ukladu, ID Znaczącej karty, ID mniej znaczącej karty, ID Kickera] Dla układu Dwie pary
//[ID Ukladu, ID Kickera, ID koloru] Dla koloru
//[ID Ukladu, ID Znaczącej karty, ID Kickera/ID mniej znaczącej karty] Dla układów: Trójka, Para, Kareta, Full House
//[ID Ukladu, ID Od, ID Do] Dla układów: Street, Mały poker
//[ID Ukladu] Dla Krolewskiego Pokera

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty_podstawy
{
    class Program
    {
        class Karty 
        {
           public string[] figura;
           public string[] kolor;
           public string[,] talia;
           public string[] reka;
           public Karty() //////////////Konstruktor tworzący talie kart//////////////
            {
                figura = new string[13] { "2","3","4","5","6","7","8","9","10","J","Q","K","A" }; 
                kolor = new string[4] { "Kier", "Trefl", "Karo", "Pik"};
                talia = new string[kolor.Length,figura.Length];

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
                        Console.WriteLine(talia[i, j]);
                    }
                }
            }
            public string[] Rozdaj()
            {
                var rand = new Random();
                reka = new string[5];
                Console.WriteLine("Rozdano: ");
                for (int i = 0; i < 5; i++)
                {
                    reka[i] = talia[rand.Next(4), rand.Next(13)];
                    for (int j = 0; j < i; j++) //////////////Petla eliminująca wylosowanie identycznej karty//////////////
                    {
                        while (reka[i] == reka[j])
                        {
                            reka[i] = talia[rand.Next(4), rand.Next(13)];
                        }
                    }
                    Console.WriteLine(reka[i]);
                }
                return reka;
            }
            public void Sprawdz(){
                //////////////Sortowanie tablicy z figurami z reka//////////////
                int[] rekaIdSort = new int[5];// {0,1,2,3,12}; //////////////Tablica posortowanych indeksow figur z ręki////////////// !!!!PRZYPISANIE TO TEST!!!               
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
                /*for(int i = 0; i < 5; i++) //////////////Petla wyłącznie do celów testowych wyświetlająca posortowane indeksy figur i kolorow wylosowanych kart//////////////
                {
                    Console.WriteLine(rekaIdSort[i]+", "+rekaKolorSort[i]);
                }*/
                string uklad = "Brak układu. Kicker " + figura[rekaIdSort[4]];
                //////////////Analizowanie ułożenia kart i przypisanie im odpowiedniej nazwy ukladu//////////////
                int licznikPowtorzen = 0;
                int licznikKolejnosci = 0;
                if (rekaKolorSort[0] == rekaKolorSort[4]) //////////////Sprawdzanie koloru; Na początku, bo to najprostsza instrukcja//////////////
                {
                    uklad = "Kolor " + kolor[rekaKolorSort[0]] + " z kickerem " + figura[rekaIdSort[4]];
                }
                for (int i = 1; i < 5; i++)
                {
                    if (rekaIdSort[i] == rekaIdSort[i - 1])
                    {
                        licznikPowtorzen++;
                        if (licznikPowtorzen == 1) //////////////Odnajdywanie 1 pary//////////////
                        {
                            uklad = "Para " + figura[rekaIdSort[i]] + " z kickerem " + figura[rekaIdSort[4]];
                            if (rekaIdSort[i] == rekaIdSort[4])
                            {
                                uklad = "Para " + figura[rekaIdSort[i]] + " z kickerem " + figura[rekaIdSort[2]];
                            }
                        }
                        else if (licznikPowtorzen == 2)
                        {
                            if (rekaIdSort[i - 1] == rekaIdSort[i - 2])//////////////Odnajdywanie trójki//////////////
                            {
                                uklad = "Trójka " + figura[rekaIdSort[i]] + " z kickerem " + figura[rekaIdSort[4]];
                                if (rekaIdSort[i] == rekaIdSort[4])
                                {
                                    uklad = "Trójka " + figura[rekaIdSort[i]] + " z kickerem " + figura[rekaIdSort[1]];
                                }
                            }
                            else //////////////Odnajdywanie 2 par//////////////
                            {
                                if ((rekaIdSort[i] == rekaIdSort[4]) && (rekaIdSort[2] != rekaIdSort[1]))
                                {
                                    uklad = "Dwie pary " + figura[rekaIdSort[i]] + " i " + figura[rekaIdSort[1]] + " z kickerem " + figura[rekaIdSort[2]];
                                }
                                else if ((rekaIdSort[i] != rekaIdSort[4]) && (rekaIdSort[2] != rekaIdSort[1]))
                                {
                                    uklad = "Dwie pary " + figura[rekaIdSort[i]] + " i " + figura[rekaIdSort[1]] + " z kickerem " + figura[rekaIdSort[4]];
                                }
                                else
                                {
                                    uklad = "Dwie pary " + figura[rekaIdSort[i]] + " i " + figura[rekaIdSort[1]] + " z kickerem " + figura[rekaIdSort[0]];
                                }
                            }
                        }
                        else if (licznikPowtorzen == 3)
                        {
                            if ((rekaIdSort[i - 1] == rekaIdSort[i - 2]) && (rekaIdSort[i - 2]) == rekaIdSort[i - 3]) //////////////Odnajdywanie karety//////////////
                            {
                                uklad = "Kareta " + figura[rekaIdSort[i - 3]] + " z kickerem " + figura[rekaIdSort[4]];
                                if (rekaIdSort[i] == rekaIdSort[4])
                                {
                                    uklad = "Kareta " + figura[rekaIdSort[i - 3]] + " z kickerem " + figura[rekaIdSort[0]];
                                }
                            }
                            else //////////////Odnajdywanie Fulla//////////////
                            {
                                if (rekaIdSort[1] != rekaIdSort[2])
                                {
                                    uklad = "Full house " + figura[rekaIdSort[2]] + " na " + figura[rekaIdSort[1]];
                                }
                                else
                                {
                                    uklad = "Full house " + figura[rekaIdSort[1]] + " na " + figura[rekaIdSort[3]];
                                }
                            }
                        }
                    }
                    else if ((rekaIdSort[i] - rekaIdSort[i - 1]) == 1) //////////////Analiza czy karty są w kolejności//////////////
                    {
                        licznikKolejnosci++;
                        if ((licznikKolejnosci == 4) || (((licznikKolejnosci == 3) && (rekaIdSort[4] == 12)) && rekaIdSort[0] == 0))
                        {
                            if (rekaKolorSort[0] == rekaKolorSort[4]) //////////////Warunek do pokerów//////////////
                            {
                                uklad = "Mały poker od " + figura[rekaIdSort[0]] + " do " + figura[rekaIdSort[4]];
                                if (rekaIdSort[0] == 0 && rekaIdSort[4] == 12)
                                {
                                    uklad = "Mały poker od " + figura[rekaIdSort[4]] + " do " + figura[rekaIdSort[3]];
                                }
                                else if (rekaIdSort[0] == 8 && rekaIdSort[4] == 12)
                                {
                                    uklad = "Królewski poker!";
                                }
                            }
                            else //////////////Street//////////////
                            {
                                uklad = "Street od " + figura[rekaIdSort[0]] + " do " + figura[rekaIdSort[4]];
                                if (rekaIdSort[0] == 0 && rekaIdSort[4] == 12)
                                {
                                    uklad = "Street od " + figura[rekaIdSort[4]] + " do " + figura[rekaIdSort[3]];
                                }
                            }
                        }
                    } 
                }
                Console.WriteLine(uklad);
            }
        };
        static void Main()
        {
            Karty poker = new Karty();
            //talia.Fan();
            while(true)
            {
                Console.Clear();
                poker.Rozdaj();
                poker.Sprawdz();
                Console.WriteLine("\nNaciśnij ENTER aby wylosować ponownie");               
                Console.WriteLine("Naciśnij Ctrl+C aby zakończyć.");
                Console.Read();
            }           
        }
    }
}

//Do zrobienia
//-Zamiast przypisania uklad = para 5 z kickerem Q zrobic tablice skladajaca sie z 3 cyfr np. 
// uklad = abcd z czego kolejno: a-uklad, b-figura znaczaca, c-figura mniej znaczaca, d-kicker; 
// dla ulatwienia okreslenia sily ukladu, moze na tablicy 2D gdzie [K,W] K-kolumna z indeksem reki i w W po kolei w dół jak w poprzednim przykladzie
// lub tablica tab[a,b,c,d]

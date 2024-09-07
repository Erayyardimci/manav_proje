using System;
using System.Collections.Generic;

namespace manav_proje
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, double> manavStok = new Dictionary<string, double>();
            Dictionary<string, double> musteriAlisveris = new Dictionary<string, double>();

            HaldenUrunAl(manavStok);
            ManavIslemleri(manavStok, musteriAlisveris);
            MusteriAlisverisi(musteriAlisveris);
        }

        static void HaldenUrunAl(Dictionary<string, double> manavStok)
        {
            Console.WriteLine("Halden manava ürün geldi.");
            while (true)
            {
                Console.WriteLine("Meyve mi Sebze mi almak istersiniz? (Çıkmak için 'çıkış' yazın)");
                string? urunTuru = Console.ReadLine()?.ToLower(); 
                if (urunTuru == "çıkış")
                    break;

                if (urunTuru != "meyve" && urunTuru != "sebze")
                {
                    Console.WriteLine("Geçersiz seçim. Lütfen 'meyve' veya 'sebze' yazın.");
                    continue;
                }

                List<string> urunler = urunTuru == "meyve" ? MeyveListesi() : SebzeListesi();
                UrunSecimi(urunler, manavStok);

                Console.WriteLine("Başka bir arzunuz var mı? (Evet/Hayır)");
                string? cevap = Console.ReadLine()?.ToLower(); 
                if (cevap == "hayır")
                    break;
            }

            Console.WriteLine("Hal işlemleri tamamlandı, manav kısmına geçiliyor.");
        }

        static void ManavIslemleri(Dictionary<string, double> manavStok, Dictionary<string, double> musteriAlisveris)
        {
            Console.WriteLine("Manav işlemleri başladı.");
            while (true)
            {
                Console.WriteLine("Meyve mi Sebze mi almak istersiniz? (Çıkmak için 'çıkış' yazın)");
                string? urunTuru = Console.ReadLine()?.ToLower(); 
                if (urunTuru == "çıkış")
                    break;

                if (urunTuru != "meyve" && urunTuru != "sebze")
                {
                    Console.WriteLine("Geçersiz seçim. Lütfen 'meyve' veya 'sebze' yazın.");
                    continue;
                }

                List<string> urunler = urunTuru == "meyve" ? MeyveListesi() : SebzeListesi();
                UrunSatisi(urunler, manavStok, musteriAlisveris);

                Console.WriteLine("Başka bir arzunuz var mı? (Evet/Hayır)");
                string? cevap = Console.ReadLine()?.ToLower(); 
                if (cevap == "hayır")
                {
                    Console.WriteLine("Afiyet olsun!");
                    break;
                }
            }
        }

        static List<string> MeyveListesi() => new List<string> { "Elma", "Armut", "Muz", "Kivi", "Çilek" };
        static List<string> SebzeListesi() => new List<string> { "Domates", "Salatalık", "Biber", "Patlıcan", "Lahana" };

        static void UrunSecimi(List<string> urunler, Dictionary<string, double> manavStok)
        {
            while (true)
            {
                Console.WriteLine("Lütfen bir ürün seçin:");
                for (int i = 0; i < urunler.Count; i++)
                    Console.WriteLine($"{i + 1}. {urunler[i]}");

                string? input = Console.ReadLine(); 
                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int secim) || secim < 1 || secim > urunler.Count)
                {
                    Console.WriteLine("Geçersiz seçim. Lütfen geçerli bir numara girin.");
                    continue;
                }

                string secilenUrun = urunler[secim - 1];
                Console.WriteLine("Kaç kilo almak istersiniz?");
                string? kiloInput = Console.ReadLine(); 
                if (string.IsNullOrEmpty(kiloInput) || !double.TryParse(kiloInput, out double kilo))
                {
                    Console.WriteLine("Geçersiz kilo değeri. Lütfen geçerli bir sayı girin.");
                    continue;
                }

                if (manavStok.ContainsKey(secilenUrun))
                    manavStok[secilenUrun] += kilo;
                else
                    manavStok[secilenUrun] = kilo;

                Console.WriteLine($"{kilo} kilo {secilenUrun} stoğa eklendi.");
                break;
            }
        }

        static void UrunSatisi(List<string> urunler, Dictionary<string, double> manavStok, Dictionary<string, double> musteriAlisveris)
        {
            while (true)
            {
                Console.WriteLine("Lütfen bir ürün seçin:");
                for (int i = 0; i < urunler.Count; i++)
                    Console.WriteLine($"{i + 1}. {urunler[i]}");

                string? input = Console.ReadLine(); 
                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int secim) || secim < 1 || secim > urunler.Count)
                {
                    Console.WriteLine("Geçersiz seçim. Lütfen geçerli bir numara girin.");
                    continue;
                }

                string secilenUrun = urunler[secim - 1];
                if (!manavStok.ContainsKey(secilenUrun) || manavStok[secilenUrun] == 0)
                {
                    Console.WriteLine($"Maalesef {secilenUrun} stokta yok.");
                    return;
                }

                Console.WriteLine("Kaç kilo almak istersiniz?");
                string? kiloInput = Console.ReadLine(); 
                if (string.IsNullOrEmpty(kiloInput) || !double.TryParse(kiloInput, out double kilo))
                {
                    Console.WriteLine("Geçersiz kilo değeri. Lütfen geçerli bir sayı girin.");
                    return;
                }

                if (manavStok[secilenUrun] >= kilo)
                {
                    manavStok[secilenUrun] -= kilo;
                    if (musteriAlisveris.ContainsKey(secilenUrun))
                        musteriAlisveris[secilenUrun] += kilo;
                    else
                        musteriAlisveris[secilenUrun] = kilo;
                    Console.WriteLine($"{kilo} kilo {secilenUrun} satıldı.");
                }
                else
                {
                    Console.WriteLine($"Stokta sadece {manavStok[secilenUrun]} kilo {secilenUrun} var, tamamını alabilirsiniz.");
                    musteriAlisveris[secilenUrun] = manavStok[secilenUrun];
                    manavStok[secilenUrun] = 0;
                }
                break;
            }
        }

        static void MusteriAlisverisi(Dictionary<string, double> musteriAlisveris)
        {
            Console.WriteLine("Alışverişinizin özeti:");
            foreach (var urun in musteriAlisveris)
            {
                Console.WriteLine($"{urun.Value} kilo {urun.Key}");
            }
            Console.WriteLine("Teşekkür ederiz, yine bekleriz!");
        }
    }
}

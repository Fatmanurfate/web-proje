using Microsoft.AspNetCore.Mvc;
using KutuphaneWeb.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace KutuphaneWeb.Controllers
{
    public class KitapController : Controller
    {
        // GÜNCELLENMİŞ GARANTİLİ KİTAP LİSTESİ (ISBN TABANLI)
        // KESİNLEŞTİRİLMİŞ KİTAP LİSTESİ (SABİT GÖRSELLER)
        private static List<Kitap> kitaplar = new List<Kitap>()
        {
            new Kitap { 
                Id = 2, 
                KitapAdi = "Suç ve Ceza", 
                Yazar = "Dostoyevski", 
                SayfaSayisi = 687,
                Ozet = "Raskolnikov'un vicdan muhasebesi ve ahlaki ikilemlerini konu alan dünya klasiği.",
                // Kaynak: Wikimedia Commons (Klasik Kapak)
                ResimUrl = "https://covers.openlibrary.org/b/isbn/9780140449136-L.jpg"
            },
            new Kitap { 
                Id = 3, 
                KitapAdi = "Sefiller", 
                Yazar = "Victor Hugo", 
                SayfaSayisi = 1724,
                Ozet = "Jean Valjean'ın yaşam mücadelesi üzerinden 19. yüzyıl Fransa'sının toplumsal eleştirisi.",
                // Kaynak: Wikimedia Commons (Klasik Cosette Çizimi)
                ResimUrl = "https://covers.openlibrary.org/b/isbn/9780451419439-L.jpg"
            },
            new Kitap { 
                Id = 4, 
                KitapAdi = "1984", 
                Yazar = "George Orwell", 
                SayfaSayisi = 352,
                Ozet = "Totaliter bir rejimin, bireyleri nasıl kontrol altına aldığını anlatan distopik bir başyapıt.",
                ResimUrl = "https://covers.openlibrary.org/b/isbn/9789750718533-L.jpg"
            },
            new Kitap { 
                Id = 5, 
                KitapAdi = "Kürk Mantolu Madonna", 
                Yazar = "Sabahattin Ali", 
                SayfaSayisi = 177,
                Ozet = "Raif Efendi'nin Almanya'da yaşadığı hüzünlü ve derin aşk hikayesi.",
                ResimUrl = "https://covers.openlibrary.org/b/isbn/9789753638029-L.jpg"
            },
            new Kitap { 
                Id = 6, 
                KitapAdi = "Simyacı", 
                Yazar = "Paulo Coelho", 
                SayfaSayisi = 188,
                Ozet = "Kendi kişisel menkıbesini gerçekleştirmek için yola çıkan Santiago'nun masalsı yolculuğu.",
                ResimUrl = "https://covers.openlibrary.org/b/isbn/9789750726439-L.jpg"
            },
            new Kitap { 
                Id = 7, 
                KitapAdi = "Şeker Portakalı", 
                Yazar = "J.M. de Vasconcelos", 
                SayfaSayisi = 182,
                Ozet = "Fakir bir ailenin hassas çocuğu Zeze'nin hayal dünyası ve hüzünlü büyüme öyküsü.",
                ResimUrl = "https://covers.openlibrary.org/b/isbn/9789750738609-L.jpg"
            },
            new Kitap { 
                Id = 8, 
                KitapAdi = "Harry Potter ve Felsefe Taşı", 
                Yazar = "J.K. Rowling", 
                SayfaSayisi = 276,
                Ozet = "Büyücülük okuluna kabul edilen yetim bir çocuğun macera dolu hikayesinin başlangıcı.",
                // Kaynak: Wikipedia (Orijinal Kapak)
                ResimUrl = "https://upload.wikimedia.org/wikipedia/en/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg"
            }
        };

        public IActionResult Index()
        {
            return View(kitaplar);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Kitap yeniKitap)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index");
            }

            if (kitaplar.Count > 0)
                yeniKitap.Id = kitaplar.Max(x => x.Id) + 1;
            else
                yeniKitap.Id = 1;
            
            // Kullanıcı resim girmezse varsayılan gri görsel ata
            if (string.IsNullOrEmpty(yeniKitap.ResimUrl))
            {
                yeniKitap.ResimUrl = "https://via.placeholder.com/150";
            }

            kitaplar.Add(yeniKitap);
            return RedirectToAction("Index");
        }
        
        public IActionResult Sil(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index");
            }

            var kitap = kitaplar.FirstOrDefault(x => x.Id == id);
            if (kitap != null)
            {
                kitaplar.Remove(kitap);
            }
            return RedirectToAction("Index");
        }
    }
}
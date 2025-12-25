using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using KutuphaneWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace KutuphaneWeb.Controllers
{
    public class AccountController : Controller
    {
        // Hafızadaki kullanıcı listesi (Varsayılan 1 Admin ekledik)
        private static List<Kullanici> kullanicilar = new List<Kullanici>()
        {
            new Kullanici { AdSoyad="Yönetici", Email="admin@kutuphane.com", Sifre="123", Rol="Admin" }
        };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string sifre)
        {
            var user = kullanicilar.FirstOrDefault(x => x.Email == email && x.Sifre == sifre);

            if (user != null)
            {
                // Giriş Başarılı: Bilgileri Session'a kaydet
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserRole", user.Rol);
                HttpContext.Session.SetString("UserName", user.AdSoyad);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Hata = "Email veya şifre hatalı!";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Kullanici k)
        {
            // Yeni kayıt olan herkes "Uye" olur, Admin olamaz.
            k.Rol = "Uye";
            kullanicilar.Add(k);
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Oturumu sil
            return RedirectToAction("Login");
        }
    }
}
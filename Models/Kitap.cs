namespace KutuphaneWeb.Models
{
    public class Kitap
    {
        public int Id { get; set; }
        public string KitapAdi { get; set; } = string.Empty;
        public string Yazar { get; set; } = string.Empty;
        public int SayfaSayisi { get; set; }
        // YENİ EKLENENLER:
        public string Ozet { get; set; } = "Özet bilgisi girilmedi.";
        public string ResimUrl { get; set; } = "https://via.placeholder.com/150"; // Varsayılan boş resim
    }
}
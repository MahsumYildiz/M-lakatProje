using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MülakatProje.Context;
using MülakatProje.Models;
using System.Text;

namespace MülakatProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly VeritabaniContext _context;
        public MusicController(VeritabaniContext context)
        {
            _context = context;
        }
        [HttpPost("sanatci-ekle2")]
        public IActionResult SanatciEkle(int albumAdet)
        {
            if(albumAdet < 6 || albumAdet > 15)
    {
                return BadRequest("Albüm adeti 6 ile 15 arasında olmalıdır.");
            }

            string harfler = "ABCÇDEFGHIİJKLMNOÖPRSŞTYVZabcçdefghıijklmnoöprsştyvz";

            // Sanatçı oluşturma
            Sanatci sanatci = new Sanatci
            {
                Ad = RandomString(harfler, 5),
                KurulusTarihi = RandomDay()
            };

            // Hata kontrolü
            if (string.IsNullOrWhiteSpace(sanatci.Ad) || sanatci.Ad.Length != 5)
            {
                return BadRequest("Sanatçı adı uzunluğu 5 karakter olmalıdır");
            }

            // Sanatçıyı veritabanına ekle
            _context.Sanatcilar.Add(sanatci);
            _context.SaveChanges();

            // Albümleri eklemek için döngü
            for (int i = 0; i < albumAdet; i++)
            {
                Album album = new Album
                {
                    Ad = RandomString(harfler, 10),
                    CikisTarihi = RandomDay(),
                    SanatciId = sanatci.Id // Albümün sanatçı ile ilişkisini ayarla
                };

                // Hata kontrolü
                if (string.IsNullOrWhiteSpace(album.Ad) || album.Ad.Length != 10)
                {
                    return BadRequest("Albüm adı uzunluğu 10 karakter olmalıdır");
                }

                // Albümü veritabanına ekle
                _context.Albumler.Add(album);
                _context.SaveChanges();

                // Albüme şarkıları eklemek için döngü
                int sarkiAdet = random.Next(6, 16); // Albümdeki şarkı sayısı 6 ile 15 arasında olmalı
                for (int j = 0; j < sarkiAdet; j++)
                {
                    Sarki sarki = new Sarki
                    {
                        Ad = RandomString(harfler, 15),
                        AlbumId = album.Id,// Şarkının albüm ile ilişkisini ayarla
                        SanatciId=sanatci.Id
                    };

                    // Hata kontrolü
                    if (string.IsNullOrWhiteSpace(sarki.Ad) || sarki.Ad.Length != 15)
                    {
                        return BadRequest("Şarkı adı uzunluğu 15 karakter olmalıdır");
                    }

                    // Şarkıyı veritabanına ekle
                    _context.Sarkilar.Add(sarki);
                }

                _context.SaveChanges(); // Şarkıları ve albümleri veritabanına kaydet
            }

            // Sanatçının ID'sini döndür
            return Ok(new { Id = sanatci.Id });
        }

        // Rastgele string üreten metot
        private static readonly Random random = new Random();

        private string RandomString(string harfler, int uzunluk)
        {
            if (uzunluk <= 0)
            {
                throw new ArgumentException("Uzunluk sıfırdan büyük olmalıdır.", nameof(uzunluk));
            }

            StringBuilder sb = new StringBuilder(uzunluk);
            for (int i = 0; i < uzunluk; i++)
            {
                sb.Append(harfler[random.Next(harfler.Length)]);
            }
            return sb.ToString();
        }

        // Rastgele tarih üreten metot
        private DateTime RandomDay()
        {
            DateTime start = new DateTime(1950, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        [HttpPost("sanatci-ekle")]
        public IActionResult SanatciEkle([FromBody] Sanatci yeniSanatci)
        {
            if (yeniSanatci == null)
            {
                return NotFound("Sanatçı bulunamadı");
            }
            _context.Sanatcilar.Add(yeniSanatci);
            _context.SaveChanges();
            return Ok("Sanatçı başarıyla eklendi.");
        }

        [HttpGet("sanatci-listele")]
        public IActionResult SanatciListele()
        {
            var sanatcilar = _context.Sanatcilar.ToList();
            return Ok(sanatcilar);
        }

        [HttpDelete("sanatci-sil/{id}")]
        public IActionResult SanatciSil(int id)
        {
            var sanatci = _context.Sanatcilar.Find(id);
            if (sanatci == null) return NotFound("Sanatçı bulunamadı.");
            _context.Sanatcilar.Remove(sanatci);
            _context.SaveChanges();
            return Ok("Sanatçı başarıyla silindi.");
        }

        [HttpPut("sanatci-guncelle/{id}")]
        public IActionResult SanatciGuncelle(int id, [FromBody] Sanatci guncellenmisSanatci)
        {
            var sanatci = _context.Sanatcilar.Find(id);
            if (sanatci == null) return NotFound("Sanatçı bulunamadı.");
            sanatci.Ad = guncellenmisSanatci.Ad;
            sanatci.KurulusTarihi = guncellenmisSanatci.KurulusTarihi;
            _context.SaveChanges();
            return Ok("Sanatçı başarıyla güncellendi.");
        }


        [HttpPost("sarki-ekle")]
        public IActionResult SarkiEkle([FromBody] Sarki yeniSarki)
        {
            if (yeniSarki == null)
            {
                return NotFound("Şarkı bulunamadı");
            }
            _context.Sarkilar.Add(yeniSarki);
            _context.SaveChanges();
            return Ok("Şarkı başarıyla eklendi.");
        }

        [HttpGet("sarki-listele")]
        public IActionResult SarkiListele()
        {
            var sarkilar = _context.Sarkilar.ToList();
            return Ok(sarkilar);
        }

        [HttpDelete("sarki-sil/{id}")]
        public IActionResult SarkiSil(int id)
        {
            var sarki = _context.Sarkilar.Find(id);
            if (sarki == null) return NotFound("Şarkı bulunamadı.");
            _context.Sarkilar.Remove(sarki);
            _context.SaveChanges();
            return Ok("Şarkı başarıyla silindi.");
        }

        [HttpPut("sarki-guncelle/{id}")]
        public IActionResult SarkiGuncelle(int id, [FromBody] Sarki guncellenmisSarki)
        {
            var sarki = _context.Sarkilar.Find(id);
            if (sarki == null) return NotFound("Şarkı bulunamadı.");
            sarki.Ad = guncellenmisSarki.Ad;
            sarki.AlbumId = guncellenmisSarki.AlbumId;
            sarki.SanatciId = guncellenmisSarki.SanatciId;
            _context.SaveChanges();
            return Ok("Şarkı başarıyla güncellendi.");
        }


        [HttpPost("album-ekle")]
        public IActionResult AlbumEkle([FromBody] Album yeniAlbum)
        {
            if (yeniAlbum == null)
            {
                return NotFound("Album bulunamadı");
            }
            _context.Albumler.Add(yeniAlbum);
            _context.SaveChanges();
            return Ok("Albüm başarıyla eklendi.");
        }

        [HttpGet("album-listele")]
        public IActionResult AlbumListele()
        {
            var albumler = _context.Albumler.ToList();
            return Ok(albumler);
        }

        [HttpDelete("album-sil/{id}")]
        public IActionResult AlbumSil(int id)
        {
            var album = _context.Albumler.Find(id);
            if (album == null) return NotFound("Albüm bulunamadı.");
            _context.Albumler.Remove(album);
            _context.SaveChanges();
            return Ok("Albüm başarıyla silindi.");
        }

        [HttpPut("album-guncelle/{id}")]
        public IActionResult AlbumGuncelle(int id, [FromBody] Album guncellenmisAlbum)
        {
            var album = _context.Albumler.Find(id);
            if (album == null) return NotFound("Albüm bulunamadı.");
            album.Ad = guncellenmisAlbum.Ad;
            album.CikisTarihi = guncellenmisAlbum.CikisTarihi;
            album.SanatciId = guncellenmisAlbum.SanatciId;
            _context.SaveChanges();
            return Ok("Albüm başarıyla güncellendi.");
        }


        [HttpPost("calmalistesi-ekle")]
        public IActionResult CalmaListesiEkle([FromBody] CalmaListesi yeniCalmaListesi)
        {
            if (yeniCalmaListesi == null)
            {
                return NotFound("Çalma Listesi bulunamadı");
            }
            _context.CalmaListeleri.Add(yeniCalmaListesi);
            _context.SaveChanges();
            return Ok("Çalma listesi başarıyla eklendi.");
        }

        [HttpGet("calmalistesi-listele")]
        public IActionResult CalmaListesiListele()
        {
            var calmaListeleri = _context.CalmaListeleri.ToList();
            return Ok(calmaListeleri);
        }

        [HttpDelete("calmalistesi-sil/{id}")]
        public IActionResult CalmaListesiSil(int id)
        {
            var calmaListesi = _context.CalmaListeleri.Find(id);
            if (calmaListesi == null) return NotFound("Çalma listesi bulunamadı.");
            _context.CalmaListeleri.Remove(calmaListesi);
            _context.SaveChanges();
            return Ok("Çalma listesi başarıyla silindi.");
        }

        [HttpPut("calmalistesi-guncelle/{id}")]
        public IActionResult CalmaListesiGuncelle(int id, [FromBody] CalmaListesi guncellenmisCalmaListesi)
        {
            var calmaListesi = _context.CalmaListeleri.Find(id);
            if (calmaListesi == null) return NotFound("Çalma listesi bulunamadı.");
            calmaListesi.Ad = guncellenmisCalmaListesi.Ad;
            _context.SaveChanges();
            return Ok("Çalma listesi başarıyla güncellendi.");
        }


        [HttpPost("calmalistesisarkisi-ekle")]
        public IActionResult CalmaListesiSarkisiEkle([FromBody] CalmaListesiSarkilari yeniCalmaListesiSarkisi)
        {
            if (yeniCalmaListesiSarkisi == null)
            {
                return NotFound("Yeni CalmaListesi Sarkisi bulunamadı");
            }
            _context.CalmaListesiSarkilari.Add(yeniCalmaListesiSarkisi);
            _context.SaveChanges();
            return Ok("Çalma listesi şarkısı başarıyla eklendi.");
        }

        [HttpGet("calmalistesisarkisi-listele")]
        public IActionResult CalmaListesiSarkisiListele()
        {
            var calmaListesiSarkilari = _context.CalmaListesiSarkilari.ToList();
            return Ok(calmaListesiSarkilari);
        }

        [HttpDelete("calmalistesisarkisi-sil/{id}")]
        public IActionResult CalmaListesiSarkisiSil(int id)
        {
            var calmaListesiSarkisi = _context.CalmaListesiSarkilari.Find(id);
            if (calmaListesiSarkisi == null) return NotFound("Çalma listesi şarkısı bulunamadı.");
            _context.CalmaListesiSarkilari.Remove(calmaListesiSarkisi);
            _context.SaveChanges();
            return Ok("Çalma listesi şarkısı başarıyla silindi.");
        }

        [HttpPut("calmalistesisarkisi-guncelle/{id}")]
        public IActionResult CalmaListesiSarkisiGuncelle(int id, [FromBody] CalmaListesiSarkilari guncellenmisCalmaListesiSarkisi)
        {
            var calmaListesiSarkisi = _context.CalmaListesiSarkilari.Find(id);
            if (calmaListesiSarkisi == null) return NotFound("Çalma listesi şarkısı bulunamadı.");
            calmaListesiSarkisi.SarkiId = guncellenmisCalmaListesiSarkisi.SarkiId;
            calmaListesiSarkisi.CalmaListesiId = guncellenmisCalmaListesiSarkisi.CalmaListesiId;
            _context.SaveChanges();
            return Ok("Çalma listesi şarkısı başarıyla güncellendi.");
        }

    }
}

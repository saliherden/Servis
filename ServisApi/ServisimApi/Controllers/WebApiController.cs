using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServisimApi.Models;

namespace ServisimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiController : ControllerBase
    {

        private readonly ServisContext _context;

        public WebApiController(ServisContext context)
        {
            _context = context;
        }

        #region Kullanici
        [HttpPost("Kullanici/Register")] //Kayıt olma- telefon daha once girilmiş ise günceller güncelleme
        public async Task<ActionResult<Kullanicilar>> PostKullanicilar(Kullanicilar kullanicilar)
        {
            var kullanicilarvarmi = await _context.Kullanicilar.CountAsync(x => x.Telefon == kullanicilar.Telefon);
            if (kullanicilarvarmi > 0)
            {
                var entity = await _context.Kullanicilar.FirstOrDefaultAsync(item => item.Telefon == kullanicilar.Telefon);

                if (entity != null)
                {
                    entity.Adi = kullanicilar.Adi;
                    entity.Soyadi = kullanicilar.Soyadi;
                    entity.Adres = kullanicilar.Adres;
                    entity.AdresLat = kullanicilar.AdresLat;
                    entity.AdresLong = kullanicilar.AdresLong;
                    entity.FirmaId = kullanicilar.FirmaId;
                    entity.Password = (kullanicilar.Adi + kullanicilar.Telefon.Substring(9)).ToLower();
                    entity.DeviceId = kullanicilar.DeviceId;
                    entity.UpdateDate = DateTime.Now;

                    await _context.SaveChangesAsync();
                    return entity;
                }

            }
            kullanicilar.CreateDate = DateTime.Now;
            kullanicilar.Username = (kullanicilar.Adi + kullanicilar.Soyadi).ToLower();
            kullanicilar.Password = (kullanicilar.Adi + kullanicilar.Telefon.Substring(9)).ToLower();
            _context.Kullanicilar.Add(kullanicilar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKullanicilar", new { id = kullanicilar.Id }, kullanicilar);
        }

        [HttpPost("Kullanici/Login")] //Kayıt olma güncelleme
        public async Task<ActionResult<Kullanicilar>> kLogin(KullaniciLoginPostModel kullanicilar)
        {
            var kullanici = await _context.Kullanicilar.FirstOrDefaultAsync(x => x.Username == kullanicilar.Username && x.Password == kullanicilar.Password);

            if (kullanici != null)
            {
                KullaniciLoginDonusModel kullanici1 = new KullaniciLoginDonusModel();
                kullanici1.Id = kullanici.Id;
                kullanici1.Adi = kullanici.Adi;
                kullanici1.Soyadi = kullanici.Soyadi;
                kullanici1.Username = kullanici.Username;
                kullanici1.Password = kullanici.Password;
                kullanici1.Telefon = kullanici.Telefon;
                kullanici1.Adres = kullanici.Adres;
                kullanici1.AdresLat = kullanici.AdresLat;
                kullanici1.AdresLong = kullanici.AdresLong;
                kullanici1.KategoriId = kullanici.KategoriId;
                kullanici1.DeviceId = kullanici.DeviceId;
                kullanici1.FirmaId = kullanici.FirmaId;
                kullanici1.versions= await VersionCheckAsync(kullanicilar.Device, kullanicilar.Version);

                return Ok(kullanici1);
            }


            return StatusCode(403, "Kullanıcı bulunamadı lütfen firmanız ile iletişime geçiniz.");
        }

        // GET: api/webapi/Duraklar/1 = 1=firmaid
        [HttpPost("Duraklarigetir")]
        public async Task<ActionResult<RotaDuraklariTimeString>> GetRotaDuraklari(DuraklariGetirModel duraklariGetir)
        {

            var rotasi = await _context.Rotalar.FirstOrDefaultAsync(x => x.RotaSaatId == duraklariGetir.RotaSaatID);//rotasaatinden rotayı bulduk
            //string d= Encoding.UTF8.GetString(rotasi.Rota).Replace("[","['");

            string bbb = "[";
            bbb += Encoding.UTF8.GetString(rotasi.Rota);

            bbb += "]";

            //JObject json = JObject.Parse(bbb);

            //byte[] base64EncodedBytes = Convert.FromBase64String(bbb);
            //string aaa = Encoding.UTF8.GetString(base64EncodedBytes);
            //Object rota = JsonConvert.DeserializeObject(bbb);

            //string output = JsonConvert.SerializeObject(bbb);

            //string foobarString = "[['Foo', 'bar'], ['Foo', 'bar'], ['Foo', 'bar']]";
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string[][]>(bbb);
            try
            {
                var rotaDuraklari = await _context.RotaDuraklari.Where(x => x.FirmaId == duraklariGetir.FirmaID && x.RotaId == rotasi.Id).ToListAsync();
                #region sadece duraklar donuyordu kapandı rotasıda eklendi
                //List<RotaDuraklariTimeString> son = new List<RotaDuraklariTimeString>();
                //foreach (var item in rotaDuraklari)
                //{
                //    RotaDuraklariTimeString c = new RotaDuraklariTimeString();
                //    c.Id = item.Id;
                //    c.DurakLat = item.DurakLat;
                //    c.DurakLong = item.DurakLong;
                //    c.DurakSaati = item.DurakSaati.ToString();
                //    c.DurakIsmi = item.DurakIsmi;
                //    c.RotaId = item.RotaId;
                //    c.FirmaId = item.FirmaId;
                //    c.CreateDate = item.CreateDate;
                //    c.UpdateDate = item.UpdateDate;

                //    son.Add(c);
                //}
                #endregion

                Rotaver ver = new Rotaver();
                foreach (var item in rotaDuraklari)
                {
                    RotaDuraklariTimeString c = new RotaDuraklariTimeString();
                    c.Id = item.Id;
                    c.DurakLat = item.DurakLat;
                    c.DurakLong = item.DurakLong;
                    c.DurakSaati = item.DurakSaati.ToString();
                    c.DurakIsmi = item.DurakIsmi;
                    c.RotaId = item.RotaId;
                    c.FirmaId = item.FirmaId;
                    c.CreateDate = item.CreateDate;
                    c.UpdateDate = item.UpdateDate;

                    ver.duraklar.Add(c);
                }
                ver.Rotadon = result;
                if (rotaDuraklari == null)
                {
                    return NotFound();
                }

                return Ok(new { duraklar = ver.duraklar, rota = ver.Rotadon });
            }
            catch (Exception)
            {

                return StatusCode(422);
            }







        }

        [HttpPost("RotaSaatlerigetir/")]
        public async Task<ActionResult<RotaSaatleri>> getRotaSaatleri(Kullanicilar kullanicilar)
        {
            var rotaDuraklari = await _context.RotaSaatleri.Where(x => x.FirmaId == kullanicilar.FirmaId /*&& x.KategoriId == kullanicilar.KategoriId*/).ToListAsync();
            List<RotaSaatleriTimeString> son = new List<RotaSaatleriTimeString>();
            //TimeSpan suan = DateTime.Now.TimeOfDay;
            //TimeSpan duraksaati;
            foreach (var item in rotaDuraklari)
            {
                //duraksaati = (TimeSpan)item.ServisSaati;
                //if (suan.CompareTo(duraksaati)==-1)
                //{
                RotaSaatleriTimeString c = new RotaSaatleriTimeString();
                c.Id = item.Id;
                c.Baslik = item.Baslik;
                c.ServisSaati = item.ServisSaati.ToString();
                c.RotaId = item.RotaId;
                c.FirmaId = item.FirmaId;
                c.CreateDate = item.CreateDate;
                c.UpdateDate = item.UpdateDate;
                son.Add(c);
                //}
            }


            if (rotaDuraklari == null)
            {
                return NotFound();
            }

            return Ok(son);
        }



        [HttpPost("SeyahatAL/")]
        public async Task<ActionResult<Seyahatler>> Seyahatal(Seyahatler seyahatler)
        {
            var bugunalmismi = await _context.Seyahatler.Where(x => x.KullaniciId == seyahatler.KullaniciId && x.IptalDurum == false).Select(x => x.SeyahatTarihi).ToListAsync();
            //            var yarinidaalmismi = await _context.Seyahatler.Where(x => x.KullaniciId == seyahatler.KullaniciId).Select(x => x.SeyahatTarihi).ToListAsync();
            DateTime kTarih = DateTime.Today;
            DateTime bTarih;
            DateTime suan = DateTime.Now;
            DateTime saat6sonrası = Convert.ToDateTime(kTarih.ToString("dd-MM-yyyy") + " " + "18:00:00");
            foreach (var item in bugunalmismi)
            {
                bTarih = item.Value;

                //if (suan.CompareTo(saat6sonrası) == 1)
                //{
                //    var yarinidaalmismi = await _context.Seyahatler.Where(x => x.KullaniciId == seyahatler.KullaniciId).Select(x => x.SeyahatTarihi).ToListAsync();
                //    DateTime bTarihi;
                //    DateTime kTarihi = DateTime.Today.AddDays(1);
                //    foreach (var itemm in yarinidaalmismi)
                //    {
                //        bTarihi = itemm.Value;
                //        if (bTarihi.CompareTo(kTarihi) == 0)
                //        {

                //            return StatusCode(403, new { mesaj = "Yarın için alınmış seyahat bulundu. Birdahaki gün seyahat almak için " + kTarih.AddDays(1).ToString("dd-MM-yyyy") + " 18:00 dan sonra deneyiniz." });

                //        }
                //    }


                //    seyahatler.SeyahatTarihi = Convert.ToDateTime(DateTime.Today.AddDays(1).ToString("dd-MM-yyyy"));
                //    seyahatler.CreateDate = DateTime.Now;
                //    _context.Seyahatler.Add(seyahatler);
                //    await _context.SaveChangesAsync();
                //    return Ok(seyahatler);
                //}

                if (bTarih.CompareTo(kTarih) == 0 && suan.CompareTo(saat6sonrası) == -1)//bugun varsa
                {

                    return StatusCode(403, new { mesaj = "Geçerli seyahatiniz bulundu.Bir sonraki güne seyahat almak için " + kTarih.ToString("dd-MM-yyyy") + " 18:00 dan sonra deneyiniz." });
                }
                DateTime kTarihi = DateTime.Today.AddDays(1);
                if (bTarih.CompareTo(kTarihi) == 0)//yarın varsa
                {
                    return StatusCode(403, new { mesaj = "Geçerli seyahatiniz bulundu.Bir sonraki güne seyahat almak için " + kTarih.AddDays(1).ToString("dd-MM-yyyy") + " 18:00 dan sonra deneye bilirsiniz." });

                }

            }
            if (suan.CompareTo(saat6sonrası) == -1)
                seyahatler.SeyahatTarihi = Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"));//18:00 dan onceyse bugune al
            else
                seyahatler.SeyahatTarihi = Convert.ToDateTime(DateTime.Today.AddDays(1).ToString("dd-MM-yyyy"));//18:00 dan sonraysa yarına al

            seyahatler.IptalDurum = false;
            seyahatler.IndiDurum = false;
            seyahatler.BindiDurum = false;
            //seyahatler.SeyahatTarihi = Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"));

            seyahatler.CreateDate = DateTime.Now;
            _context.Seyahatler.Add(seyahatler);
            await _context.SaveChangesAsync();


            return Ok(seyahatler);
        }


        [HttpPost("SeyahatListele/")]
        public async Task<ActionResult<Seyahatler>> Seyahatler(Seyahatler seyahatler)
        {
            var seyahat = await _context.Seyahatler.Where(x => x.KullaniciId == seyahatler.KullaniciId).OrderByDescending(x=>x.SeyahatTarihi).ToListAsync();



            if (seyahat.Count==0)
            {
                return StatusCode(403, new { mesaj = "Seyatiniz bulunamadı." });
            }

            List<SeyahatListeleModel> seyahatlerdizi = new List<SeyahatListeleModel>();
            for(int i=0;i<=seyahat.Count-1;i++){
                SeyahatListeleModel seyahatListele = new SeyahatListeleModel();
                seyahatListele.Id = seyahat[i].Id;
                seyahatListele.AracPlaka = await _context.Araclar.Where(x => x.Id == seyahat[i].AracId).Select(x=> x.PlakaNo).FirstOrDefaultAsync();
                seyahatListele.SurucuAdiSoyadi = await _context.Suruculer.Where(x => x.Id == seyahat[i].SurucuId).Select(x =>x.Adi+" "+x.Soyadi).FirstOrDefaultAsync();
                seyahatListele.KullaniciAdiSoyadi = await _context.Kullanicilar.Where(x => x.Id == seyahat[i].KullaniciId).Select(x => x.Adi + " " + x.Soyadi).FirstOrDefaultAsync();
                seyahatListele.RotaId = seyahat[i].RotaId;

                //List<RotaAraci> araclar = await _context.RotaAraci.Where(x => x.RotaId == seyahat[i].RotaId&&x.RotaCikisTarih==seyahat[i].SeyahatTarihi).ToListAsync();
                //seyahatListele.rotaAraclari = new List<RotaAraci>();
                //foreach (RotaAraci item in araclar)
                //{
                //    seyahatListele.rotaAraclari.Add(item);
                //}
                
                seyahatListele.RotaSaati = await _context.RotaSaatleri.Where(x => x.Id == seyahat[i].RotaSaatId).Select(x => x.Baslik + " " + x.ServisSaati.ToString().Substring(0, x.ServisSaati.ToString().Length - 11)).FirstOrDefaultAsync();
                seyahatListele.DurakAdiSaati = await _context.RotaDuraklari.Where(x => x.Id == seyahat[i].DurakId).Select(x => x.DurakIsmi + " " + x.DurakSaati.ToString().Substring(0,5)).FirstOrDefaultAsync();
                seyahatListele.FirmaAdi = await _context.Firmalar.Where(x => x.Id == seyahat[i].FirmaId).Select(x => x.FrimaAdı).FirstOrDefaultAsync();
                seyahatListele.SeyahatTarihi = (seyahat[i].SeyahatTarihi.ToString()).Substring(0,10);
                seyahatListele.IptalDurum = seyahat[i].IptalDurum;
                seyahatListele.BindiDurum = seyahat[i].BindiDurum;
                seyahatListele.IndiDurum = seyahat[i].IndiDurum;
                seyahatListele.CreateDate = seyahat[i].CreateDate;
                seyahatListele.UpdateDate = DateTime.Now;
                seyahatlerdizi.Add(seyahatListele);
            }
           
            return Ok(seyahatlerdizi);
        }


        [HttpPost("SeyahatIptali/")]
        public async Task<ActionResult<Seyahatler>> SeyahatIptal(Seyahatler seyahatler)
        {
            var seyahat = await _context.Seyahatler.FirstOrDefaultAsync(x => x.Id == seyahatler.Id&&x.IptalDurum==false);
            if (seyahat == null)
            {
                return StatusCode(403,new { mesaj = "Iptal etmek istediğiniz seyahat bulunamadı." });
            }

            seyahat.Id = seyahatler.Id;
            seyahat.AracId = seyahat.AracId;
            seyahat.SurucuId = seyahat.SurucuId;
            seyahat.KullaniciId = seyahat.KullaniciId;
            seyahat.RotaId = seyahat.RotaId;
            seyahat.RotaSaatId = seyahat.RotaSaatId;
            seyahat.DurakId = seyahat.DurakId;
            seyahat.FirmaId = seyahat.FirmaId;
            seyahat.SeyahatTarihi = seyahat.SeyahatTarihi;
            seyahat.IptalDurum = true;
            seyahat.BindiDurum = seyahat.BindiDurum;
            seyahat.IndiDurum = seyahat.IndiDurum;
            seyahat.CreateDate = seyahat.CreateDate;
            seyahat.UpdateDate = DateTime.Now;
            _context.Update(seyahat);
            await _context.SaveChangesAsync();
            return Ok(seyahat);
        }
        

        [HttpPost("AraciTakipet/")]
        public async Task<ActionResult<RotaAraci>>rotaAraci(RotaAraciPostModel araci)
        {
            var arac = await _context.RotaAraci.Where(x => x.RotaId == araci.RotaId && x.RotaCikisTarih == Convert.ToDateTime(araci.seyahatTarihi)).ToListAsync();
            if (arac.Count == 0)
            {
                return StatusCode(403, new { mesaj = "Servisiniz yola çıkmadı." });
            }
            List<RotaAraclariModel> suruculer = new List<RotaAraclariModel>();
            for(int i = 0; i <= arac.Count - 1; i++)
            {
                RotaAraclariModel aracSuruculeri = new RotaAraclariModel();
                aracSuruculeri.Id = arac[i].Id;
                aracSuruculeri.SurucuAdiSoyadi = await _context.Suruculer.Where(x => x.Id == arac[i].SurucuId).Select(x => x.Adi + " " + x.Soyadi).FirstOrDefaultAsync();
                aracSuruculeri.SurucuTelefon = await _context.Suruculer.Where(x => x.Id == arac[i].SurucuId).Select(x => x.Telefon).FirstOrDefaultAsync();
                aracSuruculeri.AracPlaka = await _context.Araclar.Where(x => x.Id == arac[i].AracId).Select(x => x.PlakaNo).FirstOrDefaultAsync();
                suruculer.Add(aracSuruculeri);
            }
            return Ok(suruculer);
        }

        [HttpPost("KullaniciDeviceID")]
        public async Task<ActionResult<Kullanicilar>> kDeviceid(Kullanicilar kullanicilar)
        {
            
            try
            {
                var kıd = await _context.Kullanicilar.FirstOrDefaultAsync(x => x.Id == kullanicilar.Id);
                if (kıd == null) {
                    return StatusCode(403, new { mesaj = "Kullanıcı bulunamadı." });
                }
                if(kullanicilar.DeviceId==null)
                    return StatusCode(403, new { mesaj = "Device id geçerli değil." });
                kıd.DeviceId = kullanicilar.DeviceId;
                await _context.SaveChangesAsync();
                return Ok(new { mesaj = kıd.Adi + " " + kıd.Soyadi + "  Kişisine Deviceid=" + kullanicilar.DeviceId + " Başarı ile eklendi." });
            }
            catch (Exception e)
            {

                return StatusCode(403,new { mesaj ="Device id ekleme başarısız." });
            }
            

            
        }

        [HttpPost("Kullanici/Servisaraclari")]
        public async Task<ActionResult<RotaAraci>> servisaraci(PostModelServisler rotaAraci)
        {
            var arac = await _context.RotaAraci.Where(x => x.RotaCikisTarih == Convert.ToDateTime(rotaAraci.Seyahattarih) &&x.RotaId==rotaAraci.RotaID).ToListAsync();
            if (arac.Count < 1)
            {
                return StatusCode(403, new { mesaj = "Güzergahınıza aid servis yola çıkmadı." });
            }
            List<ServislerModel> servisler = new List<ServislerModel>();
            for(int i = 0; i <= arac.Count-1; i++)
            {
                ServislerModel servis = new ServislerModel();
                servis.AracID = arac[i].AracId;
                servis.PlakaNo = await _context.Araclar.Where(x => x.Id == arac[i].AracId).Select(x => x.PlakaNo).FirstOrDefaultAsync();
                int? surucuid= await _context.Araclar.Where(x => x.Id == arac[i].AracId).Select(x => x.SurucuId).FirstOrDefaultAsync();
                if(surucuid>0 || surucuid != null)
                {
                    servis.SurucuID = surucuid;
                    servis.SurucuAdSoyad = await _context.Suruculer.Where(x => x.Id == surucuid).Select(x => x.Adi + " " + x.Soyadi).FirstOrDefaultAsync();
                }
                servisler.Add(servis);
            }

            return Ok(servisler);
        }

        [HttpPost("Kullanici/Bindi")]
        public async Task<ActionResult<Kullanicilar>> kBindi(PostModelKullaniciBindi post)
        {

            var seyahat = await _context.Seyahatler.FindAsync(post.SeyahatID);
            seyahat.BindiDurum = true;
            seyahat.AracId = post.AracID;
            seyahat.SurucuId = post.SurucuID;
            seyahat.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(new {mesaj="Bindi durumu başarı ile gerçekleşti" });

        }
        
        [HttpPost("Kullanici/Indi")]
        public async Task<ActionResult<Kullanicilar>> kindi(PostModelKullaniciBindi post)
        {

            var seyahat = await _context.Seyahatler.FindAsync(post.SeyahatID);
            seyahat.IndiDurum = true;
            seyahat.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(new {mesaj="İndi durumu başarı ile gerçekleşti" });

        }

        #endregion



        #region Surucu
        [HttpPost("Surucu/Login")] //Kayıt olma güncelleme
        public async Task<ActionResult<Kullanicilar>> sLogin(SurucuLoginPostModel surucu)
        {
            var kullanici = await _context.Suruculer.FirstOrDefaultAsync(x => x.Username == surucu.Username && x.Password == surucu.Password);
            var arac = await _context.Araclar.FirstOrDefaultAsync(x => x.PlakaNo == surucu.AracPlaka);

            


            if (kullanici != null && arac!=null)
            {
                //var vers =await VersionCheckAsync(surucu.Device, surucu.Version);
                SurucuLoginDonusModel don = new SurucuLoginDonusModel();
                don.Id = kullanici.Id;
                don.Adi = kullanici.Adi;
                don.Soyadi = kullanici.Soyadi;
                don.Telefon = kullanici.Telefon;
                don.Adres = kullanici.Adres;
                don.Username = kullanici.Username;
                don.Password = kullanici.Password;
                don.DeviceId = kullanici.DeviceId;
                don.FirmaId = kullanici.FirmaId;
                don.CreateDate = kullanici.CreateDate;
                don.UpdateDate = kullanici.UpdateDate;
                don.AracID = arac.Id;
                don.versions= await VersionCheckAsync(surucu.Device, surucu.Version);


                return Ok(don);
            }


            return StatusCode(403, new { mesaj= "Sürücü bulunamadı lütfen firmanız ile iletişime geçiniz." });
        }

        [HttpPost("Surucu/RotaSaatlerigetir")]
        public async Task<ActionResult<RotaSaatleri>> getSrotaSaatleri(Suruculer kullanicilar)
        {
            var rotaDuraklari = await _context.RotaSaatleri.Where(x => x.FirmaId == kullanicilar.FirmaId).ToListAsync();
            List<RotaSaatleriTimeString> son = new List<RotaSaatleriTimeString>();
            //TimeSpan suan = DateTime.Now.TimeOfDay;
            //TimeSpan duraksaati;
            foreach (var item in rotaDuraklari)
            {
                //duraksaati = (TimeSpan)item.ServisSaati;
                //if (suan.CompareTo(duraksaati)==-1)
                //{
                RotaSaatleriTimeString c = new RotaSaatleriTimeString();
                c.Id = item.Id;
                c.Baslik = item.Baslik;
                c.ServisSaati = item.ServisSaati.ToString();
                c.RotaId = item.RotaId;
                c.FirmaId = item.FirmaId;
                c.CreateDate = item.CreateDate;
                c.UpdateDate = item.UpdateDate;
                son.Add(c);
                //}
            }


            if (rotaDuraklari == null)
            {
                return NotFound();
            }

            return Ok(son);
        }


        [HttpPost("Surucu/Duraklarigetir")]
        public async Task<ActionResult<RotaDuraklariTimeString>> GetsSurucuRota(DuraklariGetirModel duraklariGetir)
        {

            var rotasi = await _context.Rotalar.FirstOrDefaultAsync(x => x.RotaSaatId == duraklariGetir.RotaSaatID);//rotasaatinden rotayı bulduk
            //string d= Encoding.UTF8.GetString(rotasi.Rota).Replace("[","['");

            string bbb = "[";
            bbb += Encoding.UTF8.GetString(rotasi.Rota);

            bbb += "]";

            //JObject json = JObject.Parse(bbb);

            //byte[] base64EncodedBytes = Convert.FromBase64String(bbb);
            //string aaa = Encoding.UTF8.GetString(base64EncodedBytes);
            //Object rota = JsonConvert.DeserializeObject(bbb);

            //string output = JsonConvert.SerializeObject(bbb);

            //string foobarString = "[['Foo', 'bar'], ['Foo', 'bar'], ['Foo', 'bar']]";
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string[][]>(bbb);
            try
            {
                var rotaDuraklari = await _context.RotaDuraklari.Where(x => x.FirmaId == duraklariGetir.FirmaID && x.RotaId == rotasi.Id).ToListAsync();
                #region sadece duraklar donuyordu kapandı rotasıda eklendi
                //List<RotaDuraklariTimeString> son = new List<RotaDuraklariTimeString>();
                //foreach (var item in rotaDuraklari)
                //{
                //    RotaDuraklariTimeString c = new RotaDuraklariTimeString();
                //    c.Id = item.Id;
                //    c.DurakLat = item.DurakLat;
                //    c.DurakLong = item.DurakLong;
                //    c.DurakSaati = item.DurakSaati.ToString();
                //    c.DurakIsmi = item.DurakIsmi;
                //    c.RotaId = item.RotaId;
                //    c.FirmaId = item.FirmaId;
                //    c.CreateDate = item.CreateDate;
                //    c.UpdateDate = item.UpdateDate;

                //    son.Add(c);
                //}
                #endregion
                DateTime now = DateTime.Now;
                
                Rotaver1 ver = new Rotaver1();
                foreach (var item in rotaDuraklari)
                {
                    RotaDuraklariTimeSurucuKulln c = new RotaDuraklariTimeSurucuKulln();
                    
                    c.Id = item.Id;
                    c.DurakLat = item.DurakLat;
                    c.DurakLong = item.DurakLong;
                    c.DurakSaati = item.DurakSaati.ToString();
                    c.DurakIsmi = item.DurakIsmi;
                    c.RotaId = item.RotaId;
                    c.FirmaId = item.FirmaId;
                    c.CreateDate = item.CreateDate;
                    c.UpdateDate = item.UpdateDate;
                    c.duraktakiler = new List<DuraktakilerModel>();
                    DuraktakilerModel d = new DuraktakilerModel();
                    var idler= await _context.Seyahatler.Where(x => x.DurakId == item.Id&&x.IptalDurum==false && x.SeyahatTarihi == Convert.ToDateTime(now.ToShortDateString())).Select(x=> x.KullaniciId).ToListAsync();
                    for (int i = 0; i < idler.Count; i++)
                    {
                        d.AdiSoyadi = await _context.Kullanicilar.Where(x => x.Id == idler[i]).Select(x=>x.Adi+" "+ x.Soyadi).FirstOrDefaultAsync();
                        d.TelefonNo = await _context.Kullanicilar.Where(x => x.Id == idler[i]).Select(x=>x.Telefon).FirstOrDefaultAsync();
                        c.duraktakiler.Add(d);
                    }
                    ver.duraklar.Add(c);
                }
                ver.Rotadon = result;
                if (rotaDuraklari == null)
                {
                    return NotFound();
                }
                object a = new { duraklar = ver.duraklar, rota = ver.Rotadon };
                return Ok(a);
            }
            catch (Exception e)
            {

                return StatusCode(422);
            }







        }

        [HttpPost("Surucu/Rotacik")]
        public async Task<ActionResult<RotaAraci>> postRotaaraci(RotaAraci rotaAraci)
        {
            rotaAraci.RotaCikisTarih=Convert.ToDateTime(DateTime.Today.ToShortDateString());
            rotaAraci.CreateDate = DateTime.Now;
            _context.RotaAraci.Add(rotaAraci);
            await _context.SaveChangesAsync();

            return Ok(rotaAraci);
        }

        [HttpPost("Surucu/DeviceID")]
        public async Task<ActionResult<RotaAraci>> postsurucudeviceid(Araclar araclar)
        {
            var arac = await _context.Araclar.FindAsync(araclar.Id);
            arac.DeviceId = araclar.DeviceId;
            if (araclar.SurucuId > 0)
            {
                arac.SurucuId = araclar.SurucuId;
            }
            await _context.SaveChangesAsync();

            return Ok(new {mesaj="Deviceid "+arac.PlakaNo+" araca başarı ile eklendi." });
        }

        [HttpPost("Surucu/Gecmis")]
        public async Task<ActionResult<RotaAraci>> postSurucuGecmis(RotaAraci arac)
        {
            var rotaAracin = await _context.RotaAraci.Where(x => x.AracId == arac.AracId &&x.SurucuId==arac.SurucuId).ToListAsync();
            
            List<SurucuGecmisModel> gecmis = new List<SurucuGecmisModel>();
            for(int i = 0; i <= rotaAracin.Count - 1; i++)
            {
                SurucuGecmisModel g = new SurucuGecmisModel();
                g.ID = i+1;
                g.AracPlakaNo = await _context.Araclar.Where(x => x.Id == rotaAracin[i].AracId).Select(x => x.PlakaNo).FirstOrDefaultAsync();
                g.SeyahatTarihi = rotaAracin[i].RotaCikisTarih.ToString().Substring(0,10);
                g.SurucuAdiSoyadi = await _context.Suruculer.Where(x => x.Id == rotaAracin[i].SurucuId).Select(x => x.Adi + " " + x.Soyadi).FirstOrDefaultAsync();
                int guzergahsaatid = await _context.Rotalar.Where(x => x.Id == rotaAracin[i].RotaId).Select(x => x.RotaSaatId).FirstOrDefaultAsync();
                g.GüzergahAdi = await _context.RotaSaatleri.Where(x => x.Id == guzergahsaatid).Select(x => x.Baslik).FirstOrDefaultAsync();
                g.IndiSayi = await _context.Seyahatler.CountAsync(x => x.AracId == rotaAracin[i].AracId && x.SurucuId == rotaAracin[i].SurucuId && x.SeyahatTarihi == rotaAracin[i].RotaCikisTarih &&x.IndiDurum==true);
                g.BindiSayi = await _context.Seyahatler.CountAsync(x => x.AracId == rotaAracin[i].AracId && x.SurucuId == rotaAracin[i].SurucuId && x.SeyahatTarihi == rotaAracin[i].RotaCikisTarih &&x.BindiDurum==true);
                gecmis.Add(g);
            }
            List<SurucuGecmisModel> gecmis1 = gecmis.OrderByDescending(x => x.SeyahatTarihi).ToList();
            return Ok(gecmis1);
        }


        #endregion

        #region Version Control method //VersionCheckAsync
        private async Task<Versions> VersionCheckAsync(int device, int version)
        {
            List<Versions> verss = await _context.Versions.OrderByDescending(x => x.Id)
                .Where(x => x.Device == device && x.Version >= version).ToListAsync();
            Versions vers;
            if (verss.Count > 0)
            {
                vers = verss.Find(x => x.Device == device);
            }
            else
            {
                vers = await _context.Versions.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Device == device);
            }



            if (!(verss.Find(x => x.Mandatory == true && x.Version != version) == null))
            {
                vers.Mandatory = true;
            }

           ;
            return vers;
        }
        #endregion
    }
}

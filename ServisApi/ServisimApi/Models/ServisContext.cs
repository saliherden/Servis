using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ServisimApi.Models
{
    public partial class ServisContext : DbContext
    {
        public ServisContext()
        {
        }

        public ServisContext(DbContextOptions<ServisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AracSahibleri> AracSahibleri { get; set; }
        public virtual DbSet<Araclar> Araclar { get; set; }
        public virtual DbSet<FirmaSahibleri> FirmaSahibleri { get; set; }
        public virtual DbSet<Firmalar> Firmalar { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kullanicilar> Kullanicilar { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<RotaAraci> RotaAraci { get; set; }
        public virtual DbSet<RotaDuraklari> RotaDuraklari { get; set; }
        public virtual DbSet<RotaSaatleri> RotaSaatleri { get; set; }
        public virtual DbSet<Rotalar> Rotalar { get; set; }
        public virtual DbSet<Seyahatler> Seyahatler { get; set; }
        public virtual DbSet<Suruculer> Suruculer { get; set; }
        public virtual DbSet<Versions> Versions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=HP7200ET;Database=Servis;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AracSahibleri>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adi).HasMaxLength(50);

                entity.Property(e => e.Adres).HasMaxLength(250);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Soyadi).HasMaxLength(50);

                entity.Property(e => e.Telefon).HasMaxLength(11);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Araclar>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AracSahibiId).HasColumnName("AracSahibiID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("DeviceID")
                    .HasMaxLength(500);

                entity.Property(e => e.FirmaId).HasColumnName("FirmaID");

                entity.Property(e => e.PlakaNo).HasMaxLength(11);

                entity.Property(e => e.SurucuId).HasColumnName("SurucuID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FirmaSahibleri>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adi).HasMaxLength(50);

                entity.Property(e => e.Adres).HasMaxLength(250);

                entity.Property(e => e.CepTel)
                    .HasColumnName("Cep_Tel")
                    .HasMaxLength(11);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.SabitTel)
                    .HasColumnName("Sabit_Tel")
                    .HasMaxLength(11);

                entity.Property(e => e.Soyadi).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Firmalar>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FirmaAdresi).HasMaxLength(250);

                entity.Property(e => e.FirmaSahibiId).HasColumnName("FirmaSahibiID");

                entity.Property(e => e.FirmaTelefonu).HasMaxLength(11);

                entity.Property(e => e.FrimaAdı).HasMaxLength(100);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.İl).HasMaxLength(50);

                entity.Property(e => e.İlce).HasMaxLength(50);
            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KategoriAciklama).HasMaxLength(250);

                entity.Property(e => e.KategoriAdi).HasMaxLength(100);
            });

            modelBuilder.Entity<Kullanicilar>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adi).HasMaxLength(50);

                entity.Property(e => e.Adres).HasMaxLength(250);

                entity.Property(e => e.AdresLat).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.AdresLong).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("DeviceID")
                    .HasMaxLength(500);

                entity.Property(e => e.FirmaId).HasColumnName("FirmaID");

                entity.Property(e => e.KategoriId).HasColumnName("KategoriID");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Soyadi).HasMaxLength(50);

                entity.Property(e => e.Telefon).HasMaxLength(11);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(80);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(80);
            });

            modelBuilder.Entity<RotaAraci>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AracId).HasColumnName("AracID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.RotaCikisTarih).HasColumnType("datetime");

                entity.Property(e => e.RotaId).HasColumnName("RotaID");

                entity.Property(e => e.SurucuId).HasColumnName("SurucuID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RotaDuraklari>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DurakIsmi).HasMaxLength(50);

                entity.Property(e => e.DurakLat).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.DurakLong).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.FirmaId).HasColumnName("FirmaID");

                entity.Property(e => e.RotaId).HasColumnName("RotaID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RotaSaatleri>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Baslik).HasMaxLength(150);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FirmaId).HasColumnName("FirmaID");

                entity.Property(e => e.KategoriId).HasColumnName("KategoriID");

                entity.Property(e => e.RotaId).HasColumnName("RotaID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Rotalar>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FirmaId).HasColumnName("FirmaID");

                entity.Property(e => e.KategoriId).HasColumnName("KategoriID");

                entity.Property(e => e.RotaIsmi).HasMaxLength(100);

                entity.Property(e => e.RotaSaatId).HasColumnName("RotaSaatID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Seyahatler>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AracId).HasColumnName("AracID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DurakId).HasColumnName("DurakID");

                entity.Property(e => e.FirmaId).HasColumnName("FirmaID");

                entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");

                entity.Property(e => e.RotaId).HasColumnName("RotaID");

                entity.Property(e => e.RotaSaatId).HasColumnName("RotaSaatID");

                entity.Property(e => e.SeyahatTarihi).HasColumnType("datetime");

                entity.Property(e => e.SurucuId).HasColumnName("SurucuID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Suruculer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adi).HasMaxLength(50);

                entity.Property(e => e.Adres).HasMaxLength(250);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("DeviceID")
                    .HasMaxLength(500);

                entity.Property(e => e.FirmaId).HasColumnName("FirmaID");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Soyadi).HasMaxLength(50);

                entity.Property(e => e.Telefon).HasMaxLength(11);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Versions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Device).HasColumnName("device");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(255);

                entity.Property(e => e.Mandatory).HasColumnName("mandatory");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

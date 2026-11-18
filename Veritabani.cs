using System;
using System.Data.SQLite;
using System.IO;

namespace KUAFÖR_RANDEVU_SİSTEMİ
{
    internal static class Veritabani
    {
        private static readonly string DosyaYolu = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "KuaforRandevuSistemi", "kuafor.db");

        public static SQLiteConnection BaglantiOlustur()
        {
            var builder = new SQLiteConnectionStringBuilder { DataSource = DosyaYolu, DateTimeFormat = SQLiteDateFormats.ISO8601 };
            return new SQLiteConnection(builder.ConnectionString);
        }

        public static void Hazirla()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DosyaYolu));
            using (var connection = BaglantiOlustur())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS personel (
                        tc_kimlik TEXT PRIMARY KEY NOT NULL, ad TEXT NOT NULL, soyad TEXT,
                        cep_telefon TEXT, dogum_tarihi TEXT NOT NULL, adres TEXT);
                        CREATE TABLE IF NOT EXISTS randevu (
                        tc_kimlik TEXT PRIMARY KEY NOT NULL, ad TEXT NOT NULL, soyad TEXT,
                        cep_telefon TEXT, randevu_tarihi TEXT NOT NULL, adres TEXT);
                        CREATE TABLE IF NOT EXISTS hizmet_fiyati (
                        hizmet TEXT PRIMARY KEY NOT NULL, kurus INTEGER NOT NULL CHECK (kurus >= 0));";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

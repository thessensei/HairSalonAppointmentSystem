using System;
using System.Data.SQLite;
using System.Globalization;
using System.Windows.Forms;

namespace KUAFÖR_RANDEVU_SİSTEMİ
{
    public partial class FiyatListesiFormu : Form
    {
        public FiyatListesiFormu()
        {
            InitializeComponent();
            Tema.Uygula(this);
            liste.Columns.Add("Hizmet", "Hizmet");
            liste.Columns.Add("Fiyat", "Fiyat (TL)");
            liste.Columns["Fiyat"].ValueType = typeof(decimal);
            liste.Columns["Fiyat"].DefaultCellStyle.Format = "N2";
            liste.Columns["Fiyat"].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("tr-TR");
            Load += (sender, e) => ListeyiYenile();
        }

        // Fiyatlar kayan nokta yerine tam sayı kuruş olarak saklanır.
        private void ListeyiYenile(Action<SQLiteConnection> degisiklik = null)
        {
            try
            {
                using (var db = Veritabani.BaglantiOlustur())
                {
                    db.Open();
                    degisiklik?.Invoke(db);
                    using (var cmd = new SQLiteCommand("SELECT hizmet, kurus FROM hizmet_fiyati ORDER BY hizmet", db))
                    using (var satirlar = cmd.ExecuteReader())
                    {
                        liste.Rows.Clear();
                        while (satirlar.Read())
                            liste.Rows.Add(satirlar.GetString(0), satirlar.GetInt64(1) / 100m);
                    }
                }
                liste.ClearSelection();
                hizmetAdi.Clear();
                fiyat.Value = 0;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Fiyat listesi işlemi tamamlanamadı: " + ex.Message, "Fiyat listesi");
            }
        }

        private void Kaydet_Click(object sender, EventArgs e)
        {
            string ad = hizmetAdi.Text.Trim();
            if (ad.Length == 0)
            {
                MessageBox.Show("Hizmet adını girin.", "Fiyat listesi");
                hizmetAdi.Focus();
                return;
            }
            long kurus = decimal.ToInt64(decimal.Round(fiyat.Value * 100m, 0, MidpointRounding.AwayFromZero));
            ListeyiYenile(db =>
            {
                using (var cmd = new SQLiteCommand(@"INSERT INTO hizmet_fiyati (hizmet, kurus) VALUES (@ad, @kurus)
                    ON CONFLICT(hizmet) DO UPDATE SET kurus = excluded.kurus", db))
                {
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@kurus", kurus);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        private void SecimDegisti(object sender, EventArgs e)
        {
            sil.Enabled = liste.SelectedRows.Count == 1;
            if (!sil.Enabled) return;
            var satir = liste.SelectedRows[0];
            hizmetAdi.Text = (string)satir.Cells["Hizmet"].Value;
            fiyat.Value = (decimal)satir.Cells["Fiyat"].Value;
        }

        private void Sil_Click(object sender, EventArgs e)
        {
            if (liste.SelectedRows.Count != 1) return;
            string ad = (string)liste.SelectedRows[0].Cells["Hizmet"].Value;
            if (MessageBox.Show($"“{ad}” listeden silinsin mi?", "Fiyat listesi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            ListeyiYenile(db =>
            {
                using (var cmd = new SQLiteCommand("DELETE FROM hizmet_fiyati WHERE hizmet = @ad", db))
                {
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.ExecuteNonQuery();
                }
            });
        }
    }
}

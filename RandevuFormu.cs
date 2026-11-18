using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;

namespace KUAFÖR_RANDEVU_SİSTEMİ
{
    public partial class RandevuFormu : Form
    {    
        SQLiteConnection con = Veritabani.BaglantiOlustur();

        private SQLiteCommand cmd;
        private DataTable dt;
       
        private string selectedTc = "";
        private DateTime selectedTarih;

        public RandevuFormu()
        {
            InitializeComponent();
            Tema.Uygula(this);
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm";
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            this.Load += Randevu_Load;    
        }

        private void Randevu_Load(object sender, EventArgs e)
        {
            Listele(); 
        }

        private void Listele()
        {
            try
            {
                dt = new DataTable();
                cmd = new SQLiteCommand("SELECT tc_kimlik, ad, cep_telefon, soyad, randevu_tarihi, adres FROM randevu", con);
                var adapter = new SQLiteDataAdapter(cmd);

                con.Open();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme Hatası: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { 
                var row = dataGridView1.Rows[e.RowIndex];

                selectedTc = row.Cells["tc_kimlik"].Value.ToString();
                selectedTarih = Convert.ToDateTime(row.Cells["randevu_tarihi"].Value);

                txtboxtc.Text = selectedTc;
                txtboxad.Text = row.Cells["ad"].Value.ToString();
                txtboxtel.Text = row.Cells["cep_telefon"].Value.ToString();
                txtboxsoyad.Text = row.Cells["soyad"].Value.ToString();
                dateTimePicker1.Value = selectedTarih;
                txtboxadres.Text = row.Cells["adres"].Value.ToString();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxad.Text) || string.IsNullOrWhiteSpace(txtboxsoyad.Text) ||
                string.IsNullOrWhiteSpace(txtboxadres.Text) || !txtboxtel.MaskCompleted)
            {
                MessageBox.Show("Ad, soyad, telefon ve adres alanlarını doldurun.");
                return;
            }


            if (string.IsNullOrWhiteSpace(txtboxtc.Text) || string.IsNullOrWhiteSpace(txtboxad.Text))
            {
                MessageBox.Show("Lütfen TC ve Ad alanlarını doldurun.");
                return;
            }

            if (txtboxtc.Text.Length != 11 || !txtboxtc.Text.All(char.IsDigit))
            {
                MessageBox.Show("TC Kimlik numarası 11 haneli olmalı ve sadece rakamlardan oluşmalıdır.");
                return;
            }

            try
            {
                cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM randevu WHERE tc_kimlik = @tc", con);
                cmd.Parameters.AddWithValue("@tc", txtboxtc.Text);

                con.Open();
                int mevcutKayitSayisi = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (mevcutKayitSayisi > 0)
                {
                    MessageBox.Show("Bu TC kimlik numarası ile zaten bir randevu alınmış.");
                    return;
                }

                cmd = new SQLiteCommand(
                    "INSERT INTO randevu (tc_kimlik, ad, cep_telefon, soyad, randevu_tarihi, adres) " +
                    "VALUES (@tc, @ad, @tel, @soyad, @tarih, @adres)", con);

                cmd.Parameters.AddWithValue("@tc", txtboxtc.Text);
                cmd.Parameters.AddWithValue("@ad", txtboxad.Text);
                cmd.Parameters.AddWithValue("@tel", txtboxtel.Text);
                cmd.Parameters.AddWithValue("@soyad", txtboxsoyad.Text);
                cmd.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@adres", txtboxadres.Text);

                con.Open();
                cmd.ExecuteNonQuery();  
                MessageBox.Show("Randevu başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme Hatası: " + ex.Message);
            }
            finally
            {
                con.Close();
                Listele();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek randevuyu seçin!");
                return;
            }

            if (MessageBox.Show("Seçili kayıt silinsin mi?", "Silme onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string silinecekTc = dataGridView1.SelectedRows[0].Cells["tc_kimlik"].Value.ToString();

            try
            {
                cmd = new SQLiteCommand("DELETE FROM randevu WHERE tc_kimlik = @tc", con);
                cmd.Parameters.AddWithValue("@tc", silinecekTc);

                con.Open();
                int silinen = cmd.ExecuteNonQuery();

                if (silinen > 0)
                {
                    MessageBox.Show("Randevu başarıyla silindi.");
                }
                else
                {
                    MessageBox.Show("Silinecek randevu bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme Hatası: " + ex.Message);
            }
            finally
            {
                con.Close();
                Listele(); 
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz satırı seçin!");
                return;
            }

            string yeniAd = txtboxad.Text;
            string yeniSoyad = txtboxsoyad.Text;
            string yeniTelefon = txtboxtel.Text;
            DateTime yeniTarih = dateTimePicker1.Value;
            string yeniAdres = txtboxadres.Text;
            string yeniTcKimlik = txtboxtc.Text;

            if (yeniTcKimlik.Length != 11 || !yeniTcKimlik.All(char.IsDigit))
            {
                MessageBox.Show("TC kimlik numarası 11 rakamdan oluşmalıdır.");
                return;
            }

            if (string.IsNullOrWhiteSpace(yeniAd) || string.IsNullOrWhiteSpace(yeniSoyad) ||
                !txtboxtel.MaskCompleted || string.IsNullOrWhiteSpace(yeniAdres) ||
                string.IsNullOrWhiteSpace(yeniTcKimlik))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];
            string selectedTc = selectedRow.Cells["tc_kimlik"].Value.ToString();
            DateTime selectedTarih = Convert.ToDateTime(selectedRow.Cells["randevu_tarihi"].Value);

            try
            {
                cmd = new SQLiteCommand(
                    "UPDATE randevu SET tc_kimlik = @yeniTc, ad = @ad, cep_telefon = @tel, soyad = @soyad, " +
                    "randevu_tarihi = @tarih, adres = @adres " +
                    "WHERE tc_kimlik = @tc", con);

                cmd.Parameters.AddWithValue("@yeniTc", yeniTcKimlik);
                cmd.Parameters.AddWithValue("@ad", yeniAd);
                cmd.Parameters.AddWithValue("@tel", yeniTelefon);
                cmd.Parameters.AddWithValue("@soyad", yeniSoyad);
                cmd.Parameters.AddWithValue("@tarih", yeniTarih);
                cmd.Parameters.AddWithValue("@adres", yeniAdres);
                cmd.Parameters.AddWithValue("@tc", selectedTc);


                con.Open();
                int güncellenen = cmd.ExecuteNonQuery();

                if (güncellenen > 0)
                {   
                    MessageBox.Show("Randevu başarıyla güncellendi.");
                    selectedRow.Cells["tc_kimlik"].Value = yeniTcKimlik;
                    selectedRow.Cells["ad"].Value = yeniAd;
                    selectedRow.Cells["soyad"].Value = yeniSoyad;
                    selectedRow.Cells["cep_telefon"].Value = yeniTelefon;
                    selectedRow.Cells["randevu_tarihi"].Value = yeniTarih;
                    selectedRow.Cells["adres"].Value = yeniAdres;
                }
                else
                {
                    MessageBox.Show("Güncellenen randevu bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme Hatası: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            HakkimizdaFormu form6gecici = new HakkimizdaFormu();
            form6gecici.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files|*.jpg|PNG Files|*.png|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog.FileName;
                pictureBox1.Load();
            }
        }

      
    }
}

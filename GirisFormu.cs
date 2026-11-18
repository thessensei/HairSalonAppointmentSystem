using System;
using System.Windows.Forms;

namespace KUAFÖR_RANDEVU_SİSTEMİ
{
    public partial class GirisFormu : Form
    {
        private int kalanDeneme = 3;

        public GirisFormu()
        {
            InitializeComponent();
            Tema.Uygula(this);
            listBox1.SelectedIndex = 0;
            AcceptButton = button1;
            ActiveControl = txtsifre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dogruSifre = DemoSifresi(listBox1.SelectedItem as string);
            if (dogruSifre == null || string.IsNullOrWhiteSpace(txtsifre.Text))
            {
                MessageBox.Show("Lütfen bir kullanıcı seçin ve şifresini girin.", "Giriş");
                txtsifre.Focus();
                return;
            }

            if (txtsifre.Text == dogruSifre)
            {
                txtsifre.Clear();
                new AnaMenuFormu().Show();
                Hide();
                return;
            }

            if (--kalanDeneme == 0)
            {
                MessageBox.Show("Deneme hakkınız kalmadı. Uygulama kapanıyor.", "Giriş");
                Close();
                return;
            }

            MessageBox.Show($"Şifre hatalı. Kalan deneme hakkı: {kalanDeneme}", "Giriş");
            txtsifre.Clear();
            txtsifre.Focus();
        }

        private static string DemoSifresi(string kullanici)
        {
            switch (kullanici)
            {
                case "Said": return "Said123";
                case "Yasin": return "Yasin123";
                case "Müdür": return "Müdür123";
                default: return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool goster = txtsifre.PasswordChar != '\0';
            txtsifre.PasswordChar = goster ? '\0' : '*';
            button2.Text = goster ? "Gizle" : "Göster";
        }
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace KUAFÖR_RANDEVU_SİSTEMİ
{
    partial class FiyatListesiFormu
    {
        private DataGridView liste;
        private TextBox hizmetAdi;
        private NumericUpDown fiyat;
        private Button kaydet;
        private Button sil;
        private FlowLayoutPanel duzenleyici;
        private Label aciklama;
        private Label hizmetEtiketi;
        private Label fiyatEtiketi;

        private void InitializeComponent()
        {
            this.liste = new DataGridView();
            this.hizmetAdi = new TextBox();
            this.fiyat = new NumericUpDown();
            this.kaydet = new Button();
            this.sil = new Button();
            this.duzenleyici = new FlowLayoutPanel();
            this.aciklama = new Label();
            this.hizmetEtiketi = new Label();
            this.fiyatEtiketi = new Label();
            ((System.ComponentModel.ISupportInitialize)this.liste).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.fiyat).BeginInit();
            this.duzenleyici.SuspendLayout();
            this.SuspendLayout();

            this.aciklama.Dock = DockStyle.Top;
            this.aciklama.Height = 40;
            this.aciklama.TextAlign = ContentAlignment.MiddleLeft;
            this.aciklama.Text = "Hizmet ve fiyatını girip kaydedin. Bir fiyatı güncellemek için listedeki hizmeti seçin.";
            this.hizmetEtiketi.Text = "Hizmet";
            this.hizmetEtiketi.AutoSize = true;
            this.hizmetEtiketi.Padding = new Padding(0, 5, 0, 0);
            this.hizmetAdi.Width = 160;
            this.hizmetAdi.MaxLength = 100;
            this.hizmetAdi.AccessibleName = "Hizmet adı";
            this.fiyatEtiketi.Text = "Fiyat (TL)";
            this.fiyatEtiketi.AutoSize = true;
            this.fiyatEtiketi.Padding = new Padding(0, 5, 0, 0);
            this.fiyat.Width = 100;
            this.fiyat.DecimalPlaces = 2;
            this.fiyat.Maximum = 999999.99m;
            this.fiyat.AccessibleName = "Fiyat (TL)";
            this.kaydet.Text = "Kaydet";
            this.kaydet.AutoSize = true;
            this.kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            this.sil.Text = "Sil";
            this.sil.AutoSize = true;
            this.sil.Enabled = false;
            this.sil.Click += new System.EventHandler(this.Sil_Click);

            this.duzenleyici.Dock = DockStyle.Top;
            this.duzenleyici.Height = 42;
            this.duzenleyici.WrapContents = false;
            this.duzenleyici.Controls.Add(this.hizmetEtiketi);
            this.duzenleyici.Controls.Add(this.hizmetAdi);
            this.duzenleyici.Controls.Add(this.fiyatEtiketi);
            this.duzenleyici.Controls.Add(this.fiyat);
            this.duzenleyici.Controls.Add(this.kaydet);
            this.duzenleyici.Controls.Add(this.sil);
            this.liste.Dock = DockStyle.Fill;
            this.liste.ReadOnly = true;
            this.liste.MultiSelect = false;
            this.liste.RowHeadersVisible = false;
            this.liste.AllowUserToAddRows = false;
            this.liste.AllowUserToDeleteRows = false;
            this.liste.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.liste.SelectionChanged += new System.EventHandler(this.SecimDegisti);

            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 350);
            this.MinimumSize = new Size(605, 280);
            this.Padding = new Padding(12);
            this.Controls.Add(this.liste);
            this.Controls.Add(this.duzenleyici);
            this.Controls.Add(this.aciklama);
            this.Name = "FiyatListesiFormu";
            this.Text = "Fiyat Listesi";
            ((System.ComponentModel.ISupportInitialize)this.liste).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.fiyat).EndInit();
            this.duzenleyici.ResumeLayout(false);
            this.duzenleyici.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}

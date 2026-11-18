using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KUAFÖR_RANDEVU_SİSTEMİ
{
    public partial class HakkimizdaFormu : Form
    {
        public HakkimizdaFormu()
        {
            InitializeComponent();
            var kaynaklar = new ComponentResourceManager(typeof(HakkimizdaFormu));
            var resim = new ImageConverter();
            pictureBox1.Image = (Image)resim.ConvertFrom(kaynaklar.GetObject("Gorsel1"));
            pictureBox2.Image = (Image)resim.ConvertFrom(kaynaklar.GetObject("Gorsel2"));
            pictureBox3.Image = (Image)resim.ConvertFrom(kaynaklar.GetObject("Gorsel3"));
            Tema.Uygula(this);
        }
    }
}

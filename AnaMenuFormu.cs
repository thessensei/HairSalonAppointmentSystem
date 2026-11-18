using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAFÖR_RANDEVU_SİSTEMİ
{
    public partial class AnaMenuFormu : Form
    {
        public AnaMenuFormu()
        {
            InitializeComponent();
            Tema.Uygula(this);
        }

        private void btnekle_Click(object sender, EventArgs e)
        {



            PersonelFormu form2gecic = new PersonelFormu();
            form2gecic.Show();
        }

        private void btntakib_Click(object sender, EventArgs e)
        {
            RandevuFormu form3gecic = new RandevuFormu();
            form3gecic.Show();
        }

        private void btnfiyat_Click(object sender, EventArgs e)
        {
            FiyatListesiFormu form4gecic = new FiyatListesiFormu();
            form4gecic.Show();
        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            CiltBakimiFormu form5gecic = new CiltBakimiFormu();
            form5gecic.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

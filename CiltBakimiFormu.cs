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
    public partial class CiltBakimiFormu : Form
    {
        public CiltBakimiFormu()
        {
            InitializeComponent();
            Tema.Uygula(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CİLDİNİZ ÇOK GÜZEL OLDU");
        }
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace KUAFÖR_RANDEVU_SİSTEMİ
{
    internal static class Tema
    {
        private static readonly Color ArkaPlan = Color.FromArgb(245, 246, 250);
        private static readonly Color Yazi = Color.FromArgb(43, 48, 65);
        private static readonly Color Vurgu = Color.FromArgb(96, 65, 139);

        public static void Uygula(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.BackColor = ArkaPlan;
            form.ForeColor = Yazi;
            Duzenle(form.Controls);
        }

        private static void Duzenle(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                var button = control as Button;
                var grid = control as DataGridView;
                if (button != null)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.BackColor = Vurgu;
                    button.ForeColor = Color.White;
                    button.UseVisualStyleBackColor = false;
                    button.Cursor = Cursors.Hand;
                }
                else if (grid != null)
                {
                    grid.BackgroundColor = Color.White;
                    grid.BorderStyle = BorderStyle.None;
                    grid.EnableHeadersVisualStyles = false;
                    grid.ColumnHeadersDefaultCellStyle.BackColor = Vurgu;
                    grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(205, 190, 225);
                    grid.DefaultCellStyle.SelectionForeColor = Yazi;
                    grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 252);
                    grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else if (control is Label)
                {
                    control.ForeColor = Yazi;
                    control.BackColor = ArkaPlan;
                }
                if (control.HasChildren) Duzenle(control.Controls);
            }
        }
    }
}

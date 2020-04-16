using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuzzLockGui
{
    public partial class Form2 : Form
    {
        private Form1 _f1;
        private Form2 _f2;
        public Form2(Form1 f1)
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.Manual;
            this.Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            _f1 = f1;
            _f2 = this;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _f1.Show();
            this.Hide();
        }
    }
}

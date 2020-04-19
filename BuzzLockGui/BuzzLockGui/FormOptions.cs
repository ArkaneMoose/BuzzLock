﻿using System;
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
    public partial class FormOptions : Form
    {
        private FormStart _f1;
        private FormOptions _f2;
        public FormOptions(FormStart f1)
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.Manual;
            this.Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            _f1 = f1;
            _f2 = this;
        }

        private void btnOptionsSave_Click(object sender, EventArgs e)
        {
            _f1._state = State.Authenticated;
            _f1.UpdateComponents();
            _f1.Show();
            this.Hide();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {

        }

    }
}

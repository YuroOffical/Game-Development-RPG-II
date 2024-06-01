using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_II
{
    public partial class FormGame : Form
    {
        Thread thread;
        public FormGame()
        {
            InitializeComponent();
        }
        
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_newgame_Click(object sender, EventArgs e)
        {
            this.Close();
            thread = new Thread(openplayercreator);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void openplayercreator(object obj)
        {
            Application.Run(new FormSaveFile(0));
        }
    }
}



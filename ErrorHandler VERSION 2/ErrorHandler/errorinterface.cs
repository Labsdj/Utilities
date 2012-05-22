using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace ErrorHandler
{
    public partial class errorinterface : Form
    
    {
        public bool onscreen;
        public errorinterface(string errormessage, bool critical, Color backCOLOR)
        {
            InitializeComponent();
            this.errorgui.Text = errormessage;
            //if (critical)
            //{
                this.BackColor = errorgui.BackColor = backCOLOR;  
            //}
            
        }
        public void errorinterface_Load(object sender, EventArgs e)
        {
            //Properties.Settings.Default.errorSHOWN = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(errorinterface_closing);
            //if (Properties.Settings.Default.errorSFX)
            //{
            //    playSFX(Properties.Settings.Default.errorWAV);
            //}
        }
        private void errorinterface_closing(object sender, FormClosingEventArgs e)
        {
            //Properties.Settings.Default.errorSHOWN = false;
        }
        protected override void OnShown(EventArgs e)
        {
            this.errorgui.SelectionLength = 0;
            this.errorgui.SelectionStart = 0;
            base.OnShown(e);
        }
        private void playSFX(string file)
        {
            try
            {
                SoundPlayer sfx = new SoundPlayer(@file);
                sfx.Play();
            }
            catch (Exception)
            {

            }
        }


    }
}
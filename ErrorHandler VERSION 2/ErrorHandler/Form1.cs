using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ErrorHandler
{
    public partial class Form1 : Form

    {
        errorHANDLER errorhandler = new errorHANDLER();
        printerMANAGER printer = new printerMANAGER();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            errorhandler.initializeLOGGING();
            textBox1.Text = errorhandler.getERRORS();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            errorhandler.displayERROR(true, false, true, "error type1", Color.Crimson);
            textBox1.Text = errorhandler.getERRORS();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            errorhandler.displayERROR(true, true, true, "error type 2", Color.Orange);
            textBox1.Text = errorhandler.getERRORS();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            errorhandler.saveLOGFILE();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            errorhandler.openLOGFILE();
            textBox1.Text = errorhandler.getERRORS();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {

            errorhandler.saveLOGFILEas();
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            printer.PRINT(errorhandler.getERRORS());
        }

        }
    }


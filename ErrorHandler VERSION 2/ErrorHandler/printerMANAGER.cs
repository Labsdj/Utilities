using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;

namespace ErrorHandler
{

    class printerMANAGER
    {
        private Font defaultFONT;
        errorHANDLER errorhandler = new errorHANDLER();

        public void PRINT(string printTEXT)
        {

            PrintDocument p = new PrintDocument();
            p.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(printTEXT, defaultFONT, new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

            };
            try
            {
                p.Print();
            }
            catch (Exception ex)
            {
                errorhandler.displayERROR(true, false, true, ex.Message, Color.LimeGreen);
            }
        }
        public void setFONT(string font, int size)
        {
            defaultFONT = new Font(font, size);
        }
       

    }

}

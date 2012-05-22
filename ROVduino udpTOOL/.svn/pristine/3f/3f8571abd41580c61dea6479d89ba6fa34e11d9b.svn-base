using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace ROVduino_udpTOOL
{

    class errorHANDLER
    {
        private string logfilename;     //path to logfile
        private string errorLOG;        //string to hold error log
        private bool criticalERROR;     //state of critical error. Allows application to handle critical states
        private string lastERROR;       //string to hold last error

        public void initializeLOGGING() //insert error log header
        {
            errorLOG += "ROVduino udpTOOL";
            errorLOG += "\r\n";
            errorLOG += "======================================================================================"; 
        }
        public void displayERROR(Boolean showPOPUP, Boolean criticalERROR, Boolean addLOG, string errorMESSAGE, Color backCOLOR)    //call this to show an error, add to log, etc.
        {
            if (showPOPUP) //popup error message
            {
                MessageBox.Show(errorMESSAGE);
            }

            if (criticalERROR)// critical state flag
            {
                this.criticalERROR = true;
            }
            if (addLOG)//add to error log
            {
                errorLOG +="\r\n\r\n";
                errorLOG +="======================================================================================";
                errorLOG +="\r\n";
                errorLOG +=System.DateTime.Now.ToString();
                errorLOG +="\r\n";
                errorLOG += errorMESSAGE;
                errorLOG +="\r\n";
                errorLOG +="======================================================================================";
                lastERROR = System.DateTime.Now.ToString() + errorMESSAGE;
            }
        }
        public void clearLOG() // clear error log.
        {
            errorLOG = "";
        }
        public void saveLOGFILE() // save log file.
        {
            if (logfilename == null)
            {
                SaveFileDialog sdlg = new SaveFileDialog();
                sdlg.Title = "Save Log File";
                sdlg.InitialDirectory = @"c:\";
                sdlg.Filter = "Text|*.txt";
                sdlg.FilterIndex = 1;
                sdlg.RestoreDirectory = true;
                if (sdlg.ShowDialog() == DialogResult.OK)
                {
                    logfilename = sdlg.FileName;
                }
            }
            if (logfilename != null)
            {
                File.WriteAllText(logfilename, errorLOG);
               // displayERROR(true, false, false, "Please send your error log to rovduino+errors@gmail.com", Color.LimeGreen);
            }
            else
            {
                displayERROR(true, false, false, "File Path Not Found (Technical: File Path returned Null)", Color.LimeGreen);
            }
        }
        public void saveLOGFILEas() //save log file as. 
        {
            SaveFileDialog sdlg = new SaveFileDialog();
            sdlg.Title = "Save Log File As";
            sdlg.InitialDirectory = @"c:\";
            sdlg.Filter = "Text|*.txt";
            sdlg.FilterIndex = 1;
            sdlg.RestoreDirectory = true;
            if (sdlg.ShowDialog() == DialogResult.OK)
            {
                logfilename = sdlg.FileName;
            }

            File.WriteAllText(logfilename, errorLOG);
        }
        public string openLOGFILE() //opens existing log file and poulates existing log
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Open Log File";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Text|*.txt";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                logfilename = fdlg.FileName;
                string openERRORS = File.ReadAllText(logfilename);
                errorLOG = openERRORS;
                return openERRORS;
            }
            if (logfilename != null)
            {
                string openERRORS = File.ReadAllText(logfilename);
                errorLOG = openERRORS;
                return openERRORS;
            }
            else
            {
                displayERROR(true, false, true, "File Path Not Found (Technical: File Path returned Null)", Color.LimeGreen);
                return null;
            }
        }
        public string getERRORS() //return master error list
        {
            return errorLOG;
        }
        public string getLASTERROR() //return last error
        {
            return lastERROR;
        }
        public void clearCRITICALFLAG() //clear the critical state flag 
        {
            criticalERROR = false;
        }
        public bool getCRITICALSTATE() //get critical state flag. use in conjunction with clearCRITICALFLAG to lock and unlock safety controls when critical error happens.
        {
            return criticalERROR;
        }

    }
}
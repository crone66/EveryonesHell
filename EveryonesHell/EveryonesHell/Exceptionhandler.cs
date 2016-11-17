using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace EveryonesHell
{
    class Exceptionhandler
    {
        private const String errorReportPath = "";
        public event EventHandler ExceptionHandled;
        private string path;

        Exceptionhandler(String path)
        {
            this.path = path;
        }

        public void handleException(Exception e)
        {

            if(e is NullReferenceException)
            {

            }

            if (ExceptionHandled != null)
                ExceptionHandled(this,EventArgs.Empty);


        }

        private void CloseProgram(String Message, String StackTrace)
        {
            createMessageBox(Message, StackTrace);
            //End Program
            GlobalReferences.Exit = true;
        }

        private void writeLog(String Message, String StackTrace)
        {
            try
            {
                File.AppendAllText(path, Message + Environment.NewLine + StackTrace);
            }
            catch(Exception e)
            {
                createMessageBox(Message, StackTrace);
            }
        }

        private void createMessageBox(String Message, String StackTrace)
        {
            File.WriteAllText(errorReportPath + ".txt", "Message: " + Message + Environment.NewLine + "StackTrace: " + StackTrace);
            
            //Start Program
            Process.Start(errorReportPath + ".exe");

        }
    }
}

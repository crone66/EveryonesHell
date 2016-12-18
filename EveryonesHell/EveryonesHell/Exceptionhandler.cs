using System;
using System.IO;
using System.Diagnostics;

namespace EveryonesHell
{
    public class ExceptionHandler
    {
        private string errorReportPath;

        public ExceptionHandler(string path)
        {
            this.errorReportPath = path;
        }

        public void HandleException(Exception e)
        {
            if (WriteLog(e.Message, e.StackTrace))
                CreateMessageBox();
        }

        private bool WriteLog(string Message, string StackTrace)
        {
            try
            {
                File.AppendAllText(errorReportPath, Message + Environment.NewLine + StackTrace);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void CreateMessageBox()
        {
            GlobalReferences.State = GameState.Exit;
            //Start Program
            Process.Start(errorReportPath + ".exe");
        }
    }
}

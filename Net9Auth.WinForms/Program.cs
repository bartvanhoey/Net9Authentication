using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net9Auth.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            Debug.WriteLine(e.Exception.Message);

            var trace = new StackTrace(e.Exception, true);

            var className = trace.GetFrame(0).GetMethod().ReflectedType?.FullName;
            var methodName = trace.GetFrame(0).GetMethod().Name;
            var lineNumber = trace.GetFrame(0).GetFileLineNumber();
            var columnNumber = trace.GetFrame(0).GetFileColumnNumber();
            

            Console.WriteLine($"ClassName: {className}");
            Console.WriteLine($"MethodName: {methodName}");
            Console.WriteLine($"LineNumber: {lineNumber}");
            Console.WriteLine($"ColumnNumber: {columnNumber}");


            



        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            Debug.WriteLine((e.ExceptionObject as Exception).Message);
        }
    }
}

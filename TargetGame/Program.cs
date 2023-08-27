using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TargetGame
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DemoClass example = new DemoClass();                //
            example.insertText("This is a demo");               //    DEMO CLASSES
            //MessageBox.Show(example.demo);                    //


            InheritDemoClass obj = new InheritDemoClass();      //
            obj.digit = 8;                                      //
            int s = obj.sum(2);                                 //
            obj.digit = s;                                      //   INHERITANCE EXAMPLE
            obj.changeDemo();                                   //
            //MessageBox.Show(obj.demo);                        //
    

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

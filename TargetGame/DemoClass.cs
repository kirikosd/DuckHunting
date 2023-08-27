using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TargetGame
{
    internal class DemoClass
    {
        public String demo;

        public void insertText(String text)
        {
           demo  = text;
        }
    }
    internal class InheritDemoClass : DemoClass
    {
        public int digit = 0;

        public int sum(int d)
        {
            return d + digit;
        }

        public void changeDemo()
        {
            demo = "digit is " + digit.ToString(); 
        }
    }
}



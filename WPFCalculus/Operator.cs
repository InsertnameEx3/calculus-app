using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalculus
{
    public class Operator
    {
        public Operator(string operatorString, int presedence)
        {
            OperatorStr = operatorString;
            Presedence = presedence;
        }
        public string OperatorStr { get; set; }
        public int Presedence { get; set; }
    }
}

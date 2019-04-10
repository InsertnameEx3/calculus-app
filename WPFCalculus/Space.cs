using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalculus
{
    class Space
    {
        public static dynamic XMin { get; set; }
        public static dynamic XMax { get; set; }
        public static dynamic YMin { get; set; }
        public static dynamic YMax { get; set; }
        public static dynamic ZMin { get; set; }
        public static dynamic ZMax { get; set; }
        public static dynamic Steps { get; set; }
        

        private static readonly Space _grid = new Space();

        public enum Formulas
        {
            
        }



        public static Space Grid { get; set; }

        public virtual void Draw()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCalculus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            Space.XMin = -10;
            Space.XMax = 10;
            Space.Steps = 1;

            Canvas.Children.Clear();
            var formula = new Formula("x^2");
            var formulas = new Formula("x");
            var formulass = new Formula("0");
            DrawLine(formula.Coordinates);
            DrawLine(formulas.Coordinates);
            DrawLine(formulass.Coordinates);

            Dictionary<dynamic, List<dynamic>> sDictionary = new Dictionary<dynamic, List<dynamic>>();
            for (int i = 0; i < formula.Coordinates.Count; i++)
            {
                List<dynamic> YCoordinates = new List<dynamic>();
                YCoordinates.Add(formula.Coordinates.Values.ElementAt(i));
                YCoordinates.Add(formulas.Coordinates.Values.ElementAt(i));
                YCoordinates.Add(formulass.Coordinates.Values.ElementAt(i));
                
                sDictionary.Add(formula.Coordinates.Keys.ElementAt(i),  YCoordinates);
                ;
            }

            for (int i = 0; i < sDictionary.Count; i++)
            {
                sDictionary.Keys.ElementAt(i);
                Console.Write("X:  " + sDictionary.Keys.ElementAt(i) + "Y:  ");
                foreach (var coordinate in sDictionary.Values.ElementAt(i))
                {
                    Console.Write(coordinate + " ");
                }
                Console.WriteLine();
            }

            // Beautiful colours
            // #333333 
            // 	#DE8E26
            //#7565C7
            //#C9DFF1
            // 	#B72224
            // 	#57B35A
        }

        private void DrawLine(Dictionary<dynamic, dynamic> points)
        {
            int i;
            int count = points.Count;
            for (i = 0; i < count - 1; i++)
            {
                Line myline = new Line();
                
                myline.Stroke = Brushes.Red;
                myline.StrokeThickness = 2;
                Console.WriteLine("From (" + points.Keys.ElementAt(i) + ", " + points.Values.ElementAt(i) + ")");
                myline.X1 = points.Keys.ElementAt(i);
                myline.Y1 = points.Values.ElementAt(i);
                Console.WriteLine("To (" + points.Keys.ElementAt(i+1) + ", " + points.Values.ElementAt(i+1) + ")");
                myline.X2 = points.Keys.ElementAt(i + 1);
                myline.Y2 = points.Values.ElementAt(i + 1);
                Canvas.SetLeft(myline, myline.X1 + 300);
                Canvas.SetTop(myline, myline.Y1 + 160);
                Canvas.Children.Add(myline);
            }
        }


      
            //var grid = new Space<int, int>();
            
        

        }
        //            foreach (var s in formula.TokenQueue)   
            //            {
            //                Console.WriteLine(s);
            //            }



    }

       
    


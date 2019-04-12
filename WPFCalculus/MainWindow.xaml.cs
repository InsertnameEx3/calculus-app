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
            //Fields that can be manipulated by the user fields
            Space.XMin = -10;
            Space.XMax = 10;
            Space.YMin = -10;
            Space.YMax = 10;
            
            Space.YScale = 1;
            Space.XScale = 1;
            Space.Steps = 1;
            var formulass = new Formula("x");
            var formulas = new Formula("x^2");

            List<Formula> allFormulas = new List<Formula>();

            allFormulas.Add(formulass);
            allFormulas.Add(formulas);

            try
            {
                Dictionary<dynamic, List<dynamic>> sDictionary = new Dictionary<dynamic, List<dynamic>>();
                for (int i = 0; i < allFormulas[0].Coordinates.Count; i++)
                {
                    List<dynamic> YCoordinates = new List<dynamic>();

                    for (var c = 0; c < allFormulas.Count; c++)
                    {
                        YCoordinates.Add(allFormulas[c].Coordinates.Values.ElementAt(i));
                    }

                    sDictionary.Add(allFormulas[0].Coordinates.Keys.ElementAt(i), YCoordinates);

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




            }
            catch { }

            //Where is x=0 and y=0??
           
           



            InitializeComponent();


            //Size of the small stripes each step
            const double stripeSize = 3;
            double xmin = 0;
            double xmax = Grid.Width;
            double ymin = Grid.Height;
            double ymax = 0;

            int xDistance = ((Space.XMin < 0 && Space.XMax < 0) || (Space.XMin >= 0 && Space.XMax >= 0))
                ? Space.XMin + Space.XMax
                : (Space.XMin < 0)
                    ? Space.XMin * -1 + Space.XMax
                    : Space.XMax * -1 + Space.XMin;


            int yDistance = ((Space.YMin < 0 && Space.YMax < 0) || (Space.YMin >= 0 && Space.YMax >= 0))
                ? Space.YMin + Space.YMax
                : (Space.YMin < 0)
                    ? Space.YMin * -1 + Space.YMax
                    : Space.YMax * -1 + Space.YMin;

            double xStep = Grid.Width / (xDistance / Space.XScale);
            double yStep = Grid.Height / (yDistance / Space.YScale);
            var index = 0;
            for (int i = Space.YMin; i < Space.YMax; i += Space.Steps)
            {
                
                if (i == 0)
                {
                    ymin -= yStep * index;
                    break;
                }
                index++;
            }

           


            Console.WriteLine(ymin + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //Check if x axis and y axis are in range (0, 0) => (x, y)  else change variable xaxis and yaxis to grid with numbers on side 
            //Halve of the screen is either xmax/2 or ymax/2 or Grid.Width/2
            // Make the X axis.
            
            
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, ymin), new Point(xmax, ymin)));
            for (double x = xmin + xStep;
                x <= Grid.Width;
                x += xStep)
            {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymin - stripeSize),
                    new Point(x, ymin + stripeSize)));
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            ymin = Grid.Height;


            //Create xgrid
            GeometryGroup xgrid_geom = new GeometryGroup();
       
            for (double x = xmin;
                x <= xmax;
                x += xStep)
            {
                xgrid_geom.Children.Add(new LineGeometry(
                    new Point(x, ymax),
                    new Point(x, ymin)));
            }

            Path xgrid_path = new Path();
            xgrid_path.StrokeThickness = 1;
            xgrid_path.Stroke = Brushes.Gray;
            xgrid_path.Data = xgrid_geom;


            


            index = 0;
            
            for (int i = Space.XMin; i < Space.XMax; i += Space.Steps)
            {
                
                if (i == 0)
                {
                    xmin += xStep * index;
                    break;
                }
                index++;
            }


            Console.WriteLine(xmin + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            
            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, ymin), new Point(xmin, ymax)));
            for (double y = ymax; y <= ymin; y += yStep)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - stripeSize, y),
                    new Point(xmin + stripeSize, y)));
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;



            xmin = 0;

            // Make the Y ayis.
            GeometryGroup ygrid_geom = new GeometryGroup();
         
            for (double y = ymax; y <= ymin; y += yStep)
            {
                ygrid_geom.Children.Add(new LineGeometry(new Point(xmax, y),new Point(xmin, y)));
            }

            Path ygrid_path = new Path();
            ygrid_path.StrokeThickness = 1;
            ygrid_path.Stroke = Brushes.Gray;
            ygrid_path.Data = ygrid_geom;


            

            Grid.Children.Add(xgrid_path);
            Grid.Children.Add(ygrid_path);
            Grid.Children.Add(yaxis_path);
            
            Grid.Children.Add(xaxis_path);





            try
            {
                DrawLines(allFormulas, xStep, yStep);
            }
            catch { }




            //use array for formulas max:7? 9? 10?



            
        }

        void DrawLines(List<Formula> listOfFormulas, double xStep, double yStep)
        {
            
            SolidColorBrush test = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DE8E26"));
            SolidColorBrush test2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7565C7"));
            SolidColorBrush test3 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C9DFF1"));
            SolidColorBrush test4 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#B72224"));
            SolidColorBrush test5 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#57B35A"));

            Brush[] brushes = { test, test2,test3,test4,test5 };

            // Beautiful colours
            // #333333 
            // 	#DE8E26
            //#7565C7
            //#C9DFF1
            // 	#B72224
            // 	#57B35A




            double xmin = 0;
            double xmax = Grid.Width;
            double ymin = 0;
            double ymax = Grid.Height;
            

           

            Console.WriteLine(listOfFormulas.Count);

            
            for (var i = 0; i < listOfFormulas.Count; i++)  //Foreach formula (shape)
            {
               
                double y = 0;
                double x = 0;
                GeometryGroup points = new GeometryGroup();
                for (var p = 0; p < listOfFormulas[i].Coordinates.Count / Space.XScale; p++)   //Foreach coordinate draw a line of the shape
                {
                    y -= yStep;
                    x += xStep;

                    Console.WriteLine("FROM = (" + listOfFormulas[i].Coordinates.Keys.ElementAt(p) + ", " + listOfFormulas[i].Coordinates.Values.ElementAt(p) + ")");
                    Console.WriteLine("TO = (" + listOfFormulas[i].Coordinates.Keys.ElementAt(p + 1) + ", "+ listOfFormulas[i].Coordinates.Values.ElementAt(p + 1) + ")");

                    //Change to positive
                    xmin = (Space.XMin < 0) ? Space.XMin * -1 : Space.XMin;
                    ymin = (Space.YMin < 0) ? Space.YMin * -1 : Space.YMin;

                    points.Children.Add(new LineGeometry(
                        new Point(listOfFormulas[i].Coordinates.Keys.ElementAt(p) * (xStep * p) + (xmin * xStep),
                            (listOfFormulas[i].Coordinates.Values.ElementAt(p) * (yStep * p)  + (ymin * yStep))),
                        new Point(listOfFormulas[i].Coordinates.Keys.ElementAt(p+1) * (xStep * (p + 1)) + (xmin * xStep),
                            (listOfFormulas[i].Coordinates.Values.ElementAt(p+1) * (yStep * (p + 1))  + (ymin * yStep)))));




                    Path xaxis_path = new Path();
                    xaxis_path.StrokeThickness = 1;
                    xaxis_path.Stroke = brushes[i];
                    xaxis_path.Data = points;
                    Grid.Children.Add(xaxis_path);



                }
                


            }

        }
        private void FormulaChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        private void XMinTextChanged(object sender, TextChangedEventArgs e)
        {
            Space.XMin = int.Parse(xMin.Text);
        }
        private void YMinTextChanged(object sender, TextChangedEventArgs e)
        {
            Space.YMin = int.Parse(yMin.Text);
        }
        private void XMaxTextChanged(object sender, TextChangedEventArgs e)
        {
            Space.XMax = int.Parse(xMax.Text);
        }
        private void YMaxTextChanged(object sender, TextChangedEventArgs e)
        {
            Space.YMax = int.Parse(yMax.Text);
        }
    }

      



    }

       
    


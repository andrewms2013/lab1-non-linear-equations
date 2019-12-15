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

namespace Lab1_non_linear_equations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private double a7;
        private double a6;
        private double a5;
        private double a4;
        private double a3;
        private double a2;
        private double a1;
        private double a0;

        private double accuracy;
        private double leftBound;
        private double rightBound;

        private int GetEquationNumber()
        {
            if ((bool)BtnEq1.IsChecked)
            {
                return 1;
            }
            else if ((bool)BtnEq2.IsChecked)
            {
                return 2;
            }
            return 3;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Output.Text = "";
                a7 = double.Parse(A7.Text);
                a6 = double.Parse(A6.Text);
                a5 = double.Parse(A5.Text);
                a4 = double.Parse(A4.Text);
                a3 = double.Parse(A3.Text);
                a2 = double.Parse(A2.Text);
                a1 = double.Parse(A1.Text);
                a0 = double.Parse(A0.Text);
                accuracy = double.Parse(Accuracy.Text);
                leftBound = double.Parse(LeftBound.Text);
                rightBound = double.Parse(RightBound.Text);

                if ((bool)BtnFalsePos.IsChecked)
                {
                    this.CountFalsePosition(this.GetEquationNumber());
                }
                else if ((bool)BtnSteffensen.IsChecked)
                {
                    this.CountSteffensen(this.GetEquationNumber());
                }
                else if ((bool)BtnIterations.IsChecked)
                {
                    this.CountSimpleIterations(this.GetEquationNumber());
                }
                else
                {
                    this.CountGrafee(this.GetEquationNumber());
                }
            }
            catch (Exception exception)
            {
                Output.Text = exception.Message;
            }

        }

        private void CountFalsePosition(int numberOfEquation)
        {
            FalsePosition falsePosition = new FalsePosition(leftBound, rightBound, getEquationByNumber(numberOfEquation), accuracy);
            List<double> roots = falsePosition.ComputateRoots();
            string output = "";
            for (int i = 0; i < roots.Count; i++)
            {
                output += $"{i + 1} iteration: x = {roots[i]}\n";
            }
            Console.WriteLine(output);
            Output.Text = output;
        }

        private void CountSteffensen(int numberOfEquation)
        {
            Steffensen simpleIterations = new Steffensen(leftBound, rightBound, getEquationByNumber(numberOfEquation), getEquationSecondDerivativeByNumber(numberOfEquation), accuracy);
            List<double> roots = simpleIterations.ComputateRoots();
            string output = "";
            for (int i = 0; i < roots.Count; i++)
            {
                output += $"{i + 1} iteration: x = {roots[i]}\n";
            }
            Console.WriteLine(output);
            Output.Text = output;
        }

        private void CountSimpleIterations(int numberOfEquation)
        {
            SimpleIterations simpleIterations = new SimpleIterations(leftBound, rightBound, getEquationByNumber(numberOfEquation), getEquationDerivativeByNumber(numberOfEquation), accuracy);
            List<double> roots = simpleIterations.ComputateRoots();
            string output = "";
            for (int i = 0; i < roots.Count; i++)
            {
                output += $"{i + 1} iteration: x = {roots[i]}\n";
            }
            Console.WriteLine(output);
            Output.Text = output;
        }

        private void CountGrafee(int numberOfEquation)
        {
            List<double> coefficients = new List<double> { a7, a6, a5, a4, a3, a2, a1, a0 };
            Graeffe grafee = new Graeffe(coefficients, getEquationByNumber(numberOfEquation), accuracy);
            List<double> roots = grafee.ComputateRoots();
            string output = "";
            for (int i = 0; i < roots.Count; i++)
            {
                output += $"x{i} = {roots[i]}\n";
            }
            Output.Text = output;
        }

        private Func<double, double> getEquationByNumber(int numberOfEquation)
        {
            switch (numberOfEquation)
            {
                case 1:
                    return getFirstEquation();
                case 2:
                    return getSecondEquation();
                default:
                    return getAlgebraicEquation();
            }
        }


        private Func<double, double> getEquationDerivativeByNumber(int numberOfEquation)
        {
            switch (numberOfEquation)
            {
                case 1:
                    return getFirstEquationDerivative();
                case 2:
                    return getSecondEquationDerivative();
                default:
                    return getAlgebraicEquationDerivative();
            }
        }


        private Func<double, double> getEquationSecondDerivativeByNumber(int numberOfEquation)
        {
            switch (numberOfEquation)
            {
                case 1:
                    return getFirstEquationSecondDerivative();
                case 2:
                    return getSecondEquationSecondDerivative();
                default:
                    return getAlgebraicSecondDerivative();
            }
        }


        private Func<double, double> getAlgebraicEquation()
        {
            Func<double, double> algebraic = x => Math.Pow(x, 7) * a7 + Math.Pow(x, 6) * a6 + Math.Pow(x, 5) * a5 + 
                    Math.Pow(x, 4) * a4 + Math.Pow(x, 3) * a3 + Math.Pow(x, 2) * a2 + x * a1 + a0;
            return algebraic;
        }

        private Func<double, double> getFirstEquation()
        {
            Func<double, double> firstEquation = x => x * Math.Cos(Math.Cosh(x - Math.PI)) + 0.3 * x;
            return firstEquation;
        }

        private Func<double, double> getSecondEquation()
        {
            Func<double, double> secondEquation = x => Math.Sqrt(5 - x) * Math.Sin(x) + x * Math.Cos(Math.PI - x);
            return secondEquation;
        }

        private Func<double, double> getAlgebraicEquationDerivative()
        {
            Func<double, double> algebraicDerivative = x => 7 * Math.Pow(x, 6) * a7 + 6 * Math.Pow(x, 5) * a6 + 5 * Math.Pow(x, 4) * a5 +
                    4 * Math.Pow(x, 3) * a4 + 3 * Math.Pow(x, 2) * a3 + 2 * x * a2 + a1;
            return algebraicDerivative;
        }

        private Func<double, double> getFirstEquationDerivative()
        {
            Func<double, double> firstEquationDerivative = x => Math.Cos(Math.Cosh(Math.PI - x)) + x * Math.Sinh(Math.PI - x) * Math.Sin(Math.Cosh(Math.PI - x)) + 0.3;
            return firstEquationDerivative;
        }

        private Func<double, double> getSecondEquationDerivative()
        {
            Func<double, double> secondEquationDerivative = x => x * Math.Sin(x) - Math.Sin(x) / (2 * Math.Sqrt(5 - x)) + Math.Sqrt(5 - x) * Math.Cos(x) - Math.Cos(x);
            return secondEquationDerivative;
        }

        private Func<double, double> getAlgebraicSecondDerivative()
        {
            Func<double, double> algebraicSecondDerivative = x => 7 * 6 * Math.Pow(x, 5) * a7 + 6 * 5 * Math.Pow(x, 4) * a6 + 5 * 4 * Math.Pow(x, 3) * a5 +
                    4 * 3 * Math.Pow(x, 2) * a4 + 3 * 2 * x * a3 + 2 * a2;
            return algebraicSecondDerivative;
        }

        private Func<double, double> getFirstEquationSecondDerivative()
        {
            Func<double, double> firstEquationSecondDerivative = x => Math.Sinh(Math.PI - x) * (2 * Math.Sin(Math.Cosh(Math.PI - x)) - x * Math.Sinh(Math.PI - x) * Math.Cos(Math.Cosh(Math.PI - x))) - x * Math.Cosh(Math.PI - x) * Math.Sin(Math.Cosh(Math.PI - x));
            return firstEquationSecondDerivative;
        }

        private Func<double, double> getSecondEquationSecondDerivative()
        {
            Func<double, double> secondEquationSecondDerivative = x => -Math.Sqrt(5 - x) * Math.Sin(x) - Math.Sin(x) / (4 * Math.Pow((5 - x), (3 / 2))) + 2 * Math.Sin(x) + x * Math.Cos(x) - Math.Cos(x) / Math.Sqrt(5 - x);
            return secondEquationSecondDerivative;
        }
    }
}

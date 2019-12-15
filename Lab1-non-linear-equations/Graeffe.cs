using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_non_linear_equations
{
    class Graeffe
    {
        private List<double> coefficients;
        private List<double> currentIterationCoefficients;
        private List<double> previousIterationCoefficients;
        private Func<double, double> function;
        private double accuracy;
        public int iteration;

        public Graeffe(List<double> coefficients, Func<double, double> function, double accuracy)
        {
            this.function = function;
            this.coefficients = new List<double>(coefficients);
            this.accuracy = accuracy;
            this.currentIterationCoefficients = new List<double>(coefficients);
        }

        public List<double> ComputateRoots()
        {
            this.iteration = 0;
            do
            {
                this.Iteration();
            }
            while (!this.ShouldMethodStop());
            return this.FindRoots();
        }

        private List<double> FindRoots()
        {
            List<double> roots = new List<double>();
            for (int i = 0; i < this.currentIterationCoefficients.Count - 1; i++)
            {
                double rootDegree = Math.Pow(2, -this.iteration);
                double currentRoot = Math.Pow(this.currentIterationCoefficients[i + 1] / this.currentIterationCoefficients[i], rootDegree);
                if (Math.Abs(this.function(currentRoot)) > Math.Abs(this.function(-currentRoot)))
                {
                    currentRoot = -currentRoot;
                }
                roots.Add(currentRoot);
            }
            return roots;
        }

        private void Iteration()
        {
            this.previousIterationCoefficients = new List<double>(currentIterationCoefficients);
            List<double> nextIterationCoefficients = new List<double>();
            for (int i = 0; i < currentIterationCoefficients.Count; i++)
            {
                double a = 0;
                for (int offset = 0; i - offset >= 0 && i + offset < currentIterationCoefficients.Count; offset++)
                {
                    int currentSign = offset % 2 == 0 ? 1 : -1;
                    int multiplier = offset == 0 ? 1 : 2;
                    a = a + currentSign * multiplier * currentIterationCoefficients[i - offset] * currentIterationCoefficients[i + offset];
                }
                nextIterationCoefficients.Add(a);
            }
            this.currentIterationCoefficients = nextIterationCoefficients;
            this.iteration += 1;
        }

        private bool ShouldMethodStop()
        {
            double vectorLength = 0;
            for (int i = 0; i < this.currentIterationCoefficients.Count; i++)
            {
                if (Double.IsInfinity(currentIterationCoefficients[i]) || Double.IsNaN(currentIterationCoefficients[i]))
                {
                    throw new Exception("Double overflow");
                }
                double currentCoeficient = currentIterationCoefficients[i];
                double previousCoeficientSquared = Math.Pow(previousIterationCoefficients[i], 2);
                vectorLength += Math.Pow(1 - (currentCoeficient / previousCoeficientSquared), 2);
            }
            double n = Math.Sqrt(vectorLength);
            return n < accuracy;
        }
    }
}

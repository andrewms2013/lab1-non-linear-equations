using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_non_linear_equations
{
    class SimpleIterations
    {
        double currentRoot;
        double lambda;
        double q;
        Func<double, double> func;
        Func<double, double> derivativeFunc;
        double accuracy;

        public SimpleIterations(double leftBound, double rightBound, Func<double, double> func, Func<double, double> derivativeFunc, double accuracy)
        {
            this.accuracy = accuracy;
            currentRoot = (leftBound + rightBound) / 2;
            if (derivativeFunc(currentRoot) < 0)
            {
                this.func = x => -1 * func(x);
                this.derivativeFunc = x => -1 * derivativeFunc(x);
            }
            else
            {
                this.func = func;
                this.derivativeFunc = derivativeFunc;
            }
            double leftBoundDerivative = this.derivativeFunc(leftBound);
            double rightBoundDerivative = this.derivativeFunc(rightBound);
            double maxDerivative = leftBoundDerivative > rightBoundDerivative ? leftBoundDerivative : rightBoundDerivative;
            double minDerivative = leftBoundDerivative > rightBoundDerivative ? rightBoundDerivative : leftBoundDerivative;
            q = (maxDerivative - minDerivative) / (maxDerivative + minDerivative);
            if(q > 1)
            {
                throw new Exception("Invalid bounds");
            }
            lambda = 2 / (maxDerivative + minDerivative);
        }

        public List<double> ComputateRoots()
        {
            List<double> roots = new List<double>();
            do
            {
                this.currentRoot = currentRoot - lambda * func(currentRoot);
                roots.Add(this.currentRoot);
            }
            while (!this.ShouldMethodStop(roots));
            return roots;
        }


        private bool ShouldMethodStop(List<double> roots)
        {
            return roots.Count > 1 ? Math.Abs(roots[roots.Count - 1] - roots[roots.Count - 2]) <= accuracy * ((1 - q) / q) : false;
        }
    }
}

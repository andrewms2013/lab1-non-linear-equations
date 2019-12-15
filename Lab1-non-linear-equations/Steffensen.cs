using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_non_linear_equations
{
    class Steffensen
    {
        double currentRoot;
        Func<double, double> func;
        double accuracy;

        public Steffensen(double leftBound, double rightBound, Func<double, double> func, Func<double, double> secondDerivativeFunc, double accuracy)
        {
            this.accuracy = accuracy;
            Console.WriteLine(secondDerivativeFunc(leftBound));
            if(func(leftBound) * secondDerivativeFunc(leftBound) > 0)
            {
                currentRoot = leftBound;
            }
            else if(func(rightBound) * secondDerivativeFunc(rightBound) > 0)
            {
                currentRoot = rightBound;
            }
            else
            {
                throw new Exception("Invalid bounds");
            }
            this.func = func;
        }

        public List<double> ComputateRoots()
        {
            List<double> roots = new List<double>();
            do
            {
                this.currentRoot = currentRoot - (Math.Pow(func(this.currentRoot), 2) / (func(currentRoot + func(currentRoot)) - func(currentRoot)));
                roots.Add(this.currentRoot);
            }
            while (!this.ShouldMethodStop(roots));
            return roots;
        }


        private bool ShouldMethodStop(List<double> roots)
        {
            return roots.Count > 1 ? Math.Abs(roots[roots.Count - 1] - roots[roots.Count - 2]) < accuracy : false;
        }
    }
}

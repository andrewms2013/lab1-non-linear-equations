using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_non_linear_equations
{
    class FalsePosition
    {
        double leftBound;
        double rightBound;
        double currentRoot;
        double accuracy;
        Func<double, double> func;

        public FalsePosition(double leftBound, double rightBound, Func<double, double> func, double accuracy)
        {
            this.accuracy = accuracy;
            this.rightBound = rightBound;
            this.leftBound = leftBound;
            this.func = func;
            if (func(leftBound) * func(rightBound) > 0)
            {
                throw new Exception("Incorrect interval");
            }
        }

        public List<double> ComputateRoots()
        {
            List<double> roots = new List<double>();
            do
            {
                this.currentRoot = this.leftBound - (this.func(this.leftBound) * (this.rightBound - this.leftBound) / (this.func(this.rightBound) - this.func(this.leftBound)));
                if (func(this.leftBound) * func(this.currentRoot) < 0)
                {
                    this.rightBound = this.currentRoot;
                }
                else
                {
                    this.leftBound = this.currentRoot;
                }
                roots.Add(this.currentRoot);
            }
            while (!this.ShouldMethodStop(roots));
            return roots;
        }

        private bool ShouldMethodStop(List<double> roots)
        {
            return roots.Count > 1 ? Math.Abs(roots[roots.Count - 1] - roots[roots.Count - 2]) < accuracy : false;
        }

        private double GetCurrentRoot()
        {
            return this.currentRoot;
        }
    }
}

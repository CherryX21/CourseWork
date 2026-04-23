#pragma warning disable
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork.Solvers
{
    public interface IMatrixInverter
    {
        double[,] Invert(double[,] matrix);
    }
}

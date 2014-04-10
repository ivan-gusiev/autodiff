using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SMath = System.Math;

namespace AutoDiff
{
    public static class Math
    {
        /// <summary>
        /// Calculates a sine of dual number
        /// </summary>
        public static DualNumber Sin(DualNumber value)
        {
            return new DualNumber(SMath.Sin(value.Value), value.Derivative * SMath.Cos(value.Value));
        }

        /// <summary>
        /// Calculates a cosine of dual number
        /// </summary>
        public static DualNumber Cos(DualNumber value)
        {
            return new DualNumber(SMath.Cos(value.Value), -value.Derivative * SMath.Sin(value.Value));
        }

        /// <summary>
        /// Calculates an exponent of dual number
        /// </summary>
        public static DualNumber Exp(DualNumber value)
        {
            return new DualNumber(SMath.Exp(value.Value), value.Derivative * SMath.Exp(value.Value));
        }

        /// <summary>
        /// Calculates a natural logarithm of dual number
        /// </summary>
        public static DualNumber Log(DualNumber value)
        {
            return new DualNumber(SMath.Log(value.Value), value.Derivative / value.Value);
        }

        /// <summary>
        /// Calculates a power of dual number
        /// </summary>
        public static DualNumber Pow(DualNumber value, double power)
        {
            return new DualNumber(SMath.Pow(value.Value, power), value.Derivative * power * SMath.Pow(value.Value, power - (double)1));
        }
        
        /// <summary>
        /// Calculates a square of dual number
        /// </summary>
        public static DualNumber Squared(this DualNumber value)
        {
            return new DualNumber(SMath.Pow(value.Value, (double)2), value.Derivative * (double)2 * value.Value);
        }

        /// <summary>
        /// Calculates an absolute value of dual number
        /// </summary>
        public static DualNumber Abs(DualNumber value)
        {
            return new DualNumber(SMath.Abs(value.Value), value.Derivative * (double)SMath.Sign(value.Value));
        }

        #region Functional

        /// <summary>
        /// Creates an autodiff function from two regular functions, one for actual value, one for derivative
        /// </summary>
        /// <param name="getValue">Value transform, e.g. for x^2 it is x => x * x</param>
        /// <param name="getDerivative">Derivative transform, e.g. for x^2 it is x => 2 * x</param>
        /// <returns>A function from DualNumber to DualNumber</returns>
        public static Func<DualNumber, DualNumber> ToDual(Func<double, double> getValue, Func<double, double> getDerivative)
        {
            return d => new DualNumber(getValue(d.Value), getDerivative(d.Value) * d.Derivative);
        }

        /// <summary>
        /// Turns an autodiff function to a normal function, assuming that the argument is actually variable
        /// </summary>
        /// <param name="fn">Autodiff function, such as AutoDiff.Math.Log</param>
        /// <returns>A function that takes a double and calculates f(x) and f'(x)</returns>
        public static Func<double, DualNumber> Parameterize(Func<DualNumber, DualNumber> fn)
        {
            return value => fn(DualNumber.Variable(value));
        }
        
        #endregion



    }
}

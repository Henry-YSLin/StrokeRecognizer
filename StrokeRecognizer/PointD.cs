using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer
{
    [Serializable]
    /// <summary>
    /// PointD - Defaults to 0,0
    /// </summary>
    public struct PointD : IFormattable
    {
        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------

        #region Public Methods




        /// <summary>
        /// Compares two PointD instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two PointD instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='point1'>The first PointD to compare</param>
        /// <param name='point2'>The second PointD to compare</param>
        public static bool operator ==(PointD point1, PointD point2)
        {
            return point1.X == point2.X &&
                   point1.Y == point2.Y;
        }

        /// <summary>
        /// Compares two PointD instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two PointD instances are exactly unequal, false otherwise
        /// </returns>
        /// <param name='point1'>The first PointD to compare</param>
        /// <param name='point2'>The second PointD to compare</param>
        public static bool operator !=(PointD point1, PointD point2)
        {
            return !(point1 == point2);
        }
        /// <summary>
        /// Compares two PointD instances for object equality.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the two PointD instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='point1'>The first PointD to compare</param>
        /// <param name='point2'>The second PointD to compare</param>
        public static bool Equals(PointD point1, PointD point2)
        {
            return point1.X.Equals(point2.X) &&
                   point1.Y.Equals(point2.Y);
        }

        /// <summary>
        /// Equals - compares this PointD with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the object is an instance of PointD and if it's equal to "this".
        /// </returns>
        /// <param name='o'>The object to compare to "this"</param>
        public override bool Equals(object o)
        {
            if ((null == o) || !(o is PointD))
            {
                return false;
            }

            PointD value = (PointD)o;
            return PointD.Equals(this, value);
        }

        /// <summary>
        /// Equals - compares this PointD with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if "value" is equal to "this".
        /// </returns>
        /// <param name='value'>The PointD to compare to "this"</param>
        public bool Equals(PointD value)
        {
            return PointD.Equals(this, value);
        }


        /// <summary>
        /// Returns the HashCode for this PointD
        /// </summary>
        /// <returns>
        /// int - the HashCode for this PointD
        /// </returns>
        public override int GetHashCode()
        {
            // Perform field-by-field XOR of HashCodes
            return X.GetHashCode() ^
                   Y.GetHashCode();
        }


        /// <summary>
        /// Offset - update the location by adding offsetX to X and offsetY to Y
        /// </summary>
        /// <param name="offsetX"> The offset in the x dimension </param>
        /// <param name="offsetY"> The offset in the y dimension </param>
        public void Offset(double offsetX, double offsetY)
        {
            _x += offsetX;
            _y += offsetY;
        }

        /// <summary>
        /// Operator PointD + PointD
        /// </summary>
        /// <returns>
        /// PointD - The result of the addition
        /// </returns>
        /// <param name="point"> The first PointD to be added </param>
        /// <param name="vector"> The second PointD to be added </param>
        public static PointD operator +(PointD point1, PointD point2)
        {
            return new PointD(point1._x + point2._x, point1._y + point2._y);
        }

        /// <summary>
        /// Add: PointD + PointD
        /// </summary>
        /// <returns>
        /// PointD - The result of the addition
        /// </returns>
        /// <param name="point"> The first PointD to be added </param>
        /// <param name="vector"> The second PointD to be added </param>
        public static PointD Add(PointD point1, PointD point2)
        {
            return new PointD(point1._x + point2._x, point1._y + point2._y);
        }

        /// <summary>
        /// Operator PointD - PointD
        /// </summary>
        /// <returns>
        /// PointD - The result of the subtraction
        /// </returns>
        /// <param name="point1"> The PointD from which point2 is subtracted </param>
        /// <param name="point2"> The PointD subtracted from point1 </param>
        public static PointD operator -(PointD point1, PointD point2)
        {
            return new PointD(point1._x - point2._x, point1._y - point2._y);
        }

        /// <summary>
        /// Subtract: PointD - PointD
        /// </summary>
        /// <returns>
        /// PointD - The result of the subtraction
        /// </returns>
        /// <param name="point1"> The PointD from which point2 is subtracted </param>
        /// <param name="point2"> The PointD subtracted from point1 </param>
        public static PointD Subtract(PointD point1, PointD point2)
        {
            return new PointD(point1._x - point2._x, point1._y - point2._y);
        }

        /// <summary>
        /// Operator PointD * double
        /// </summary>
        public static PointD operator *(PointD point, double coef)
        {
            return new PointD(point._x * coef, point._y * coef);
        }

        /// <summary>
        /// Multiply: PointD * double
        /// </summary>
        public static PointD Multiply(PointD point, double coef)
        {
            return new PointD(point._x * coef, point._y * coef);
        }

        /// <summary>
        /// Operator PointD / double
        /// </summary>
        public static PointD operator /(PointD point, double coef)
        {
            return new PointD(point._x / coef, point._y / coef);
        }

        /// <summary>
        /// Distance from this PointD to another PointD
        /// </summary>
        /// <param name="point"> The other PointD </param>
        /// <returns> Distance between two points </returns>
        public double Distance(PointD point)
        {
            return Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2));
        }

        #endregion Public Methods

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------

        #region Public Properties

        /// <summary>
        ///     X - double.  Default value is 0.
        /// </summary>
        public double X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }

        }

        /// <summary>
        ///     Y - double.  Default value is 0.
        /// </summary>
        public double Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }

        }

        #endregion Public Properties

        //------------------------------------------------------
        //
        //  Protected Methods
        //
        //------------------------------------------------------

        #region Protected Methods





        #endregion ProtectedMethods

        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------

        #region Internal Methods









        #endregion Internal Methods

        //------------------------------------------------------
        //
        //  Internal Properties
        //
        //------------------------------------------------------

        #region Internal Properties


        /// <summary>
        /// Creates a string representation of this object based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ToString()
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(null /* format string */, null /* format provider */);
        }

        /// <summary>
        /// Creates a string representation of this object based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(null /* format string */, provider);
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(format, provider);
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            // Helper to get the numeric list separator for a given culture.
            char separator = ',';
            return String.Format(provider,
                                 "{1:" + format + "}{0}{2:" + format + "}",
                                 separator,
                                 _x,
                                 _y);
        }



        #endregion Internal Properties

        //------------------------------------------------------
        //
        //  Dependency Properties
        //
        //------------------------------------------------------

        #region Dependency Properties



        #endregion Dependency Properties

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------

        #region Private Fields


        private double _x;
        private double _y;




        #endregion Internal Fields

        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Constructor which accepts the X and Y values
        /// </summary>
        /// <param name="x">The value for the X coordinate of the new PointD</param>
        /// <param name="y">The value for the Y coordinate of the new PointD</param>
        public PointD(double x, double y)
        {
            _x = x;
            _y = y;
        }

        #endregion Constructors
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer
{
    [Serializable]
    /// <summary>
    /// CentralTendency - Defaults to Mean:0,Std:0
    /// </summary>
    public struct CentralTendency : IFormattable
    {
        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------

        #region Public Methods




        /// <summary>
        /// Compares two CentralTendency instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two CentralTendency instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='ct1'>The first CentralTendency to compare</param>
        /// <param name='ct2'>The second CentralTendency to compare</param>
        public static bool operator ==(CentralTendency ct1, CentralTendency ct2)
        {
            return ct1.Mean == ct2.Mean &&
                   ct1.StandardDeviation == ct2.StandardDeviation;
        }

        /// <summary>
        /// Compares two CentralTendency instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two CentralTendency instances are exactly unequal, false otherwise
        /// </returns>
        /// <param name='ct1'>The first CentralTendency to compare</param>
        /// <param name='ct2'>The second CentralTendency to compare</param>
        public static bool operator !=(CentralTendency ct1, CentralTendency ct2)
        {
            return !(ct1 == ct2);
        }
        /// <summary>
        /// Compares two CentralTendency instances for object equality.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the two CentralTendency instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='ct1'>The first CentralTendency to compare</param>
        /// <param name='ct2'>The second CentralTendency to compare</param>
        public static bool Equals(CentralTendency ct1, CentralTendency ct2)
        {
            return ct1.Mean.Equals(ct2.Mean) &&
                   ct1.StandardDeviation.Equals(ct2.StandardDeviation);
        }

        /// <summary>
        /// Equals - compares this CentralTendency with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the object is an instance of CentralTendency and if it's equal to "this".
        /// </returns>
        /// <param name='o'>The object to compare to "this"</param>
        public override bool Equals(object o)
        {
            if ((null == o) || !(o is CentralTendency))
            {
                return false;
            }

            CentralTendency value = (CentralTendency)o;
            return CentralTendency.Equals(this, value);
        }

        /// <summary>
        /// Equals - compares this CentralTendency with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if "value" is equal to "this".
        /// </returns>
        /// <param name='value'>The CentralTendency to compare to "this"</param>
        public bool Equals(CentralTendency value)
        {
            return CentralTendency.Equals(this, value);
        }
        /// <summary>
        /// Returns the HashCode for this CentralTendency
        /// </summary>
        /// <returns>
        /// int - the HashCode for this CentralTendency
        /// </returns>
        public override int GetHashCode()
        {
            // Perform field-by-field XOR of HashCodes
            return Mean.GetHashCode() ^
                   StandardDeviation.GetHashCode();
        }


        /// <summary>
        /// Offset - update the mean by adding offset to it
        /// </summary>
        /// <param name="offset"> The offset for mean </param>
        public void OffsetMean(double offset)
        {
            _mean += offset;
        }


        /// <summary>
        /// OffsetStandardDeviation - update the standard deviation by adding offset to it
        /// </summary>
        /// <param name="offset"> The offset for standard deviation </param>
        public void OffsetStandardDeviation(double offset)
        {
            _standardDeviation += offset;
        }


        /// <summary>
        /// ScaleMean - update the mean by multiplying factor to it
        /// </summary>
        /// <param name="factor"> The factor for multiplication </param>
        public void ScaleMean(double factor)
        {
            _mean *= factor;
        }


        /// <summary>
        /// ScaleStandardDeviation - update the standard deviation by multiplying factor to it
        /// </summary>
        /// <param name="factor"> The factor for multiplication </param>
        public void ScaleStandardDeviation(double factor)
        {
            _standardDeviation *= factor;
        }

        /// <summary>
        /// Operator CentralTendency + double
        /// Calculate the effect if this addition is applied to all data in the sample
        /// i.e. applies the addition to the mean
        /// </summary>
        /// <returns>
        /// CentralTendency - The result of the addition
        /// </returns>
        /// <param name="ct"> The affected CentralTendency </param>
        /// <param name="value"> The number to be added </param>
        public static CentralTendency operator +(CentralTendency ct, double value)
        {
            return new CentralTendency(ct._mean + value, ct._standardDeviation);
        }

        /// <summary>
        /// Operator CentralTendency - double
        /// Calculate the effect if this subtraction is applied to all data in the sample
        /// i.e. applies the subtraction to the mean
        /// </summary>
        /// <returns>
        /// CentralTendency - The result of the subtraction
        /// </returns>
        /// <param name="ct"> The affected CentralTendency </param>
        /// <param name="value"> The number to be subtracted </param>
        public static CentralTendency operator -(CentralTendency ct, double value)
        {
            return new CentralTendency(ct._mean - value, ct._standardDeviation);
        }

        /// <summary>
        /// Operator CentralTendency * double
        /// Calculate the effect if this multiplication is applied to all data in the sample
        /// i.e. applies the multiplication to both the mean and the standard deviation
        /// </summary>
        /// <returns>
        /// CentralTendency - The result of the multiplication
        /// </returns>
        /// <param name="ct"> The affected CentralTendency </param>
        /// <param name="value"> The number to be multiplied </param>
        public static CentralTendency operator *(CentralTendency ct, double value)
        {
            return new CentralTendency(ct._mean * value, ct._standardDeviation * value);
        }

        /// <summary>
        /// Operator CentralTendency / double
        /// Calculate the effect if this division is applied to all data in the sample
        /// i.e. applies the division to both the mean and the standard deviation
        /// </summary>
        /// <returns>
        /// CentralTendency - The result of the division
        /// </returns>
        /// <param name="ct"> The affected CentralTendency </param>
        /// <param name="value"> The divisor </param>
        public static CentralTendency operator /(CentralTendency ct, double value)
        {
            return new CentralTendency(ct._mean / value, ct._standardDeviation / value);
        }


        #endregion Public Methods

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------

        #region Public Properties

        /// <summary>
        ///     Mean - double.  Default value is 0.
        /// </summary>
        public double Mean
        {
            get
            {
                return _mean;
            }

            set
            {
                _mean = value;
            }

        }

        /// <summary>
        ///     Standard Deviation - double.  Default value is 0.
        /// </summary>
        public double StandardDeviation
        {
            get
            {
                return _standardDeviation;
            }

            set
            {
                _standardDeviation = value;
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
        /// Creates a string representation of this object based on the format string
        /// passed in. 
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(string format)
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(format, null);
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
                                 "Mean:{1:" + format + "},Std:{2:" + format + "}",
                                 separator,
                                 _mean,
                                 _standardDeviation);
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
        //  Internal Fields
        //
        //------------------------------------------------------

        #region Internal Fields


        internal double _mean;
        internal double _standardDeviation;




        #endregion Internal Fields

        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Constructor which accepts the mean and standard deviation values
        /// </summary>
        /// <param name="mean">The value for the mean of the new CentralTendency</param>
        /// <param name="standardDeviation">The value for the standard deviation of the new CentralTendency</param>
        public CentralTendency(double mean, double standardDeviation)
        {
            _mean = mean;
            _standardDeviation = standardDeviation;
        }

        /// <summary>
        /// Constructor which accepts a collection of data to compute mean and standard deviation
        /// </summary>
        /// <param name="samples"> A collection of sample data </param>
        public CentralTendency(IEnumerable<double> samples)
        {
            double sum = samples.Sum();
            double count = Math.Max(1, samples.Count());
            double mean = sum / count;
            _mean = mean;
            double variation = samples.Sum(x => Math.Pow(x - mean, 2));
            _standardDeviation = Math.Sqrt(variation / count);
        }

        #endregion Constructors
    }
}

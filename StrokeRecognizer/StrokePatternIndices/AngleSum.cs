using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class AngleSum : StrokePatternIndex
    {
        public override string Name => "Angle Sum";

        public override double ComputeIndex(Stroke stroke)
        {
            double sum = 0;
            for (int i = 1; i < stroke.Points.Count - 1; i++)
            {
                sum += getAngle(stroke.Points[i - 1], stroke.Points[i], stroke.Points[i + 1]);
            }
            return sum;
        }

        private static double getAngle(PointD p1, PointD p2, PointD p3)
        {
            double angle = Math.Abs(Math.Atan2(p3.Y - p2.Y, p3.X - p2.X) - Math.Atan2(p2.Y - p1.Y, p2.X - p1.X)) % (Math.PI * 2);
            return Math.Min(angle, Math.PI * 2 - angle);
        }
    }
}

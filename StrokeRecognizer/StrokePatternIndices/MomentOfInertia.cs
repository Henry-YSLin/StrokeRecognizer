using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class MomentOfInertia : StrokePatternIndex
    {
        public override string Name => "Moment of inertia";

        public override double ComputeIndex(Stroke stroke)
        {
            return stroke.Points.Sum(x => Math.Pow(x.Distance(new PointD(50, 50)), 2));
        }
    }
}

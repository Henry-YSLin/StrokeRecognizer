using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class PointsCount : StrokePatternIndex
    {
        public override string Name => "Points count";

        public override double ComputeIndex(Stroke stroke)
        {
            return stroke.Points.Count;
        }
    }
}

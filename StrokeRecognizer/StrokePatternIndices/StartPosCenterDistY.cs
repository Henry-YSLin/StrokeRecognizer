using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class StartPosCenterDistY : StrokePatternIndex
    {
        public override string Name => "Start position from center Y";

        public override double ComputeIndex(Stroke stroke)
        {
            return Math.Abs(stroke.Points.First().Y - Stroke.SCALE_SIZE / 2);
        }
    }
}

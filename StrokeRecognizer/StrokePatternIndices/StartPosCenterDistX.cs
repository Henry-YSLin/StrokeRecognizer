using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class StartPosCenterDistX : StrokePatternIndex
    {
        public override string Name => "Start position from center X";

        public override double ComputeIndex(Stroke stroke)
        {
            return Math.Abs(stroke.Points.First().X - Stroke.SCALE_SIZE / 2);
        }
    }
}

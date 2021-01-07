using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class EndPosX : StrokePatternIndex
    {
        public override string Name => "End position X";

        public override double ComputeIndex(Stroke stroke)
        {
            return stroke.Points.Last().X;
        }
    }
}

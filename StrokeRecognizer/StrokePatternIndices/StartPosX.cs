using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class StartPosX : StrokePatternIndex
    {
        public override string Name => "Start position X";

        public override double ComputeIndex(Stroke stroke)
        {
            return stroke.Points.First().X;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class StartPosY : StrokePatternIndex
    {
        public override string Name => "Start position Y";

        public override double ComputeIndex(Stroke stroke)
        {
            return stroke.Points.First().Y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class CenterOfMassY : StrokePatternIndex
    {
        public override string Name => "Center of mass Y";

        public override double ComputeIndex(Stroke stroke)
        {
            return stroke.Points.Average(x => x.Y);
        }
    }
}

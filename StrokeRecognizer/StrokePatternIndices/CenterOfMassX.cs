using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class CenterOfMassX : StrokePatternIndex
    {
        public override string Name => "Center of mass X";

        public override double ComputeIndex(Stroke stroke)
        {
            return stroke.Points.Average(x => x.X);
        }
    }
}

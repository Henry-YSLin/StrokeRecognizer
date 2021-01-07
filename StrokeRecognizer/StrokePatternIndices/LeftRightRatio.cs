using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class LeftRightRatio : StrokePatternIndex
    {
        public override string Name => "Left right ratio";

        public override double ComputeIndex(Stroke stroke)
        {
            double left = stroke.Points.Where(x => x.X < Stroke.SCALE_SIZE / 2).Count();
            if (left == stroke.Points.Count) return 1;
            return left / (stroke.Points.Count - left);
        }
    }
}

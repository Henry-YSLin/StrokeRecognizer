using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    class TopBottomRatio : StrokePatternIndex
    {
        public override string Name => "Top bottom ratio";

        public override double ComputeIndex(Stroke stroke)
        {
            double bottom = stroke.Points.Where(x => x.Y < Stroke.SCALE_SIZE / 2).Count();
            if (bottom == stroke.Points.Count) return 1;
            return bottom / (stroke.Points.Count - bottom);
        }
    }
}

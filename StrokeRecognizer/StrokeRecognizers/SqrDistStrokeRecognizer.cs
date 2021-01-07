using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrokeRecognizer.StrokePatternIndices;

namespace StrokeRecognizer.StrokeRecognizers
{
    public class SqrDistStrokeRecognizer : StrokeRecognizer
    {
        public SqrDistStrokeRecognizer(IEnumerable<StrokePattern> patterns) 
        {
            this.patterns = patterns.ToList();
            IndexAllPatterns();
        }

        public override StrokePattern Recogonize(Stroke stroke)
        {
            var result = Patterns
                .MinBy(x => PatternIndices.Sum(y => Math.Pow(y.GetStandardScore(x, stroke), 2)));
            return result.First();
        }
    }
}

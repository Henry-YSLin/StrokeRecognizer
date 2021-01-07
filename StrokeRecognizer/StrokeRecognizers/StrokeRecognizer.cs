using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StrokeRecognizer.StrokePatternIndices;

namespace StrokeRecognizer.StrokeRecognizers
{
    public abstract class StrokeRecognizer
    {
        protected List<StrokePattern> patterns;

        public List<StrokePattern> Patterns
        {
            get { return patterns; }
            set { patterns = value; }
        }


        protected List<StrokePatternIndex> patternIndices;

        public List<StrokePatternIndex> PatternIndices
        {
            get { return patternIndices; }
            set { patternIndices = value; }
        }

        public StrokeRecognizer()
        {
            patternIndices = this.GetType().Assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(StrokePatternIndex)))
                .Select(x => (StrokePatternIndex)Activator.CreateInstance(x))
                .Where(x => x.IsEnabled)
                .ToList();
        }

        public void IndexAllPatterns()
        {
            this.patternIndices.ForEach(x =>
            {
                x.ClearIndexedPatterns();
                x.IndexPatterns(this.patterns);
            });
        }

        public abstract StrokePattern Recogonize(Stroke stroke);
    }
}

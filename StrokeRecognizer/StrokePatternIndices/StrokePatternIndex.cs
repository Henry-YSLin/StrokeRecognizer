using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer.StrokePatternIndices
{
    public abstract class StrokePatternIndex
    {
        public abstract string Name { get; }

        public virtual bool IsEnabled { get => true; }

        private readonly Dictionary<StrokePattern, CentralTendency> indices = new Dictionary<StrokePattern, CentralTendency>();

        public List<StrokePattern> IndexedStrokePatterns
        {
            get
            {
                return indices.Keys.ToList();
            }
        }

        public abstract double ComputeIndex(Stroke stroke);

        public CentralTendency ComputeCentralTendency(StrokePattern pattern)
        {
            return new CentralTendency(pattern.SampleStrokes.AsParallel().Select(x => ComputeIndex(x)));
        }

        public void IndexPattern(StrokePattern pattern)
        {
            if (indices.ContainsKey(pattern))
            {
                indices[pattern] = ComputeCentralTendency(pattern);
            }
            else
            {
                indices.Add(pattern, ComputeCentralTendency(pattern));
            }
        }

        public CentralTendency GetPatternCentralTendency(StrokePattern pattern)
        {
            if (!indices.ContainsKey(pattern))
                throw new InvalidOperationException("The provided StrokePattern is not indexed.");
            return indices[pattern];
        }

        public void IndexPatterns(IEnumerable<StrokePattern> patterns)
        {
            patterns.ForEach(x => IndexPattern(x));
        }

        public void ClearIndexedPatterns()
        {
            indices.Clear();
        }

        public double GetStandardScore(StrokePattern pattern, Stroke stroke)
        {
            if (!indices.ContainsKey(pattern))
                throw new InvalidOperationException("The provided StrokePattern is not indexed.");

            CentralTendency ct = indices[pattern];
            return (ComputeIndex(stroke) - ct.Mean) / Math.Max(0.01, ct.StandardDeviation);
        }

        public double GetIndexVariation()
        {
            CentralTendency meansCt = new CentralTendency(indices.Values.Select(x => x.Mean));
            CentralTendency stdCt = new CentralTendency(indices.Values.Select(x => x.StandardDeviation));
            return meansCt.StandardDeviation / stdCt.Mean;
        }
    }
}

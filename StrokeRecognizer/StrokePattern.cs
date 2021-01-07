using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeRecognizer
{
    public class StrokePattern
    {
        private List<Stroke> sampleStrokes = new List<Stroke>();

        public List<Stroke> SampleStrokes
        {
            get { return sampleStrokes; }
            set { sampleStrokes = value; }
        }

        private string patternName;

        public string PatternName
        {
            get { return patternName; }
            set { patternName = value; }
        }

        public override string ToString()
        {
            return "Stroke Pattern - " + PatternName;
        }
    }
}

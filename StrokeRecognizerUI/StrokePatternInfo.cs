using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using StrokeRecognizer;

namespace StrokeRecognizerUI
{
    public class StrokePatternInfo : INotifyPropertyChanged
    {
        private void NotifyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private StrokePattern strokePattern;

        public event PropertyChangedEventHandler PropertyChanged;

        public StrokePattern StrokePattern
        {
            get { return strokePattern; }
            set
            {
                strokePattern = value;
                NotifyChanged(nameof(StrokePattern));
                NotifyChanged(nameof(StrokeCollection));
                NotifyChanged(nameof(StatsString));
            }
        }

        private bool showDetails;

        public bool ShowDetails
        {
            get { return showDetails; }
            set
            {
                showDetails = value;
                NotifyChanged(nameof(ShowDetails));
                NotifyChanged(nameof(StatsString));
            }
        }


        public StrokeRecognizer.StrokeRecognizers.StrokeRecognizer Recognizer { get; set; }

        public StrokeCollection StrokeCollection
        {
            get
            {
                return new StrokeCollection(
                    StrokePattern.SampleStrokes.Select(x => new System.Windows.Ink.Stroke(
                        new System.Windows.Input.StylusPointCollection(
                            x.Points.Select(y => new System.Windows.Input.StylusPoint(y.X, y.Y))
                        )
                    )
                ));
            }
        }

        public string StatsString
        {
            get
            {
                if (!ShowDetails) return null;
                StringBuilder sb = new StringBuilder();
                sb.Append("\r\n\r\n");
                Recognizer.PatternIndices.ForEach(x => sb.AppendLine(x.Name + ": " + x.GetPatternCentralTendency(StrokePattern).ToString("F2")));
                return sb.ToString();
            }
        }

        public StrokePatternInfo(StrokeRecognizer.StrokeRecognizers.StrokeRecognizer recognizer, StrokePattern pattern)
        {
            Recognizer = recognizer;
            StrokePattern = pattern;
        }
    }
}

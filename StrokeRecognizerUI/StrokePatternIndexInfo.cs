using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using StrokeRecognizer;
using StrokeRecognizer.StrokePatternIndices;

namespace StrokeRecognizerUI
{
    public class StrokePatternIndexInfo : INotifyPropertyChanged
    {
        private void NotifyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private StrokePatternIndex strokePatternIndex;

        public StrokePatternIndex StrokePatternIndex
        {
            get { return strokePatternIndex; }
            set
            {
                strokePatternIndex = value;
                NotifyChanged(nameof(StrokePatternIndex));
                NotifyChanged(nameof(SeriesCollection));
                NotifyChanged(nameof(Labels));
            }
        }

        public StrokePatternIndexInfo(StrokeRecognizer.StrokeRecognizers.StrokeRecognizer recognizer, StrokePatternIndex strokePatternIndex)
        {
            this.strokePatternIndex = strokePatternIndex;
            Recognizer = recognizer;
            SeriesCollection = new SeriesCollection
            {
                new OhlcSeries()
                {
                    Values = new ChartValues<OhlcPoint>(recognizer.Patterns.Select(x => new OhlcPoint(0,0,0,0))),
                    MaxColumnWidth = double.PositiveInfinity,
                },
            };

            Labels = recognizer.Patterns.Select(x => x.PatternName).ToArray();
        }

        public StrokeRecognizer.StrokeRecognizers.StrokeRecognizer Recognizer { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        private string[] _labels;

        public event PropertyChangedEventHandler PropertyChanged;

        public void RefreshData()
        {
            for (int i = 0; i < Recognizer.Patterns.Count; i++)
            {
                OhlcPoint point = (OhlcPoint)SeriesCollection[0].Values[i];
                CentralTendency ct = StrokePatternIndex.GetPatternCentralTendency(Recognizer.Patterns[i]);
                point.High = ct.Mean + ct.StandardDeviation;
                point.Low = ct.Mean - ct.StandardDeviation;
                point.Open = ct.Mean;
                point.Close = ct.Mean;
            }
            NotifyChanged(nameof(SeriesCollection));
            NotifyChanged(nameof(Labels));
        }

        public string[] Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                NotifyChanged(nameof(Labels));
            }
        }
    }
}

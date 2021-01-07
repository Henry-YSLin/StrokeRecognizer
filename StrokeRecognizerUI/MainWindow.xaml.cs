using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StrokeRecognizer;
using StrokeRecognizer.StrokeRecognizers;
using MoreLinq;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using LiveCharts.Configurations;

namespace StrokeRecognizerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            PatternInfos = new ObservableCollection<StrokePatternInfo>();
            recognizer.Patterns.Select(x => new StrokePatternInfo(recognizer, x) { ShowDetails = false }).ForEach(x => PatternInfos.Add(x));
            IndexInfos = new ObservableCollection<StrokePatternIndexInfo>();
            recognizer.PatternIndices.Select(x => new StrokePatternIndexInfo(recognizer, x)).ForEach(x => IndexInfos.Add(x));
        }

        private void NotifyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool trainingMode;
        public bool TrainingMode
        {
            get => trainingMode;
            set
            {
                trainingMode = value;
                NotifyChanged(nameof(TrainingMode));
            }
        }

        private StrokePatternInfo selectedPattern;

        public StrokePatternInfo SelectedPattern
        {
            get { return selectedPattern; }
            set
            {
                selectedPattern = value;
                NotifyChanged(nameof(SelectedPattern));
            }
        }



        public string IndicesStats
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                recognizer.PatternIndices.Select(x => (x.Name, x.GetIndexVariation()))
                    .OrderByDescending(x => x.Item2)
                    .Select(x => $"{x.Name} - {x.Item2:F2}")
                    .ForEach(x => sb.AppendLine(x));
                return sb.ToString();
            }
        }

        private bool isDirty = true;

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
        }

        SqrDistStrokeRecognizer recognizer = new SqrDistStrokeRecognizer(new[] {
            new StrokePattern() { PatternName = "Cross" },
            new StrokePattern() { PatternName = "Circle" },
            new StrokePattern() { PatternName = "V" },
            new StrokePattern() { PatternName = "Upside Down V" },
            new StrokePattern() { PatternName = "Horizontal Line" },
            new StrokePattern() { PatternName = "Vertical Line" },
            new StrokePattern() { PatternName = "Z" },
            new StrokePattern() { PatternName = "Vertical Double Loop" },
            new StrokePattern() { PatternName = "Horizontal Double Loop" },
            new StrokePattern() { PatternName = "Double Cross" },
            new StrokePattern() { PatternName = "Sawtooth" },
            new StrokePattern() { PatternName = "Epsilon" },
            new StrokePattern() { PatternName = "S" },
        });

        private ObservableCollection<StrokePatternInfo> patternInfos;
        public ObservableCollection<StrokePatternInfo> PatternInfos
        {
            get => patternInfos;
            set
            {
                patternInfos = value;
                NotifyChanged(nameof(PatternInfos));
            }
        }

        private ObservableCollection<StrokePatternIndexInfo> indexInfos;
        public ObservableCollection<StrokePatternIndexInfo> IndexInfos
        {
            get => indexInfos;
            set
            {
                indexInfos = value;
                NotifyChanged(nameof(IndexInfos));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void refreshView()
        {
            NotifyChanged(nameof(PatternInfos));
            listBox.Items.Refresh();
            NotifyChanged(nameof(IndicesStats));
            indexInfos.ForEach(x => x.RefreshData());
            NotifyChanged(nameof(IndexInfos));
        }

        private void inkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
            inkCanvas.Strokes.Add(e.Stroke);
            if (TrainingMode)
            {
                if (listBox.SelectedItem == null) return;
                StrokePatternInfo info = listBox.SelectedItem as StrokePatternInfo;
                if (info == null) return;
                info.StrokePattern.SampleStrokes.Add(new StrokeRecognizer.Stroke(e.Stroke.StylusPoints.Select(x => new PointD(x.X, x.Y))));
                refreshView();
                isDirty = true;
            }
            else
            {
                if (isDirty)
                    recognizer.IndexAllPatterns();
                isDirty = false;
                StrokePattern pattern = recognizer.Recogonize(new StrokeRecognizer.Stroke(e.Stroke.StylusPoints.Select(x => new PointD(x.X, x.Y))));
                listBox.SelectedItem = patternInfos.First(x => x.StrokePattern == pattern);
                refreshView();
                listBox.ScrollIntoView(listBox.SelectedItem);
            }
        }

        private void btnRemoveLast_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem == null) return;
            StrokePatternInfo info = listBox.SelectedItem as StrokePatternInfo;
            if (info == null) return;
            if (info.StrokePattern.SampleStrokes.Count == 0) return;
            info.StrokePattern.SampleStrokes.Remove(info.StrokePattern.SampleStrokes.Last());
            refreshView();
            isDirty = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "*.json|*.json";
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, JsonConvert.SerializeObject(recognizer.Patterns));
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Loading data will overwrite current training data. Are you sure you want to continue?", "Stroke Recognizer", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.json|*.json";
            if (dialog.ShowDialog() == true)
            {
                JArray jsonResponse = JArray.Parse(File.ReadAllText(dialog.FileName));
                List<StrokePattern> patterns = new List<StrokePattern>();
                foreach (var item in jsonResponse)
                {
                    patterns.Add(item.ToObject<StrokePattern>());
                }
                recognizer.Patterns = patterns;
                PatternInfos = new ObservableCollection<StrokePatternInfo>();
                recognizer.Patterns.Select(x => new StrokePatternInfo(recognizer, x)).ForEach(x => PatternInfos.Add(x));
                recognizer.IndexAllPatterns();
                refreshView();
                isDirty = false;
            }
        }

        private void btnShowDetails_Click(object sender, RoutedEventArgs e)
        {
            PatternInfos.ForEach(x => x.ShowDetails = !x.ShowDetails);
        }

        private void btnRecompute_Click(object sender, RoutedEventArgs e)
        {
            if (isDirty)
                recognizer.IndexAllPatterns();
            isDirty = false;
            refreshView();
        }
    }
}

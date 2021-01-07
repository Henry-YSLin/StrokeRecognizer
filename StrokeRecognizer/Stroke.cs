using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StrokeRecognizer.Helper;

namespace StrokeRecognizer
{
    public class Stroke
    {
        public const double SCALE_SIZE = 100;
        private const double DEFAULT_RESAMPLING_PRECISION = SCALE_SIZE / 20;

        [JsonProperty(PropertyName = "HorizontalDirection")]
        private StrokeHorizontalDirection horizontalDirection;

        [JsonIgnore]
        public StrokeHorizontalDirection HorizontalDirection
        {
            get { return horizontalDirection; }
            private set { horizontalDirection = value; }
        }


        [JsonProperty(PropertyName = "VerticalDirection")]
        private StrokeVerticalDirection verticalDirection;

        [JsonIgnore]
        public StrokeVerticalDirection VerticalDirection
        {
            get { return verticalDirection; }
            private set { verticalDirection = value; }
        }


        private double resamplingPrecision;

        public double ResamplingPrecision
        {
            get { return resamplingPrecision; }
            private set { resamplingPrecision = value; }
        }


        [JsonProperty(PropertyName = "Points")]
        private List<PointD> points;

        [JsonIgnore]
        public IReadOnlyList<PointD> Points
        {
            get => points
                .Select(x => new PointD(
                    HorizontalDirection == StrokeHorizontalDirection.LeftIsPositive ? SCALE_SIZE - x.X : x.X,
                    VerticalDirection == StrokeVerticalDirection.DownIsPositive ? SCALE_SIZE - x.Y : x.Y
                    ))
                .ToList()
                .AsReadOnly();
        }

        private void normalize()
        {
            if (verticalDirection == StrokeVerticalDirection.DownIsPositive)
            {
                points = points.Select(x => new PointD(x.X, -x.Y)).ToList();
            }
            if (horizontalDirection == StrokeHorizontalDirection.LeftIsPositive)
            {
                points = points.Select(x => new PointD(-x.X, x.Y)).ToList();
            }


            double maxX = double.NegativeInfinity;
            double maxY = double.NegativeInfinity;
            double minX = double.PositiveInfinity;
            double minY = double.PositiveInfinity;
            points.ForEach(x =>
            {
                maxX = Math.Max(maxX, x.X);
                maxY = Math.Max(maxY, x.Y);
                minX = Math.Min(minX, x.X);
                minY = Math.Min(minY, x.Y);
            });
            if (maxX - minX > maxY - minY)
            {
                double offset = (maxX - minX - (maxY - minY)) / 2;
                minY -= offset;
                maxY += offset;
            }
            else
            {
                double offset = (maxY - minY - (maxX - minX)) / 2;
                minX -= offset;
                maxX += offset;
            }
            points = points.Select(x => new PointD(x.X.Map(minX, maxX, 0, SCALE_SIZE), x.Y.Map(minY, maxY, 0, SCALE_SIZE))).ToList();


            if (points.Count < 2) return;

            List<(PointD, double)> dist = new List<(PointD, double)>();
            foreach (PointD point in points)
            {
                if (dist.Count == 0)
                {
                    dist.Add((point, 0));
                }
                else
                {
                    dist.Add((point, dist.Last().Item1.Distance(point) + dist.Last().Item2));
                }
            }
            List<PointD> newPoints = new List<PointD>();
            double distSum = 0;
            double rDist = ResamplingPrecision;
            newPoints.Add(points.First());
            for (int i = 1; i < points.Count; i++)
            {
                while (dist[i].Item2 - distSum > rDist)
                {
                    distSum += ResamplingPrecision;
                    newPoints.Add((points[i] - points[i - 1]) / (dist[i].Item2 - dist[i - 1].Item2) * (distSum - dist[i - 1].Item2) + points[i - 1]);
                    rDist = ResamplingPrecision;
                }
                rDist -= dist[i].Item2 - distSum;
            }
            if (rDist < ResamplingPrecision)
            {
                newPoints.Add(points.Last());
            }
            points = newPoints;
        }

        public Stroke() { }

        /// <summary>
        /// Constructor - Takes in a collection of points and normalize them
        /// </summary>
        /// <param name="points"> The collection of points </param>
        /// <param name="horizontal"> Horizontal sign convention of the list of points </param>
        /// <param name="vertical"> Vertical sign convention of the list of points </param>
        public Stroke(IEnumerable<PointD> points,
            StrokeHorizontalDirection horizontal = StrokeHorizontalDirection.RightIsPositive,
            StrokeVerticalDirection vertical = StrokeVerticalDirection.DownIsPositive,
            double resamplingPrecision = DEFAULT_RESAMPLING_PRECISION)
        {
            this.points = points.ToList();
            this.horizontalDirection = horizontal;
            this.verticalDirection = vertical;
            this.resamplingPrecision = resamplingPrecision;
            normalize();
        }
    }

    public enum StrokeHorizontalDirection
    {
        RightIsPositive,
        LeftIsPositive
    }

    public enum StrokeVerticalDirection
    {
        UpIsPositive,
        DownIsPositive
    }
}

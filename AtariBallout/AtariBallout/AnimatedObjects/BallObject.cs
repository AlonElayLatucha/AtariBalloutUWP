using AtariBallout.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace AtariBallout.AnimatedObjects
{
    public class BallObject : AnimatedObject
    {
        public Ellipse ellipse;
        public BallType ballType;
        public int yDistancePerMove;

        public readonly int ELLIPSE_INITIAL_CANVAS_LEFT = 416;
        public readonly int ELLIPSE_INITIAL_CANVAS_TOP = 335;

        public readonly int DEFAULT_Y_LOCATION = 75;
        public readonly int DEFAULT_X_LOCATION = 416;

        public BallObject(int width, int height, int distancePerMove, Canvas canvas, BallType ballType, Ellipse ellipse)
            : base(
            width: 50, height: 50, distancePerMove, canvas)
        {
            this.ballType = ballType;
            this.ellipse = ellipse;

            this.width = width;
            this.height = height;

            ellipse.Height = height;
            ellipse.Width = width;

            yDistancePerMove = distancePerMove;
        }

        public Point GetLocation()
        {
            return new Point(xLocation(), yLocation());
        }

        public Size GetSize()
        {
            return new Size(width, height);
        }

        public void ResetLocation()
        {
            Canvas.SetLeft(ellipse, DEFAULT_X_LOCATION);
            Canvas.SetTop(ellipse, DEFAULT_Y_LOCATION);
        }

        public double xLocation()
        {
            return Canvas.GetLeft(ellipse);
        }

        public double yLocation()
        {
            return Canvas.GetTop(ellipse);
        }
    }
}

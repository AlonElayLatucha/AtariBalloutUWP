using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace AtariBallout.AnimatedObjects
{
    public class RectangleObject : AnimatedObject
    {
        DispatcherTimer blink_timer = new DispatcherTimer();

        AcrylicBrush originalFill;

        private int blinkTimer = 0;

        public readonly int DEFAULT_Y_LOCATION = 590;
        public readonly int DEFAULT_X_LOCATION = 341;

        public Rectangle rectangle;

        public RectangleObject(int width, int height, int distancePerMove, Canvas canvas, Rectangle rectangle)
            : base(
            width: 200, height: 20, distancePerMove, canvas)
        {
            this.rectangle = rectangle;
            this.height = height;
            this.width = width;

            // Applying Settings
            rectangle.Width = width;
            rectangle.Height = height;

            blink_timer.Interval = TimeSpan.FromMilliseconds(500);
            blink_timer.Tick += Timer_Tick;

            originalFill = (AcrylicBrush)rectangle.Fill;
        }

        public Point GetLocation()
        {
            Point p = new Point(xLocation(), yLocation());
            Debug.WriteLine($"\n\n\n\n\n\nRECT'S LOCATION: {p}\n\n\n\n\n\n");
            return p;
        }

        public void ResetLocation()
        {
            Canvas.SetTop(rectangle, DEFAULT_Y_LOCATION);
            Canvas.SetLeft(rectangle, DEFAULT_X_LOCATION);
        }

        public Size GetSize()
        {
            return new Size(width, height);
        }

        public double xLocation()
        {
            return Canvas.GetLeft(rectangle);
        }

        public double yLocation()
        {
            return Canvas.GetTop(rectangle);
        }

        public void BlinkWhite()
        {
            if (App.Current.RequestedTheme == ApplicationTheme.Light)
                rectangle.Fill = new SolidColorBrush(Colors.Black);
            else if (App.Current.RequestedTheme == ApplicationTheme.Dark)
                rectangle.Fill = new SolidColorBrush(Colors.White);
            blink_timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            blinkTimer += 1;
            if (blinkTimer == 1)
            {
                rectangle.Fill = originalFill;
                this.blink_timer.Stop();
                blinkTimer = 0;
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AtariBallout.AnimatedObjects
{
    public class AnimatedObject
    {
        public int width;
        public int height;
        public double distancePerMove;
        public Canvas canvas;

        protected AnimatedObject(int width, int height, double distancePerMove, Canvas canvas)
        {
            this.distancePerMove = distancePerMove;
            this.canvas = canvas;
        }
    }
}

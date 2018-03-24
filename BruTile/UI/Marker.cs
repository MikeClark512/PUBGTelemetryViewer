using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BruTile.UI
{
    public class Marker
    {
        public bool Visible { get; private set; }
        
        private double x;
        public double X { 
            get { return x;}
        }
        private double y;
        public double Y
        {
            get
            {
                return y;
            }
        }

        public object UIElement { get; set; }

        public System.Windows.Point[] Points { get; set; }
        
        public int ImageIndex { get; private set; }
        public string Text { get; private set; }
        public string Description { get; private set; }
        public int ZIndex { get; private set; }
        public string Type { get; private set; }

        public Marker(double x, double y, bool visible, int imageIndex,string type, string subtype, string text, string description, int zIndex)
        {
            double xt = ((x / 32768) * 1312);
            double yt = ((y / 32768) * 1312);
            ////if (x < 0)
            ////    x = 0;


            ////if (y < 0)
            ////    y = 0;
            ImageIndex = imageIndex;
            Text = text;
            Description = description;
            this.x = Math.Round(xt);
            this.y = Math.Round(-yt);
            Visible = visible;
            Type = type;
            ZIndex = zIndex;
        }
    }
}

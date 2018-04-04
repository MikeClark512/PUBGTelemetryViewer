using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BruTile.UI
{
    public class Marker
    {
        public enum Eventtype
        {
            Marker,
            Plane_leaving,
            Parachute_leaving,
            Boosting,
            Heal,

        };

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
        public Eventtype Type { get; private set; }
        public DateTimeOffset timeOffset { get; private set; }
        public int ElapsedTime { get; private set; }


        public Marker(double x, double y, bool visible, int imageIndex, DateTimeOffset time2 , string text, string description, int zIndex, Eventtype type = Eventtype.Marker)
        {
            double xt = ((x / 50) * 32);
            double yt = ((y / 50) * 32);

            ImageIndex = imageIndex;
            Text = text;
            Description = description;
            this.x = Math.Round(xt);
            this.y = Math.Round(-yt);
            Visible = visible;
            Type = type;

            timeOffset = time2;
            ZIndex = zIndex;
        }

        public Marker(double x, double y, bool visible, int imageIndex, DateTimeOffset time2, int time, string text, string description, int zIndex, Eventtype type = Eventtype.Marker)
        {
            double xt = ((x / 50) * 32);
            double yt = ((y / 50) * 32);

            ImageIndex = imageIndex;
            Text = text;
            Description = description;
            this.x = Math.Round(xt);
            this.y = Math.Round(-yt);
            Visible = visible;
            Type = type;
            this.ElapsedTime = time;
            timeOffset = time2;
            ZIndex = zIndex;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BruTile.UI
{
    public class Ellipse
    {
        public enum ZoneType
        {
            Blue_Zone,
            Red_Zone,
        };


        public bool Visible { get; private set; }

        private double x;
        public double X
        {
            get { return x; }
        }
        private double y;
        public double Y
        {
            get { return y; }
        }

        private double radius;
        public double Radius
        {
            get { return radius; }
        }
        public object UIElement { get; set; }

        public System.Windows.Point[] Points { get; set; }
        public int ZIndex { get; private set; }
        public ZoneType Type { get; private set; }
        public string Subtype { get; private set; }
        public int time { get; private set; }

        public Ellipse(int time, double x, double y, double radius, bool visible, ZoneType type, int zIndex)
        {
            double xt = ((x / 50) * 32);
            double yt = ((x / 50) * 32);
            this.x = Math.Round(xt);
            this.y = Math.Round(-yt);
            this.radius = Math.Round(((radius / 50) * 32));
            Visible = visible;
            Type = type;
            this.time = time;
            ZIndex = zIndex;
        }
    }
}

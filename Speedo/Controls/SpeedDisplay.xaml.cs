﻿// new

using System;
using System.Device.Location;

namespace Speedo.Controls
{
    public partial class SpeedDisplay : SpeedControl
    {
        private int dataCount;

        private double average;
        public double Average
        {
            get { return average; }
            set { SetProperty( ref average, value ); }
        }

        private double max;
        public double Max
        {
            get { return max; }
            set { SetProperty( ref max, value ); }
        }

        private double current;
        public double Current
        {
            get { return current; }
            set { SetProperty( ref current, value ); }
        }

        public SpeedDisplay()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        protected override void ChangeUnits( double factor )
        {
            Average *= factor;
            Max *= factor;
            Current *= factor;
        }

        protected override void ChangeSpeed( double speed, GeoCoordinate position )
        {
            int factoredSpeed = (int) Math.Round( speed * SpeedUtils.GetFactor( Unit ) );
            Average = ( Average * dataCount + factoredSpeed ) / ( dataCount + 1 );
            Max = Math.Max( Max, factoredSpeed );
            Current = factoredSpeed;

            dataCount++;
        }

        protected override void Clear()
        {
            dataCount = 0;
            Average = Max = Current = 0;
        }
    }
}
﻿namespace DotScreencap
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Represents the screen region class.
    /// </summary>
    public class ScreenRegion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenRegion"/> class.
        /// Used to select a screen region for animations, screenshots and videos.
        /// Use: SetUpperLeftCorner(), SetLowerRightCorner().
        /// </summary>
        public ScreenRegion(Point upperleft, Point lowerright)
        {
            this.UpperLeftCorner = upperleft;
            this.LowerRightCorner = lowerright;
        }

        /// <summary>
        /// Gets or sets the upper left corner position.
        /// </summary>
        public Point UpperLeftCorner
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lower right corner position.
        /// </summary>
        public Point LowerRightCorner
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the upper left corner to the current mouse position.
        /// </summary>
        public void SetUpperLeftCorner()
        {
            this.UpperLeftCorner = Control.MousePosition;
        }

        /// <summary>
        /// Sets the lower right corner to the current mouse position.
        /// </summary>
        public void SetLowerRightCorner()
        {
            this.LowerRightCorner = Control.MousePosition;
        }
    }
}
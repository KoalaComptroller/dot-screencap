﻿namespace DotScreencap
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents the ScreenCapture class.
    /// </summary>
    public sealed class ScreenCapture
    {
        private AnimationCreator animationCreator;
        private Bitmap screenBitmap;
        private BitmapImage screenBitmapImage;
        private PictureCreator pictureCreator;
        private Rectangle screenSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenCapture"/> class.
        /// </summary>
        public ScreenCapture()
        {
            this.ScreenSize = Screen.PrimaryScreen.Bounds;
            this.animationCreator = new AnimationCreator(this);
        }

        /// <summary>
        /// Is fired after a screenshot is taken.
        /// </summary>
        public event EventHandler<ScreenCaptureOnScreenshotTakenEventArgs> OnScreenshotTaken;

        /// <summary>
        /// Is fired after an animation is created.
        /// </summary>
        public event EventHandler<ScreenCaptureOnAnimationCreatedEventArgs> OnAnimationCreated;

        /// <summary>
        /// Gets the animation creator.
        /// Used to subscribe to the OnOutOfMemoryExceptionThrown event.
        /// </summary>
        public AnimationCreator AnimationCreator
        {
            get
            {
                return this.animationCreator;
            }
        }

        /// <summary>
        /// Gets or sets the screen bitmap.
        /// </summary>
        public Bitmap ScreenBitmap
        {
            get
            {
                return this.screenBitmap;
            }
            set
            {
                this.screenBitmap = value;
            }
        }

        /// <summary>
        /// Gets the ScreenBitmapImage.
        /// Can be used for WPF ImageBox.
        /// </summary>
        public BitmapImage ScreenBitmapImage
        {
            get
            {
                return this.screenBitmapImage;
            }

            private set
            {
                this.screenBitmapImage = value;
            }
        }

        /// <summary>
        /// Gets the ScreenSize.
        /// </summary>
        public Rectangle ScreenSize
        {
            get
            {
                return this.screenSize;
            }

            private set
            {
                this.screenSize = value;
            }
        }

        /// <summary>
        /// Saves a *.jpg to the execution folder.
        /// </summary>
        /// <param name="filename">Possibly specified filename.</param>
        public void TakeScreenshot(params string[] filename)
        {
            this.GetBitmapOfScreen();
            this.ConvertBitmapToBitmapImage();

            if (this.ScreenBitmapImage == null)
            {
                throw new NullReferenceException();
            }

            if (filename.Length < 1)
            {
                this.pictureCreator = new PictureCreator(this.ScreenBitmapImage);
            }
            else
            {
                this.pictureCreator = new PictureCreator(this.ScreenBitmapImage, filename[0]);
            }

            this.pictureCreator.SaveScreenshotAsJPG();
            this.FireOnScreenshotTaken();
        }

        /// <summary>
        /// Saves an animated *.gif to the execution folder.
        /// </summary>
        /// <param name="frames">Amount of frames that will be captured.</param>
        /// <param name="wait">Time in ms between each frame.</param>
        public void CreateGIF(int frames, int wait)
        {
            this.animationCreator.SaveAnimationAsGif(frames, wait);
            this.FireOnAnimationCreated();
        }

        /// <summary>
        /// Creates a Bitmap of the users screen.
        /// </summary>
        public void GetBitmapOfScreen()
        {
            if (this.ScreenSize == null)
            {
                throw new NullReferenceException();
            }

            this.screenBitmap = new Bitmap(this.screenSize.Width, this.screenSize.Height);
            Graphics screen = Graphics.FromImage(this.screenBitmap);
            screen.CopyFromScreen(0, 0, 0, 0, new Size(this.screenSize.Width, this.screenSize.Height));
        }

        /// <summary>
        /// Converts a Bitmap to a BitmapImage using a memory stream.
        /// </summary>
        private void ConvertBitmapToBitmapImage()
        {
            if (this.screenBitmap == null)
            {
                throw new NullReferenceException();
            }

            MemoryStream ms = new MemoryStream();
            this.screenBitmap.Save(ms, ImageFormat.Bmp);
            this.ScreenBitmapImage = new BitmapImage();
            this.ScreenBitmapImage.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            this.ScreenBitmapImage.StreamSource = ms;
            this.ScreenBitmapImage.EndInit();
        }

        private void FireOnScreenshotTaken()
        {
            this.OnScreenshotTaken(this, new ScreenCaptureOnScreenshotTakenEventArgs(this, this.pictureCreator));
        }

        private void FireOnAnimationCreated()
        {
            this.OnAnimationCreated(this, new ScreenCaptureOnAnimationCreatedEventArgs(this, this.AnimationCreator));
        }
    }
}

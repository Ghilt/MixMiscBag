using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandpiles3D
{
    public partial class Sandpiles3DRender : Form
    {
        private const int ZOOM_ENLARGE = 2000;

        private int[,] space;
        private Presenter presenter;
        private Bitmap bmp;

        private static readonly Dictionary<int, Color> heightMap = new Dictionary<int, Color>{
            { 0, Color.Yellow},
            { 1, Color.Brown},
            { 2, Color.AliceBlue},
            { 3, Color.DarkSeaGreen},
            { 4, Color.Red},
            { 5, Color.Violet},
            { 6, Color.Olive},
            { 7, Color.White}
        };
        public Sandpiles3DRender(Presenter presenter)
        {
            this.presenter = presenter;
            space = new int[1, 1];
            InitializeComponent();
            bmp = new Bitmap(1, 1);
        }

        internal void updatePerformanceCounter(String text)
        {
            textBoxPerfromance.Text = text;
        }

        internal void SetIterateButtonEnabled(bool enabled)
        {
            iterateButton.Enabled = enabled;
        }

        internal void SetIterationCounter(string iterationCounter)
        {
            iterationCounterTextBox.Text = iterationCounter;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            for (int x = 0; x < space.GetLength(0); x++)
            {
                for (int y = 0; y < space.GetLength(1); y++)
                {
                    if (space[x, y] >= 7)
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, heightMap[space[x, y]]);
                    }
                }
            }
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

            }
            //renderArea.Image = ResizeImage(bmp, ZOOM_ENLARGE, ZOOM_ENLARGE);
            renderArea.SizeMode = PictureBoxSizeMode.StretchImage;
            renderArea.Image = bmp;
        }

        internal void ToggleStartToggleButton(String text)
        {
            startButton.Text = text;
        }

        public void DrawSandpiles(int[,] space)
        {
            if (bmp.Height == 1)
            {
                bmp.Dispose();
                bmp = new Bitmap(space.GetLength(0), space.GetLength(1));
            }
            this.space = space;
            Refresh();
        }

        private void iterate_Button_Click(object sender, EventArgs e)
        {
            presenter.OnIterateButton();
        }

        public static Bitmap ResizeImage(Bitmap image, int width, int height) // leaky/ineffiecient
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        private void StartToggleClicked(object sender, EventArgs e)
        {
            presenter.OnStartToggleClicked(((Button)sender).Text);
        }
    }
}

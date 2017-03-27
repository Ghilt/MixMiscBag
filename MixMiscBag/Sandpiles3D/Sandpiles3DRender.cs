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

        internal void ToggleStartToggleButton(String text)
        {
            startButton.Text = text;
        }

        public void DrawSandpiles(int[,] space)
        {

            if (bmp != null)
            {
                bmp.Dispose();
            }

            bmp = new Bitmap(space.GetLength(0), space.GetLength(1));

            this.space = space;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
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
            int picSize = renderArea.Width > renderArea.Height ? renderArea.Width : renderArea.Height;
            bmp = ResizeImage(bmp, picSize, picSize);
            renderArea.Image = bmp;
            Refresh();
        }

        public void DrawSandpiles(Color[,] projection)
        {

            if (bmp != null)
            {
                bmp.Dispose();
            }

            bmp = new Bitmap(projection.GetLength(0), projection.GetLength(1));
            bmp.SetResolution(projection.GetLength(0), projection.GetLength(1));

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    bmp.SetPixel(x, y, projection[x, y]);
                }
            }
            int picSize = renderArea.Width > renderArea.Height ? renderArea.Width : renderArea.Height;
            bmp = ResizeImage(bmp, picSize, picSize);
            renderArea.Image = bmp;
            Refresh();
        }

        private void iterate_Button_Click(object sender, EventArgs e)
        {
            presenter.OnIterateButton();
        }

        public static Bitmap ResizeImage(Bitmap image, int width, int height)
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
            image.Dispose();
            return destImage;
        }

        internal void ShowDialog(string v)
        {
            //print error in statusfield somewhere
        }

        private void StartToggleClicked(object sender, EventArgs e)
        {
            presenter.OnStartToggleClicked(((Button)sender).Text);
        }

        private void StartStateListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.OnSelectStartFromList(((ListBox) sender).SelectedItem.ToString());
        }

        private void sizeTextBox_Leave(object sender, EventArgs e)
        {
            presenter.ChangeSizeOfModel(sizeXTextBox.Text, sizeYTextBox.Text, sizeZTextBox.Text);
        }
    }
}

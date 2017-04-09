using Sandpiles3DWPF.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sandpiles3DWPF.ViewModel
{
    //http://stackoverflow.com/questions/1315621/implementing-inotifypropertychanged-does-a-better-way-exist/1316566#1316566
    class SandpilesViewModel : INotifyPropertyChanged
    {

        private SandpilesCalculator model;
        private BackgroundSandpilesWorker worker;

        private string sizeX;
        public string SizeX
        {
            get { return sizeX; }
            set { sizeX = value; OnPropertyChanged(); }
        }

        private string sizeY;
        public string SizeY
        {
            get { return sizeY; }
            set { sizeY = value; OnPropertyChanged(); }
        }

        private string sizeZ;
        public string SizeZ
        {
            get { return sizeZ; }
            set { sizeZ = value; OnPropertyChanged(); }
        }

        private string iterationDuration;
        public string IterationDuration
        {
            get { return iterationDuration; }
            set { iterationDuration = value; OnPropertyChanged(); }
        }

        private WriteableBitmap image2D;
        public WriteableBitmap Image2D
        {
            get {
                if (image2D == null) 
                {
                    image2D = BitmapFactory.New(model.width, model.height);
                }
                return image2D;
            }
            set { image2D = value; OnPropertyChanged(); }
        }

        private ICommand startIterationCommand; 
        public ICommand StartIterationCommand
        {
            get
            {
                if (startIterationCommand == null) //I find this very ugly
                {
                    startIterationCommand = new RelayCommand(p => this.StartIterate(), p => true );
                }
                return startIterationCommand;
            }
        }


        private void StartIterate()
        {
            worker.StartIteration();
        }


        public void LoadSandpiles()
        {
            int width = 101;
            int height = 101;
            int depth = 31;
            model = new SandpilesCalculator(ModelPropertyChanged, width, height, depth);
            worker = new BackgroundSandpilesWorker(ModelPropertyChanged, model);
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e) // TODO not sure if this is correct, may be missing all the value of mvvm. Something in me wanna connect the bound fields more directly to the fields in the model, with no backing fields for the accessors
        {
            string triggerMethod = e.PropertyName;
            if (triggerMethod == nameof(SandpilesCalculator.AllPropertiesChanged))
            {
                SandpilesCalculator m = sender as SandpilesCalculator;
                SizeX = "" + m.width;
                SizeY = "" + m.height;
                SizeZ = "" + m.depth;
            }
            else if (triggerMethod == nameof(SandpilesCalculator.Iterate))
            {
                SandpilesCalculator m = sender as SandpilesCalculator;
                Application.Current?.Dispatcher.Invoke(() => DrawSandpiles(m)); // run on UI-thread
            }
            else if (triggerMethod == nameof(BackgroundSandpilesWorker.IterationFinished))
            {
                BackgroundSandpilesWorker w = sender as BackgroundSandpilesWorker;
                IterationDuration = "" + w.lastIterationDuration;
            }
        }

        public void DrawSandpiles(SandpilesCalculator m)
        {
            Color[,] projection = m.Get2DProjection().dim2Projection;
            using (image2D.GetBitmapContext())
            {
                for (int x = 0; x < image2D.Width; x++)
                {
                    for (int y = 0; y < image2D.Height; y++)
                    {
                        Image2D.SetPixel(x, y, projection[x, y]);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

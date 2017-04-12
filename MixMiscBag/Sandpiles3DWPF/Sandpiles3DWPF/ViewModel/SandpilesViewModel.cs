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

        private int sizeX;
        public string SizeX
        {
            get { return "" + sizeX; }
            set { sizeX = Int32.Parse(value); OnPropertyChanged(); }
        }

        private int sizeY;
        public string SizeY
        {
            get { return "" + sizeY; }
            set { sizeY = Int32.Parse(value); OnPropertyChanged(); }
        }

        private int sizeZ;
        public string SizeZ
        {
            get { return "" + sizeZ; }
            set { sizeZ = Int32.Parse(value); OnPropertyChanged(); }
        }

        private ICommand setSizeCommand;
        public ICommand SetSizeCommand
        {
            get
            {
                return setSizeCommand = setSizeCommand ?? new RelayCommand(p => ReSize(sizeX, sizeY, sizeZ), p => true);
            }
        }

        private bool xCoordEnabled = true;
        public bool XCoordEnabled
        {
            get { return xCoordEnabled; }
            set { xCoordEnabled = value; OnPropertyChanged(); }
        }
        private bool yCoordEnabled = true;
        public bool YCoordEnabled
        {
            get { return yCoordEnabled; }
            set { yCoordEnabled = value; OnPropertyChanged(); }
        }
        private bool zCoordEnabled = true;
        public bool ZCoordEnabled
        {
            get { return zCoordEnabled; }
            set { zCoordEnabled = value; OnPropertyChanged(); }
        }

        private int setXCoord;
        public string SetXCoord
        {
            get { return "" + setXCoord; }
            set { setXCoord = CapStringNumber(value, sizeX); OnPropertyChanged(); }
        }

        private int setYCoord;
        public string SetYCoord
        {
            get { return "" + setYCoord; }
            set { setYCoord = CapStringNumber(value, sizeY); OnPropertyChanged(); }
        }

        private int setZCoord;
        public string SetZCoord
        {
            get { return "" + setZCoord; }
            set { setZCoord = CapStringNumber(value, sizeZ); OnPropertyChanged(); }
        }

        private int setCoordValue;
        public string SetCoordValue
        {
            get { return "" + setCoordValue; }
            set { setCoordValue = Int32.Parse(value); OnPropertyChanged(); }
        }

        private ICommand setCoordValueCommand;
        public ICommand SetCoordValueCommand
        {
            get
            {
                return setCoordValueCommand = setCoordValueCommand ?? new RelayCommand(p => model.FillValues(
                    new bool[]{xCoordEnabled,yCoordEnabled,zCoordEnabled},
                    new int[]{setXCoord, setYCoord, setZCoord}, 
                    setCoordValue), p => true);
            }
        }

        private WriteableBitmap image2D;
        public WriteableBitmap Image2D
        {
            get
            {
                return image2D = image2D ?? BitmapFactory.New(model.width, model.height);
            }
            set { image2D = value; OnPropertyChanged(); }
        }

        private int iterationDuration;
        public string IterationDuration
        {
            get { return "" + iterationDuration; }
            set { iterationDuration = Int32.Parse(value); OnPropertyChanged(); }
        }

        private int numberOfIterations;
        public string NumberOfIterations
        {
            get { return "" + numberOfIterations; }
            set { numberOfIterations = Int32.Parse(value); OnPropertyChanged(); }
        }

        private ICommand startIterationCommand;
        public ICommand StartIterationCommand
        {
            get
            {
                return startIterationCommand = startIterationCommand ?? new RelayCommand(p => { worker.StartIteration(); IsIterating = true; }, p => true);
            }
        }

        private ICommand stopIterationCommand;
        public ICommand StopIterationCommand
        {
            get
            {
                return stopIterationCommand = stopIterationCommand ?? new RelayCommand(p => worker.StopIteration(), p => true);
            }
        }

        private ICommand iterateOneCommand;
        public ICommand IterateOneCommand
        {
            get
            {
                return iterateOneCommand = iterateOneCommand ?? new RelayCommand(p => worker.Iterate(), p => true);
            }
        }

        private bool isIterating;
        public bool IsIterating
        {
            get { return isIterating; }
            set { isIterating = value; OnPropertyChanged(); }
        }

        public void LoadSandpiles(int width, int height, int depth)
        {
            model = new SandpilesCalculator(ModelPropertyChanged, width, height, depth);
            worker = new BackgroundSandpilesWorker(ModelPropertyChanged, model);
            
        }

        public void ReSize(int width, int height, int depth) // Find better way to reset to initial state
        {
            setSizeCommand = null;
            startIterationCommand = null;
            stopIterationCommand = null;
            setCoordValueCommand = null;
            iterateOneCommand = null;
            worker.StopIteration();
            worker.PropertyChanged -= ModelPropertyChanged;
            model.PropertyChanged -= ModelPropertyChanged;
            IsIterating = false;
            Image2D = BitmapFactory.New(width, height);
            model = new SandpilesCalculator(ModelPropertyChanged, width, height, depth);
            worker = new BackgroundSandpilesWorker(ModelPropertyChanged, model);

        }

        private int CapStringNumber(string number, int maxNumber)
        {
            return Int32.Parse(number) >= maxNumber ? maxNumber - 1 : Int32.Parse(number);
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e) // TODO not sure if this is correct, may be missing all the value of mvvm. Something in me wanna connect the bound fields more directly to the fields in the model, with no backing fields for the accessors
        {
            string triggerMethod = e.PropertyName;
            if (null != sender as SandpilesCalculator)
            {
                SandpilesCalculator m = sender as SandpilesCalculator;
                SizeX = "" + m.width;
                SizeY = "" + m.height;
                SizeZ = "" + m.depth;
            }
            else if (triggerMethod == BackgroundSandpilesWorker.PROPERTY_CHANGED_ITERATION_FINISHED)
            {
                BackgroundSandpilesWorker w = sender as BackgroundSandpilesWorker;
                IterationDuration = "" + w.lastIterationDuration;
                NumberOfIterations = "" + w.iterationData.iteration;
                if (Application.Current != null)
                {
                    Application.Current.Dispatcher.Invoke(() => DrawSandpiles(w.iterationData.dim2Projection)); // run on UI-thread
                }
            }
            else if (triggerMethod == BackgroundSandpilesWorker.PROPERTY_CHANGED_CONTINUOUS_ITERATION_STOPPED)
            {
                IsIterating = false;
            }
        }

        public void DrawSandpiles(Color[,] projection)
        {
            using (Image2D.GetBitmapContext())
            {
                for (int x = 0; x < Image2D.Width; x++)
                {
                    for (int y = 0; y < Image2D.Height; y++)
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

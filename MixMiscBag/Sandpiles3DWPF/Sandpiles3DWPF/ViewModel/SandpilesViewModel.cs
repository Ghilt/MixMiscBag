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

        #region Size control

        public ObservableField<string>[] SizeDim { get; set; }

        private ICommand setSizeCommand;
        public ICommand SetSizeCommand
        {
            get
            {
                return setSizeCommand = setSizeCommand ?? new RelayCommand(p => ReSize(SizeDim[0].ToInt(), SizeDim[1].ToInt(), SizeDim[2].ToInt()));
            }
        }

        #endregion //Size

        #region Quick menu control

        private ICommand[] quickMenuCommand; // make this data fill a data grid later google datagrid
        public ICommand[] QuickMenuCommand
        {
            get
            {
                return quickMenuCommand = quickMenuCommand ?? CreateQuickCommandList();
            }
        }

        private ICommand[] CreateQuickCommandList()
        {
            return new ICommand[]{
             new RelayCommand("Fill max",p => model.FillMax()),
             new RelayCommand("Fill 7",p => model.Fill(7)),
             new RelayCommand("Mid 7", p => model.SetPosition(model.getMidX(), model.getMidY(), model.getMidZ(), 7)),
             new RelayCommand("Mid 100", p => model.SetPosition(model.getMidX(), model.getMidY(), model.getMidZ(), 100)),
             new RelayCommand("RGB 7", p => {
                    model.FillValues(new bool[] { false, true, true }, new int[] { 0, (int)(model.height * 0.1), (int)(model.depth * 0.1) }, 7);
                    model.FillValues(new bool[] { false, true, true }, new int[] { 0, (int)(model.height * 0.5), (int)(model.depth * 0.5) }, 7);
                    model.FillValues(new bool[] { false, true, true }, new int[] { 0, (int)(model.height * 0.9), (int)(model.depth * 0.9) }, 7);
             }),
            new RelayCommand("Grid 7", p => {
                    model.FillValues(new bool[] { false, true, true }, new int[] { 0, (int)(model.height * 0.1), (int)(model.depth * 0.1) }, 7);
                    model.FillValues(new bool[] { false, true, true }, new int[] { 0, (int)(model.height * 0.9), (int)(model.depth * 0.3) }, 7);
                    model.FillValues(new bool[] { true, false, true }, new int[] { (int)(model.width * 0.9), 0, (int)(model.depth * 0.7) }, 7);
                    model.FillValues(new bool[] { true, false, true }, new int[] { (int)(model.width * 0.1), 0, (int)(model.depth * 0.9) }, 7);
             })};
        }

        #endregion //Quick

        #region Advanced settings control
        
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
            set { setXCoord = CapStringNumber(value, SizeDim[0].ToInt()); OnPropertyChanged(); }
        }

        private int setYCoord;
        public string SetYCoord
        {
            get { return "" + setYCoord; }
            set { setYCoord = CapStringNumber(value, SizeDim[1].ToInt()); OnPropertyChanged(); }
        }

        private int setZCoord;
        public string SetZCoord
        {
            get { return "" + setZCoord; }
            set { setZCoord = CapStringNumber(value, SizeDim[2].ToInt()); OnPropertyChanged(); }
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
                    new bool[] { xCoordEnabled, yCoordEnabled, zCoordEnabled },
                    new int[] { setXCoord, setYCoord, setZCoord },
                    setCoordValue));
            }
        }

        #endregion //Advanced

        #region Iteration control
        
        private ICommand startIterationCommand;
        public ICommand StartIterationCommand
        {
            get
            {
                return startIterationCommand = startIterationCommand ?? new RelayCommand(p => { worker.StartIteration(); IsIterating = true; });
            }
        }

        private ICommand stopIterationCommand;
        public ICommand StopIterationCommand
        {
            get
            {
                return stopIterationCommand = stopIterationCommand ?? new RelayCommand(p => worker.StopIteration());
            }
        }

        private ICommand iterateOneCommand;
        public ICommand IterateOneCommand
        {
            get
            {
                return iterateOneCommand = iterateOneCommand ?? new RelayCommand(p => worker.Iterate());
            }
        }

        private bool isIterating;
        public bool IsIterating
        {
            get { return isIterating; }
            set { isIterating = value; OnPropertyChanged(); }
        }

        #endregion // iteration

        #region Render control

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

        #endregion // render

        public SandpilesViewModel()
        {
            SizeDim = ObservableField<string>.CreateFields(3);
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
                SizeDim[0].Value = "" + m.width;
                SizeDim[1].Value = "" + m.height;
                SizeDim[2].Value = "" + m.depth;
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

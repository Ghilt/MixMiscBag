using Sandpiles3DWPF.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sandpiles3DWPF.ViewModel
{
    //http://stackoverflow.com/questions/1315621/implementing-inotifypropertychanged-does-a-better-way-exist/1316566#1316566
    class SandpilesViewModel : INotifyPropertyChanged
    {

        private SandpilesCalculator model;

        private string sizeX;
        public string SizeX
        {
            get{ return sizeX; }
            set{ sizeX = value; OnPropertyChanged(); }
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


        public void LoadSandpiles()
        {
            int width = 101;
            int height = 101;
            int depth = 31;
            model = new SandpilesCalculator(ModelPropertyChanged, width, height, depth);
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e) // TODO not sure if this is correct, may be missing all the value of mvvm. Something in me wanna connect the bound fields more directly to the fields in the model, with no backing fields for the accessors
        {
            if (e.PropertyName == nameof(SandpilesCalculator.AllPropertiesChanged) )
            {
                SandpilesCalculator m = sender as SandpilesCalculator;
                sizeX = "" + m.width;
                sizeY = "" + m.height;
                sizeZ = "" + m.depth;
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

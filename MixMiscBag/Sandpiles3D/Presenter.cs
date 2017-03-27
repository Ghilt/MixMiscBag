using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandpiles3D
{

    // http://people.reed.edu/~davidp/web_sandpiles/
    public class Presenter
    {

        private static readonly Dictionary<string, Action<SandpilesCalculator>> startStateMap = new Dictionary<string, Action<SandpilesCalculator>>{
            { "Fill 7", m => m.Fill(7)},
            { "Mid 7", m => m.SetPosition(m.getMidX(), m.getMidY(), m.getMidZ(), 7)},
            { "TopLeftBack 7", m => m.SetPosition(0,0,0, 7)},
            { "BottomRightFront 7", m => m.SetPosition(m.width - 1, m.height - 1, m.depth - 1, 7)},
            { "Fill 6", m => m.FillMax()}
        };

        private long lastIterationDuration;
        private SandpilesCalculator model;
        private Sandpiles3DRender view;
        BackgroundWorker bw = new BackgroundWorker();

        public Presenter()
        {
            int size = 101;
            model = new SandpilesCalculator(size, size, 21);
            //model.Fill(7);
            //model.SetPosition(model.getMidX(), model.getMidY(), model.getMidZ(), 100);
            //model.SetPosition(0, 0, model.depth - 1, 20);
            //model.SetPosition(model.width - 1, model.height - 1, 0, 20);

            //model.SetPosition(0, 0, 0, 8);
            //model.SetPosition(model.width - 1, 0, model.depth - 1, 8);
            //model.SetPosition(model.width - 1, model.height - 1, 0, 9);
            //model.SetPosition(model.width - 1, model.height - 1, model.depth - 1, 8);
            //model.SetPosition(6, 46, 6, 9);
            //model.SetPosition(46, 6, 6, 9);
            //model.SetPosition(model.width - 1, model.height - 1, model.depth - 1, 11);
            //model.SetPosition(0, 0, 0, 7);
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += PerformIteration;
            bw.RunWorkerCompleted += IterationFinished;
        }

        public void OnIterateButton()
        {
            view.SetIterateButtonEnabled(false);
            bw.RunWorkerAsync();
        }

        internal void IterationFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            view.updatePerformanceCounter(lastIterationDuration + "");
            //view.DrawSandpiles(model.GetCrossSection(CROSS_SECTION_TARGET / 4, true, false, false)); // Do this on background thread as well
            view.DrawSandpiles(model.Get2DProjection());
            view.SetIterateButtonEnabled(true);
            view.SetIterationCounter(model.iterationCounter + "");
        }

        internal void ContinousIteration(object sender, RunWorkerCompletedEventArgs e)
        {
            view.SetIterateButtonEnabled(true);
            bw.RunWorkerAsync();
        }

        internal void PerformIteration(object sender, DoWorkEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            model.Iterate();
            watch.Stop();
            lastIterationDuration = watch.ElapsedMilliseconds;
        }

        internal void SetView(Sandpiles3DRender view)
        {
            this.view = view;
        }

        internal void OnStartToggleClicked(string text)
        {
            if (text.Equals("Start"))
            {
                bw.RunWorkerAsync();
                view.ToggleStartToggleButton("Stop");
                bw.RunWorkerCompleted += ContinousIteration;
            }
            else
            {
                bw.RunWorkerCompleted -= ContinousIteration;
                view.ToggleStartToggleButton("Start");
            }
        }

        internal void OnSelectStartFromList(string selection)
        {
            startStateMap[selection](model);
        }

        internal void ChangeSizeOfModel(string xSizeString, string ySizeString, string zSizeString)
        {
            int xSize;
            int ySize;
            int zSize;
            bool validInput = Int32.TryParse(xSizeString, out xSize);
            validInput = Int32.TryParse(ySizeString, out ySize) && validInput;
            validInput = Int32.TryParse(zSizeString, out zSize) && validInput;
            if (validInput)
            {
                bw.CancelAsync();
                model = new SandpilesCalculator(xSize, ySize, zSize);
            }
            else
            {
                view.ShowDialog("Size not parseable to integer");
            }
        }
    }
}
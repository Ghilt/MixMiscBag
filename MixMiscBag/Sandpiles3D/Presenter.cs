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
        ModelBackgroundWorker bw;

        public Presenter()
        {
            int size = 101;
            model = new SandpilesCalculator(size, size, 71);
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

        }

        public void OnIterateButton()
        {
            bw = CreateNewWorker();
            view.SetIterateButtonEnabled(false);
            bw.DoWork += PerformIteration;
            bw.RunWorkerCompleted += (x, y) => view.SetIterateButtonEnabled(true);
            bw.RunWorkerCompleted += IterationFinished;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync();
        }

        private ModelBackgroundWorker CreateNewWorker()
        {
            if (bw != null)
            {
                bw.CancelAsync();
                bw.Dispose();
            }
            ModelBackgroundWorker w = new ModelBackgroundWorker(model);
            w.WorkerSupportsCancellation = true;
            return w;
        }

        internal void OnStartToggleClicked(string text)
        {
            if (text.Equals("Start"))
            {
                bw = CreateNewWorker();
                view.SetIterateButtonEnabled(false);
                bw.DoWork += PerformIteration;
                bw.RunWorkerCompleted += IterationFinished;
                bw.RunWorkerCompleted += ContinousIteration;
                bw.RunWorkerAsync();
                view.ToggleStartToggleButton("Stop");
            }
            else
            {
                view.SetIterateButtonEnabled(true);
                bw.CancelAsyncModel();
                view.ToggleStartToggleButton("Start");
            }
        }

        internal void IterationFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            view.updatePerformanceCounter(lastIterationDuration + "");
            //view.DrawSandpiles(model.GetCrossSection(CROSS_SECTION_TARGET / 4, true, false, false)); // Do this on background thread as well
            view.DrawSandpiles(model.Get2DProjection());
            view.SetIterationCounter(model.iterationCounter + "");
        }

        internal void ContinousIteration(object sender, RunWorkerCompletedEventArgs e)
        {
            ModelBackgroundWorker worker = (ModelBackgroundWorker)sender;
            worker.RunWorkerAsync();
        }

        internal void PerformIteration(object sender, DoWorkEventArgs e)
        {
            ModelBackgroundWorker worker = (ModelBackgroundWorker)sender;
            if (worker.IsCancelled())
            {
                worker.RunWorkerCompleted -= IterationFinished;
                worker.RunWorkerCompleted -= ContinousIteration;
                e.Cancel = true;
            }
            else
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                worker.model.Iterate();
                watch.Stop();
                lastIterationDuration = watch.ElapsedMilliseconds;
            }

        }

        internal void SetView(Sandpiles3DRender view)
        {
            this.view = view;
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
                bw.CancelAsyncModel();
                model = new SandpilesCalculator(xSize, ySize, zSize);
            }
            else
            {
                view.ShowDialog("Size not parseable to integer");
            }
        }
    }
}
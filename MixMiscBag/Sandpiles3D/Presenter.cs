using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandpiles3D
{

    //inspiration http://people.reed.edu/~davidp/web_sandpiles/
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
            bw.DoWork += PerformSingleIteration;
            bw.RunWorkerCompleted += (x, y) => view.SetIterateButtonEnabled(true);
            bw.RunWorkerCompleted += IterationFinished;
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
                bw.WorkerReportsProgress = true;
                view.SetIterateButtonEnabled(false);
                bw.DoWork += PerformContinousIteration;
                bw.ProgressChanged += IterationFinished;
                bw.RunWorkerAsync();
                view.ToggleStartToggleButton("Stop");
            }
            else
            {
                view.SetIterateButtonEnabled(true);
                bw.CancelAsync();
                view.ToggleStartToggleButton("Start");
            }
        }

        internal void IterationFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            SandPilesIterationData result = e.Result as SandPilesIterationData;
            UpdateUiWithModelData(result);
        }

        internal void IterationFinished(object sender, ProgressChangedEventArgs e)
        {
            SandPilesIterationData state = e.UserState as SandPilesIterationData;
            UpdateUiWithModelData(state);
        }

        private void UpdateUiWithModelData(SandPilesIterationData d)
        {
            view.updatePerformanceCounter(lastIterationDuration + "");
            //view.DrawSandpiles(model.GetCrossSection(CROSS_SECTION_TARGET / 4, true, false, false)); // Do this on background thread as well
            view.DrawSandpiles(d.dim2Projection);
            view.SetIterationCounter(d.iteration + ""); // create data container with iteration + duration in it
        }

        internal void PerformContinousIteration(object sender, DoWorkEventArgs e)
        {
            ModelBackgroundWorker worker = (ModelBackgroundWorker)sender;

            while (!worker.CancellationPending)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                worker.model.Iterate();
                watch.Stop();
                lastIterationDuration = watch.ElapsedMilliseconds;
                worker.ReportProgress(0, worker.model.Get2DProjection());
            }   
        }

        internal void PerformSingleIteration(object sender, DoWorkEventArgs e)
        {
            ModelBackgroundWorker worker = (ModelBackgroundWorker)sender;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            worker.model.Iterate();
            watch.Stop();
            lastIterationDuration = watch.ElapsedMilliseconds;
            e.Result = worker.model.Get2DProjection();
        }

        internal void Initialize(Sandpiles3DRender view)
        {
            this.view = view;
            view.UpdateModelSizeTexts(model.width, model.height, model.depth);
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
                if (bw != null) bw.CancelAsync();
                model = new SandpilesCalculator(xSize, ySize, zSize);
            }
            else
            {
                view.ShowDialog("Size not parseable to integer");
            }
        }
    }
}
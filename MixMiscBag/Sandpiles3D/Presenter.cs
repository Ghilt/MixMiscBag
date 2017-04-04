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
        public enum VisualizationMode
        {
            Flatten,
            CrossSection,
            ThreeDimensions
        }
        private static readonly Dictionary<string, Action<SandpilesCalculator>> startStateMap = new Dictionary<string, Action<SandpilesCalculator>>{
            { SanpileStrings.quick_access_fill_6, m => m.FillMax()},
            { SanpileStrings.quick_access_fill_7, m => m.Fill(7)},
            { SanpileStrings.quick_access_mid_7, m => m.SetPosition(m.getMidX(), m.getMidY(), m.getMidZ(), 7)},
            { SanpileStrings.quick_access_mid_100, m => m.SetPosition(m.getMidX(), m.getMidY(), m.getMidZ(), 100)},
            { SanpileStrings.quick_access_top_left_back_7, m => m.SetPosition(0,0,0, 7)},
            { SanpileStrings.quick_access_bottom_right_front_7, m => m.SetPosition(m.width - 1, m.height - 1, m.depth - 1, 7)},
        };

        private VisualizationMode mode = VisualizationMode.Flatten;
        private long lastIterationDuration;
        private SandpilesCalculator model;
        private Sandpiles3DRender view;
        ModelBackgroundWorker bw;

        public Presenter()
        {
            int size = 101;
            model = new SandpilesCalculator(size, size, 71);
            //model.Fill(7);
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
            switch (mode)
            {
                case VisualizationMode.Flatten:
                    view.DrawSandpiles(d.dim2Projection);
                    break;
                case VisualizationMode.CrossSection:
                    view.DrawSandpiles(model.GetCrossSection(model.width / 2, true, false, false));
                    break;
                case VisualizationMode.ThreeDimensions:
                    //TODO
                    break;
            }
            view.updatePerformanceCounter(lastIterationDuration + "");
            view.SetIterationCounter(d.iteration + "");
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
                if (lastIterationDuration < 20 && model.IsStable())
                {
                    System.Threading.Thread.Sleep(100);
                }
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
            int xSize, ySize, zSize;
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

        internal void OnSetModelValue(bool xEnabled, bool yEnabled, bool zEnabled, string xPosString, string yPosString, string zPosString, string valueString)
        {
            int xPos, yPos, zPos, value;
            bool validInput = Int32.TryParse(xPosString, out xPos);
            validInput = Int32.TryParse(yPosString, out yPos) && validInput;
            validInput = Int32.TryParse(zPosString, out zPos) && validInput;
            validInput = Int32.TryParse(valueString, out value) && validInput;
            if (validInput && model.IsValidCoordinate(xPos, yPos, zPos))
            {
                model.FillValues(new bool[] { xEnabled, yEnabled, zEnabled }, new int[] { xPos, yPos, zPos }, value);
            }
            else
            {
                view.ShowDialog("Values not parseable to integer");
            }
        }

        internal void OnVisualizationChanged(VisualizationMode mode)
        {
            this.mode = mode;
        }

    }
}
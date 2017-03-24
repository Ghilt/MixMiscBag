using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandpiles3D
{

    // http://people.reed.edu/~davidp/web_sandpiles/
    public class Presenter
    {


        private const int CROSS_SECTION_TARGET = 65;
        private const int SIZE = CROSS_SECTION_TARGET * 2;
        private const int POS_MIDDLE = CROSS_SECTION_TARGET * 2;

        private long lastIterationDuration;
        private SandpilesCalculator model;
        private Sandpiles3DRender view;
        BackgroundWorker bw = new BackgroundWorker();

        public Presenter()
        {
            model = new SandpilesCalculator(SIZE, SIZE, SIZE / 4);
            model.FillMax();
            model.SetPosition(CROSS_SECTION_TARGET, CROSS_SECTION_TARGET, CROSS_SECTION_TARGET / 4, 9);

            //model.SetPosition(SIZE / 8 - 5, 0, 0, 8);
            //model.SetPosition(SIZE / 8 + 5, SIZE - 1, SIZE - 1, 9);
            //model.SetPosition(0, 0, 0, 8);
            //model.SetPosition(SIZE - 1, 0, (SIZE / 4) - 1, 8);
            //model.SetPosition(SIZE - 1, SIZE - 1, 0, 8);
            //model.SetPosition(SIZE - 1, SIZE - 1, (SIZE / 4) - 1, 8);
            model.SetPosition(6, 46, 6, 9);
            model.SetPosition(46, 6, 6, 29);
            model.SetPosition(SIZE - 5, SIZE - 5, SIZE/4 - 5, 9);

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
    }
}
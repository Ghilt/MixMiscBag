using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandpiles3D
{

    // http://people.reed.edu/~davidp/web_sandpiles/
    public class Presenter
    {


        private long lastIterationDuration;
        private SandpilesCalculator model;
        private Sandpiles3DRender view;
        BackgroundWorker bw = new BackgroundWorker();

        public Presenter()
        {
            int size = 101;
            model = new SandpilesCalculator(size, size, size);
            model.FillMax();
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
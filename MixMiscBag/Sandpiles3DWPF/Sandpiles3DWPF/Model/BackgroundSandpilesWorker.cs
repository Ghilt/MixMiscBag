using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandpiles3DWPF.Model
{
    class BackgroundSandpilesWorker
    {
        private SandpilesCalculator model;
        BackgroundWorker bw;
        private long lastIterationDuration;

        public BackgroundSandpilesWorker(SandpilesCalculator model)
        {
            this.model = model;
            bw = new BackgroundWorker();
        }

        internal void Iterate()
        {
            bw.DoWork += PerformSingleIteration;
            bw.RunWorkerCompleted += IterationFinished;
            bw.RunWorkerAsync();
        }

        internal void StartIteration()
        {
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += PerformContinousIteration;
            bw.ProgressChanged += IterationFinished;
            bw.RunWorkerAsync();
        }

        internal void PerformSingleIteration(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            model.Iterate();
            watch.Stop();
            lastIterationDuration = watch.ElapsedMilliseconds;
            e.Result = model.Get2DProjection();
        }

        private void PerformContinousIteration(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            while (!worker.CancellationPending)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                model.Iterate();
                watch.Stop();
                lastIterationDuration = watch.ElapsedMilliseconds;
                worker.ReportProgress(0, model.Get2DProjection());
                if (lastIterationDuration < 20 && model.IsStable())
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        private void IterationFinished(object sender, ProgressChangedEventArgs e)
        {
            //TODO send duration maybe
        }

        internal void IterationFinished(object sender, RunWorkerCompletedEventArgs e)
        {

        }

    }
}

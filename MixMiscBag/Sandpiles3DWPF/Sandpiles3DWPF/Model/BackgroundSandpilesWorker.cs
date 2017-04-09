using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sandpiles3DWPF.Model
{
    class BackgroundSandpilesWorker : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private SandpilesCalculator model;
        BackgroundWorker bw;
        public long lastIterationDuration { get; private set; }

        public BackgroundSandpilesWorker(PropertyChangedEventHandler propertyChangedListener, SandpilesCalculator model)
        {
            this.model = model;
            bw = new BackgroundWorker();
            PropertyChanged += propertyChangedListener;

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
            OnPropertyChanged();
        }

        internal void IterationFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            OnPropertyChanged();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            //invoke if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

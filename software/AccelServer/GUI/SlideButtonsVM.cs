using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class SlideButtonsVM: BindableBase
    {
        private int from;
        private int to;
        private bool nextEnabled;
        private bool prevEnabled;
        private SpinnerVM stepSpinner;

        private IList<string> outerProperties;

        public SlideButtonsVM(int from=0, int to=300, IList<string> outerProperties = null)
        {
            this.from = from;
            this.to = to;
            this.outerProperties = outerProperties;
            stepSpinner = new SpinnerVM(1, 300);
            OnPrevClick = new DelegateCommand(() => { this.From -= Step; this.To -= Step; RaiseOuterProperties(); });
            OnNextClick = new DelegateCommand(() => { this.From += Step; this.To += Step; RaiseOuterProperties(); });
        }

        public int Step { get { return stepSpinner.Count; }}
        public int From { get { return from; } private set { from = value; RaisePropertyChanged("From"); } }
        public int To { get { return to; } private set { to = value; RaisePropertyChanged("To"); } }
        public int Length { get { return To - From; } }
        public bool IsNextEnabled { get { return nextEnabled; }  set { nextEnabled = value; RaisePropertyChanged("IsNextEnabled"); } }
        public bool IsPrevEnabled { get { return prevEnabled; }  set { prevEnabled = value; RaisePropertyChanged("IsPrevEnabled"); } }

        public SpinnerVM StepSpinner { get => stepSpinner; set { stepSpinner = value; RaisePropertyChanged("StepSpinner");  } }
        public DelegateCommand OnPrevClick { get; }
        public DelegateCommand OnNextClick { get; }

      

        private void RaiseOuterProperties()
        {
            if (outerProperties != null)
                foreach (string str in outerProperties)
                {
                    RaisePropertyChanged(str);
                }
        }
    }
}

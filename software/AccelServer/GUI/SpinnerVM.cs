using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class SpinnerVM: BindableBase
    {
        private int count;
        private bool isMinusEnabled;
        private bool isPlusEnabled;

        private int minValue;
        private int maxValue;

        private IList<string> outerProperties;
        public SpinnerVM(int minValue = 1, int maxValue = 5, IList<string> outerProperties=null)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.outerProperties = outerProperties;
            OnMinusClicked = new DelegateCommand(() => Minus());
            OnPlusClicked = new DelegateCommand(() => Plus());
            Count = minValue;
            IsMinusEnabled = false;
            IsPlusEnabled = true;
        }

        public int Count { get { return count; } private set { count = value; RaisePropertyChanged("CountStr"); } }
        public string CountStr => Count.ToString();
       
        public DelegateCommand OnMinusClicked { get; }
        public DelegateCommand OnPlusClicked { get; }
        public bool IsMinusEnabled { get { return isMinusEnabled; }  set { isMinusEnabled = value; RaisePropertyChanged("IsMinusEnabled"); } }
        public bool IsPlusEnabled { get { return isPlusEnabled; }  set { isPlusEnabled = value; RaisePropertyChanged("IsPlusEnabled"); } }

        private void Minus()
        {
            Count--;
            if (Count == minValue)
                IsMinusEnabled = false;
            if (Count <= maxValue)
                IsPlusEnabled = true;
            RaiseOuterProperties();
          
        }

        private void Plus()
        {
            Count++;
            if (Count == maxValue)
                IsPlusEnabled = false;
            if (Count >= minValue)
                IsMinusEnabled = true;
            RaiseOuterProperties();
            
        }

        private void RaiseOuterProperties()
        {
            if(outerProperties != null)
                foreach(string str in outerProperties)
                {
                    RaisePropertyChanged(str);
                }
        }

    }
}

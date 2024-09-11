namespace ExampleCompositionMEF
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class VMmainWindow : INotifyPropertyChanged
    {
        /// <summary>
        /// The list of calculators (loaded dynamically by MEF).
        /// </summary>
        private List<ICalculator> calculators = new List<ICalculator>();
        private ObservableCollection<string> myParts = new ObservableCollection<string>();
        private string selectedPart = string.Empty;
        private int selectedIndex = 0;
        private decimal value1 = 0;
        private decimal value2 = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="VMmainWindow"/> class.
        /// </summary>
        /// <param name="pCalculators">The p calculators.</param>
        public VMmainWindow(IEnumerable<ICalculator> pCalculators)
        {
            this.calculators = pCalculators.ToList<ICalculator>();
            
            foreach (var ca in this.calculators)
            {
                myParts.Add($"{ca.Description} ({ca.Version})");
            }

            if (this.calculators.Any())
            {
                this.SelectedIndex = 0;
                string desc = $"{calculators.First<ICalculator>().Description}";
                this.SelectedPart = desc;
            }
        }      

        public ObservableCollection<string> MyParts
        {
            get { return myParts; }
            set
            { 
                this.myParts = value; 
                this.OnPropertyChanged(); 
            }
        }

        public string SelectedPart
        {
            get { return selectedPart; }
            set
            { 
                this.selectedPart = value; 
                this.OnPropertyChanged(); 
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set 
            { 
                selectedIndex = value; 
                this.OnPropertyChanged(); 
            }
        }

        public decimal Value1 
        {
            get { return value1; }
            set
            { 
                this.value1 = value; 
                this.OnPropertyChanged(); 
            }
        }

        public decimal Value2
        {
            get { return value2; }
            set
            { 
                this.value2 = value; 
                this.OnPropertyChanged(); 
            }
        }

        decimal result = 0;
        public decimal Result
        {
            get { return result; }
        }

        
        #region Calculation Command

        RelayCommand calcCommand = null;
        
        /// <summary>
        /// Gets the execute command.
        /// </summary>
        public RelayCommand CalcCommand
        {
            get
            {
                if (calcCommand == null)
                {
                    calcCommand = new RelayCommand(param => this.Calculate(), param => true); 
                }
                return calcCommand;
            }
        }

        /// <summary>
        /// Create the letters.
        /// </summary>
        public void Calculate()
        {
            try
            {
                ICalculator c = (from calc in calculators
                                 where calc.Description == SelectedPart
                                 select calc).FirstOrDefault();

                if (c != null)
                {
                    result = c.Calculate(Value1, Value2);
                    OnPropertyChanged("Result");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion Calculation Command

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler == null)
            {
                return;
            }

            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }

        #endregion INotifyPropertyChanged
    }
}

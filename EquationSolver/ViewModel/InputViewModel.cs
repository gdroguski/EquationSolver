using System.ComponentModel;
using System.Windows;
using EquationSolver.Logic;
using EquationSolver.Logic.Commands;
using EquationSolver.Logic.PropertyHelpers;
using EquationSolver.Model;

namespace EquationSolver.ViewModel
{
    public class InputViewModel : PropertyChangedBase
    {
        private readonly InputModel _inputModel;
        private string _equationText;

        public InputViewModel(InputModel inputModel)
        {
            _inputModel = inputModel;
            _inputModel.PropertyChanged += InputModelOnPropertyChanged;

            _equationText = $"{_inputModel.FunctionText}=0";

            HelpCommand = new DelegateCommand(Help);
        }

        private void InputModelOnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName.Equals(nameof(_inputModel.FunctionText)))
                EquationText = $"{_inputModel.FunctionText}=0";
        }

        private static void Help()
        {
            MessageBox.Show(FunctionsInfo.AvailableFunctions, "Functions available", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        public DelegateCommand HelpCommand { get; private set; }

        public double IntervalStart
        {
            get { return _inputModel.IntervalStart; }
            set { _inputModel.IntervalStart = value; }
        }

        public double IntervalEnd
        {
            get { return _inputModel.IntervalEnd; }
            set { _inputModel.IntervalEnd = value; }
        }

        public string FunctionText
        {
            get { return _inputModel.FunctionText; }
            set { _inputModel.FunctionText = value; }
        }

        public string EquationText
        {
            get { return _equationText; }
            set
            {
                if (_equationText == value)
                    return;

                _equationText = value;
                OnPropertyChanged();
            }
        }

        public double Step
        {
            get { return _inputModel.Step; }
            set { _inputModel.Step = value; }
        }

        public double Epsilon
        {
            get { return _inputModel.Epsilon; }
            set { _inputModel.Epsilon = value; }
        }
    }
}
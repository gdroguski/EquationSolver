using EquationSolver.Logic.PropertyHelpers;

namespace EquationSolver.Model
{
    public class InputModel : PropertyChangedBase
    {
        private string _functionText;
        private double _intervalEnd;
        private double _intervalStart;
        private double _step;
        private double _epsilon;

        public InputModel()
        {
            LoadDefaultData();
        }

        private void LoadDefaultData()
        {
            _intervalStart = 3.1415/2;
            _intervalEnd = 3*3.1415/2;
            _functionText = "Sin(x)";
            _step = 0.01;
            _epsilon = 0.001;
        }

        public double IntervalStart
        {
            get { return _intervalStart; }

            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_intervalStart == value)
                    return;

                _intervalStart = value;
                OnPropertyChanged();
            }
        }

        public double IntervalEnd
        {
            get { return _intervalEnd; }

            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_intervalEnd == value)
                    return;

                _intervalEnd = value;
                OnPropertyChanged();
            }
        }

        public string FunctionText
        {
            get { return _functionText; }
            set
            {
                if (_functionText == value)
                    return;

                _functionText = value;
                OnPropertyChanged();
            }
        }

        public double Step
        {
            get { return _step; }
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_step == value)
                    return;

                _step = value;
                OnPropertyChanged(); ;
            }
        }

        public double Epsilon
        {
            get { return _epsilon; }
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_epsilon == value)
                    return;

                _epsilon = value;
                OnPropertyChanged(); ;
            }
        }
    }
}
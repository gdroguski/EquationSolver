using System;
using EquationSolver.Logic.PropertyHelpers;

namespace EquationSolver.Model
{
    public class SolverModel : PropertyChangedBase
    {
        private double _result;
        private string _message;

        public SolverModel()
        {
            LoadDefaultData();
        }

        private void LoadDefaultData()
        {
            _result = Math.PI;
            _message = string.Empty;
        }

        public double Result
        {
            get { return _result; }
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if(_result == value)
                    return;

                _result = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                if(_message == value)
                    return;

                _message = value;
                OnPropertyChanged();
            }
        }
    }
}
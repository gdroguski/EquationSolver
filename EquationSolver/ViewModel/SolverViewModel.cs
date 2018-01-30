using System;
using System.Globalization;
using System.IO;
using EquationSolver.Logic.Commands;
using EquationSolver.Logic.Parser;
using EquationSolver.Logic.PropertyHelpers;
using EquationSolver.Model;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Series;

namespace EquationSolver.ViewModel
{
    public class SolverViewModel : PropertyChangedBase
    {
        private readonly SolverModel _solverModel;
        private readonly InputModel _inputModel;
        private readonly DrawingModel _drawingModel;

        public SolverViewModel(SolverModel solverModel, InputModel inputModel, DrawingModel drawingModel)
        {
            _solverModel = solverModel;
            _inputModel = inputModel;
            _drawingModel = drawingModel;

            InitializeCommand();
        }

        private void InitializeCommand()
        {
            SolveCommand = new DelegateCommand(Solve);
            SaveCommand = new DelegateCommand(Save);
        }

        private void Solve()
        {
            _drawingModel.PlotModel.Series.Clear();

            try
            {
                var dataPoints = CreatePoints(_inputModel.Step);
                var result = FindSolution(dataPoints, _inputModel.Epsilon);

                _drawingModel.PlotModel.Series.Add(dataPoints);

                Result = result;
                Message = string.Empty;
            }
            catch (ArgumentException ae)
            {
                Result = double.NaN;
                Message = ae.Message;
            }
            catch (ArithmeticException ae)
            {
                var dataPoints = CreatePoints(_inputModel.Step);
                _drawingModel.PlotModel.Series.Add(dataPoints);

                Result = double.NaN;
                Message = ae.Message;
            }
            finally
            {
                _drawingModel.PlotModel.InvalidatePlot(true);
            }

        }

        private LineSeries CreatePoints(double step)
        {
            if(step <= 0)
                throw new ArgumentException("Step should be grater than zero");

            var dataPoints = new LineSeries {Color = OxyColor.FromRgb(0, 113, 207)};

            var x = _inputModel.IntervalStart;

            while (x <= _inputModel.IntervalEnd)
            {
                if (Math.Abs(x) < step / 2)
                    x = 0;
                
                var parsedValue = new ExpressionParser(FunctionValue(x)).Parse();
                var currentPoint = new DataPoint(x, parsedValue);

                dataPoints.Points.Add(currentPoint);

                x += step;
            }

            return dataPoints;
        }

        private string FunctionValue(double x)
        {
            return _inputModel.FunctionText.ToLower().Replace("x", "("+x.ToString(CultureInfo.InvariantCulture)+")");
        }

        private double FindSolution(DataPointSeries dataPoints, double epsilon)
        {
            if (epsilon <= 0)
                throw new ArgumentException("Epsilon should be grater than zero");

            var result = dataPoints.Points.Find(p => Math.Abs(p.Y) <= epsilon).X;

            if (result >= _inputModel.IntervalStart && result <= _inputModel.IntervalEnd)
                return result;

            throw new ArithmeticException("No solution found in given interval");
        }

        private void Save()
        {
            var resultToSave = $"Equation:{Environment.NewLine}" +
                               $"{_inputModel.FunctionText}=0{Environment.NewLine}" +
                               $"Interval:{Environment.NewLine}" +
                               $"[{_inputModel.IntervalStart}, {_inputModel.IntervalEnd}]{Environment.NewLine}" +
                               $"Step: {_inputModel.Step}{Environment.NewLine}" +
                               $"Epsilon: {_inputModel.Epsilon}{Environment.NewLine}" +
                               $"Solution:{Environment.NewLine}" +
                               $"x = {Result}";

            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "All Supported Files (*.txt)|*.txt",
                FilterIndex = 2
            };
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, resultToSave);
        }

        public DelegateCommand SolveCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }

        public double Result
        {
            get { return _solverModel.Result; }
            set
            {
                _solverModel.Result = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get { return _solverModel.Message; }
            set
            {
                _solverModel.Message = value;
                OnPropertyChanged();
            }
        }
    }
}
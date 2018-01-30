using System;
using EquationSolver.Logic.PropertyHelpers;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace EquationSolver.Model
{
    public class DrawingModel : PropertyChangedBase
    {
        private PlotModel _plotModel;

        public DrawingModel()
        {
            LoadDefaultPlotModel();
        }

        private void LoadDefaultPlotModel()
        {
            _plotModel = new PlotModel();
            _plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, ExtraGridlines = new double[] { 0 } });
            _plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, ExtraGridlines = new double[] { 0 } });
            var function = new FunctionSeries(Math.Sin, Math.PI/2, 3*Math.PI/2, 0.1)
            {
                Color = OxyColor.FromRgb(0, 113, 207)
            };

            _plotModel.Series.Add(function);
        }

        public PlotModel PlotModel
        {
            get { return _plotModel; }

            set
            {
                if (_plotModel == value)
                    return;

                _plotModel = value;
                OnPropertyChanged();
            }
        }
    }
}
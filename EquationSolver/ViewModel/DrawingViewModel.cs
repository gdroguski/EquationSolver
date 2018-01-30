using EquationSolver.Logic.PropertyHelpers;
using EquationSolver.Model;
using OxyPlot;

namespace EquationSolver.ViewModel
{
    public class DrawingViewModel : PropertyChangedBase
    {
        private readonly DrawingModel _drawingModel;

        public DrawingViewModel(DrawingModel drawingModel)
        {
            _drawingModel = drawingModel;
        }

        public PlotModel MainPlotModel
        {
            get { return _drawingModel.PlotModel; }
            set { _drawingModel.PlotModel = value; }
        }
    }
}
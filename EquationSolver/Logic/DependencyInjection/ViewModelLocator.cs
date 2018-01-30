using Autofac;
using EquationSolver.Model;
using EquationSolver.ViewModel;

namespace EquationSolver.Logic.DependencyInjection
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DrawingModel>().AsSelf().SingleInstance();
            builder.RegisterType<InputModel>().AsSelf().SingleInstance();
            builder.RegisterType<SolverModel>().AsSelf();
            builder.Register(c => new DrawingViewModel(c.Resolve<DrawingModel>())).AsSelf();
            builder.Register(c => new InputViewModel(c.Resolve<InputModel>())).AsSelf();
            builder.Register(c => new SolverViewModel(c.Resolve<SolverModel>(), c.Resolve<InputModel>(), c.Resolve<DrawingModel>())).AsSelf();

            _container = builder.Build();
        }

        public DrawingViewModel DrawingViewModel => _container.Resolve<DrawingViewModel>();

        public InputViewModel InputViewModel => _container.Resolve<InputViewModel>();

        public SolverViewModel SolverViewModel => _container.Resolve<SolverViewModel>();
    }
}
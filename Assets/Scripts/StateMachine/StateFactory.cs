using StateMachine.States;

namespace StateMachine
{
    public class StateFactory : IStateFactory
    {
        // private readonly IObjectResolver _container;
        //
        // public StateFactory(IObjectResolver  container) => 
        //     _container = container;
        
        // public T GetState <T>() where T : IExitableState => 
        //     _container.Resolve<T>();
        public T GetState<T>() where T : IState
        {
            throw new System.NotImplementedException();
        }
    }
}
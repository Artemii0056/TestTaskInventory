using StateMachine.States;

namespace StateMachine
{
    public class BaseStateMachine
    {
        private readonly IStateFactory _stateFactory;
        private IState _currentState;

        public BaseStateMachine(IStateFactory stateFactory) => 
            _stateFactory = stateFactory;

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state?.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _currentState?.Exit();

            TState state = _stateFactory.GetState<TState>();
            _currentState = state;

            return state;
        }
    }
}
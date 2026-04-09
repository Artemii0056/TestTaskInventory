using StateMachine.States;

namespace StateMachine
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void Update(float deltaTime);
    }
}
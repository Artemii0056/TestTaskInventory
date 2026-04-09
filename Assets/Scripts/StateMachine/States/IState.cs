namespace StateMachine.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
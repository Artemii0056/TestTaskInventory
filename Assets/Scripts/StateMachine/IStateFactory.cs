using StateMachine.States;

namespace StateMachine
{
    public interface IStateFactory
    {
        T GetState <T>() where T : IExitableState;
    }
}
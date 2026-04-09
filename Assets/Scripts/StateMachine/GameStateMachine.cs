namespace StateMachine
{
    public class GameStateMachine : BaseStateMachine, IGameStateMachine
    {
        public GameStateMachine(IStateFactory stateFactory) : base(stateFactory)
        {
        }
    }
}
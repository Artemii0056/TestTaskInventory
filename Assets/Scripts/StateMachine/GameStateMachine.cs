namespace StateMachine
{
    public class GameStateMachine : BaseStateMachine
    {
        public GameStateMachine(IStateFactory stateFactory) : base(stateFactory)
        {
        }
    }
}
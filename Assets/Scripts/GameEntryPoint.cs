using StateMachine;
using StateMachine.States;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class GameEntryPoint : IStartable
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameEntryPoint(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Start()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}
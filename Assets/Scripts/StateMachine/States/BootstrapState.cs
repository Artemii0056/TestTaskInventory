using Infrastructure.StaticData;
using Unity.VisualScripting;

namespace StateMachine.States
{
    public class BootstrapState : State
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(IGameStateMachine gameStateMachine, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _staticDataService.LoadAll();
            //_gameStateMachine.Enter<LoadProgress>();
        }

        public void Exit()
        {
            
        }
    }
}
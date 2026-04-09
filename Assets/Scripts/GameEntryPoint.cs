using StateMachine;
using StateMachine.States;
using VContainer.Unity;

public class GameEntryPoint : IStartable
{
    private readonly IGameStateMachine _gameStateMachine;

    public GameEntryPoint(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Start()
    {
        _gameStateMachine.Enter<BootstrapState>();
    }
}
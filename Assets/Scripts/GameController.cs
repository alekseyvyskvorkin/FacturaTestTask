using TestTask.Handlers;
using TestTask.Spawn;
using TestTask.UI;

public class GameController
{
    private Spawner _spawner;
    private InputHandler _inputHandler;
    private PoolContainer _poolContainer;
    private CameraHandler _cameraHandler;
    private UIService _uiService;
    private ExecuteHandler _executeHandler;

    public GameController(Spawner spawner,
        InputHandler inputHandler,
        PoolContainer pool,
        CameraHandler cameraHandler,
        UIService uiService,
        ExecuteHandler executeHandler)
    {
        _spawner = spawner;
        _inputHandler = inputHandler;
        _poolContainer = pool;
        _cameraHandler = cameraHandler;
        _uiService = uiService;
        _executeHandler = executeHandler;

        _inputHandler.OnClick += StartPlay;
    }

    public void PrepareToPlay()
    {
        _poolContainer.DeactivateOldEnemies();        
        _poolContainer.Car.OnSpawn?.Invoke();
        _spawner.SpawnEnemyAtStartPlay();

        _cameraHandler.OnPrepareToPlay();

        _uiService.ShowText("Tap to play");

        _inputHandler.OnClick -= PrepareToPlay;
        _inputHandler.OnClick += StartPlay;        
    }

    public void StartPlay()
    { 
        _inputHandler.OnClick -= StartPlay;
        _inputHandler.OnTouchMove += _poolContainer.Car.Weapon.Rotate;

        _cameraHandler.OnStartPlay();

        _uiService.HideText();

        _poolContainer.Car.OnDie += EndGame;
        _poolContainer.Car.OnFinish += EndGame;

        _spawner.StartSpawn();

        _executeHandler.enabled = true;
    }

    public void EndGame()
    {
        _executeHandler.enabled = false;

        _inputHandler.OnTouchMove -= _poolContainer.Car.Weapon.Rotate;
        _inputHandler.OnClick += PrepareToPlay;

        _poolContainer.Car.OnDie -= EndGame;
        _poolContainer.Car.OnFinish -= EndGame;

        if (_poolContainer.Car.IsDead)
            _uiService.ShowText("You Lose");
        else
            _uiService.ShowText("You Win");
    }
}
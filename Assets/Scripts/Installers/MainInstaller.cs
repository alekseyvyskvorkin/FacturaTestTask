using TestTask.Handlers;
using TestTask.Settings;
using TestTask.Spawn;
using TestTask.UI;
using TestTask.Views;
using UnityEngine;
using Zenject;

namespace TestTask.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Config _config;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private UIService _uiService;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private CameraHandler _cameraHandler;
        [SerializeField] private ExecuteHandler _executeHandler;
        [SerializeField] private FinishLineView _finishLine;
        [SerializeField] private PoolContainer _poolContainer;

        public override void InstallBindings()
        {
            Container.Bind<Config>().FromInstance(_config).AsSingle().NonLazy();
            Container.Bind<InputHandler>().FromInstance(_inputHandler).AsSingle().NonLazy();
            Container.Bind<UIService>().FromInstance(_uiService).AsSingle().NonLazy();
            Container.Bind<Spawner>().FromInstance(_spawner).AsSingle().NonLazy();
            Container.Bind<CameraHandler>().FromInstance(_cameraHandler).AsSingle().NonLazy();
            Container.Bind<ExecuteHandler>().FromInstance(_executeHandler).AsSingle().NonLazy();
            Container.Bind<FinishLineView>().FromInstance(_finishLine).AsSingle().NonLazy();
            Container.Bind<PoolContainer>().FromInstance(_poolContainer).AsSingle().NonLazy();

            Container.Bind<Factory>().AsSingle().NonLazy();
            Container.Bind<GameController>().AsSingle().NonLazy();
        }
    }
}
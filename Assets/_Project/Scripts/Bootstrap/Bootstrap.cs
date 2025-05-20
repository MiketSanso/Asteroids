using GameScene.Common.ChangeSceneService;
using Zenject;

namespace GameScene.Common
{
    public class Bootstrap : IInitializable
    {
        private SceneChanger _sceneChanger;
        
        public Bootstrap(SceneChanger sceneChanger)
        {
            _sceneChanger = sceneChanger;
        }

        public void Initialize()
        {
            _sceneChanger.ActivateMenu();     
        }
    }
}
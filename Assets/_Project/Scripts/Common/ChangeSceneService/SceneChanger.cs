using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene.Common.ChangeSceneService
{
    public class SceneChanger
    {
        public void ActivateGameScene()
        {
            SceneManager.LoadScene(2);
        }
        
        public void ActivateMenu()
        {
            SceneManager.LoadScene(1);
        }
        
        public void CloseGame()
        {
            Application.Quit();
        }
    }
}
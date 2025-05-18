using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene.Common
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene(1);          
        }
    }
}
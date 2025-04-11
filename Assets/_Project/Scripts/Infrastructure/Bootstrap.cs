using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene(1);          
        }
    }
}
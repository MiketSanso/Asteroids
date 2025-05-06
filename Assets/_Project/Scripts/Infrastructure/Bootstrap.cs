using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene(1);          
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene(1);          
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSwitchScene : MonoBehaviour
{
    [SerializeField] private int _numberScene;
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(Input);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void Input()
    {
        SceneManager.LoadScene(_numberScene);
    }
}

using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Zenject;
using UnityEngine;

namespace GameScene.Common
{
    public class Authentication : IInitializable
    {
        public void Initialize()
        {
            Auth().Forget();
        }
        
        private async UniTask Auth()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Sign in Success");
            }
            catch (AuthenticationException ex)
            {
                Debug.LogError("Error Auth!");
                Debug.LogException(ex);
            }
        }
    }
}
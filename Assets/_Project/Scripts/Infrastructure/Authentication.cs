using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class Authentication
    {
        public async UniTask Auth()
        {
            await UnityServices.InitializeAsync();
            
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
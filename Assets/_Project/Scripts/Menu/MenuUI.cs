using System;
using Cysharp.Threading.Tasks;
using GameScene.Models.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameScene.Menu
{
    public class MenuUI : MonoBehaviour
    {
        public TMP_Text StatusText;
        public Button ButtonNoAds;
        public Button ButtonExit;
        public Button ButtonStartGame;
        
        private AnimateShopConfig _animateShopData;  
        private ReportTextsData _reportTextsData;

        [Inject]
        private void Construct(ReportTextsData reportTextsData)
        {
            _reportTextsData = reportTextsData;
        }

        public void InitializeConfig(AnimateShopConfig animateShopData)
        {
            _animateShopData = animateShopData;
        }

        public async void SetFailedText()
        {
            await UpdateTextInfo(_reportTextsData.TextFailed);
        }
        
        public async void SetSuccessText()
        {
            await UpdateTextInfo(_reportTextsData.TextSuccess);
        }
        
        public async void SetUnavailableText()
        {
            await UpdateTextInfo(_reportTextsData.TextUnavailable);
        }

        private async UniTask UpdateTextInfo(string textMessage)
        {
            StatusText.text = textMessage;
            StatusText.alpha = 1;

            while (StatusText.alpha != 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_animateShopData.TimeStep));
                StatusText.alpha -= _animateShopData.TimeStep;
            }
        }
    }
}

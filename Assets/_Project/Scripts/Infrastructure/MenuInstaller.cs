using GameScene.Models.Configs;
using UnityEngine;
using Zenject;

namespace GameSystem.Common.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private ReportTextsData _reportTextsData;

        public override void InstallBindings()
        {
            Container.Bind<ReportTextsData>().FromInstance(_reportTextsData).AsSingle();
        }
    }
}
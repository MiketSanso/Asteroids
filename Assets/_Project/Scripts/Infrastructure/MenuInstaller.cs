using GameScene.Menu;
using GameScene.Models.Configs;
using UnityEngine;
using Zenject;

namespace GameSystem.Common.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private ReportTextsData _reportTextsData;
        [SerializeField] private MenuUI _menuUI;

        public override void InstallBindings()
        {
            Container.Bind<ReportTextsData>().FromInstance(_reportTextsData).AsSingle();
            Container.BindInterfacesTo<MenuPresenter>().AsSingle().WithArguments(_menuUI);
        }
    }
}
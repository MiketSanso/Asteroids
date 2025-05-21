using System;

namespace GameScene.Common
{
    public interface IGameEndEventer
    {
        public event Action OnRestart;
        public event Action OnResume;
    }
}
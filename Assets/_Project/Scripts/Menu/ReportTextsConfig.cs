using UnityEngine;

namespace GameScene.Models.Configs
{
    [CreateAssetMenu(fileName = "ReportTextsData", menuName = "ReportTextsData", order = 0)]
    public class ReportTextsData : ScriptableObject
    {
        public string TextSuccess;
        public string TextUnavailable;
        public string TextFailed;
    }
}
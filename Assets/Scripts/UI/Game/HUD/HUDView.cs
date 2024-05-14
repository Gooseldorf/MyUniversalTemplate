using TMPro;
using UI.Base;
using UnityEngine;

namespace UI.Game.HUD
{
    public class HUDView : PanelViewBase
    {
        [SerializeField] private TextMeshProUGUI scoreTMP;
        [SerializeField] private TextMeshProUGUI levelTMP;

        public void SetScore(int score) => scoreTMP.text = score.ToString();

        public void SetLevel(int level) => levelTMP.text = level.ToString();
    }
}

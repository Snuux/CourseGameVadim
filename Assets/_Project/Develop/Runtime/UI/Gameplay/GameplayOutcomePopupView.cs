using _Project.Develop.Runtime.UI.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayOutcomePopupView : PopupViewBase
    {
        [field: SerializeField] private TMP_Text _infoText;
        
        public void SetInfo(string text) => _infoText.text = text;

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            animation
                .Append(_infoText
                    .DOFade(1, .2f)
                    .From(0));
        }
    }
}
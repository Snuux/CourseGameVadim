using _Project.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.AbilitySelectPopup
{
    public class AbilityIcon : MonoBehaviour, IView
    {
        [SerializeField] private Image _image;
        [SerializeField] private Transform _levelParent;
        [SerializeField] private TMP_Text _text;
        
        public void ShowLevel() => _levelParent.gameObject.SetActive(true);
        
        public void HideLevel() => _levelParent.gameObject.SetActive(false);
        
        public void SetIcon(Sprite sprite) => _image.sprite = sprite;

        public void SetLevel(string level) => _text.text = level;
    }
}
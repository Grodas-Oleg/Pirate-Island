using PixelCrew.Model.Data;
using UnityEngine;

namespace PixelCrew.UI.HUD.Dialogs
{
    public class PersonalizedDialogBoxController : DialogBoxController
    {
        [SerializeField] private DialogContent _right;

        protected override DialogContent CurrentContent => CurreSentence.Side == Side.Left ? _content : _right;

        protected override void OnStartDialogAnimation()
        {
            _right.gameObject.SetActive(CurreSentence.Side == Side.Right);
            _content.gameObject.SetActive(CurreSentence.Side == Side.Left);
            
            base.OnStartDialogAnimation();
        }
    }
}
using System.Collections;
using PixelCrew.Creatures.Hero;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.UI.HUD.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;

        [Space] [SerializeField] private float _textSpeed = 0.1f;

        [Header("Sounds")] [SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        [Space] [SerializeField] protected DialogContent _content;

        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        private DialogData _data;
        private int _currentSentence;
        private AudioSource _sfxSource;
        private Coroutine _typingRoutine;
        private GameObject _hero;

        protected Sentence CurreSentence => _data.Sentences[_currentSentence];

        private void Start()
        {
            _hero = GameObject.FindWithTag("Player");
            _sfxSource = AudioUtils.FindSfxSource();
        }

        public void ShowDialog(DialogData data)
        {
            _data = data;
            _currentSentence = 0;
            CurrentContent.Text.text = string.Empty;
            _hero.GetComponent<PlayerInput>().actions.Disable();
            _container.SetActive(true);
            _sfxSource.PlayOneShot(_open);
            _animator.SetBool(IsOpen, true);
        }

        private IEnumerator TypeDialogText()
        {
            CurrentContent.Text.text = string.Empty;
            var sentence = CurreSentence;
            var avatar = DefsFacade.I.Avatar.Get(sentence.Icon).Icon;
            CurrentContent.TrySetIcon(avatar);
            foreach (var letter in sentence.Value)
            {
                CurrentContent.Text.text += letter;
                _sfxSource.PlayOneShot(_typing);
                yield return new WaitForSeconds(_textSpeed);
            }

            _typingRoutine = null;
        }

        protected virtual DialogContent CurrentContent => _content;

        public void OnSkip()
        {
            if (_typingRoutine == null) return;

            StopTypeAnimation();
            CurrentContent.Text.text = _data.Sentences[_currentSentence].Value;
        }

        public void OnContinue()
        {
            StopTypeAnimation();
            _currentSentence++;

            var isDialogComplete = _currentSentence >= _data.Sentences.Length;
            if (isDialogComplete)
            {
                HideDialogBox();
            }
            else
            {
                OnStartDialogAnimation();
            }
        }

        private void HideDialogBox()
        {
            _animator.SetBool(IsOpen, false);
            _sfxSource.PlayOneShot(_close);
            _hero.GetComponent<PlayerInput>().actions.Enable();
        }

        private void StopTypeAnimation()
        {
            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);
            _typingRoutine = null;
        }

        protected virtual void OnStartDialogAnimation()
        {
            _typingRoutine = StartCoroutine(TypeDialogText());
        }


        private void OnEndDialogAnimation()
        {
        }
    }
}
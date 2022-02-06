using PixelCrew.Utils;
using TMPro;
using UnityEngine;

namespace PixelCrew.Components.PopupDamageText
{
    public class DamagePopup : MonoBehaviour
    {
        private TextMeshPro _text;

        private float _disappearTimer;
        private const float MaxDisappearTimer = 1;
        private static int _sortingOrder;

        private Color _textColor;
        private Vector3 _moveVector;

        private const string ContainerName = "### SPAWNED ###";

        public static DamagePopup Create(Vector3 position, int damageValue, bool criticalHit)
        {
            var container = GameObject.Find(ContainerName);
            if (container == null)
            {
                container = new GameObject(ContainerName);
            }

            var damagePopupPosition =
                Instantiate(GameAsset.I.damagePopup, position, Quaternion.identity, container.transform);
            var damagePopup = damagePopupPosition.GetComponent<DamagePopup>();
            damagePopup.Setup(damageValue, criticalHit);

            return damagePopup;
        }

        private void Awake()
        {
            _text = transform.GetComponent<TextMeshPro>();
        }

        private void Setup(int damageValue, bool criticalHit)
        {
            _text.SetText(damageValue.ToString());
            if (!criticalHit)
            {
                _text.fontSize = 2;
                _textColor = Color.yellow;
            }
            else
            {
                _text.fontSize = 3;
                _textColor = Color.red;
            }

            _text.color = _textColor;
            _disappearTimer = MaxDisappearTimer;
            _moveVector = new Vector3(.7f, 1) * 2;

            _sortingOrder++;
            _text.sortingOrder = _sortingOrder;
        }

        private void Update()
        {
            transform.position += _moveVector * Time.deltaTime;
            _moveVector -= _moveVector * 8f * Time.deltaTime;
            var scaleValue = 1f;
            if (_disappearTimer > MaxDisappearTimer * 0.5f)
            {
                transform.localScale += Vector3.one * scaleValue * Time.deltaTime;
            }
            else
            {
                transform.localScale -= Vector3.one * scaleValue * Time.deltaTime;
            }

            _disappearTimer -= Time.deltaTime;
            const float disappearSpeed = 2f;
            if (!(_disappearTimer < 0)) return;

            _textColor.a -= disappearSpeed * Time.deltaTime;
            _text.color = _textColor;
            if (_textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
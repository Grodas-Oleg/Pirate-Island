using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelCrew.UI.Widgets.DragAndDrop
{
    public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform _defaultPosition;
        private Transform _placeholderPosition;

        private GameObject _placeholder = null;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _placeholder = new GameObject("_placeholder", typeof(RectTransform));
            _placeholder.transform.SetParent(transform.parent);

            var placeholderRect = _placeholder.GetComponent<RectTransform>();
            var itemRect = GetComponent<RectTransform>().rect.size;
            placeholderRect.sizeDelta = new Vector2(itemRect.x, itemRect.y);

            _placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

            _defaultPosition = transform.parent;
            _placeholderPosition = _defaultPosition;
            transform.SetParent(transform.parent.parent);

            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;

            if (_placeholder.transform.parent != _placeholderPosition)
                _placeholder.transform.SetParent(_placeholderPosition);

            int newSbIndex = _placeholderPosition.childCount;

            for (int i = 0; i < _placeholderPosition.childCount; i++)
            {
                if (transform.position.x < _placeholderPosition.GetChild(i).position.x)
                {
                    newSbIndex = i;
                    if (_placeholder.transform.GetSiblingIndex() < newSbIndex)
                        newSbIndex--;

                    break;
                }
            }

            _placeholder.transform.SetSiblingIndex(newSbIndex);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(_defaultPosition);
            transform.SetSiblingIndex(_placeholder.transform.GetSiblingIndex());
            _canvasGroup.blocksRaycasts = true;

            Destroy(_placeholder);
        }
    }
}
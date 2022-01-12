using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelCrew.UI.Widgets.DragAndDrop
{
    // public class DropItem : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    // {
    //     public void OnDrop(PointerEventData eventData)
    //     {
    //         var item = eventData.pointerDrag.GetComponent<DragItem>();
    //         if (item != null)
    //             item._defaultPosition = transform;
    //     }
    //
    //     public void OnPointerEnter(PointerEventData eventData)
    //     {
    //         if (eventData.pointerDrag == null) return;
    //
    //         var item = eventData.pointerDrag.GetComponent<DragItem>();
    //         if (item != null)
    //             item._placeholderPosition = transform;
    //     }
    //
    //     public void OnPointerExit(PointerEventData eventData)
    //     {
    //         if (eventData.pointerDrag == null) return;
    //
    //         var item = eventData.pointerDrag.GetComponent<DragItem>();
    //         if (item != null && item._placeholderPosition == transform)
    //             item._placeholderPosition = transform;
    //     }
    // }
}
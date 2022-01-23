using PixelCrew.Model;
using PixelCrew.Model.Data;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Interactions
{
    public class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] _requierd;
        [SerializeField] private bool _removeAfterUse;

        [SerializeField] private UnityEvent _onSuccess;
        [SerializeField] private UnityEvent _onFail;

        public void Check()
        {
            var session = GameSession.Instance;
            var areAllRequirementsMet = true;
            foreach (var item in _requierd)
            {
                var numItems = session.Data.Inventory.Count(item.Id);
                if (numItems < item.Value)
                    areAllRequirementsMet = false;
            }

            if (areAllRequirementsMet)
            {
                if (_removeAfterUse)
                {
                    foreach (var item in _requierd)
                        session.Data.Inventory.Remove(item.Id, item.Value);
                    _onSuccess.Invoke();
                }
            }
            else
            {
                _onFail?.Invoke();
            }
        }
    }
}
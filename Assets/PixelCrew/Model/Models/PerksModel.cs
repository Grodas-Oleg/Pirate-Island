using System;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.Model.Models
{
    public class PerksModel : IDisposable
    {
        private readonly PlayerData _data;
        public readonly StringProperty InterfaceSelection = new StringProperty();

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public event Action OnChanged;

        public PerksModel(PlayerData data)
        {
            _data = data;
            InterfaceSelection.Value = DefsFacade.I.Perks.All[0].Id;

            _trash.Retain(_data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
            _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public bool IsDoubleJumpSupported => _data.Perks.IsUnlocked("double-jump");
        public bool IsDashSupported => _data.Perks.IsUnlocked("dash");
        public bool IsSuperThrowSupported => _data.Perks.IsUnlocked("super-throw");
        public bool IsShieldSupported => _data.Perks.IsUnlocked("shield");

        public bool IsCannonSupported => _data.Perks.IsUnlocked("cannon");
        // public bool IsCannonSupported => _data.Perks.Used.Value == "cannon";
        // public bool IsDoubleJumpSupported => _data.Perks.IsUnlocked("double-jump");

        public void Unlock(string id)
        {
            var def = DefsFacade.I.Perks.Get(id);
            var isEnoughResources = _data.Inventory.IsEnough(def.Price);

            if (!isEnoughResources) return;
            _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
            _data.Perks.AddPerk(id);

            OnChanged?.Invoke();
        }

        public float PerkCooldown(string perkId)
        {
            var def = DefsFacade.I.Perks.Get(perkId).Cooldown;
            return def;
        }

        public static Sprite PerkSprite(string id)
        {
            var def = DefsFacade.I.Perks.Get(id);
            return def.Icon;
        }

        public bool IsUnlocked(string perkId)
        {
            return _data.Perks.IsUnlocked(perkId);
        }

        public bool CanBuy(string perkId)
        {
            var def = DefsFacade.I.Perks.Get(perkId);
            return _data.Inventory.IsEnough(def.Price);
        }

        public void Dispose()
        {
            _trash.Dispose();
        }
    }
}
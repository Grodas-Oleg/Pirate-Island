using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Model.Definitions.Player;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] private List<LevelProgresses> _progresses;

        public int GetLevel(StatId id)
        {
            var progresses = _progresses.FirstOrDefault(x => x.Id == id);
            return progresses?.Level ?? 0;
        }

        public void LevelUp(StatId id)
        {
            var progresses = _progresses.FirstOrDefault(x => x.Id == id);
            if (progresses == null)
                _progresses.Add(new LevelProgresses(id, 1));
            else
                progresses.Level++;
        }
    }

    [Serializable]
    public class LevelProgresses
    {
        public StatId Id;
        public int Level;

        public LevelProgresses(StatId id, int level)
        {
            Id = id;
            Level = level;
        }
    }
}
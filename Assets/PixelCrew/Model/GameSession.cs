using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PixelCrew.Components.LevelManagement;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Model.Models;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private int _levelIndex;
        [SerializeField] private PlayerData _data;
        [SerializeField] private string _defaultCheckpoint;

        public static GameSession Instance { get; private set; }

        public int LevelIndex { get; private set; }
        public PlayerData Data => _data;
        private PlayerData _save;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public QuickInventoryModel QuickInventory { get; private set; } //public access with private modification
        public BigInventoryModel BigInventory { get; private set; } //public access with private modification
        public PerksModel PerksModel { get; private set; }
        public StatsModel StatsModel { get; private set; }
        public ShopModel ShopModel { get; private set; }

        private List<string> _checkpoints = new List<string>();

        private void Awake()
        {
            // level_start
            // level_index
            // level_complete
            var existSession = GetExistSession();
            if (existSession != null)
            {
                existSession.StartSession(_defaultCheckpoint, _levelIndex);
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitModels();
                DontDestroyOnLoad(this);
                Instance = this;
                StartSession(_defaultCheckpoint, _levelIndex);
            }
        }

        private void StartSession(string defaultCheckpoint, int levelIndex)
        {
            SetChecked(defaultCheckpoint);
            LevelIndex = levelIndex;
            TrackSessionStart(levelIndex);
            LoadUIs();
            SpawnHero();
        }

        private void TrackSessionStart(int levelIndex)
        {
            var eventParams = new Dictionary<string, object>
            {
                {"level_index", levelIndex}
            };
            AnalyticsEvent.Custom("level_start", eventParams);
        }

        private void SpawnHero()
        {
            var checkpoints = FindObjectsOfType<CheckPointComponent>();
            var lastCheckPoint = _checkpoints.Last();
            foreach (var checkPoint in checkpoints)
            {
                if (checkPoint.Id == lastCheckPoint)
                {
                    checkPoint.SpawnHero();
                    break;
                }
            }
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(_data);
            _trash.Retain(QuickInventory);

            BigInventory = new BigInventoryModel(_data);
            _trash.Retain(BigInventory);

            PerksModel = new PerksModel(_data);
            _trash.Retain(PerksModel);

            StatsModel = new StatsModel(_data);
            _trash.Retain(StatsModel);

            ShopModel = new ShopModel(_data);
            _trash.Retain(ShopModel);

            _data.HP.Value = (int) StatsModel.GetValue(StatId.Hp);
        }

        private void LoadUIs()
        {
            LoadOnScreenControls();
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        [Conditional("USE_ONSCREEN_CONTROLS")]
        private void LoadOnScreenControls()
        {
            SceneManager.LoadScene("Controls", LoadSceneMode.Additive);
        }

        private GameSession GetExistSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            return sessions.FirstOrDefault(gameSession => gameSession != this);
        }

        public void Save()
        {
            _save = _data.Clone();
        }

        public void LoadLastSave()
        {
            _data = _save.Clone();

            _trash.Dispose();
            InitModels();
        }

        public bool IsChecked(string id)
        {
            return _checkpoints.Contains(id);
        }

        public void SetChecked(string id)
        {
            if (!_checkpoints.Contains(id))
            {
                Save();
                _checkpoints.Add(id);
            }
        }

        private readonly List<string> _removedItems = new List<string>();

        public bool RestoreState(string Id)
        {
            return _removedItems.Contains(Id);
        }

        public void StoreState(string state)
        {
            if (!_removedItems.Contains(state))
                _removedItems.Add(state);
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
            _trash.Dispose();
        }
    }
}
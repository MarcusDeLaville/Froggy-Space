using UnityEngine;

namespace Items.Chest
{
    [RequireComponent(typeof(ChestInteraction))]
    [RequireComponent(typeof(ResourceChestSpawner))]
    [RequireComponent(typeof(ChestOpener))]
    [RequireComponent(typeof(ChestCollisionHandler))]
    public class Chest : MonoBehaviour
    {
        private bool _isOpened;
        private ResourceChestSpawner _resourceChestSpawner;
        private ChestSaver _saver;

        private ChestInteraction _interaction;
        private ChestOpener _chestOpener;
        private ChestCollisionHandler _collisionHandler;
        private Advertisement _advertisement;

        private void Awake()
        {
            _resourceChestSpawner = GetComponent<ResourceChestSpawner>();
            _chestOpener = GetComponent<ChestOpener>();
            _interaction = GetComponent<ChestInteraction>();
            _interaction.enabled = false;
            _collisionHandler = GetComponent<ChestCollisionHandler>();
        }

        private void OnEnable()
        {
            _interaction.Opening += OnChestOpening;
        }

        private void OnDisable()
        {
            _interaction.Opening -= OnChestOpening;
        }

        private void Start()
        {
            _collisionHandler.Init(_interaction);
            
            if(_interaction is AdvertisingChestInteraction advertising)
                advertising.InitAdvertisement(_advertisement);
            
            string id = GetInstanceID().ToString();
            _saver = new ChestSaver(id);
            _isOpened = _saver.TryLoad().IsOpened;
            if (_isOpened == false)
            {
                _interaction.enabled = true;
                _resourceChestSpawner.SpawnResources();
            }
            else
            {
                _chestOpener.ChangeSprite();
                _chestOpener.enabled = false;
            }
        }

        private void OnChestOpening()
        {
            _isOpened = true;
            _saver.TrySave(new ChestData(_isOpened));
            _interaction.enabled = false;
        }

        public void ClearSaves()
        {
            _saver.TrySave(new ChestData(false));
        }

        public void Inject(Advertisement advertisement)
        {
            _advertisement = advertisement;
        }
    }
}
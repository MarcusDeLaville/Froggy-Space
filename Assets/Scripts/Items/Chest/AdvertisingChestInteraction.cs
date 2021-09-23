using System;

namespace Items.Chest
{
    public class AdvertisingChestInteraction : ChestInteraction
    {
        private Advertisement _advertisement;

        public override event Action Opening;

        private void OnDisable()
        {
            if( (object) _advertisement != null)
                _advertisement.Callback.AdShown -= OnAdShown;
        }
    
        public override void Interact()
        {
            _advertisement.ShowAd();
        }

        public void InitAdvertisement(Advertisement advertisement)
        {
            _advertisement = advertisement;
            _advertisement.Callback.AdShown += OnAdShown;
        }
    
        private void OnAdShown()
        {
            Opening?.Invoke();
        }
    }
}
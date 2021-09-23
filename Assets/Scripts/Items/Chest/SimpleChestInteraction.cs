using System;

namespace Items.Chest
{
    public class SimpleChestInteraction : ChestInteraction
    {
        public override event Action Opening;

        public override void Interact()
        {
            Opening?.Invoke();
        }
    }
}
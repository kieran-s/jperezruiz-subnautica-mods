using SMLHelper.V2.Json;
using UnityEngine;

namespace BetterScannerRoomUpgrades.MonoBehaviors
{
    /**
     * Component used to refresh the scanner room when the mod config changes
     * Because Subnautica only updates the scanner room when a module is added or removed
     * we need to request Subnautica to update when the mod config changes
     */
    public class MapRoomFunctionalityConfigSyncComponent: MonoBehaviour
    {
        private MapRoomFunctionality scannerRoom;
        private void Awake()
        {
            // Get the scanner room to update
            scannerRoom = GetComponentInParent<MapRoomFunctionality>();
            // Subscribe to the mod config file save event
            Main.s_modConfig.OnFinishedSaving += OnConfigChange;
        }

        private void OnConfigChange(object sender, ConfigFileEventArgs config)
        {
            // mark as dirty to force the update in the next tick
            // this is usually triggered when modifying the modules
            scannerRoom.containerIsDirty = true;
        }

        private void OnDestroy()
        {
            // If the scanner room is destroyed, this OnDestroy will be executed
            // unsubscribe from the config file save event
            Main.s_modConfig.OnFinishedSaving -= OnConfigChange;
        }
    }
}
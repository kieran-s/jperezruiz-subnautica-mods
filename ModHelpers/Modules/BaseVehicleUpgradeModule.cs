using System;
using System.IO;
using System.Reflection;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections;
using SMLHelper.V2.Assets;

namespace ModHelpers.Modules
{

    /**
     * Base for the Vehicle upgrades
     */
    public abstract class BaseVehicleUpgradeModule: Equipable
    {
        public readonly int UpgradeLevel;
        
        public override TechGroup GroupForPDA => TechGroup.Cyclops;
        public override string AssetsFolder => "Assets";
        protected BaseVehicleUpgradeModule(
            string name,
            string friendlyName,
            string description
        ) : base
        (
            name,
            friendlyName,
            description
        )
        {
        }

        public override QuickSlotType QuickSlotType => QuickSlotType.Passive;
        /**
         * Return the tech type to copy as base.
         * Used in GetGameObjectAsync
         */
        public abstract TechType GetTechTypeToCopy();
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var taskResult = new TaskResult<GameObject>();
            yield return CraftData.InstantiateFromPrefabAsync(GetTechTypeToCopy(), taskResult);
            var obj = taskResult.Get();
            
            // Get the TechTags and PrefabIdentifiers
            var techTag = obj.GetComponent<TechTag>();
            var prefabIdentifier = obj.GetComponent<PrefabIdentifier>();

            // Change them so they fit to our requirements.
            techTag.type = TechType;
            prefabIdentifier.ClassId = ClassID;
            gameObject.Set(obj);
        }
    }
}
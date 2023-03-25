namespace ModUtils.Behaviours;
using System.Collections.Generic;
using System.Linq;
using CyclopsHelpers.Modules;
using UnityEngine;
using UnityEngine.UI;
public class CyclopsCustomUpgradeModules : MonoBehaviour
    {
        private List<BaseCyclopsUpgradeModule> _modules = new List<BaseCyclopsUpgradeModule>();
        private SubRoot cyclops;

        public List<BaseVehicleUpgradeModule> Modules => _modules.Concat(new List<BaseVehicleUpgradeModule>())
                                                                 .ToList();

        private void Start()
        {
            cyclops = GetComponent<SubRoot>();
            RegisterHudIcons();
        }

        public void RegisterModules(List<BaseCyclopsUpgradeModule> modules)
        {
            var nonDuplicated =
                modules.Where
                (
                    module => _modules.Find(existingModule => existingModule.TechType == module.TechType) == null
                );
            _modules = _modules.Concat(nonDuplicated)
                               .ToList();
            if (cyclops != null)
            {
                RegisterHudIcons();
            }
        }

        private void RegisterHudIcons()
        {
            Transform Icons = cyclops.transform.Find("UpgradeConsoleHUD/UpgradeConsole_Canvas_Main/Icons");
            var currentIcons = Icons.GetComponents<CyclopsUpgradeConsoleIcon>()
                                    .ToList();
            var nonRegisteredIcons =
                _modules.Where
                (
                    module => currentIcons.Find(icon => icon.upgradeType == module.TechType) == null
                );
            nonRegisteredIcons.ForEach
            (
                module =>
                {
                    GameObject icon = MonoBehaviour.Instantiate
                    (
                        Icons.Find("ShieldIcon")
                             .gameObject,
                        Icons,
                        false
                    );
                    icon.GetComponent<CyclopsUpgradeConsoleIcon>()
                        .upgradeType = module.TechType;
                    icon.GetComponent<Image>()
                        .sprite = module.GetUpgradeConsoleHUDSprite();
                }
            );
        }
    }
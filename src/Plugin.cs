using BepInEx;
using Voxels.TowerDefense;
using Voxels.TowerDefense.CampaignGeneration.CampaignAc3;

namespace BBB
{
    [BepInPlugin("nacu.bringbackberserkers", "Bring Back Berserkers", VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string VERSION = "1.0";

        public void OnEnable()
        {
            On.Voxels.TowerDefense.GameSetup.Awake += AddBerserkerToSpawnPool;
            Logger.LogInfo("Bring Back Berserkers loaded");
        }

        private void AddBerserkerToSpawnPool(On.Voxels.TowerDefense.GameSetup.orig_Awake orig, GameSetup self)
        {
            orig.Invoke(self);
            if (LevelStateObjectReferences.dict.TryGetValue("Viking_Berserker", out _))
            {
                (LevelStateObjectReferences.dict["Viking_Berserker"] as VikingReference).GetComponent<LevelGuessable>().probability = (LevelStateObjectReferences.dict["Viking_AxeThrower"] as VikingReference).GetComponent<LevelGuessable>().probability;
                (LevelStateObjectReferences.dict["Viking_Berserker"] as VikingReference).GetComponent<LevelRule>().condition.expression = "(fraction > 0.15 && fraction < 0.4) || (fraction > 0.55 && fraction < 0.8) || fraction > 0.95";
            }
        }
    }
}
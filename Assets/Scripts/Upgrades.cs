using System;

using UnityEngine;



    namespace BallistaShooter
    {
        public class Upgrades : MonoSingleton<Upgrades>
        {
            public const string filename = "upgrades.dat";

            
             

        [Serializable]
            private class UpgradeSave
            {
                public UpgradeAsset asset;
                public int level = 0;
            }

        private new void Awake()
        {
            base.Awake();
            if(!(Saver<UpgradeSave[]>.TryLoad(filename, ref save)))
            {
                foreach (var upgrade in Instance.save)
                {
                    upgrade.level = 0;
                    Saver<UpgradeSave[]>.Save(filename, Instance.save);
                    
                }
            }
        }

        [SerializeField] private UpgradeSave[] save;
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach(var upgrade in Instance.save)
            {
                if(upgrade.asset==asset)
                {
                    upgrade.level += 1;
                    Saver<UpgradeSave[]>.Save(filename, Instance.save);
                }
            }

        }

        public static int GetTotalCost()
        {
            int result = 0;
            foreach (var upgrade in Instance.save)
            {
                for (int i = 0; i < upgrade.level; i++)
                    result += upgrade.asset.costByLevel[i];
            }
            return result;
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
           foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    Debug.Log("upgrade " + asset.name + "score " + upgrade.level);
                    return upgrade.level;
                }
            }
            return 0;
        }

        }
    }



using UnityEngine;
using UnityEngine.Serialization;

namespace CannonShooter
{
    

    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        [Header("View")]
        public Sprite sprite;

        [Header("settings")]
        public int[] costByLevel = { 1 };

        [FormerlySerializedAs("name")] public string UpgradeName;
    }

    //didjifddifj//

    //comment after reset//
}

using UnityEngine;

namespace BallistaShooter
{
    

    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("View")]
        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController animation;

        [Header("Character settings")]
        public float moveSpeed = 1;
        public int hp=1;
        public int score=1;
        public float radius = 0.19f;
        public int damage = 1;
        public int gold = 1;
    }
}
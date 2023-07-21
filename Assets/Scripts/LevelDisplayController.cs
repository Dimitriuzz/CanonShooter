
using UnityEngine;

namespace BallistaShooter
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] levels;
        [SerializeField] private BranchLevel[] branchlevels;
    
        void Start()
        {
            var drawLevel = 0;
            var score = -1;

            while (score != 0 && drawLevel<levels.Length )
            {
                score=levels[drawLevel++].Initialaise();
                
            }

            for (int i=drawLevel; i<levels.Length;i++)
            {
                levels[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < branchlevels.Length; i++)
            {
                branchlevels[i].TryActivate();
            }
        }

        
    }
}

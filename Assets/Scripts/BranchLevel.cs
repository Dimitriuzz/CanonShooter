using UnityEngine;
using UnityEngine.UI;

namespace BallistaShooter
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel rootLevel;
        [SerializeField] private Text pointText;
       [SerializeField]  private int needPoints = 3;

        public bool RootIsActive { get {
                return rootLevel.IsComplete ; } }

        internal void TryActivate()
        {
            gameObject.SetActive(rootLevel.IsComplete);
            Debug.Log("Total Score"+MapCompletion.Instance.TotalScore.ToString()) ;
            if (needPoints>MapCompletion.Instance.TotalScore)
            {
                pointText.text = needPoints.ToString();
                
                
            }
            else
            { 
                pointText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialaise();
            }
            
        }

    }
}

using UnityEngine;
using UnityEngine.Events;

namespace CannonShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }
    
    public class LevelController : MonoSingleton<LevelController>
    {

        
        

        [SerializeField] protected UnityEvent m_EventLevelCompleted;
        
        private ILevelCondition[] m_Conditions;

        private bool m_IsLevelCompleted; 

       

        [SerializeField] protected int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        #region Unity events

        protected void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void Update()
        {
            if (!m_IsLevelCompleted)
            {
                //m_LevelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }
        #endregion
        
        public void LevelCompleted()
        {
            m_EventLevelCompleted?.Invoke();
        }
        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0)
                return;

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            {
                if (v.IsCompleted)
                    numCompleted++;
            }

            if(numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;
                LevelCompleted();

                // Notify level sequence Unit3 code
                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }
    }
}
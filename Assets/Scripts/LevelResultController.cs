using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    
    public class LevelResultController : MonoSingleton<LevelResultController>
    { 

        [SerializeField] private GameObject m_PanelSuccess;
        [SerializeField] private GameObject m_PanelFailure;
        [SerializeField] private GameObject m_PanelResults;

        [SerializeField] private Text m_LevelTime;
        [SerializeField] private Text m_Gold;
        [SerializeField] private Text m_TotalScore;
        [SerializeField] private Text m_TotalKills;
        [SerializeField] private Text m_Record;

        private int timeScore;
        private int killsScore;
        private int goldScore;
        private int score;

        
        public void Show(bool result)
        {
            if (Player.Instance.mode == Player.gameMode.Real)
            {
                Debug.Log(Player.Instance.mode);
                ResetPlayerStats();
                if (m_PanelSuccess.gameObject != null) m_PanelSuccess?.gameObject.SetActive(result);
                if (m_PanelFailure.gameObject != null) m_PanelFailure?.gameObject.SetActive(!result);
                if (m_PanelResults.gameObject != null) m_PanelResults.gameObject.SetActive(true);
                UpdateCurrentLevelStats();
                UpdateVisualStats();
            }
           
        }

       
        public void OnPlayNext()
        {
            LevelSequenceController.Instance.AdvanceLevel();
        }

       
        public void OnRestartLevel()
        {
            LevelSequenceController.Instance.RestartLevel();
        }


        public class Stats
        {
            public int numKills;
            public int time;
            public int score;
            public int gold;
        }

       
        public static Stats TotalStats { get; private set; }

      
        public static void ResetPlayerStats()
        {
            TotalStats = new Stats();
        }

       
        private void UpdateCurrentLevelStats()
        {
            TotalStats.numKills = Player.Instance.NumKills;
            TotalStats.time = (int)(CSLevelController.Instance.ReferenceTime-Player.Instance.m_CurrentTime);
            TotalStats.gold = Player.Instance.Gold;
            

            killsScore = TotalStats.numKills * 10;
            timeScore = (int)TotalStats.time*2;
            goldScore = TotalStats.gold * 2;

            TotalStats.score = killsScore+timeScore+goldScore;

            

            score=PlayerPrefs.GetInt("Score", 0);

            if (TotalStats.score>score)
            {
                m_Record.text = "Установлен новый рекорд!";
                PlayerPrefs.SetInt("Score", TotalStats.score);
                PlayerPrefs.SetInt("Kills", TotalStats.numKills);
                PlayerPrefs.SetInt("Time", TotalStats.time);
                PlayerPrefs.SetInt("Gold", TotalStats.gold);
            }
            else m_Record.text = "К сожалению рекорд "+score.ToString()+" не побит";

            
        }

      
        private void UpdateVisualStats()
        {
            if (m_LevelTime != null)
            {
                m_LevelTime.text = "Время " + TotalStats.time.ToString() + " счет " + timeScore.ToString();
                m_TotalScore.text = "Общий счет " + TotalStats.score.ToString();
                m_Gold.text = "Осталось золота " + TotalStats.gold.ToString() + " счет " + goldScore.ToString();
                m_TotalKills.text = "Убито врагов " + TotalStats.numKills.ToString() + " счет " + killsScore.ToString();
            }
        }
    }
}
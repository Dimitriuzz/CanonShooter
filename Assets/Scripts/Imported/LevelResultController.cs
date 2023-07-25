using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    /// <summary>
    /// Панель результатов уровня. Должна лежать в каждом уровне без галочки DoNotDestroyOnLoad.
    /// </summary>
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

        /// <summary>
        /// Показываем окошко результатов. Выставляем нужные кнопочки в зависимости от успеха.
        /// </summary>
        /// <param name="result"></param>
        public void Show(bool result)
        {
            /*if (result)
            {
                UpdateCurrentLevelStats();
                UpdateVisualStats();
            }*/
            ResetPlayerStats();
            m_PanelSuccess?.gameObject.SetActive(result);
            m_PanelFailure?.gameObject.SetActive(!result);
            m_PanelResults.gameObject.SetActive(true);
            UpdateCurrentLevelStats();
            UpdateVisualStats();
           
        }

        /// <summary>
        /// Запускаем следующий уровен. Дергается эвентом с кнопки play next.
        /// </summary>
        public void OnPlayNext()
        {
            LevelSequenceController.Instance.AdvanceLevel();
        }

        /// <summary>
        /// Рестарт уровня. Дергается эвентом с кнопки restart в случае фейла уровня.
        /// </summary>
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

        /// <summary>
        /// Общая статистика за эпизод.
        /// </summary>
        public static Stats TotalStats { get; private set; }

        /// <summary>
        /// Сброс общей статистики за эпизод. Вызывается перед началом эпизода.
        /// </summary>
        public static void ResetPlayerStats()
        {
            TotalStats = new Stats();
        }

        /// <summary>
        /// Собирает статистику по текущему уровню.
        /// </summary>
        /// <returns></returns>
        private void UpdateCurrentLevelStats()
        {
            TotalStats.numKills = Player.Instance.NumKills;
            TotalStats.time = (int)(LevelController.Instance.ReferenceTime-Player.Instance.m_CurrentTime);
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

            // бонус за время прохождения.
            //int timeBonus = LevelController.Instance.ReferenceTime - (int)LevelController.Instance.LevelTime;

            //if(timeBonus > 0)
            // TotalStats.score += timeBonus;
        }

        /// <summary>
        /// Метод обновления статов уровня.
        /// </summary>
        private void UpdateVisualStats()
        {
            m_LevelTime.text = "Время "+TotalStats.time.ToString()+" счет "+timeScore.ToString();
            m_TotalScore.text = "Общий счет "+TotalStats.score.ToString();
            m_Gold.text = "Осталось золота " + TotalStats.gold.ToString() + " счет " + goldScore.ToString();
            m_TotalKills.text = "Убито врагов "+TotalStats.numKills.ToString()+ " счет " + killsScore.ToString();
        }
    }
}
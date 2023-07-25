
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CannonShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] Button continueButton;

        private int score;
        private int numKills;
        private int time;
        private int gold;
        [SerializeField] private Text m_LevelTime;
        [SerializeField] private Text m_Gold;
        [SerializeField] private Text m_TotalScore;
        [SerializeField] private Text m_TotalKills;
        public void NewGame()
        {
           
            SceneManager.LoadScene(1);

        }
        

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void ShowRecord()
        {
            score=PlayerPrefs.GetInt("Score", 0);
            numKills=PlayerPrefs.GetInt("Kills", 0);
            time=PlayerPrefs.GetInt("Time", 0);
            gold=PlayerPrefs.GetInt("Gold", 0);
            m_LevelTime.text = "Время " + time.ToString();
            m_TotalScore.text = "Общий счет " + score.ToString();
            m_Gold.text = "Осталось золота " + gold.ToString();
            m_TotalKills.text = "Убито врагов " + numKills.ToString();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
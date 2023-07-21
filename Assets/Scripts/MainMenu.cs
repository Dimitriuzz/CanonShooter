
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BallistaShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] Button continueButton;
        public void NewGame()
        {
            FileHandler.Reset(MapCompletion.filename);
            FileHandler.Reset(Upgrades.filename);
            SceneManager.LoadScene(1);

        }
        private void Start()
        {
            continueButton.interactable= FileHandler.HasFile(MapCompletion.filename);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
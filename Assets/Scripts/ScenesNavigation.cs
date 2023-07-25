using UnityEngine;
using UnityEngine.SceneManagement;

namespace CannonShooter
{

    public class ScenesNavigation : MonoBehaviour
    {
        public enum Scenes { main=0, map=1, intro1=2,level1=3}

        [SerializeField] private Scenes sceneToGo;
        public void GoToMainMenu()
        {
            SceneManager.LoadScene((int)sceneToGo);
        }

    }

}

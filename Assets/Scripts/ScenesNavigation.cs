using UnityEngine;
using UnityEngine.SceneManagement;

namespace CannonShooter
{

    public class ScenesNavigation : MonoBehaviour
    {
        public enum Scenes { main=0, intro1=1,level1=2}

        [SerializeField] private Scenes sceneToGo;
        public void GoToScene(Scenes scenes)
        {
            SceneManager.LoadScene((int)scenes);
        }
       public void GoToScene()
        {
            SceneManager.LoadScene((int)sceneToGo);
        }

    }

}

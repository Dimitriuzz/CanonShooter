using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CannonShooter
{
    public class ChooseBalls : MonoBehaviour
    {
        [SerializeField] private GameObject ballista;

        private Cannon cannon;

        private void Start()
        {
            cannon = ballista.GetComponent<Cannon>();
        }
        public void ChoosePierce()
        {
            cannon.SetBall(0);
        }

        public void ChooseFire()
        {
            cannon.SetBall(1);
        }

        public void ChooseIce()
        {
            cannon.SetBall(2);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallistaShooter
{
    public class ChooseArrow : MonoBehaviour
    {
        [SerializeField] private GameObject ballista;

        private Shoot shoot;

        private void Start()
        {
            shoot = ballista.GetComponent<Shoot>();
        }
        public void ChoosePierce()
        {
            shoot.SetArrow(0);
        }

        public void ChooseFire()
        {
            shoot.SetArrow(1);
        }

        public void ChooseIce()
        {
            shoot.SetArrow(2);
        }


    }
}

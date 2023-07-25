using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CannonShooter
{
    public class CameraShake : MonoSingleton<CameraShake>
    {
        
        public Animator camShake;
        
        public void ShakeCamera()
        {
            camShake.SetTrigger("Shake");
        }    
        
    }
}

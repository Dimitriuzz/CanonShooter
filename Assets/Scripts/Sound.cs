using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CannonShooter
{ 
    public enum Sound
    { 
        BGM=0,
        Arrow=1,
        ArrowHit=2,
        BGM1=3,
        Ice=4,
        IceHit=5,
        Dying=6,
        Damage=7,
        Fire=8,
        FireHit=9



    }

    public static class SoundExtension
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.PlaySound(sound);
        }
    }
}

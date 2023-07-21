using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallistaShooter
{
    public class SoundHook : MonoBehaviour
    {
        public Sound m_Sound;
        public void Play()
        {
            m_Sound.Play();
        }
    }
}

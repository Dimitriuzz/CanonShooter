using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallistaShooter
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private Sound m_Music;
        // Start is called before the first frame update
        void Start()
        {
            SoundPlayer.Instance.PlayMusic(m_Music);

        }


    }
}

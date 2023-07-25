using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CannonShooter
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoSingleton<SoundPlayer>
    {
        private AudioSource m_AS;
        [SerializeField] private Sounds m_Sounds;
        //[SerializeField] private AudioClip m_BGM;
        [SerializeField] private float m_MusicVolume;
        [SerializeField] private float m_SoundVolume;

        private new void Awake()
        {
            base.Awake();
            m_AS = GetComponent<AudioSource>();
            
        }
        public void PlaySound(Sound sound)
        {
            m_AS.PlayOneShot(m_Sounds[sound],m_SoundVolume);
        }

        public void PlayMusic(Sound music)
        {
            Instance.m_AS.clip = m_Sounds[music];
            Instance.m_AS.volume = m_MusicVolume;
            Instance.m_AS.Play();
            

        }

    }
}

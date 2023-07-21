
using UnityEngine;

using System;

namespace BallistaShooter
{

    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        public const string filename = "completiondata.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }
        public static void SaveEpisodeResult(int levelScore)
        {
            if(Instance)
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }

        [SerializeField] private EpisodeScore[] completionData;
        [SerializeField] private int totalScore;

        

        public int TotalScore => totalScore;

        
        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
            UpdateTotalScore();
            
        }

        private void UpdateTotalScore()
        {
            totalScore = 0;
            foreach (var episodeScore in completionData)
            {
                totalScore += episodeScore.score;
            }

        }
       
        public int GetEpisodeScore(Episode m_episode)
        {
           foreach (var data in completionData)
            {
                if (data.episode == m_episode)
                    return data.score;
            }
            return 0;
        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
           foreach (var item in completionData)
            {
                if(item.episode==currentEpisode)
                {
                    if (levelScore > item.score)
                    {
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, completionData);
                        UpdateTotalScore();
                    }
                }
            }
        }
    }
}

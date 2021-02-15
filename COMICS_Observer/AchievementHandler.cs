using System;
using System.IO;
using System.Collections.Generic;

namespace COMICS_Observer
{
    public class AchievementHandler : IObserver
    {
        private const string FileName = "achievements.txt";
        
        private List<Achievement> _unlockedAchievements = new List<Achievement>();
        private int _stepsCounter = 0;

        public AchievementHandler()
        {
            if (File.Exists(FileName))
            {
                string[] stringAchievements = File.ReadAllLines(FileName);
                foreach (string stringAchievement in stringAchievements)
                {
                    Achievement achievement;
                    if (Achievement.TryParse(stringAchievement, out achievement))
                    {
                        _unlockedAchievements.Add(achievement);
                    }
                }
            }
        }
        
        public void OnNotify(EventType eventName, object data)
        {
            if (eventName != EventType.UnlockAchievement)
            {
                return;
            }

            Achievement achievementName = (Achievement) data;

            if (_unlockedAchievements.Contains(achievementName))
            {
                return;
            }

            if (achievementName == Achievement.TenSteps && _stepsCounter < 9)
            {
                _stepsCounter++;
                return;
            }
            
            _unlockedAchievements.Add(achievementName);

            List<string> stringAchievements = new List<string>();
            foreach (Achievement collectedAchievement in _unlockedAchievements)
            {
                stringAchievements.Add(collectedAchievement.ToString());
            }
            File.WriteAllLines(FileName, stringAchievements);

            Console.Clear();
            Console.WriteLine($"Achievement unlocked: {achievementName}");
            Console.ReadKey();
        }
    }
}
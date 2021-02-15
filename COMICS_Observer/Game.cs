using System;
using System.Collections.Generic;

namespace COMICS_Observer
{
    public class Game : IObservable
    {
        private List<IObserver> _observers = new List<IObserver>();
        private int _playerX;
        private int _playerY;
        private bool _inGame;

        public Game(AchievementHandler achievementHandler)
        {
            RegisterObserver(achievementHandler);
        }
        
        public void StartGame()
        {
            _inGame = true;
            
            Notify(EventType.UnlockAchievement, 
                Achievement.StartGame);
            
            Loop();
        }

        private void MovePlayer(int x, int y)
        {
            _playerX += x;
            _playerY += y;

            if (_playerX < 0)
            {
                _playerX = 0;
            }
            else if (_playerX > 10)
            {
                _playerX = 10;
            }

            if (_playerY < 0)
            {
                _playerY = 0;
            }
            else if (_playerY > 10)
            {
                _playerY = 10;
            }
            
            Notify(EventType.UnlockAchievement,
                Achievement.TenSteps);
        }

        private void Loop()
        {
            while (_inGame)
            {
                Render();

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    MovePlayer(0, -1);
                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    MovePlayer(0, 1);
                }

                if (key.Key == ConsoleKey.LeftArrow)
                {
                    MovePlayer(-1, 0);
                }

                if (key.Key == ConsoleKey.RightArrow)
                {
                    MovePlayer(1, 0);
                }

                if (key.Key == ConsoleKey.Escape)
                {
                    _inGame = false;
                }

                if (_playerX == 5 && _playerY == 5)
                {
                    Notify(EventType.UnlockAchievement, 
                        Achievement.PlayerAt5);
                }
            }
        }

        private void Render()
        {
            Console.Clear();
            Console.CursorLeft = _playerX;
            Console.CursorTop = _playerY;
            
            Console.Write("@");
        }

        public void RegisterObserver(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void UnregisterObserver(IObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void Notify(EventType eventName, object data)
        {
            foreach (IObserver observer in _observers)
            {
                observer.OnNotify(eventName, data);
            }
        }
    }
}
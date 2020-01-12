using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using ZerglingRush.Models;

namespace ZerglingRush.ViewModels
{
    public class ZerglingRushViewModel
    {
        public List<Zergling> Zerglings = new List<Zergling>();

        public bool GameIsRunning { get; set; }
        
        public CommandCenter TheCommandCenter { get; set; }

        public void StartGame()
        {
            GameIsRunning = true;
            CreateCommandCenter();
        }

        private void CreateCommandCenter()
        {
            TheCommandCenter = new CommandCenter();
            TheCommandCenter.Health = 1000;
        }

        public void SpawnZergling()
        {
            Zerglings.Add(new Zergling() 
            { 
                Health = 30,
                Location = new Point(335, 800)
            });
        }

        public void AttackCommandCenter(Zergling zergling)
        {
            if (zergling.Location.X >= 330 && zergling.Location.Y >= 45 && 
                zergling.Location.X <= 470 && zergling.Location.Y <= 185)
                TheCommandCenter.Health -= 4;
        }

        public void ProcessClickAt(int x, int y)
        {
            var targetZergling = Zerglings.FirstOrDefault(
                zergling => 
                (x >= zergling.Location.X && x <= zergling.Location.X + 20)
                && (y >= zergling.Location.Y && y <= zergling.Location.Y + 20));

            if (targetZergling != null)
            {
                targetZergling.Health -= 10; 
            }
        }
    }
}
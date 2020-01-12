using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZerglingRush.Models
{
    public class CommandCenter
    {
        public readonly Point Location = new Point(335, 50);
        public readonly int Width = 130;
        public readonly int Height = 130;

        public int Health { get; set; }
    }
}

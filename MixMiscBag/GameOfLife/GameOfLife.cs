using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixMiscBag
{
    class GameOfLife
    {
        private int[, ,] map;
        private volatile bool randomizePoints = false;
        int lengthX, lengthY, animationDelay;
        bool inverted, darkVsLight, multiColor;
        string aliveChar = "■";
        ConsoleColor versusColor = ConsoleColor.Green;
        ConsoleColor mainColor = ConsoleColor.Red;
        int multiColorPulse = 0;


        public GameOfLife(string[] input)
        {
            int inputLengthX = 100, inputLengthY = 100;
            animationDelay = 25;
            lengthX = Math.Max(inputLengthX, Console.LargestWindowWidth);
            lengthY = Math.Max(inputLengthY, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth - 5, Console.LargestWindowHeight - 5);
            Console.SetWindowPosition(0, 0);
            this.map = new int[lengthX, lengthY, 2];
            for (int y = 0; y < inputLengthX; y++)
            {
                for (int x = 0; x < inputLengthY; x++)
                {
                    map[x, y, 0] = input[y][x] == '#' ? 1 : 0;
                }
            }
            map[0, 0, 0] = 1;
            map[0, lengthY - 1, 0] = 1;
            map[lengthX - 1, 0, 0] = 1;
            map[lengthX - 1, lengthY - 1, 0] = 1;
            updateNeighbourMap(map);
        }

        private void updateNeighbourMap(int[, ,] map)
        {
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    int neighbours = 0;
                    for (int neighbourY = y - 1; neighbourY < y + 2; neighbourY++)
                    {
                        if (neighbourY < 0 || neighbourY >= lengthY) continue;
                        for (int neighbourX = x - 1; neighbourX < x + 2; neighbourX++)
                        {
                            if (neighbourX < 0 || neighbourX >= lengthX) continue;
                            if (neighbourX == x && neighbourY == y) continue;
                            if (map[neighbourX, neighbourY, 0] == 1) neighbours++;
                        }
                    }
                    map[x, y, 1] = neighbours;
                }
            }
        }

        private int countLights(int[, ,] map)
        {
            int total = 0;
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    total += map[x, y, 0];
                }
            }
            return total;
        }

        private int[, ,] calculateNextTick(int[, ,] map)
        {
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    if (map[x, y, 0] == 1)
                    {
                        if (map[x, y, 1] == 2 || map[x, y, 1] == 3)
                        {
                            map[x, y, 0] = 1;
                        }
                        else
                        {
                            map[x, y, 0] = 0;
                        }
                    }
                    else
                    {
                        if (map[x, y, 1] == 3)
                        {
                            map[x, y, 0] = 1;
                        }
                        else
                        {
                            map[x, y, 0] = 0;
                        }
                    }
                }
            }
            updateNeighbourMap(map);
            return map;
        }

        internal void animateGameOfLife(object obj)
        {
            ConsoleColor clr = (ConsoleColor)(new Random().Next(Enum.GetNames(typeof(ConsoleColor)).Length));
            Console.ForegroundColor = clr;
            int iteration = 0;
            while (true)
            {
                iteration++;
                string output = printMap(map, true);
                Console.Write(" q: randomize points, w/e: decrease/increase speed (" + (999 - animationDelay) + "), r: new color, t: " + (inverted ? "on " : "off") + ", y: " + ((darkVsLight) ? "on " : "off") + ", u: " + (multiColor ? "on " : "off") + ", iteration:" + iteration);
                if (randomizePoints)
                {
                    randomizeMap();
                    randomizePoints = false;
                }
                map = calculateNextTick(map);
            }
        }

        private void randomizeMap()
        {
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    if (map[x, y, 1] < 5)
                    {
                        map[x, y, 1] += new Random().Next(4);
                    }
                }
            }
        }

        private string printMap(int[, ,] map, bool printOutput)
        {
            string alive = inverted ? " " : aliveChar;
            string dead = inverted ? aliveChar : " ";
            string retString = "";
            int fittedLengthX = Math.Min(Console.WindowWidth - 2, map.GetLength(0));
            int fittedLengthY = Math.Min(Console.WindowHeight - 2, map.GetLength(1));
            int vs = darkVsLight ? fittedLengthX / 2 : int.MaxValue;
            for (int y = 0; y < fittedLengthY; y++)
            {
                for (int x = 0; x < fittedLengthX; x++)
                {
                    if (map[x, y, 0] == 1)
                    {
                        retString += (x > vs) ? dead : alive;
                    }
                    else
                    {
                        retString += (x > vs) ? alive : dead;
                    }
                }
                retString += "\n";
            }
            if (printOutput)
            {
                System.Threading.Thread.Sleep(animationDelay);
                if (multiColor)
                {
                    multiColorPulse++;
                    if (multiColorPulse > fittedLengthY)
                    {
                        mainColor = versusColor;
                        multiColorPulse = 0;
                        multiColor = false;
                    }
                    Console.ForegroundColor = versusColor;
                    Console.SetCursorPosition(0, 0);
                    int lowerBound = multiColorPulse * fittedLengthX + multiColorPulse;
                    Console.Write(retString.Substring(0, lowerBound));
                    Console.ForegroundColor = mainColor;
                    Console.Write(retString.Substring(lowerBound));
                }
                else
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write(retString);
                }

            }
            return retString;
        }

        internal void commandPressed(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.KeyChar)
            {
                case 'q':
                    randomizePoints = true;
                    break;
                case 'w':
                    animationDelay += 10;
                    break;
                case 'e':
                    animationDelay += (animationDelay < 10) ? -animationDelay : -10;
                    break;
                case 'r':
                    mainColor = (ConsoleColor)(new Random().Next(Enum.GetNames(typeof(ConsoleColor)).Length));
                    Console.ForegroundColor = mainColor;
                    break;
                case 't':
                    inverted = !inverted;
                    break;
                case 'y':
                    darkVsLight = !darkVsLight;
                    break;
                case ' ':
                    aliveChar = "■";
                    break;
                case 'u':
                    multiColor = !multiColor;
                    multiColorPulse = 0;
                    versusColor = (ConsoleColor)(new Random().Next(Enum.GetNames(typeof(ConsoleColor)).Length));
                    break;
                default:
                    aliveChar = consoleKeyInfo.KeyChar + "";
                    break;
            }
        }
    }
}

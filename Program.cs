using System;
using System.Collections.Generic;
using System.Threading;

class FallingRocks
{
    struct Unit
    {
        public int x;
        public int y;
        public ConsoleColor color;
        public char symbol;
    }
    static void Main()
    {
        Console.SetWindowSize(60, 30);
        Console.SetBufferSize(60, 30);
        char[] symbols = { '^', '@', '*', '&', '+', '%', '$', '#', '!', '.', ';' };
        String[] colorNames = ConsoleColor.GetNames(typeof(ConsoleColor));
        int numColors = colorNames.Length;
        List<Unit> RocksList = new List<Unit>();
        string player = "(0)";
        int playerPosX = Console.WindowWidth / 2;
        int playerPosY = 26;
        ulong score = 0;
        byte lives = 3;
        Random rnd = new Random();
        ConsoleKey rightArrow = ConsoleKey.RightArrow;
        ConsoleKey leftArrow = ConsoleKey.LeftArrow;
        while (true)
        {
            Thread.Sleep(150);
            Console.Clear();
            for (int i = 0; i < rnd.Next(1, 3); i++)
            {
                Unit newInitRock = new Unit();
                newInitRock.x = rnd.Next(59);
                newInitRock.y = 0;
                newInitRock.color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorNames[rnd.Next(numColors)]);
                if (newInitRock.color == ConsoleColor.Black)
                {
                    newInitRock.color = ConsoleColor.Green;
                }
                else
                {
                    newInitRock.color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorNames[rnd.Next(numColors)]);
                }
                newInitRock.symbol = symbols[rnd.Next(symbols.Length)];
                RocksList.Add(newInitRock);
            }
            List<Unit> newList = new List<Unit>();
            for (int i = 0; i < RocksList.Count; i++)
            {
                Unit oldRock = RocksList[i];
                Unit NewMovedRock = new Unit();
                NewMovedRock.x = oldRock.x;
                NewMovedRock.y = oldRock.y + 1;
                NewMovedRock.color = oldRock.color;
                NewMovedRock.symbol = oldRock.symbol;
                if (NewMovedRock.x >= playerPosX && NewMovedRock.x < playerPosX + 3 && NewMovedRock.y == playerPosY && lives > 0)
                {
                    lives--;
                }
                if (NewMovedRock.y < 27)
                {
                    newList.Add(NewMovedRock);
                }
            }
            RocksList = newList;
            foreach (Unit rock in RocksList)
            {
                Console.SetCursorPosition(rock.x, rock.y);
                Console.ForegroundColor = rock.color;
                Console.Write(rock.symbol);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(playerPosX, playerPosY);
            Console.Write(player);
            Console.SetCursorPosition(0, 27);
            Console.Write(new String('-', 60));
            Console.SetCursorPosition(0, 28);
            Console.Write("lives : " + lives);
            Console.SetCursorPosition(0, 29);
            Console.Write("score : " + score);
            if (Console.KeyAvailable && playerPosX <= 57 && playerPosX >= 0)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                while (Console.KeyAvailable) { Console.ReadKey(true); }
                if (key.Key == rightArrow && playerPosX < 57)
                {
                    playerPosX += 1;
                }
                if (key.Key == leftArrow && playerPosX > 0)
                {
                    playerPosX -= 1;
                }
            }
            if (lives == 0)
            {
                break;
            }
            score++;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(0, 28);
        Console.WriteLine("Game Over");
        Console.WriteLine("Final score : " + score);
    }
}
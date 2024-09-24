using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        private const int Width = 20;
        private const int Height = 20;
        private const int InitialSnakeLength = 3;
        private bool gameOver;
        private Direction currentDirection;
        private List<Position> snake;
        private Position food;
        private Random random;

        public Game()
        {
            random = new Random();
            snake = new List<Position>();
            currentDirection = Direction.Right;
            gameOver = false;
            InitializeSnake();
            GenerateFood();
        }

        public void Start()
        {
            while (!gameOver)
            {
                Draw();
                Input();
                Logic();
                Thread.Sleep(100);
            }

            Console.Clear();
            Console.SetCursorPosition(Width / 2 - 5, Height / 2);
            Console.WriteLine("Game Over!");
        }

        private void InitializeSnake()
        {
            for (int i = 0; i < InitialSnakeLength; i++)
            {
                snake.Add(new Position(Width / 2 - i, Height / 2));
            }
        }

        private void GenerateFood()
        {
            food = new Position(random.Next(0, Width), random.Next(0, Height));
        }

        private void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y <= Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    if (x == 0 || x == Width || y == 0 || y == Height)
                    {
                        Console.Write("#");
                    }
                    else if (snake.Any(s => s.X == x && s.Y == y))
                    {
                        Console.Write("O");
                    }
                    else if (food.X == x && food.Y == y)
                    {
                        Console.Write("F");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        private void Input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.W:
                        if (currentDirection != Direction.Down) currentDirection = Direction.Up;
                        break;
                    case ConsoleKey.S:
                        if (currentDirection != Direction.Up) currentDirection = Direction.Down;
                        break;
                    case ConsoleKey.A:
                        if (currentDirection != Direction.Right) currentDirection = Direction.Left;
                        break;
                    case ConsoleKey.D:
                        if (currentDirection != Direction.Left) currentDirection = Direction.Right;
                        break;
                }
            }
        }

        private void Logic()
        {
            Position head = snake.First();
            Position newHead = new Position(head.X, head.Y);

            switch (currentDirection)
            {
                case Direction.Up:
                    newHead.Y--;
                    break;
                case Direction.Down:
                    newHead.Y++;
                    break;
                case Direction.Left:
                    newHead.X--;
                    break;
                case Direction.Right:
                    newHead.X++;
                    break;
            }

            if (newHead.X >= Width || newHead.X < 0 || newHead.Y >= Height || newHead.Y < 0 || snake.Skip(1).Any(s => s.X == newHead.X && s.Y == newHead.Y))
            {
                gameOver = true;
                return;
            }

            snake.Insert(0, newHead);

            if (newHead.X == food.X && newHead.Y == food.Y)
            {
                GenerateFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private struct Position
        {
            public int X;
            public int Y;

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}

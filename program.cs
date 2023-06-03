using System;
using System.Threading;

namespace  PongGame
{
    class program
    {
        static int width = 40;
        static int height = 20;
        static int paddleSize = 4;
        static int paddle1Y = 0;
        static int paddle2Y = 0;
        static int ballX = 0;
        static int ballY = 0;
        static int ballSpeedX = 1;
        static int ballSpeedY = 1;

        static bool running = true;

        static void Main(string[] args)
        {
            Console.Title = "Pong Game";
            Console.CursorVisible = false;
            Console.SetWindowSize(width + 1, height + 1);
            Console.SetBufferSize(width + 1, height + 1);

            paddle1Y = height / 2 - paddleSize / 2;
            paddle2Y = height / 2 - paddleSize / 2;
            ballX = width / 2;
            ballY = height / 2;

            Thread inputThread = new Thread(ProcessInput);
            inputThread.Start();

            while (running)
            {
                Update();
                Render();
                Thread.Sleep(50);
            }

            inputThread.Join();
        }

        static void ProcessInput()
        {
            while (running)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.W && paddle1Y > 0)
                        paddle1Y-- ;
                    if (key.Key == ConsoleKey.S && paddle1Y < height - paddleSize)
                        paddle1Y++ ;
                    if (key.Key == ConsoleKey.UpArrow && paddle2Y > 0)
                        paddle2Y-- ;
                    if (key.Key == ConsoleKey.DownArrow && paddle2Y < height - paddleSize)
                        paddle2Y++ ;
                }
            }
        }

        static void Update()
        {
            ballX += ballSpeedX;
            ballY += ballSpeedY;

            if (ballX == 0 || ballX == width)
            {
                ballSpeedX *= -1;
            }
            if (ballY == 0 || ballY == height - 1)
            {
                ballSpeedY *= -1;
            }
            if (ballX == 1 && ballY >= paddle1Y && ballY < paddle1Y + paddleSize)
            {
                ballSpeedX = 1;
            }
            if (ballX == width - 1 && ballY >= paddle2Y && ballY < paddle2Y + paddleSize)
            {
                ballSpeedX = -1;
            }
        }

        static void Render()
        {
            Console.Clear();

            for (int i = 0; i <= height; i++)
            {
                Console.SetCursorPosition(width, i);
                Console.Write("|");
            }

            for (int i = 0; i < paddleSize; i++)
            {
                Console.SetCursorPosition(0, paddle1Y + i);
                Console.Write("#");
                Console.SetCursorPosition(width - 1, paddle2Y + i);
                Console.Write("#");
            }

            Console.SetCursorPosition(ballX, ballY);
            Console.Write("0");

            Console.SetCursorPosition(width / 2 - 8. height + 1);
            Console.Write("Use W/S and Up/Down arrows to control the paddles");

            Console.SetCursorPosition(width / 2 - 4, 0);
            Console.Write("PONG");
        }
    }
}

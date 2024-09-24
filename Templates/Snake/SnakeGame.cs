using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class SnakeGame : Form
{
    private Timer timer;
    private List<Point> snake;
    private Point food;
    private int direction;
    private Random random;

    public SnakeGame()
    {
        this.DoubleBuffered = true;
        this.Width = 400;
        this.Height = 400;

        snake = new List<Point> { new Point(5, 5) };
        random = new Random();
        food = GenerateFood();
        direction = 1;

        timer = new Timer();
        timer.Interval = 100;
        timer.Tick += new EventHandler(Update);
        timer.Start();

        this.KeyDown += new KeyEventHandler(OnKeyDown);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        foreach (var point in snake)
            e.Graphics.FillRectangle(Brushes.Green, point.X * 10, point.Y * 10, 10, 10);

        e.Graphics.FillRectangle(Brushes.Red, food.X * 10, food.Y * 10, 10, 10);
    }

    private void Update(object sender, EventArgs e)
    {
        Point head = snake[0];
        Point newHead = head;

        switch (direction)
        {
            case 1: newHead.X++; break;
            case 2: newHead.Y++; break;
            case 3: newHead.X--; break;
            case 4: newHead.Y--; break;
        }

        snake.Insert(0, newHead);
        
        if (newHead == food)
        {
            food = GenerateFood();
        }
        else
        {
            snake.RemoveAt(snake.Count - 1);
        }

        Invalidate();
    }

    private Point GenerateFood()
    {
        return new Point(random.Next(0, this.Width / 10), random.Next(0, this.Height / 10));
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Right && direction != 3) direction = 1;
        else if (e.KeyCode == Keys.Down && direction != 4) direction = 2;
        else if (e.KeyCode == Keys.Left && direction != 1) direction = 3;
        else if (e.KeyCode == Keys.Up && direction != 2) direction = 4;
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new SnakeGame());
    }
}

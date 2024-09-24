using System;
using System.Windows.Forms;

public class PongGame : Form
{
    private Timer timer;
    private int ballXSpeed = 5, ballYSpeed = 5;
    private int ballX = 200, ballY = 200;
    private int paddle1Y = 150, paddle2Y = 150;

    public PongGame()
    {
        this.DoubleBuffered = true;
        timer = new Timer();
        timer.Interval = 20;
        timer.Tick += new EventHandler(Update);
        timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(Brushes.Black, 20, paddle1Y, 10, 100);
        e.Graphics.FillRectangle(Brushes.Black, this.Width - 30, paddle2Y, 10, 100);
        e.Graphics.FillEllipse(Brushes.Red, ballX, ballY, 20, 20);
    }

    private void Update(object sender, EventArgs e)
    {
        ballX += ballXSpeed;
        ballY += ballYSpeed;

        if (ballY <= 0 || ballY >= this.Height - 20) ballYSpeed = -ballYSpeed;
        if (ballX <= 30 && ballY >= paddle1Y && ballY <= paddle1Y + 100) ballXSpeed = -ballXSpeed;
        if (ballX >= this.Width - 50 && ballY >= paddle2Y && ballY <= paddle2Y + 100) ballXSpeed = -ballXSpeed;

        if (ballX <= 0 || ballX >= this.Width) ResetBall();

        Invalidate();
    }

    private void ResetBall()
    {
        ballX = 200;
        ballY = 200;
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new PongGame());
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

public class PlatformGame : Form
{
    private Timer timer;
    private int playerX = 50, playerY = 300;
    private bool isJumping = false;
    private int jumpSpeed = 0;

    public PlatformGame()
    {
        this.DoubleBuffered = true;
        this.Width = 400;
        this.Height = 400;

        timer = new Timer();
        timer.Interval = 20;
        timer.Tick += new EventHandler(Update);
        timer.Start();

        this.KeyDown += new KeyEventHandler(OnKeyDown);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(Brushes.Blue, playerX, playerY, 20, 20);
        e.Graphics.FillRectangle(Brushes.Brown, 0, 380, this.Width, 20); 
    }

    private void Update(object sender, EventArgs e)
    {
        if (isJumping)
        {
            playerY -= jumpSpeed;
            jumpSpeed -= 1;
            if (playerY >= 300)
            {
                playerY = 300;
                isJumping = false;
            }
        }
        Invalidate();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space && !isJumping)
        {
            isJumping = true;
            jumpSpeed = 15;
        }
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new PlatformGame());
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongSpiel
{
    public class PongForm : Form
    {
        private int ballXSpeed = 5; // Geschwindigkeit des Balls in X-Richtung
        private int ballYSpeed = 5; // Geschwindigkeit des Balls in Y-Richtung
        private int ballSize = 20; // Größe des Balls
        private Rectangle ball; // Rechteck, das den Ball darstellt
        private Rectangle playerPaddle; // Rechteck für das Spieler-Paddle
        private Rectangle enemyPaddle; // Rechteck für das Gegner-Paddle
        private int playerScore = 0; // Punktestand des Spielers
        private int enemyScore = 0; // Punktestand des Gegners
        private const int PaddleWidth = 10; // Breite der Paddles
        private const int PaddleHeight = 100; // Höhe der Paddles

        public PongForm()
        {
            this.DoubleBuffered = true; // Verhindert Flickern
            this.ClientSize = new Size(800, 600); // Setze die Größe des Fensters
            ball = new Rectangle(ClientSize.Width / 2 - ballSize / 2, ClientSize.Height / 2 - ballSize / 2, ballSize, ballSize);
            playerPaddle = new Rectangle(20, ClientSize.Height / 2 - PaddleHeight / 2, PaddleWidth, PaddleHeight);
            enemyPaddle = new Rectangle(ClientSize.Width - 40, ClientSize.Height / 2 - PaddleHeight / 2, PaddleWidth, PaddleHeight);

            Timer timer = new Timer();
            timer.Interval = 20; // Millisekunden
            timer.Tick += new EventHandler(GameLoop); // Event-Handler für den Timer
            timer.Start();

            this.Paint += new PaintEventHandler(OnPaint); // Event-Handler für das Zeichnen
            this.KeyDown += new KeyEventHandler(OnKeyDownHandler); // Event-Handler für Tasteneingaben
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            // Zeichne Ball und Paddles
            e.Graphics.FillEllipse(Brushes.White, ball);
            e.Graphics.FillRectangle(Brushes.White, playerPaddle);
            e.Graphics.FillRectangle(Brushes.White, enemyPaddle);
            // Zeichne Punktestände
            e.Graphics.DrawString($"Player: {playerScore}", this.Font, Brushes.White, 10, 10);
            e.Graphics.DrawString($"Enemy: {enemyScore}", this.Font, Brushes.White, ClientSize.Width - 100, 10);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // Bewegung des Balls
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            // Kollision mit der oberen und unteren Wand
            if (ball.Top <= 0 || ball.Bottom >= ClientSize.Height)
            {
                ballYSpeed = -ballYSpeed; // Richtung umkehren
            }

            // Kollision mit den Paddles
            if (ball.IntersectsWith(playerPaddle) || ball.IntersectsWith(enemyPaddle))
            {
                ballXSpeed = -ballXSpeed; // Richtung umkehren
            }

            // Punktestand aktualisieren
            if (ball.Left <= 0)
            {
                enemyScore++; // Punkt für den Gegner
                ResetBall(); // Ball zurücksetzen
            }
            else if (ball.Right >= ClientSize.Width)
            {
                playerScore++; // Punkt für den Spieler
                ResetBall(); // Ball zurücksetzen
            }

            Invalidate(); // Neuzeichnen der Form
        }

        private void ResetBall()
        {
            ball.X = ClientSize.Width / 2 - ballSize / 2; // Setze den Ball in die Mitte
            ball.Y = ClientSize.Height / 2 - ballSize / 2;
            ballXSpeed = -ballXSpeed; // Richtung umkehren
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e) // Event-Handler für Tasteneingaben
        {
            // Steuerung des Spieler-Paddles
            if (e.KeyCode == Keys.Up && playerPaddle.Top > 0)
            {
                playerPaddle.Y -= 10; // Paddle nach oben bewegen
            }
            else if (e.KeyCode == Keys.Down && playerPaddle.Bottom < ClientSize.Height)
            {
                playerPaddle.Y += 10; // Paddle nach unten bewegen
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles(); // Visual Styles aktivieren
            Application.SetCompatibleTextRenderingDefault(false); // Kompatibilität setzen
            Application.Run(new PongForm()); // Hauptformular starten
        }
    }
}

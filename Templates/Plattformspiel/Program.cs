using System;
using System.Collections.Generic; // Für die Verwendung von Listen
using System.Drawing;
using System.Windows.Forms;

public class PlatformGame : Form
{
    // Timer für die Spielaktualisierung
    private Timer timer;
    // Spielerposition
    private int playerX = 50, playerY = 300; 
    // Ist der Spieler am Springen?
    private bool isJumping = false; 
    // Geschwindigkeit des Sprungs
    private int jumpSpeed = 0; 
    // Y-Position des Bodens
    private const int groundY = 300;

    // Plattformen (x, y, width, height) - Liste von Plattformen
    private List<Rectangle> platforms = new List<Rectangle>
    {
        new Rectangle(50, 250, 100, 10), // Erste Plattform
        new Rectangle(200, 200, 100, 10), // Zweite Plattform
        new Rectangle(100, 150, 100, 10) // Dritte Plattform
    };

    // Hindernisse (x, y, width, height) - Liste von Hindernissen
    private List<Rectangle> obstacles = new List<Rectangle>
    {
        new Rectangle(150, 270, 20, 20), // Erstes Hindernis
        new Rectangle(300, 260, 20, 20) // Zweites Hindernis
    };

    // Punkte (Münzen) - Liste von Münzen
    private List<Rectangle> coins = new List<Rectangle>
    {
        new Rectangle(80, 230, 10, 10), // Erste Münze
        new Rectangle(210, 180, 10, 10) // Zweite Münze
    };

    private int score = 0; // Spielerpunkte

    // Konstruktor der Plattformspiel-Klasse
    public PlatformGame()
    {
        this.DoubleBuffered = true; // Aktiviert Double Buffering für ein flüssigeres Rendering
        this.Width = 400; // Breite des Fensters
        this.Height = 400; // Höhe des Fensters

        // Timer initialisieren
        timer = new Timer();
        timer.Interval = 20; // Update-Intervall in Millisekunden
        timer.Tick += new EventHandler(Update); // Event-Handler, der die Update-Methode bei jedem Tick aufruft
        timer.Start(); // Timer starten

        // Event-Handler für Tasteneingaben
        this.KeyDown += new KeyEventHandler(OnKeyDown);
        this.KeyUp += new KeyEventHandler(OnKeyUp); // Event-Handler für Tastentasten loslassen
    }

    // Spielerbewegung in horizontaler Richtung
    private bool moveLeft = false; // Flag, um zu überprüfen, ob nach links bewegt wird
    private bool moveRight = false; // Flag, um zu überprüfen, ob nach rechts bewegt wird
    private const int moveSpeed = 5; // Geschwindigkeit der seitlichen Bewegung

    // Methode zum Zeichnen von Grafiken auf dem Formular
    protected override void OnPaint(PaintEventArgs e)
    {
        // Spieler als blauen Quadrat zeichnen
        e.Graphics.FillRectangle(Brushes.Blue, playerX, playerY, 20, 20);
        // Boden als braunes Rechteck zeichnen
        e.Graphics.FillRectangle(Brushes.Brown, 0, groundY, this.Width, 20);
        
        // Plattformen zeichnen
        foreach (var platform in platforms)
        {
            e.Graphics.FillRectangle(Brushes.Gray, platform); // Plattformen grau färben
        }

        // Hindernisse zeichnen
        foreach (var obstacle in obstacles)
        {
            e.Graphics.FillRectangle(Brushes.Red, obstacle); // Hindernisse rot färben
        }

        // Münzen zeichnen
        foreach (var coin in coins)
        {
            e.Graphics.FillRectangle(Brushes.Gold, coin); // Münzen gold färben
        }

        // Punktestand zeichnen
        e.Graphics.DrawString($"Score: {score}", new Font("Arial", 12), Brushes.Black, 10, 10); // Aktuellen Punktestand anzeigen
    }

    // Update-Methode, die bei jedem Timer-Tick aufgerufen wird
    private void Update(object sender, EventArgs e)
    {
        // Seitliche Bewegung
        if (moveLeft)
        {
            playerX -= moveSpeed; // Spieler nach links bewegen
        }
        if (moveRight)
        {
            playerX += moveSpeed; // Spieler nach rechts bewegen
        }

        if (isJumping)
        {
            // Wenn der Spieler springt
            playerY -= jumpSpeed; // Spieler nach oben bewegen
            jumpSpeed -= 1; // Gravitationseffekt

            // Überprüfen, ob der Spieler den Boden erreicht hat
            if (playerY >= groundY)
            {
                playerY = groundY; // Setzt die Y-Position des Spielers auf den Boden
                isJumping = false; // Setzt das Springen auf false
            }

            // Überprüfen auf Plattformen
            foreach (var platform in platforms)
            {
                // Überprüfen, ob der Spieler auf der Plattform landet
                if (playerX + 20 > platform.X && playerX < platform.X + platform.Width &&
                    playerY + 20 > platform.Y && playerY + 20 < platform.Y + platform.Height)
                {
                    playerY = platform.Y - 20; // Spieler auf der Plattform landen
                    isJumping = false; // Springen beenden
                }
            }
        }
        else
        {
            // Wenn der Spieler nicht springt, überprüfe, ob er Münzen sammelt
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                // Überprüfen, ob der Spieler die Münze berührt
                if (playerX + 20 > coins[i].X && playerX < coins[i].X + coins[i].Width &&
                    playerY + 20 > coins[i].Y && playerY + 20 < coins[i].Y + coins[i].Height)
                {
                    coins.RemoveAt(i); // Münze sammeln
                    score += 10; // Punktestand erhöhen
                }
            }
        }

        // Überprüfen auf Kollision mit Hindernissen
        foreach (var obstacle in obstacles)
        {
            // Überprüfen, ob der Spieler mit einem Hindernis kollidiert
            if (playerX + 20 > obstacle.X && playerX < obstacle.X + obstacle.Width &&
                playerY + 20 > obstacle.Y && playerY + 20 < obstacle.Y + obstacle.Height)
            {
                // Game Over Logik, wenn der Spieler ein Hindernis berührt
                MessageBox.Show("Game Over! Du bist mit einem Hindernis kollidiert.");
                Application.Exit(); // Beendet das Spiel
            }
        }

        Invalidate(); // Fordert eine Neuzeichnung des Formulars an
    }

    // Event-Handler für Tasteneingaben
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        // Überprüfen, ob die Leertaste gedrückt wurde und der Spieler nicht bereits springt
        if (e.KeyCode == Keys.Space && !isJumping)
        {
            isJumping = true; // Aktiviert den Sprung
            jumpSpeed = 15; // Setzt die Sprunggeschwindigkeit (höherer Wert = höherer Sprung)
        }

        // Überprüfen auf seitliche Bewegungen
        if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
        {
            moveLeft = true; // Aktiviert die Bewegung nach links
        }
        if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
        {
            moveRight = true; // Aktiviert die Bewegung nach rechts
        }
    }

    // Event-Handler für Tasteneingaben, wenn die Taste losgelassen wird
    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        // Deaktiviert die seitliche Bewegung, wenn die Tasten losgelassen werden
        if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
        {
            moveLeft = false; // Deaktiviert die Bewegung nach links
        }
        if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
        {
            moveRight = false; // Deaktiviert die Bewegung nach rechts
        }
    }

    // Einstiegspunkt der Anwendung
    [STAThread]
    public static void Main()
    {
        // Startet die Anwendung mit einer Instanz von PlatformGame
        Application.Run(new PlatformGame());
    }
}

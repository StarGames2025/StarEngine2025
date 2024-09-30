using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class SnakeGame : Form
{
    private Timer timer; // Timer zur Steuerung der Spielaktualisierungen
    private List<Point> snake; // Liste von Punkten, die die Segmente der Schlange darstellen
    private Point food; // Punkt, der die Position des Essens darstellt
    private int direction; // Aktuelle Bewegungsrichtung der Schlange
    private Random random; // Zufallszahlengenerator für die Platzierung des Essens

    public SnakeGame()
    {
        this.DoubleBuffered = true; // Reduziert Flackern beim Zeichnen
        this.Width = 400; // Breite des Spielfensters
        this.Height = 400; // Höhe des Spielfensters

        snake = new List<Point> { new Point(5, 5) }; // Initialisiert die Schlange mit einem Segment an Position (5, 5)
        random = new Random(); // Initialisiert den Zufallszahlengenerator
        food = GenerateFood(); // Generiert das erste Essen an einer zufälligen Position
        direction = 1; // Setzt die Anfangsrichtung auf rechts

        timer = new Timer(); // Erstellt einen neuen Timer
        timer.Interval = 100; // Setzt das Intervall auf 100 Millisekunden
        timer.Tick += new EventHandler(Update); // Registriert die Update-Methode, die bei jedem Tick aufgerufen wird
        timer.Start(); // Startet den Timer

        this.KeyDown += new KeyEventHandler(OnKeyDown); // Registriert die OnKeyDown-Methode für Tasteneingaben
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        // Zeichnet die Schlange auf das Spielfeld
        foreach (var point in snake)
            e.Graphics.FillRectangle(Brushes.Green, point.X * 10, point.Y * 10, 10, 10); // Zeichnet jedes Segment der Schlange

        e.Graphics.FillRectangle(Brushes.Red, food.X * 10, food.Y * 10, 10, 10); // Zeichnet das Essen in Rot
    }

    private void Update(object sender, EventArgs e)
    {
        Point head = snake[0]; // Holt den Kopf der Schlange
        Point newHead = head; // Initialisiert den neuen Kopf mit der aktuellen Position

        // Bestimmt die neue Kopfposition basierend auf der aktuellen Richtung
        switch (direction)
        {
            case 1: newHead.X++; break; // Bewegt den Kopf nach rechts
            case 2: newHead.Y++; break; // Bewegt den Kopf nach unten
            case 3: newHead.X--; break; // Bewegt den Kopf nach links
            case 4: newHead.Y--; break; // Bewegt den Kopf nach oben
        }

        snake.Insert(0, newHead); // Fügt den neuen Kopf am Anfang der Schlange hinzu
        
        // Überprüft, ob der neue Kopf die Position des Essens erreicht hat
        if (newHead == food)
        {
            food = GenerateFood(); // Generiert neues Essen, wenn die Schlange frisst
        }
        else
        {
            snake.RemoveAt(snake.Count - 1); // Entfernt das letzte Segment der Schlange, wenn kein Essen gefressen wurde
        }

        Invalidate(); // Fordert eine Neuzeichnung des Fensters an
    }

    private Point GenerateFood()
    {
        // Generiert eine zufällige Position für das Essen im Spielfeld
        return new Point(random.Next(0, this.Width / 10), random.Next(0, this.Height / 10));
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        // Behandelt die Tasteneingaben zur Steuerung der Schlange
        if (e.KeyCode == Keys.Right && direction != 3) direction = 1; // Rechts
        else if (e.KeyCode == Keys.Down && direction != 4) direction = 2; // Unten
        else if (e.KeyCode == Keys.Left && direction != 1) direction = 3; // Links
        else if (e.KeyCode == Keys.Up && direction != 2) direction = 4; // Oben
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new SnakeGame()); // Startet die Anwendung
    }
}

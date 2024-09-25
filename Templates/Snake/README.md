# Snake Game

Ein einfaches Snake-Spiel, das in C# unter Verwendung von Windows Forms entwickelt wurde. Die Schlange bewegt sich auf dem Bildschirm und wächst, wenn sie das Essen erreicht.

## Anforderungen

- .NET Framework oder Mono (für die Kompilierung unter Linux)
- Ein Texteditor oder eine IDE (z.B. Visual Studio, Visual Studio Code)

## Kompilierung

Mit dem folgenden Befehl kannst du das Spiel in eine `.exe`-Datei kompilieren:

```bash
mcs -target:winexe -out:SnakeGame.exe SnakeGame.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll
```

### Erklärung der Flags
- `-target:winexe`: Kompiliert die Anwendung als Windows-Anwendung (GUI).
- `-out:SnakeGame.exe`: Legt den Namen der Ausgabedatei fest.
- `-r:System.Windows.Forms.dll`: Referenziert die Windows Forms-Bibliothek.
- `-r:System.Drawing.dll`: Referenziert die Drawing-Bibliothek für die Grafikdarstellung.

## Start

Um das Spiel zu starten, benutze den folgenden Befehl:

```bash
./SnakeGame.exe
```

## Steuerung

- **Pfeiltasten**: Bewege die Schlange in die entsprechende Richtung:
  - **Rechts**: Schlange bewegt sich nach rechts
  - **Links**: Schlange bewegt sich nach links
  - **Oben**: Schlange bewegt sich nach oben
  - **Unten**: Schlange bewegt sich nach unten

## Spielregeln

- Die Schlange beginnt mit einer Länge von einem Segment und wächst um ein Segment, wenn sie das rote Essen erreicht.
- Das Spiel endet, wenn die Schlange gegen sich selbst oder die Wand stößt (kann in Zukunft implementiert werden).
- Ziel ist es, so viel Essen wie möglich zu sammeln und die maximale Länge der Schlange zu erreichen.

## Lizenz

Dieses Projekt steht unter der MIT-Lizenz. Siehe die LICENSE-Datei für weitere Informationen.
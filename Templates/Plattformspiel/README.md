# Plattformspiel

Ein einfaches Plattformspiel in C# mit grundlegenden Spielmechaniken wie Springen, seitlicher Bewegung und dem Sammeln von Punkten.

## Voraussetzungen

Stelle sicher, dass du die Mono-Entwicklungsumgebung installiert hast, um C#-Programme kompilieren und ausführen zu können.

## Compile

Mit dem folgenden Befehl kannst du das Spiel in eine `.exe`-Datei kompilieren:

```bash
mcs -target:winexe -out:Plattformspiel.exe Plattformspiel.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll
```

## Start

Um das Spiel zu starten, benutze den folgenden Befehl:

```bash
./Plattformspiel.exe
```

## Steuerung

- **Bewege nach links**: Drücke die **Pfeiltaste nach links** oder die **A**-Taste.
- **Bewege nach rechts**: Drücke die **Pfeiltaste nach rechts** oder die **D**-Taste.
- **Springen**: Drücke die **Leertaste**.

## Funktionen

- **Plattformen**: Der Spieler kann auf verschiedenen Plattformen landen.
- **Hindernisse**: Der Spieler muss Hindernisse vermeiden.
- **Punkte sammeln**: Münzen können gesammelt werden, um den Punktestand zu erhöhen.

## Hinweise

- Um das Spiel zu beenden, klicke auf das Schließen-Symbol des Fensters oder drücke `Alt + F4`.
- Bei Kollisionen mit Hindernissen erscheint eine "Game Over"-Nachricht, und das Spiel wird beendet.

## Lizenz

Dieses Projekt ist unter der MIT-Lizenz lizenziert. Siehe die `LICENSE`-Datei für Details.
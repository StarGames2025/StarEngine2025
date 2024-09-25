# StarEngine2025

**StarEngine2025** ist eine flexible und erweiterbare Game Engine, die es Entwicklern ermöglicht, 2D-Spiele einfach zu erstellen und zu bearbeiten. Die Engine bietet eine benutzerfreundliche Oberfläche zur Codeausführung, Projektverwaltung und Anpassung der Benutzeroberfläche.

## Inhaltsverzeichnis

- [Features](#features)
- [Installation](#installation)
- [Verwendung](#verwendung)
- [Projektstruktur](#projektstruktur)
- [Beitrag leisten](#beitrag-leisten)
- [Lizenz](#lizenz)
- [Kontakt](#kontakt)

## Features

- **Codeausführung**: Kompiliert und führt C#-Code in Echtzeit aus.
- **Projektverwaltung**: Erstellen, Speichern und Laden von Spielprojekten.
- **Einstellungen**: Anpassung von Farbschemata und Schriftarten.
- **Benutzeroberfläche**: Intuitive UI mit Unterstützung für Dunkel- und Hellmodus.
- **Vorlagen**: Unterstützung für vorgefertigte Spielvorlagen.

## Installation

1. Klone das Repository:
   ```bash
   git clone https://github.com/StarGames2025/StarEngine2025.git
   ```
   
2. Navigiere in das Projektverzeichnis:
   ```bash
   cd StarEngine2025
   ```

3. Stelle sicher, dass die notwendigen Abhängigkeiten (z.B. Mono) installiert sind. 

4. Baue das Projekt mit dem folgenden Befehl (Voraussetzung: Mono Compiler muss installiert sein):
   ```bash
   mcs -out:StarEngine2025.exe Program.cs MainForm.cs CodeExecutionLogic.cs EditorLogic.cs ProjectLogic.cs ProjectTreeLogic.cs SettingsLogic.cs TemplateLogic.cs
   ```

5. Starte die Anwendung:
   ```bash
   mono StarEngine2025.exe
   ```

## Verwendung

- **Neues Projekt erstellen**: Wählen Sie im Menü "Datei" die Option "Neues Projekt" und geben Sie den Projektnamen ein.
- **Projekt öffnen**: Um ein bestehendes Projekt zu öffnen, wählen Sie "Projekt öffnen" aus dem Menü.
- **Codeausführung**: Geben Sie den C#-Code in das Textfeld ein und verwenden Sie die Ausführungsoptionen, um den Code zu kompilieren und auszuführen.
- **Einstellungen**: Passen Sie das Erscheinungsbild der Anwendung über das Menü "Einstellungen" an.

## Beitrag leisten

Beiträge sind willkommen! Bitte folgen Sie diesen Schritten:

1. Forken Sie das Repository.
2. Erstellen Sie einen neuen Branch (`git checkout -b feature-xyz`).
3. Nehmen Sie Ihre Änderungen vor und committen Sie diese (`git commit -m 'Add some feature'`).
4. Pushen Sie Ihren Branch (`git push origin feature-xyz`).
5. Erstellen Sie einen Pull-Request.

## Lizenz

Dieses Projekt ist unter der MIT-Lizenz lizenziert. Siehe die [LICENSE](LICENSE) Datei für Details.
---
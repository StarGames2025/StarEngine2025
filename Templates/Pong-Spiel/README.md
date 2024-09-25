# PongSpiel

## Introduction
PongSpiel is a simple implementation of the classic Pong game, developed in C#. The game features a player-controlled paddle and an enemy paddle, with a ball that bounces between them. Players score points by getting the ball past their opponent's paddle.

## Features
- **Two-player gameplay**: Control the player paddle and compete against an AI opponent.
- **Score tracking**: Keep track of player scores displayed on the screen.
- **Responsive controls**: Move the paddle up and down using keyboard input.
- **Smooth graphics**: Double buffering to prevent flickering during rendering.

## Requirements
- .NET Framework or Mono to compile and run the game.

## Compile
To compile the game into an executable, use the following command:
```bash
mcs -target:winexe -out:PongSpiel.exe PongSpiel.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll
```

## Start
To start the game, run the executable:
```bash
./PongSpiel.exe
```

## Controls
- **Up Arrow (↑)**: Move the player paddle up.
- **Down Arrow (↓)**: Move the player paddle down.

## Game Logic
- The game is played in a rectangular window where the ball bounces off the top and bottom walls, as well as the paddles.
- Each player has a paddle that can be moved up or down to hit the ball.
- When the ball passes a paddle, the opposing player scores a point.
- The ball resets to the center after a point is scored.

## Troubleshooting
- **Game does not start**: Ensure that you have the .NET Framework or Mono installed correctly. Check for any compilation errors.
- **Graphics issues**: If you experience flickering, ensure that `DoubleBuffered` is set to `true` in the form constructor.

## Future Improvements
- Add AI for the enemy paddle to provide a challenge.
- Implement sound effects for paddle hits and score events.
- Enhance the graphical interface with more appealing visuals and animations.

## Contribution
Contributions are welcome! Feel free to fork the repository, make improvements, and submit pull requests.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.
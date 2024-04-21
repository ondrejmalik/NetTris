using System;

namespace Tetris.Game.Networking.Commands;

[Serializable]
// convert lastPieceGridPos to cords X,Y separeted by ; also check if null insert empty string
public class PacketCommandGameOver() : PacketCommandBase(PacketCommandType.GameOver, "GameOver")
{
}

using Godot;
using System;

public class background : Sprite
{
    public const int ScreenWidth = 320;
    public const int ScrollSpeed = 20;

    public override void _Process(float delta)
    {
        Position += new Vector2(-ScrollSpeed * delta, (float)0.0);
        if (Position.x <= -ScreenWidth)
        {
            Position += new Vector2(ScreenWidth, (float)0.0);
        }
    }
}

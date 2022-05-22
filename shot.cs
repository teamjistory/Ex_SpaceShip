using Godot;
using System;

public class shot : Area2D
{
   public const int ScreenWidth = 320;
   public const int MoveSpeed = 200;
    
    public override void _Process(float delta)
    {
        Position += new Vector2(MoveSpeed * delta, 0);
        if (Position.x > ScreenWidth + 8)
        {
            QueueFree();
        }
    }  


}

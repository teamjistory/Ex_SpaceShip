using Godot;
using System;

public class shot : Area2D
{
   public const int ScreenWidth = 320;
   public const int MoveSpeed = 150;
    
    public override void _Process(float delta)
    {
        Position += new Vector2(MoveSpeed * delta, ((float)0.0));
        if (Position.x >= ScreenWidth + 8)
        {
            QueueFree();
        }
    }

    public void _on_shot_area_entered(Area2D area2D)
    {

        if (area2D.IsInGroup("asteroid"))
        {
            QueueFree();
        }
    }
    
    


}

using Godot;
using System;

public class player : Area2D
{
    private const int MoveSpeed = 150;
    private const int ScreenWidth = 320;
    private const int ScreenHeight = 180;
   
    public PackedScene ShotScene;

    public override void _Ready()
    {
       
        ShotScene.ResourcePath  = ResourceLoader.Load("res://shot.tscn").ResourcePath;
       
    }

    public override void _Process(float delta)
    {      
        var inputDir = new Vector2();
        if (Input.IsKeyPressed((int) KeyList.Up)) inputDir.y -= 1;
        if (Input.IsKeyPressed((int) KeyList.Down)) inputDir.y += 1;
        if (Input.IsKeyPressed((int) KeyList.Left)) inputDir.x -= 1;
        if (Input.IsKeyPressed((int) KeyList.Right)) inputDir.x += 1;
        
        Position += (delta * MoveSpeed) * inputDir;
        
        if (Position.x < 0.0)
        {
            var position = Position;
            position.x = 0;
            Position = position;
        }
        
        else if (Position.x > ScreenWidth)
        {
            var position = Position;
            position.x = ScreenWidth;
            Position = position;
        }
        
        if (Position.y < 0.0)
        {
            var position = Position;
            position.y = 0;
            Position = position;
        }
        
        else if (Position.y > ScreenHeight)
        {
            var position = Position;
            position.y = ScreenHeight;
            Position = position;
        }

        if (Input.IsKeyPressed((int) KeyList.Space))
        {
            var stageNode = GetParent();
            var shotInstance = (shot)ShotScene.Instance();
          
            shotInstance.Position = Position;
            stageNode.AddChild(shotInstance);
           
            
        }
        
    }
}


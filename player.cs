using Godot;
using System;

public class player : Area2D
{
    private const int MoveSpeed = 50;
    private const int ScreenWidth = 320;
    private const int ScreenHeight = 180;

    public bool canShot = true;
    
    [Signal]
    public delegate void signalDestroyed();
    
   /// [Export]
   /// public PackedScene ShotScene;

    public PackedScene shotScene = (PackedScene) GD.Load("res://shot.tscn");
   
   public PackedScene explosionScene = (PackedScene) GD.Load("res://explosion.tscn");
   
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

        if (Input.IsKeyPressed((int) KeyList.Space)  && canShot)
        {
            var stageNode = GetParent();
            //  var shotInstance = (shot)ShotScene.Instance();

            var shotInstance = shotScene.Instance() as shot;
            var shotInstance2 = shotScene.Instance() as shot;

            shotInstance.Position = Position + new Vector2(9, -5);
            shotInstance2.Position = Position + new Vector2(9, 5);
            
            stageNode.AddChild(shotInstance);
            stageNode.AddChild(shotInstance2);
            
            canShot = false;
            GetNode<Timer>("reload_timer").Start();


        }
       
    }

    public void _on_reload_timer_timeout()
    {
        canShot = true;
    }
    
    

    
    public void _on_player_area_entered(Area2D area2D)
    {
        if (area2D.IsInGroup(("asteroid")))
        {
            var stageNode = GetParent();
            var explosionInstance = shotScene.Instance() as Node2D;
            explosionInstance.Position = Position;
            stageNode.AddChild(explosionInstance);

         EmitSignal(nameof(signalDestroyed));
         QueueFree();

        } 
        
    }  
    
    
}


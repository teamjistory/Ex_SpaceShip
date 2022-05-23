using Godot;
using System;
using System.Xml.Schema;

public class asteroid : Area2D
{
   public PackedScene explosionScene =  GD.Load("res://explosion.tscn") as PackedScene;
   
   public int moveSpeed = 100;
   public bool scoreEmitted = false;

   [Signal]
   public delegate void signalScore();

   public void _on_asteroid_area_entered(Area2D area2D)
   {
      if (area2D.IsInGroup("shot") || area2D.IsInGroup(("player")))
      {
         if (!scoreEmitted)
         {
            scoreEmitted = true;
            EmitSignal(nameof(signalScore));
            QueueFree();
            var stageNode = GetParent<Node>();
            var explosionInstance = (Node2D) explosionScene.Instance();
            explosionInstance.Position = Position;
            stageNode.AddChild(explosionInstance);
            //stageNode.RemoveChild(explosionInstance);
         }
      }
      
   }

  

   public override void _Process(float delta)
   {
      Position -= new Vector2(moveSpeed * delta, 0);
      if(Position.x <= -100) QueueFree();
     
      

   }
}

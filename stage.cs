using Godot;
using System;
using System.Threading;
using Timer = Godot.Timer;

public class stage : Node2D
{
    public PackedScene asteroidScene = (PackedScene) GD.Load("res://asteroid.tscn");
    
    private const int ScreenWidth = 320;
    private const int ScreenHeight = 180;

    public int score = 0;
    public bool isGameOver = false;

    public override void _Ready()
    {
        GD.Randomize();
        GetNode<Area2D>("player").Connect("signalDestroyed", this, "_on_player_signalDestroyed");

        GetNode<Timer>("spawn_timer").Connect("timeout", this, "_on_spawn_timer_timeout");
        
    }


    

    public void _on_player_signalDestroyed()
    {
        GetNode<Label>("ui/retry").Show();
        isGameOver = true;

    }
    
    public void _on_spawn_timer_timeout()
    {
        var asteroidInstance = asteroidScene.Instance() as asteroid;
        asteroidInstance.Position = new Vector2(ScreenWidth + 8, (float) GD.RandRange(0, ScreenHeight));

        asteroidInstance.Connect(("signalScore"), this, "_on_player_score");


    }
    
    
    public void _on_player_score()
    {
        score++;
        GetNode<Label>("ui/score").Text = "Score: " + score.ToString();


    }
    
}

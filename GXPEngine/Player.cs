using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Player : Vehicle
{
    public enum State
    {
        Idle,
        Down,
        Up
    }

    public float lastFuel = 0;

    List<PlayerAnimation> playerAnimations; //Playeranimation object list, one for each state
    PlayerAnimation currentPlayerAnimation; //Player animation object of currently active state
    State currentState; //Currently active state
    float speed = .75f;
    float lastShot;
    bool isInSpecialState = true;

    public Player(int iHealth,string filename,Scene scene) : base(iHealth,filename,scene)
    {
        playerAnimations = new List<PlayerAnimation>();
        alpha = 0;
        rotation = 90;
        health = iHealth;
        CreateChildren();
    }
    public override void Update()
    {
        MovePlayer();
        Shoot();
        UpdateFuel();
        base.Update();
    }

    void CreateChildren()
    {
        PlayerAnimation playerIdle = new PlayerAnimation(CoreParameters.playerPath+"Idle.png", 3, 1, Player.State.Idle);
        playerAnimations.Add(playerIdle);

        PlayerAnimation playerUp = new PlayerAnimation(CoreParameters.playerPath+"Up.png", 6, 1, Player.State.Up);
        playerAnimations.Add(playerUp);

        PlayerAnimation playerDown = new PlayerAnimation(CoreParameters.playerPath+"Down.png", 6, 1, Player.State.Down);
        playerAnimations.Add(playerDown);

        currentPlayerAnimation = playerIdle;
        currentPlayerAnimation.visible = true;

        foreach (PlayerAnimation playerState in playerAnimations)
        {
            AddChild(playerState);
        }
    }

    void MovePlayer()
    {
        if (y > 0+ (width / 2) && Input.GetKey(Key.UP))
        {
            y -= speed * Time.deltaTime;
            SetCurrentState(State.Up);
        } else if (y < 600-(width/2) && Input.GetKey(Key.DOWN))
        {
            y += speed * Time.deltaTime;
            SetCurrentState(State.Down);
        } else
        {
            SetCurrentState(State.Idle);
        }
    }
    public override void Shoot()
    {
        if (Input.GetKey(Key.SPACE) && Time.time > lastShot + CoreParameters.playerFireSpeed)
        {
            if (isInSpecialState)
            {
                scene.AddChild(new Bullet(x, y + height / 2, 0, this));
                scene.AddChild(new Bullet(x, y - height / 2, 0, this));
            }
            scene.AddChild(new Bullet(x, y, 0, this));
            lastShot = Time.time;
        }
    }
    public void UpdateFuel()
    {
        if (Time.time > lastFuel + CoreParameters.maxTimeBetweenFuel)
        {
            health = 0;
            if (health <= 0)
            {
                if (this is Player)
                    Console.WriteLine("thats why");
                    scene.playerAlive = false;
                Destroy();
            }
        }
    }

    void SetCurrentState(State pStateToSwitchTo)
    {
        // If state changed, set new currentPlayerAnimation and deactivate last PlayerAnimation
        if (pStateToSwitchTo != currentState)
        {
            currentPlayerAnimation.visible = false;
            foreach (PlayerAnimation playerAnimation in playerAnimations)
            {
                if (playerAnimation.state == pStateToSwitchTo)
                {
                    currentPlayerAnimation = playerAnimation;
                    currentPlayerAnimation.visible = true;
                    currentPlayerAnimation.SetFrame(0);
                    currentState = currentPlayerAnimation.state;
                }
            }
        }
    }
}

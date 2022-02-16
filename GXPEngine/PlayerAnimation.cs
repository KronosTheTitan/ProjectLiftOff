using System;
using GXPEngine;

class PlayerAnimation : AnimationSprite
{
    //Interface
    private Player.State _state;

    public PlayerAnimation(String pSpritesheet, int pSpritesheetCol, int pSpritesheetRow, Player.State pState) : base(pSpritesheet, pSpritesheetCol, pSpritesheetRow, -1, true, false)
    {
        SetOrigin(width / 2, height / 2);
        SetCycle(0, pSpritesheetCol);
        _state = pState;
        visible = false;
    }

    void Update()
    {
        int clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);
        Animate(CoreParameters.playerAnimationSpeed * clampedDeltaTime);
    }

    //Interface
    public Player.State state
    {
        get => _state;
    }
}

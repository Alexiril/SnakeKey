﻿using Engine.BaseComponents;
using Engine.BaseTypes;

namespace Game.Components.MainMenu;

internal class MoveMainMenuButtonsEntrance : Component
{
    public override void Start() => _startMilliseconds = ActualGameTime.TotalGameTime.TotalMilliseconds;

    public override void Update()
    {
        if (!(ActualGameTime.TotalGameTime.TotalMilliseconds - _startMilliseconds > 2500)) return;
        if (Transform.Position.Y > Master.ViewportCenter.Y + Sprite.Height)
        {
            var delta = DeltaTime * .5f * Transform.GlobalUp;
            for (var i = 3; i < 8; i++) GameObject.GetGameObjectByIndex(i).Transform.Position += delta;
        }
        else GameObject.DestroyComponent(this);
    }

    private double _startMilliseconds;
}

﻿using System;
using System.Collections.Generic;
using Engine.BaseSystems;
using Engine.BaseTypes;
using Microsoft.Xna.Framework;

namespace Engine.BaseComponents;

public class Animator : Component
{
    public bool Playing { get; set; }

    public AnimationInformation AnimationInformation { get; set; }

    public Animator SetPlaying(bool playing)
    {
        Playing = playing;
        return this;
    }

    public Animator SetAnimationInformation(AnimationInformation information)
    {
        AnimationInformation = information;
        return this;
    }

    public Vector2 Size => _objectSprite.Size;

    public int Width => _objectSprite.Width;

    public int Height => _objectSprite.Height;

    public override void Start()
    {
        _objectSprite = Sprite;
        Master.OnBeforeDraw += ResetAnimationFrame;
    }

    public override void Update() => _framesDistance = 1000 / AnimationInformation.Framerate;

    public override void Unload()
    {
        Master.OnBeforeDraw -= ResetAnimationFrame;
        _objectSprite = null;
    }

    protected override List<Type> Requirements => new (){ typeof(Sprite) };

    private void ResetAnimationFrame()
    {
        if (!Playing) return;
        if (_timeFromLastFrame < _framesDistance)
        {
            _timeFromLastFrame += (float)ActualGameTime.ElapsedGameTime.TotalMilliseconds;
            return;
        }
        _currentFrame = (_currentFrame + 1) % AnimationInformation.Frames.Count;
        _objectSprite.Texture = AnimationInformation.Frames[_currentFrame];
        _timeFromLastFrame = 0;
    }

    private int _currentFrame;

    private Sprite _objectSprite;

    private float _framesDistance;

    private float _timeFromLastFrame;

}

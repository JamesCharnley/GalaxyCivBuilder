using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FSpriteInfo
{
    public Sprite Image;
    public Vector2 ImageLocalPosition;
    public Vector2 ImageScale;
    public int ImageLayer;
}
[System.Serializable]
public struct FRenderInfo
{
    public FSpriteInfo[] Images;
    public Vector2 Position;
}

public enum EInteractableType
{
    Planet
}
public abstract class InteractableObject
{
    public DisplayInfo DisplayInformation;
    public Guid OwnerId;
    public EMenuType MenuType;
    public FRenderInfo RenderInfo;
}

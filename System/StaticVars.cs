using UnityEngine;

public enum RaycastState
{
    Inactive,
    Click,
    Drag,
    Swipe
}

public enum RepairStage
{
    Wall,
    Floor,
    Window,
    Paint,
    Mopping,
    Reset
}
public static class CameraStrings
{
    public static string FirstCamera = "FirstCamera";
    public static string SecondCamera = "SecondCamera";
    public static string ThirdCamera = "ThirdCamera";
    public static string FourthCamera = "FourthCamera";
}
public static class PrefKeys
{
    public static string Money = "Money";
}

public static class AnimationHashes
{
    
}
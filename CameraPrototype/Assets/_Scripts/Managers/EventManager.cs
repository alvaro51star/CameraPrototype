using System;

public static class EventManager
{
    //Camara
    public static Action OnUsingCamera;
    public static Action OnNotUsingCamera;
    public static Action OnTakingPhoto;
    public static Action OnRemovePhoto;

    //Gato
    public static Action OnCatPetted;

    //Puerta
    public static Action OnDoorLocked;
    public static Action OnDoorUnLocked;

    //Interactions
    public static Action OnIsReading;
    public static Action OnStopReading;
    public static Action<int> OnAddRoll;

    //Enemies
    public static Action<float, float> OnTimeAdded;
    public static Action<string> OnStatusChange;

    //Level
    public static Action<int> OnLevelIntensityChange;
}

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
    public static Action OnDoorOpened;
    public static Action OnDoorClosed;

    //Interactions
    public static Action OnIsReading;
    public static Action OnStopReading;
    public static Action<int> OnAddRoll;

    //Enemies
    public static Action<float, float> OnTimeAdded;
    public static Action<string> OnStatusChange;
    public static Action OnEnemyRevealed;

    //Level
    public static Action<int> OnLevelIntensityChange;
}

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

    //Interactions
    public static Action OnIsReading;
    public static Action OnStopReading;
    public static Action<int> OnAddRoll;

    //Enemies
    public static Action<float, float> OnTimeAdded;
    public static Action<string> OnStatusChange;
}

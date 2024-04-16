using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    //Camara
    public static Action UsingCamera;
    public static Action NotUsingCamera;
    public static Action TakingPhoto;
    public static Action RemovePhoto;
    public static Action NoPhotosLeft;
    public static Action CanTakePhotosAgain;//para cuando te quedas sin fotos pero coges carrete
    //Gato
    public static Action CatPetted;
    //
    public static Action IsReading;
    public static Action StopReading;
}

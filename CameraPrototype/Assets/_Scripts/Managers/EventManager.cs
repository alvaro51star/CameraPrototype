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

    //Gato
    public static Action CatPetted;
    //
    public static Action IsReading;
    public static Action StopReading;
}

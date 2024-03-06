using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour
{
    //Variables
    private List<Sprite> m_photos;

    public void AddPhoto(Sprite newPhoto)
    {
        m_photos.Add(newPhoto);
    }

    //public Sprite[] GetTandaPhoto(int photoQuantity)
    //{
    //    Sprite[] photos = new Sprite[photoQuantity];
    //    for (int i = 0; i < photoQuantity; i++)
    //    {
    //        photos[i] = m_photos[i];
    //    }
    //}
}

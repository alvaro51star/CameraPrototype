using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using UnityEngine.UI;
using TMPro;
using Directory = System.IO.Directory;
using File = System.IO.File;

//using static UnityEngine.Windows.File;

public class AlbumManager : MonoBehaviour
{
    //Variables
    public static AlbumManager instance;
    private List<Sprite> m_photos = new List<Sprite>();
    
    private int m_numPhotosTaken = 1;
    private string filePath;
    
    [SerializeField] TextMeshProUGUI texto;  
    

    private int index = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        texto.text = Application.persistentDataPath;
        CreateDirectory();
    }

    public void AddPhoto(Sprite newPhoto)
    {
        Texture2D texture = new Texture2D(newPhoto.texture.width, newPhoto.texture.height);
        Rect rect = new Rect(0, 0, newPhoto.texture.width, newPhoto.texture.height);
        var pixels = newPhoto.texture.GetPixels((int)rect.x, (int)rect.y, (int)newPhoto.texture.width, (int)newPhoto.texture.height);
        texture.SetPixels(pixels);
        texture.Apply();

        //Sprite sprite =  Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
        //                                   new Vector2(0.5f, 0.5f), 100);
        //
        //m_photos.Add(sprite);
        
        byte[] byteArray = texture.EncodeToPNG();
        //filePath = Application.persistentDataPath + "/Photo" + SceneManager.GetActiveScene().name + m_numPhotosTaken + ".png";
        //filePath = Application.persistentDataPath+ "/Resources/" + "Fotos" + SceneManager.GetActiveScene().name + "/Photo" + m_numPhotosTaken + ".png";
        File.WriteAllBytes(filePath + "/Photo" + m_numPhotosTaken + ".png", byteArray);
        m_numPhotosTaken++;
    }

    public List<Sprite>  GetTandaPhoto(int pageIndex, int maxPhotoPerPage)
    {
        List<Sprite> sprites = new List<Sprite>();
        int photoIndex = pageIndex * maxPhotoPerPage;
        
        for (int i = 0; i < maxPhotoPerPage && photoIndex < m_photos.Count; i++)
        {
            print("photo index: " + photoIndex);
            sprites.Add(m_photos[photoIndex]);
            photoIndex++;
        }
        return sprites;
    }

    public int GetPhotoCount()
    {
        return m_photos.Count;
    }

    [ContextMenu("Create Directory")]
    public void CreateDirectory()
    {
        filePath = Application.persistentDataPath + "/Resources/" + "Fotos" + SceneManager.GetActiveScene().name;
        Directory.CreateDirectory(filePath);
    }
}

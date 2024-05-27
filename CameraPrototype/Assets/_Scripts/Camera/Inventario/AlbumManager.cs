using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour
{
    //Variables
    public static AlbumManager instance;
    private List<Sprite> m_photos = new List<Sprite>();
    

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

    public void AddPhoto(Sprite newPhoto)
    {
        Texture2D texture = new Texture2D(newPhoto.texture.width, newPhoto.texture.height);
        Rect rect = new Rect(0, 0, newPhoto.texture.width, newPhoto.texture.height);
        var pixels = newPhoto.texture.GetPixels((int)rect.x, (int)rect.y, (int)newPhoto.texture.width, (int)newPhoto.texture.height);
        texture.SetPixels(pixels);
        texture.Apply();

        Sprite sprite =  Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                                           new Vector2(0.5f, 0.5f), 100);

        m_photos.Add(sprite);
    }

    public List<Sprite>  GetTandaPhoto(int pageIndex, int maxPhotoPerPage)
    {
        List<Sprite> sprites = new List<Sprite>();
        int photoIndex = pageIndex * maxPhotoPerPage;
        
        //print("photopermpage" + maxPhotoPerPage);
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
}

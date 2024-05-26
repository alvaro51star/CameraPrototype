using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour
{
    //Variables
    public static AlbumManager instance;
    private List<Sprite> m_photos = new List<Sprite>();
    [SerializeField] Image[] m_image;
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

    public void AddPhoto(Texture2D newPhoto)
    {

        Sprite photoSprite = Sprite.Create(newPhoto, new Rect(0, 0, newPhoto.width, newPhoto.height),
                                           new Vector2(0.5f, 0.5f), 100);
        m_photos.Add(photoSprite);
        //m_image[index].sprite = newPhoto;
        //index++;
    }

    public IEnumerator AddPhotoCoroutine( Sprite newPhoto)
    {
        yield return new WaitForEndOfFrame();
    }

    //public Sprite[] GetTandaPhoto(int photoQuantity)
    //{
    //    Sprite[] photos = new Sprite[photoQuantity];
    //    for (int i = 0; i < photoQuantity; i++)
    //    {
    //        photos[i] = m_photos[i];
    //    }
    //}

    public List<Sprite>  GetTandaPhoto(int pageIndex, int maxPhotoPerPage)
    {
        List<Sprite> sprites = new List<Sprite>();
        int photoIndex = pageIndex * maxPhotoPerPage;
        for (int i = 0; i < maxPhotoPerPage; i++)
        {
            if (photoIndex < m_photos.Count)
            {
                sprites.Add(m_photos[photoIndex]);
            }
            photoIndex++;
        }
        return sprites;
    }

    public int GetPhotoCount()
    {
        return m_photos.Count;
    }
}

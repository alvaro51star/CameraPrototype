using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SavePhoto : MonoBehaviour
{
    private int m_numPhotosTaken = 1;
    byte[] fotoData;
    Texture2D tex;
    //[SerializeField] Camera myCamera;

    //[SerializeField] private Image image;
    private string filePath;
   
    public void PhotoSave (Sprite spriteToSave) //no tiene sentido pasarle un sprite, hay que cambiar esto
    {
        //Texture2D renderResult = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, true);
        //Rect rect = new Rect(0, 0, texture.width, texture.height);

        //print("rect: " + rect.width + ", " + rect.height);
        //print ("texture: " + texture.width + ", " + texture.height);
        //print("renderResult: " +renderResult.width + ", " + renderResult.height);

        //texture.ReadPixels(rect, 0, 0);

        Texture2D T = new Texture2D((int)spriteToSave.rect.width, (int)spriteToSave.rect.height);

        var pixels = spriteToSave.texture.GetPixels((int)spriteToSave.textureRect.x, (int)spriteToSave.textureRect.y,
                                                (int)spriteToSave.textureRect.width, (int)spriteToSave.textureRect.height);
        T.SetPixels(pixels);
        T.Apply();
        print(T);

        //image.material.mainTexture = T;

        byte[] byteArray = T.EncodeToPNG();
        filePath = Application.dataPath + "/Resources/" + "Fotos" + SceneManager.GetActiveScene().name;
        System.IO.File.WriteAllBytes(filePath + "/Photo" + m_numPhotosTaken + ".png", byteArray);
        m_numPhotosTaken++;
    }

    [ContextMenu("SeePicture")]//despues quitar esto
    public void SeePicture()
    {
        string[] filePaths = Directory.GetFiles(Application.dataPath + "/Resources/Fotos" + SceneManager.GetActiveScene().name, "*.png");
        if (File.Exists(filePaths[0]))
        {
            fotoData = File.ReadAllBytes(filePaths[0]);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fotoData);
            Sprite photoSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f), 100);
            //image.sprite = photoSprite;
        }
    }

}

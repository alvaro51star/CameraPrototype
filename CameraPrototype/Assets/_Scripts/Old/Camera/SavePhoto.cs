using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SavePhoto : MonoBehaviour
{
    private int m_numPhotosTaken = 1;
    private string filePath;
   
    public void PhotoSave (Texture2D textureToSave)
    {
        Rect rect = new Rect(0, 0, textureToSave.width, textureToSave.height);
        var pixels = textureToSave.GetPixels((int)rect.x, (int)rect.y, (int)textureToSave.width, (int)textureToSave.height);
        textureToSave.SetPixels(pixels);
        textureToSave.Apply();        

        byte[] byteArray = textureToSave.EncodeToPNG();
        filePath = Application.dataPath + "/Resources/" + "Fotos" + SceneManager.GetActiveScene().name;
        System.IO.File.WriteAllBytes(filePath + "/Photo" + m_numPhotosTaken + ".png", byteArray);
        m_numPhotosTaken++;
    }   

}

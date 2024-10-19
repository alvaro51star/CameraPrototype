using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour //NO ESTA ACABADO, SIGUE SIENDO UN CAOS
{
    //Variables
    public static UIManager instance;
    
    [Header("UI Game Objects:")]
    [SerializeField] private GameObject m_go_diaryPanel;
    [SerializeField] private GameObject m_go_storyBookPanel;

    [SerializeField] private GameObject m_go_loseMenu;
    [SerializeField] private GameObject m_go_winMenu;
    [SerializeField] private GameObject m_go_cameraUI;

    [Header("Notes UI")]
    [SerializeField] private GameObject m_go_notePanel;
    [SerializeField] private TextMeshProUGUI m_TMPUGUI_note;
    public bool m_isReading;

    [Header("Interaction")]
    [SerializeField] private GameObject m_go_interactInputImage;
    [SerializeField] private GameObject m_go_punteroInteraction;
    [SerializeField] private GameObject m_go_punteros;
    [SerializeField] private TextMeshProUGUI m_TMPUGUI_interaction;
    [SerializeField] private GameObject m_go_lockImage;
    [SerializeField] private GameObject m_go_petImage;
    private bool m_isLockedDoor;
    private bool m_isCatPetted;
    
    [Header("Dialogue (temporary)")]
    public GameObject go_DialoguePanel;
    public bool isInDialogue;
    
    [Header("Vault UI")]//caja fuerte, si ponemos safe no se entiende xd
    [SerializeField] private GameObject m_go_safePanel;
    [SerializeField] private GameObject m_go_redLight;
    [SerializeField] private GameObject m_go_greenLight;
    [SerializeField] private TextMeshProUGUI m_TMPUGUI_safeNumber;

    [Header("Album")]
    [SerializeField] private GameObject m_go_albumPanel;
    [SerializeField] private GameObject m_go_closeUpImagePanel;
    [SerializeField] private GameObject m_go_prevAlbumButtom;
    [SerializeField] private GameObject m_go_nextAlbumButtom;
    [SerializeField] private GameObject[] m_go_albumPhotos;
    [SerializeField] private Image[] m_img_albumPhotosSprites;
    [SerializeField] private Image m_img_closeUpPhoto;
    private int m_albumPage = -1; //empieza en -1
    
    private bool m_isAbleToPause = true;

    #region Getters&Setters

    public bool GetIsGamePaused()//no borro esta funcion de aqui para no generar cambios en PlayerBehaviour
    {
        return MenuButtons.instance.GetIsGamePaused();
    }
    

    public bool GetIsReading()
    {
        return m_isReading;
    }

    public void SetIsReading(bool mode)
    {
        m_isReading = mode;
    }

    #endregion


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
    
    
    public void PauseMenu()
    {
        if (m_isAbleToPause)
        {
            MenuButtons.instance.SetPauseMenu(true);
            m_go_cameraUI.SetActive(false);
            SetPointersActive(false);
            SetInteractionText(false, "");
            
            MenuButtons.instance.SetGameMode(GameModes.UI);
        }
    }
    
    public void DiaryButton()
    {
        if (!m_go_diaryPanel.activeSelf)
        {
            m_go_diaryPanel.SetActive(true);
            MenuButtons.instance.SetPauseMenu(false);//funcionamiento provisional
        }
        else
        {
            MenuButtons.instance.SetPauseMenu(true);//funcionamiento provisional
            m_go_diaryPanel.SetActive(false);
        }
    }

    public void StoryBook()
    {
        if (!m_go_storyBookPanel.activeSelf)
        {
            m_go_storyBookPanel.SetActive(true);
            MenuButtons.instance.SetPauseMenu(false);//no entiendo por que se desactivaba aqui esto


            SetPointersActive(false);
            EventManager.OnIsReading?.Invoke();
            m_isReading = true;
            SetInteractionText(false, "");
        }
        else
        {
            m_go_storyBookPanel.SetActive(false);

            SetPointersActive(true);
            EventManager.OnStopReading?.Invoke();
            m_isReading = false;
        }
    }

    #region EndMenus

    public void ActivateLoseMenu()
    {
        m_isAbleToPause = false;
        m_go_cameraUI.SetActive(false);
        m_go_loseMenu.SetActive(true);
        SetPointersActive(false);
        SetInteractionText(false, "");
        
        MenuButtons.instance.SetGameMode(GameModes.UI);
    }
    
    public void ActivateWinMenu()
    {
        MenuButtons.instance.SetGameMode(GameModes.UI);
        
        m_go_cameraUI.SetActive(false);
        m_go_winMenu.SetActive(true);
        SetPointersActive(false);
        SetInteractionText(false, "");
    }

    #endregion
    

    //notes
    public void ActivateNote(string noteText)
    {
        m_TMPUGUI_note.text = noteText;
        m_go_notePanel.SetActive(true);
        SetPointersActive(false);
        SetInteractionText(false, "");
        EventManager.OnIsReading?.Invoke();
        m_isReading = true;

        //m_isGamePaused = true; //no entiendo esto, entonces al abrir una nota no se para el tiempo y tal??
        MenuButtons.instance.SetIsGamePaused(true);
    }
    public void DeactivateNote()
    {
        m_TMPUGUI_note.text = " ";
        m_go_notePanel.SetActive(false);
        EventManager.OnStopReading?.Invoke();
        SetPointersActive(true);
        m_isReading = false;

        MenuButtons.instance.SetIsGamePaused(false);
    }

    #region InteractiveObjectsHUD
    //Interaction
    public void ShowInput(bool mode)
    {
        m_go_interactInputImage.SetActive(mode);
    }

    public void ChangeInteractionPointer(bool mode)
    {
        if (mode)
        {
            m_go_punteroInteraction.SetActive(true);
            ShowInput(true);
        }
        else
        {
            m_go_punteroInteraction.SetActive(false);
            ShowInput(false);
        }
    }

    public void SetInteractionText(bool mode, string text)
    {
        if (mode)
        {
            m_TMPUGUI_interaction.gameObject.SetActive(true);
            m_TMPUGUI_interaction.text = text;
        }
        else
        {
            m_TMPUGUI_interaction.text = "";
            m_TMPUGUI_interaction.gameObject.SetActive(false);
        }
    }

    public void ChangeDoorLock(bool mode)
    {
        if (m_go_lockImage.activeSelf != mode)
        {
            m_go_punteros.SetActive(!mode);
            ShowInput(!mode);
            m_go_lockImage.SetActive(mode);
            m_isLockedDoor = mode;
        }
    }

    public void ChangePetCat(bool mode)
    {
        if (m_go_petImage.activeSelf != mode)
        {
            m_go_punteros.SetActive(!mode);
            ShowInput(!mode);
            m_go_petImage.SetActive(mode);
            m_isCatPetted= mode;
        }
    }

    public void InteractionAvialable(bool mode, bool isLockedDoor, bool isCat)//me da miedo cambiar el nombre por si toco otros scripts
    {
        if (isLockedDoor && !isCat)
        {
            ChangeDoorLock(true);
            ChangePetCat(false);
            SetInteractionText(false, "");
        }
        else if(!isLockedDoor && !isCat)
        {
            ChangeDoorLock(false);
            ChangePetCat(false);
            ChangeInteractionPointer(mode);
        }
        else if (!isLockedDoor && isCat)
        {
            ChangeDoorLock(false);
            ChangePetCat(true);
            SetInteractionText(false, "");
        }
    }

    public void SetPointersActive(bool mode)
    {
        m_go_punteros.SetActive(mode);
        m_go_lockImage.SetActive(m_isLockedDoor);
        m_go_petImage.SetActive(m_isCatPetted);
        if (!mode)
        {
            ShowInput(false);
        }
    }
    #endregion
    

    #region InteractiveObjectsUI
    //puzles
    //Caja fuerte
    
    public void ShowSafe(bool mode)
    {
        m_go_safePanel.SetActive(mode);
        if (!mode)
        {
            EventManager.OnStopReading?.Invoke();
            SetPointersActive(true);
            SetInteractionText(false, "");
            m_isReading = false;

            //m_isGamePaused = false;
            MenuButtons.instance.SetIsGamePaused(false);
        }
        else
        {
            EventManager.OnIsReading?.Invoke();
            SetPointersActive(false);
            m_isReading = true;

            //m_isGamePaused = true;
            MenuButtons.instance.SetIsGamePaused(true);
        }
    }

    public void ShowLight(bool mode, bool redLight)
    {
        if (mode)
        {
            if (redLight)
            {
                m_go_redLight.SetActive(true);
                m_go_greenLight.SetActive(false);
            }
            else
            {
                m_go_redLight.SetActive(false);
                m_go_greenLight.SetActive(true);
            }
        }
        else
        {
            m_go_redLight.SetActive(false);
            m_go_greenLight.SetActive(false);
        }
    }

    public void ChangeCodeDisplay(string num)
    {
        m_TMPUGUI_safeNumber.text = num;
    }
    #endregion


    #region Album

    public void ShowAlbum(bool mode)
    {
        if (mode)
        {
            m_go_albumPanel.SetActive(true);
            AdvanceAlbumPage();
        }
        else
        {
            m_go_closeUpImagePanel.SetActive(false);
            m_go_albumPanel.SetActive(false);
            m_albumPage = -1;
        }
    }

    public void AdvanceAlbumPage()
    {
        m_albumPage++;
        List<Sprite> sprites = AlbumManager.instance.GetTandaPhoto(m_albumPage, m_img_albumPhotosSprites.Length);
        bool showButtons = false;
        for (int i = 0; i < m_go_albumPhotos.Length; i++) //cambiar a lista
        {
            if (i < sprites.Count)
            {
                m_go_albumPhotos[i].SetActive(true);
                m_img_albumPhotosSprites[i].sprite = sprites[i];
                showButtons = true;
            }
            else
            {
                m_go_albumPhotos[i].gameObject.SetActive(false);
            }
        }
        if (showButtons == false)
        {
            m_go_prevAlbumButtom.SetActive(false);
            m_go_nextAlbumButtom.SetActive(false);
        }
        else
        {
            if (m_albumPage != 0)
            {
                m_go_prevAlbumButtom.SetActive(true);
            }
            else
            {
                m_go_prevAlbumButtom.SetActive(false);
            }

            if (m_albumPage * m_img_albumPhotosSprites.Length + sprites.Count < AlbumManager.instance.GetPhotoCount())
            {
                m_go_nextAlbumButtom.SetActive(true);
            }
            else
            {
                m_go_nextAlbumButtom.SetActive(false);
            }
        }
    }

    public void GoBackAlbumPage()
    {
        if (m_albumPage > 0)
        {
            m_albumPage--;
            List<Sprite> sprites = AlbumManager.instance.GetTandaPhoto(m_albumPage, m_img_albumPhotosSprites.Length);
            bool showButtons = false;
            for (int i = 0; i < m_go_albumPhotos.Length; i++) //cambiar a lista
            {
                if (i < sprites.Count)
                {
                    m_go_albumPhotos[i].SetActive(true);
                    m_img_albumPhotosSprites[i].sprite = sprites[i];
                    showButtons = true;
                }
                else
                {
                    m_go_albumPhotos[i].gameObject.SetActive(false);
                }
            }
            if (showButtons == false)
            {
                m_go_prevAlbumButtom.SetActive(false);
                m_go_nextAlbumButtom.SetActive(false);
            }
            else
            {
                if (m_albumPage != 0)
                {
                    m_go_prevAlbumButtom.SetActive(true);
                }
                if (m_albumPage * m_img_albumPhotosSprites.Length + sprites.Count < AlbumManager.instance.GetPhotoCount())
                {
                    m_go_nextAlbumButtom.SetActive(true);
                }
                else
                {
                    m_go_nextAlbumButtom.SetActive(false);
                }
            }
        }
        if (m_albumPage == 0)
        {
            m_go_prevAlbumButtom.SetActive(false);
        }
    }

    public void ShowCloseUpPhoto(Image photo)
    {
        m_go_closeUpImagePanel.SetActive(true);
        m_img_closeUpPhoto.sprite = photo.sprite;
    }

    public void CloseCloseUpPhoto()
    {
        m_go_closeUpImagePanel.SetActive(false);
    }


    #endregion
    
}

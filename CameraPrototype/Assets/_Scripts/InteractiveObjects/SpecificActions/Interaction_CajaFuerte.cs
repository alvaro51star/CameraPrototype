using UnityEngine;
using UnityEngine.Serialization;

public class Interaction_CajaFuerte : AffectsIndirectly
{
    //Variables 
    [SerializeField] private Animator m_animatr;
    [SerializeField] private int m_maxCodeNumber;
    [SerializeField] private string m_str_correctCode;
    private bool m_isOpen = false;
    private string m_str_actualCode = "";

    //Custom
    protected override void FirstAction()
    {
        if (!m_isOpen)
        {
            UIManager.instance.ShowSafe(true);
            EventManager.OnIsReading?.Invoke();
        }
    }

    public void AddActualCode(GameObject button)
    {
        ReproduceKeySound();
        if (m_str_actualCode.Length < m_maxCodeNumber)
        {
            m_str_actualCode += button.name;
        }
        UIManager.instance.ChangeCodeDisplay(m_str_actualCode);
        UIManager.instance.ShowLight(false, true);
    }

    public void ReproduceKeySound()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.safeboxButtons /*, this.transform.position */);
    }

    public void DeleteActualCode()
    {
        ReproduceKeySound();
        m_str_actualCode = "";
        UIManager.instance.ChangeCodeDisplay(m_str_actualCode);
    }

    public void CheckCode()
    {
        ReproduceKeySound();
        if (m_str_actualCode == m_str_correctCode)
        {
            m_isOpen = true;
            UIManager.instance.ShowLight(true, false);

            AudioManager.Instance.PlayOneShot(FMODEvents.instance.rightCode /*, this.transform.position */);
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.unlockSafe /*, this.transform.position */);
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.openSafe /*, this.transform.position */);
            //Lineas de FMOD para codigo correcto, desbloquear y abrir, aunque la de abrir se podr�a aparte pa hacerla con animaci�n

            IChangeActiveMode(m_go_targetObject, true);
            GetComponent<InteractiveObject>().enabled = false;
            m_animatr.SetInteger("Abrir", 1);
        }
        else
        {
            UIManager.instance.ShowLight(true, true);
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.wrongCode /*, this.transform.position */);
            //Linea FMOD de sonido de codigo incorrecto
        }
    }
}

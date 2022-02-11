using UnityEngine;
using UnityEngine.Events;

public class LeverMove : MonoBehaviour
{
    private LeverBase _leverBase;
    private bool SwitchOn = true;
    public bool UnLocked;

    private UnityEvent LeverTurnOn;
    public GameObject HighlightTurnOnOff;
    public GameObject HighlightFollow;
    


    private void Start()
    {
        _leverBase = GetComponentInParent<LeverBase>();
        LeverTurnOn = _leverBase.LeverTurnOnReaction;
        if (UnLocked == false)
        {
            LightTurnOff();
        }
    }

    public void HighlightFollowOn()
    {
        HighlightFollow.transform.rotation = Quaternion.Euler(-15,0,0);
    }
    public void HighlightFollowOff()
    {
        HighlightFollow.transform.rotation = Quaternion.Euler(15,0,0);
    }
    
    public void LightTurnOn()
    {
        HighlightTurnOnOff.SetActive(true);
    }

    public void LightTurnOff()
    {
        HighlightTurnOnOff.SetActive(false);
    }
    void ChangeBool()
    {
        SwitchOn = !SwitchOn;
    }
    
    void TurnOnLever()
    {
        gameObject.transform.rotation = Quaternion.Euler(-15,0,0);
        HighlightFollowOn();
        LeverTurnOn?.Invoke();
    }

    public void ChangeLockedStatus()
    {
        UnLocked = !UnLocked;
        if (UnLocked)
        {
            LightTurnOn();
        }
        else
        {
            LightTurnOff();
        }
    }

    void TurnOffLever()
    {
        HighlightFollowOff();
        gameObject.transform.rotation = Quaternion.Euler(15,0,0);
    }

    public void ChangeLeverState()
    {
        if (UnLocked)
        {


            if (SwitchOn)
            {
                TurnOnLever();
            }
            else
            {
                TurnOffLever();
            }

            ChangeBool();

        }
    }
}

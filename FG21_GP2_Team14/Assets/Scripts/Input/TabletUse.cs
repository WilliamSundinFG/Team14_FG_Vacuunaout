using UnityEngine;

public class TabletUse : MonoBehaviour
{
    bool isDown = true;
    bool map = false;

    //[SerializeField]
    float waitTime = 5f;

    public void SwitchDisplay()
    {
        map = !map;
        if (map) { ViewMap(); }
        else { ViewChecklist(); }
    }

    public void UseTablet()
    {
        if (isDown) { TabletUp(); }
        else { TabletDown(); }
    }

    private void ViewChecklist()
    {
        HideMap();
        gameObject.transform.Find("Canvas").gameObject.SetActive(true);
    }

    private void ViewMap()
    {
        HideChecklist();
        gameObject.transform.Find("Tablet_Map_Doors").gameObject.SetActive(true);
        gameObject.transform.Find("Tablet_Map_Names").gameObject.SetActive(true);
        gameObject.transform.Find("Tablet_Map").gameObject.SetActive(true);
    }

    private void HideChecklist()
    {
        gameObject.transform.Find("Canvas").gameObject.SetActive(false);
    }

    private void HideMap()
    {
        gameObject.transform.Find("Tablet_Map_Doors").gameObject.SetActive(false);
        gameObject.transform.Find("Tablet_Map_Names").gameObject.SetActive(false);
        gameObject.transform.Find("Tablet_Map").gameObject.SetActive(false);
    }




    void TabletUp()
    {
        isDown = false;
        gameObject.GetComponent<Animator>().SetTrigger("TabletUp");
        gameObject.GetComponent<Animator>().ResetTrigger("TabletDown");
    }

    void TabletDown()
    {
        isDown = true;
        gameObject.GetComponent<Animator>().SetTrigger("TabletDown");
        gameObject.GetComponent<Animator>().ResetTrigger("TabletUp");
    }
}
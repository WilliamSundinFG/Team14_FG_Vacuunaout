using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeIn : MonoBehaviour
{
    [SerializeField]
    private GameObject textOne;

    [SerializeField]
    private GameObject textTwo;

    [SerializeField]
    private GameObject textThree;

    [SerializeField]
    private GameObject textFour;   
    
    [SerializeField]
    private GameObject textFive;

    [SerializeField]
    private GameObject icon;

    [SerializeField]
    float fadeTime;

    float ogT;

    GameObject backObject;

    GameObject creditsObject;

    void Start()
    {
        ogT = fadeTime;
        backObject = gameObject.transform.Find("Back").gameObject;
        creditsObject = gameObject.transform.Find("CreditsText").gameObject;
        FadeInText();
    }

    public void FadeInText()
    {
        fadeTime = 2f;
        StartCoroutine(FadeTextToFullAlpha(fadeTime, textOne.GetComponent<TMPro.TextMeshProUGUI>()));
        StartCoroutine(FadeTextToFullAlpha(fadeTime, textTwo.GetComponent<TMPro.TextMeshProUGUI>()));
        StartCoroutine(FadeTextToFullAlpha(fadeTime, textThree.GetComponent<TMPro.TextMeshProUGUI>()));
        StartCoroutine(FadeTextToFullAlpha(fadeTime, textFour.GetComponent<TMPro.TextMeshProUGUI>()));
        StartCoroutine(FadeTextToFullAlpha(fadeTime, textFive.GetComponent<TMPro.TextMeshProUGUI>()));

        StartCoroutine(FadeIconToFullAlpha(fadeTime, icon.GetComponent<Image>()));

    }

    public IEnumerator FadeIconToFullAlpha(float t, Image i)
    {
        t = fadeTime;

        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }

        t = ogT;
    }


    public IEnumerator FadeTextToFullAlpha(float t, TMPro.TextMeshProUGUI i)
    {
        t = fadeTime;

        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }

        t = ogT;
    }


    public void FadeOutText()
    {
        fadeTime = 2f;
        StartCoroutine(FadeTextToZeroAlpha(fadeTime, textOne.GetComponent<TMPro.TextMeshProUGUI>()));
        StartCoroutine(FadeTextToZeroAlpha(fadeTime, textTwo.GetComponent<TMPro.TextMeshProUGUI>()));
        StartCoroutine(FadeTextToZeroAlpha(fadeTime, textThree.GetComponent<TMPro.TextMeshProUGUI>()));
        StartCoroutine(FadeTextToZeroAlpha(fadeTime, textFour.GetComponent<TMPro.TextMeshProUGUI>()));
        StartCoroutine(FadeTextToZeroAlpha(fadeTime, textFive.GetComponent<TMPro.TextMeshProUGUI>()));
        
        StartCoroutine(FadeIconToZeroAlpha(fadeTime, icon.GetComponent<Image>()));
    }

    public IEnumerator FadeIconToZeroAlpha(float t, Image i)
    {
        t = fadeTime;

        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        t = ogT;
    }



    public IEnumerator FadeTextToZeroAlpha(float t, TMPro.TextMeshProUGUI i)
    {
        t = fadeTime;

        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        t = ogT;
    }

    public void EnableBack()
    {
        backObject.SetActive(true);
    }

    public void DisableBack()
    {
        backObject.SetActive(false);
    }

    public void EnableCredits()
    {
        creditsObject.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(0.4f, creditsObject.GetComponent<TMPro.TextMeshProUGUI>()));

    }
    public void DisableCredits()
    {
        fadeTime = 0.2f;

        StartCoroutine(FadeTextToZeroAlpha(3f, creditsObject.GetComponent<TMPro.TextMeshProUGUI>()));

        //creditsObject.SetActive(false);
    }
}

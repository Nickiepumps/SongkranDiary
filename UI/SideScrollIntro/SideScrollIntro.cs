using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SideScrollIntro : GameSubject
{
    [Header("Intro Properties")]
    [SerializeField] private GameObject introGroup;
    [SerializeField] private RectMask2D introWipeMask;
    private float wipeMaskPaddingValue;
    [SerializeField] private TMP_Text introText;
    [SerializeField] private Image paperBG;
    [SerializeField] private Animation closingAnim;
    [SerializeField] private float introTransitionTime;
    [Header("Knouck out Properties")]
    public GameObject knouckOutGroup;
    [SerializeField] private RectMask2D knockOutwipeMask;
    private float knockOutwipeMaskPaddingValue;
    [SerializeField] private Image knockOutImage;
    [SerializeField] private float knockOutTransitionTime;
    [HideInInspector] public bool finishCoroutine = false;
    [HideInInspector] public bool finishIntro = false;

    private void Start()
    {
        wipeMaskPaddingValue = introWipeMask.GetComponent<RectTransform>().rect.width;
        knockOutwipeMaskPaddingValue = knockOutImage.GetComponent<RectTransform>().rect.width;
        StartCoroutine(StartIntroWipeTransition());
    }
    private IEnumerator StartIntroWipeTransition()
    {
        finishCoroutine = false;
        float currentWipeValue = wipeMaskPaddingValue;
        while(currentWipeValue > 0)
        {
            introWipeMask.padding = new Vector4(currentWipeValue -= introTransitionTime, 0, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        while (currentWipeValue < wipeMaskPaddingValue)
        {
            introWipeMask.padding = new Vector4(0, 0, currentWipeValue += introTransitionTime, 0);
            yield return null;
        }
        introText.text = "ลุย!!";
        yield return new WaitForSeconds(1f);
        while (currentWipeValue > 0)
        {
            introWipeMask.padding = new Vector4(currentWipeValue -= introTransitionTime, 0, 0, 0);
            yield return null;
        }
        finishIntro = true;
        NotifySideScrollGameObserver(SideScrollGameState.StartRound);
        closingAnim.Play();
        yield return new WaitForSeconds(1f);
        finishCoroutine = true;
        introGroup.gameObject.SetActive(false);
    }
    public IEnumerator StartKnockOutWipeTransition()
    {
        finishCoroutine = false;
        float currentWipeValue = knockOutwipeMaskPaddingValue;
        while (currentWipeValue > 0)
        {
            knockOutwipeMask.padding = new Vector4(0, 0, currentWipeValue -= knockOutTransitionTime, 0);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (currentWipeValue < knockOutwipeMaskPaddingValue)
        {
            knockOutwipeMask.padding = new Vector4(currentWipeValue += knockOutTransitionTime, 0, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        finishCoroutine = true;
        knouckOutGroup.gameObject.SetActive(false);
    }
}

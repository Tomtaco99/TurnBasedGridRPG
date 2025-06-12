using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    [SerializeField]
    private Camera uiCamera;
    private Text tooltipText;
    private RectTransform backgroundRectTransform;
    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<Text>();
        HideTooltip();
        //ShowTooltip("Random tool tip text");
    }
    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
        backgroundRectTransform.gameObject.transform.position = tooltipText.gameObject.transform.position;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }
    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }

}

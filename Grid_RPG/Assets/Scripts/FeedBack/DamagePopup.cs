using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(FeedBackController.Instance.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopup;
    }
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        transform.DOScale(Vector3.zero, .8f);
        textMesh.DOFade(0, 0.8f);
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = 1f;
    }

    private void Update()
    {
        float moveYSpeed = 8;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 5f;
            textColor.a = disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(this);
            }
        }
    }
}

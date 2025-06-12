using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class SkillToolTip : MonoBehaviour
{
    [Range(0, 1)]
    public int index = 0;
    private void Start()
    {
        UpdateTooltip();
    }

    public void UpdateTooltip()
    {
        GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(CombatManager.Instance.actionList[0].entity.skills[index].description);
        GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
    }
}

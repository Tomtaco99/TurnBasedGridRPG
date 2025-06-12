using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM.Utils;

public class popupTesting : MonoBehaviour
{
    private void Start()
    {
        //DamagePopup.Create(Vector3.zero, 4);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DamagePopup.Create(Utilities.GetMouseWorldPosition(), 4);
        }
    }
}

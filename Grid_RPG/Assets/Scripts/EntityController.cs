using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : TemporalSingleton<EntityController>
{
    private Movement selectedEntity;
    private Movement previousEntity;

    // Update is called once per frame
    void Update()
    {

    }

    public void TryGetSelected(Movement entity)
    {
        if (selectedEntity != null)
        {
            if (entity != selectedEntity)
            {
                previousEntity = selectedEntity;
                previousEntity.IsSelected = false;
                selectedEntity = entity;
            }

        }
        else
        {
            selectedEntity = entity;
        }
    }
}

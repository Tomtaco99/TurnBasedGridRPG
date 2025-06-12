using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/New Cast Effect")]
public class CastEffect : Effect
{

    public void Cast(GameObject Instigator, GameObject[] targets)
    {
        Anim();
        Sound();
    }

    public override void Anim()
    {
        throw new System.NotImplementedException();
    }

    public override void Sound()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator PlayAnimations()
    {
        yield return null;
    }

    IEnumerator PlaySounds()
    {
        yield return null;
    }

    public override void ApplyHit(TargetGroup target)
    {
        throw new System.NotImplementedException();
    }
}

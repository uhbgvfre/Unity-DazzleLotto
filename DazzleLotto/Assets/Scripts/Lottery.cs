using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lottery : MonoBehaviour
{
    public List<Text> txtsOnFloor;
    [Header("Animation")]
    public ParticleSystem mangerUplightVFX;
    public ParticleSystem DownlightVFX;

    void Start()
    {
        txtsOnFloor.ForEach(x => x.text = string.Empty);
    }

    public void StartAnimation(Action onEnd)
    {
        StartCoroutine(Animation(onEnd));
    }

    IEnumerator Animation(Action onEnd)
    {
        mangerUplightVFX.Play();

        var w1 = new WaitForSeconds(1f);
        yield return w1;

        DownlightVFX.Play();

        var w2 = new WaitForSeconds(.5f);
        yield return w2;

        if (onEnd != null) onEnd.Invoke();
    }

}



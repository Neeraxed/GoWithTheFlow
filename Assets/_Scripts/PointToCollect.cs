using System;
using UnityEngine;
using UnityEngine.Events;

public class PointToCollect : MonoBehaviour
{
    public static int PointToCollectLayer => LayerMask.NameToLayer("PointToCollect");
    public static event Action PointToCollectTriggered;

    private void OnTriggerEnter(Collider other)
    {
        PointToCollectTriggered?.Invoke();
    }

    public void Collect(ref int count)
    {
        count++;
        gameObject.SetActive(false);
    }
}

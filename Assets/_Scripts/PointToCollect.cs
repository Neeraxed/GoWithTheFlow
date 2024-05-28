using System;
using UnityEngine;

public class PointToCollect : MonoBehaviour
{
    public static int PointToCollectLayer => LayerMask.NameToLayer("PointToCollect");
    public static event Action PointToCollectTriggered;

    public void Collect(ref int count)
    {
        count++;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        PointToCollectTriggered?.Invoke();
    }
}

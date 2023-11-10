using System;
using UnityEngine;

public class CollectableMan : MonoBehaviour
{
    public float pointToCollectActivationRadius = 2f;
    public static event Action CollectedAmountChanged;

    private Collider[] pointToCollectCollidersBuffer = new Collider[1];

    void Update()
    {
        DetectPointToCollect();
    }

    public void DetectPointToCollect()
    {
        var hits = Physics.OverlapSphereNonAlloc(this.transform.position, pointToCollectActivationRadius, pointToCollectCollidersBuffer, 1 << PointToCollect.PointToCollectLayer);

        if (hits > 0)
        {
            var pointToCollect = pointToCollectCollidersBuffer[0].GetComponent<PointToCollect>();
            pointToCollect.Collect(ref PlayerBehaviour.CollectedAmount);
            CollectedAmountChanged?.Invoke();
        }
    }
}

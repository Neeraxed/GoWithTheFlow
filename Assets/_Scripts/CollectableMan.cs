using System;
using UnityEngine;

public class CollectableMan : MonoBehaviour
{
    public static event Action CollectedAmountChanged;

    [SerializeField] private float pointToCollectActivationRadius = 3f;

    private Collider[] pointToCollectCollidersBuffer = new Collider[1];
    
    public void DetectPointToCollect()
    {
        var hits = Physics.OverlapSphereNonAlloc(this.transform.position, pointToCollectActivationRadius, pointToCollectCollidersBuffer, 1 << PointToCollect.PointToCollectLayer);

        if (hits > 0)
        {
            var pointToCollect = pointToCollectCollidersBuffer[0].GetComponent<PointToCollect>();
            pointToCollect.Collect(ref GameManager.CollectedAmount);
            CollectedAmountChanged?.Invoke();
        }
    }

    private void OnEnable()
    { 
        PointToCollect.PointToCollectTriggered += DetectPointToCollect;
    }

    private void OnDisable()
    { 
        PointToCollect.PointToCollectTriggered -= DetectPointToCollect;
    }
}
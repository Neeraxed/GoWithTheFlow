using System;
using UnityEngine;

public class CollectableMan : MonoBehaviour
{
    public static event Action CollectedAmountChanged;

    [SerializeField] private float _pointToCollectActivationRadius = 3f;

    private Collider[] _pointToCollectCollidersBuffer = new Collider[1];
    
    public void DetectPointToCollect()
    {
        var hits = Physics.OverlapSphereNonAlloc(this.transform.position, _pointToCollectActivationRadius, _pointToCollectCollidersBuffer, 1 << PointToCollect.PointToCollectLayer);

        if (hits > 0)
        {
            var pointToCollect = _pointToCollectCollidersBuffer[0].GetComponent<PointToCollect>();
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

using UnityEngine;

public class PointToCollect : MonoBehaviour
{
    public static int PointToCollectLayer => LayerMask.NameToLayer("PointToCollect");

    public void Collect(ref int count)
    {
        count++;
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using YG;

public class YGAdsProvider : MonoBehaviour
{
    public YandexGame sdk;

    public void AdButton()
    {
        sdk._RewardedShow(1);
    }
}

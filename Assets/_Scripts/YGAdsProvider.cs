using UnityEngine;
using YG;

public class YGAdsProvider : MonoBehaviour
{
    [SerializeField] private YandexGame _sdk;

    public void AdButton()
    {
        _sdk._RewardedShow(1);
    }
}

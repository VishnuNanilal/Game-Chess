using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public AudioSource channelType;
    public Toggle toggle;

    public void ChannelTypeChanger()
    {
        //Debug.Log(toggle.isOn);

        if (toggle.isOn)
            channelType.spatialBlend = 1f;
        else
            channelType.spatialBlend = 0f;
    }
}

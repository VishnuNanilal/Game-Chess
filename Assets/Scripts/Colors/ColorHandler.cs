using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHandler : MonoBehaviour
{
    public Image panel;

    public void ColorChanger(string colorName)
    {

        if (colorName == "red")
        {
            panel.color = new Color(0.4705882f, 0.2156863f, 0.1921569f);
        }
        else
        if (colorName == "blue")
        {
            panel.color = new Color(0.1921569f, 0.3787505f, 0.4705882f);
        }
        else
        if (colorName == "yellow")
        {
            panel.color = new Color(0.5283019f, 0.4792353f, 0.1719473f);
        }
        else if (colorName == "green")
        {
            panel.color = new Color(0.1921569f, 0.4823529f, 0.2078431f);
        }

    }
}

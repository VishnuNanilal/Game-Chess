using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontManager : MonoBehaviour
{
    public SO_LanguageChange so;

    public Text newGame;
    public Text settings;
    public Text exit;

    public TextMeshProUGUI soundsText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI stereoText;
    public TextMeshProUGUI fontText;
    public TextMeshProUGUI backgroundColorText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI fontSmallText;
    public TextMeshProUGUI fontLargeText;
    public TextMeshProUGUI fontColorText;
    public TextMeshProUGUI languagesText;

    public Text back;

    public void NewLanguage(SO_LanguageChange newLanguage)
    {
        so = newLanguage;
        ChangeLanguage();
    }
    private void ChangeLanguage()
    {
        newGame.text = so.newGame;
        settings.text = so.settings;
        exit.text = so.exit;

        soundsText.text = so.sounds;
        volumeText.text = so.volume;
        stereoText.text = so.stereo;
        fontText.text = so.font;
        backgroundColorText.text = so.backgroundColor;
        sizeText.text = so.size;
        fontSmallText.text = so.fontSmall;
        fontLargeText.text = so.fontLarge;
        fontColorText.text = so.fontColor;
        languagesText.text = so.languages;
        back.text = so.back;

    }

    public void ChangeColor(string color)
    {
        float colorValue;

        if (color == "white")
            colorValue = 1;
        else
            colorValue = 0.1f;

        newGame.color = new Color(colorValue, colorValue, colorValue);
        settings.color = new Color(colorValue, colorValue, colorValue);
        exit.color = new Color(colorValue, colorValue, colorValue);

        soundsText.color = new Color(colorValue, colorValue, colorValue);
        volumeText.color = new Color(colorValue, colorValue, colorValue);
        stereoText.color = new Color(colorValue, colorValue, colorValue);
        fontText.color = new Color(colorValue, colorValue, colorValue);
        fontColorText.color = new Color(colorValue, colorValue, colorValue);
        backgroundColorText.color = new Color(colorValue, colorValue, colorValue);
        sizeText.color = new Color(colorValue, colorValue, colorValue);
        languagesText.color = new Color(colorValue, colorValue, colorValue);
    }    
}

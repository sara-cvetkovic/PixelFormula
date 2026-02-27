using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetLeaderboardItemInfo : MonoBehaviour
{
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI driverNameText;
    public Image teamLogoImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetPositionText(string newPosition)
    {
        positionText.text = newPosition;
    }

    public void SetDriverNameText(string newDriverName)
    {
        driverNameText.text = newDriverName;
    }

    public void SetTeamLogo(Sprite logo)
    {
        if (teamLogoImage != null && logo != null)
            teamLogoImage.sprite = logo;
    }
}

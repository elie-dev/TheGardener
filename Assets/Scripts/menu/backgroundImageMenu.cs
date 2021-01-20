using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundImageMenu : MonoBehaviour
{
    public List<Sprite> sprites;
    private Image img;

    // Start is called before the first frame update
    void Awake()
    {
        int currentHour = System.DateTime.Now.Hour;
        img = GetComponent<Image>();
        switch (currentHour)
        {
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                img.sprite = sprites[0];
                break;
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
                img.sprite = sprites[1];
                break;
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
                img.sprite = sprites[2];
                break;
            case 23:
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                img.sprite = sprites[3];
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [SerializeField] private string characterName;
    [SerializeField] private int characterHealth = 3;
    [SerializeField] private int XOffset = -30;
    [SerializeField] private int YOffset = -85;

    private int height = 15;
    private int width = 50;
    private GUIStyle boxStyle;

    private float boxXPos = 0;
    private float boxYPos = 0;

    private bool showBox = false;

    private int healthWidth = 50;
    private int characterLevel = 3;
    private int widthTextBox = 25;

    private GameObject cameraMain;

    private Texture2D resultTexture;
    private Texture2D resultTexture2;

    private void Start()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera");

        characterLevel = characterHealth;

        widthTextBox = setBoxWidth(characterName);

        resultTexture2 = MakeTex(widthTextBox, 30, new Color(0.0f, 0.0f, 0.0f, 0.0f));
        resultTexture = MakeTex(healthWidth, 10, new Color(1.0f, 0.0f, 0.0f, 0.75f));

        activateHealthBox();
    }

    public void decreaseHealth()
    {
        characterHealth--;
        healthWidth = (width / characterLevel) * characterHealth;

        resultTexture = MakeTex(healthWidth, 10, new Color(1.0f, 0.0f, 0.0f, 0.75f));

        if (characterHealth <= 0)
        {
            StartCoroutine(destroyCharacter());
        }
    }

    IEnumerator destroyCharacter()
    {
        yield return new WaitForSeconds(0.8f);

        this.gameObject.SendMessage("destroyProcess", SendMessageOptions.DontRequireReceiver);

        this.gameObject.SetActive(false);
    }

    public void activateHealthBox()
    {
        setCurrentBoxPosition();
        setShowBox(true);
    }

    public void showHealthBox(bool show)
    {
        setShowBox(show);
    }

    void OnGUI()
    {
        if (showBox)
        {
            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = 20;

            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cameraMain.GetComponent<Camera>(), transform.position);
            GUI.Box(new Rect((screenPoint.x + XOffset), (Screen.height - screenPoint.y + YOffset), width, height), new GUIContent(" "), boxStyle);

            //
            if (healthWidth > 0)
            {
                boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.background = resultTexture;
                GUI.Box(new Rect((screenPoint.x + XOffset), (Screen.height - screenPoint.y + YOffset), healthWidth, height), new GUIContent(" "), boxStyle);
            }

            //
            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = 20;
            widthTextBox = setBoxWidth(characterName);
            boxStyle.normal.textColor = Color.white;
            boxStyle.normal.background = resultTexture2;

            GUI.Box(new Rect((screenPoint.x + XOffset), (Screen.height - screenPoint.y + YOffset - 30.0f), widthTextBox, 30), new GUIContent(characterName), boxStyle);
        }
    }

    private int setBoxWidth(string characterName)
    {
        widthTextBox = characterName.Length * 12;

        return widthTextBox;
    }

    private void setShowBox(bool show)
    {
        if (show)
        {
            showBox = true;
        }
        else
        {
            showBox = false;
        }
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        if (width <= 0 || height <= 0)
        {
            width = 1;
            height = 1;
        }

        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

    private void setCurrentBoxPosition()
    {
        boxXPos = transform.position.x;
        boxYPos = transform.position.y;
    }
}

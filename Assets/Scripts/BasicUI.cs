using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicUI : MonoBehaviour
{
    [SerializeField] private Texture2D boxTexture;

    private GUIStyle boxStyle;

    void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int width = Screen.width / 12;
        int height = Screen.height / 12;
        int buffer = 10;

        boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.fontSize = height / 2;

        if (boxTexture != null)
        {
            boxStyle.normal.background = boxTexture;
        }

        List<string> itemList = Managers.Inventory.GetItemList();

        foreach (string item in itemList)
        {
            int count = Managers.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item);
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent(" " + (count > 1 ? count : "") + " ", image), boxStyle);
            posX += width + buffer;
        }
    }
}

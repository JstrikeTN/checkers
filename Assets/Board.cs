using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static int n = 8;
    public GameObject Cell;
    public static GameObject[,] g = new GameObject[n, n];
    public static Vector2 cmp;

    void Start()
    {
        
        Vector2 cs = transform.gameObject.GetComponent<RectTransform>().sizeDelta, size = Cell.GetComponent<RectTransform>().sizeDelta;
        cs.x /= 2;
        cs.y /= 2;
        float left = (cs.x - size.x) * -1, top = (cs.y - size.y);
        Color[] colors = new Color[] {Color.white, Color.black };
        Image drt = Cell.GetComponent<Image>(), Pawn = Cell.transform.Find("Pawn").GetComponent<Image>();
        for (int i = 0; i < n; i++)
        {
            if (i % 2 == 0) { 
                colors[0] = Color.black;
                colors[1] = Color.white;
            } else {
                colors[0] = Color.white;
                colors[1] = Color.black;
            }
            for (int j = 0; j < n; j++)
            {
                drt.color = colors[(((j % 2) == 0) ? 0 : 1)];
                if (i == (n / 2) - 1 || i == (n / 2) || drt.color == Color.white) Pawn.enabled = false;
                else Pawn.enabled = true;
                if (i < (n / 2)) Pawn.color = Color.red;
                else Pawn.color = Color.blue;
                if (drt.color == Color.white) Cell.transform.Find("k2").GetComponent<Image>().enabled = false;
                else Cell.transform.Find("k2").GetComponent<Image>().enabled = true;

                g[i ,j] = Instantiate(Cell);
                g[i, j].transform.SetParent(transform.Find("Board"));
                g[i, j].transform.localPosition = new Vector3(left, top);
                g[i, j].transform.name = i + "&" + j;
                left += size.x;
            }
            left = (cs.x - size.x) * -1;
            top -= size.y;
        }
    }

    public static void changeActivePawn() 
    {
        for ( int i = 0; i < 8; i++ )
            for ( int j = 0; j < 8; j++)
                g[i, j].transform.Find("k").gameObject.GetComponent<Image>().enabled = false;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    static string k, k2 = "";
    static Image Pawn;
    static Color currentColor = Color.blue;
    public GameObject P;

    public void PawnClick(RectTransform t) 
    {   
        Board.changeActivePawn();
        Pawn = t.transform.Find("Pawn").GetComponent<Image>();
        if (currentColor == Pawn.color)
        {
            string name = t.name;
            int i, j;
            i = Convert.ToInt32((name.Split('&'))[0]);
            j = Convert.ToInt32((name.Split('&'))[1]);
            int colorIndex = -1;
            if (Pawn.color == Color.red) colorIndex = 1;

            try 
            {
                if (!Board.g[i + colorIndex, j - 1].transform.Find("Pawn").GetComponent<Image>().enabled)
                {
                    Board.g[i + colorIndex, j - 1].transform.Find("k").GetComponent<Image>().enabled = true;
                }
                else if ( Board.g[i + colorIndex, j - 1].transform.Find("Pawn").GetComponent<Image>().color != Pawn.color && 
                        !Board.g[i + (colorIndex * 2), j - 2].transform.Find("Pawn").GetComponent<Image>().enabled )
                {
                    Board.g[i + (colorIndex * 2), j - 2].transform.Find("k").GetComponent<Image>().enabled = true;
                    k2 = (i + colorIndex) + " " + (j - 1);
                }
            } catch {}

            try {
                if (!Board.g[i + colorIndex, j + 1].transform.Find("Pawn").GetComponent<Image>().enabled)
                {
                    Board.g[i + colorIndex, j + 1].transform.Find("k").GetComponent<Image>().enabled = true;
                }
                else if (Board.g[i + colorIndex, j + 1].transform.Find("Pawn").GetComponent<Image>().color != Pawn.color &&
                        !Board.g[i + (colorIndex * 2), j + 2].transform.Find("Pawn").GetComponent<Image>().enabled)
                {
                    Board.g[i + (colorIndex * 2), j + 2].transform.Find("k").GetComponent<Image>().enabled = true;
                    k2 = (i + colorIndex) + " " + (j + 1);
                }
            } catch {}

            k = i + " " + j;
        }
    }
    public Text r, w;

    public void countPawns(char c)
    {
        if (c == 'w') Board.cmp.x++;
        else Board.cmp.y++;
        if (Board.cmp.x >= 12)
        {
            P.gameObject.SetActive(true);
            P.transform.Find("w").GetComponent<Text>().text = "Перемога синіх";
        }
        if (Board.cmp.y >= 12)
        {
            P.gameObject.SetActive(true);
            P.transform.Find("w").GetComponent<Text>().text = "Перемога червоних";
        }
    }

    public void movePawn(RectTransform t)
    {
        Board.changeActivePawn();
        string name = t.name;
        int i, j;
        i = Convert.ToInt32((name.Split('&'))[0]);
        j = Convert.ToInt32((name.Split('&'))[1]);
        Board.g[i, j].transform.Find("Pawn").GetComponent<Image>().color = currentColor;
        Board.g[i, j].transform.Find("Pawn").GetComponent<Image>().enabled = true;
        i = Convert.ToInt32((k.Split(' '))[0]);
        j = Convert.ToInt32((k.Split(' '))[1]);
        Board.g[i, j].transform.Find("Pawn").GetComponent<Image>().enabled = false;
        if (k2 != "" && 
            ((Convert.ToInt32((name.Split('&'))[1]) - 1) == Convert.ToInt32((k2.Split(' '))[1]) ||
            (Convert.ToInt32((name.Split('&'))[1]) + 1) == Convert.ToInt32((k2.Split(' '))[1])))
        {
            i = Convert.ToInt32((k2.Split(' '))[0]);
            j = Convert.ToInt32((k2.Split(' '))[1]);
            Board.g[i, j].transform.Find("Pawn").GetComponent<Image>().enabled = false;
            if (currentColor == Color.blue)
                countPawns('w');
            else 
                countPawns('r');
            r.text = Board.cmp.x + "";
            w.text = Board.cmp.y + "";

            k2 = "";
        }
        if (currentColor == Color.blue)
            currentColor = Color.red;
        else 
            currentColor = Color.blue;
    }
}


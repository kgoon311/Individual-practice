using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UnitType
{ 
    Unit,
    Node,
    OpenNode,
    Wall,
    Start,
    End
}

public class UnitScript : MonoBehaviour
{
    public UnitType type = UnitType.Unit;

    [SerializeField] private SpriteRenderer image;
    [SerializeField] private TextMesh gTxt,hTxt,fTxt;
    public float f_g,f_h,f_f;

    void Update()
    {
        gTxt.text = f_g.ToString();
        hTxt.text = f_h.ToString();
        fTxt.text = f_f.ToString();
        switch(type)
        {
            case UnitType.Unit:
                {
                    image.color = Color.white;
                    break;
                }
            case UnitType.Node:
                {
                    image.color = Color.yellow;
                    break;
                }
            case UnitType.Wall:
                {
                    image.color = Color.black ;
                    break;
                }
            case UnitType.Start:
                {
                    image.color = Color.red;
                    break;
                }
            case UnitType.End:
                {
                    image.color = Color.blue;
                    break;
                }
        }
    }
}

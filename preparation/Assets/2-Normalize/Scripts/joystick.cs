using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystick : MonoBehaviour
{
    [SerializeField] float limitePos;
    private bool move;
    private Vector3 mousePos;
    private Vector3 beforePos;
    private RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        beforePos = transform.position;
    }

    void Update()
    {
        if(move == true)
        {
            MoveJoyCon();
            if (Input.GetMouseButtonUp(0)) 
            {
                move = false;
                transform.position = beforePos;    
            }
        }
    }
    private void MoveJoyCon()
    {
        mousePos = Input.mousePosition;
        rectTransform.position = Camera.main.ScreenToWorldPoint(mousePos);
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, -limitePos, limitePos),
            Mathf.Clamp(transform.localPosition.y, -limitePos, limitePos),
            0
            );
        Debug.Log(transform.localPosition.x);
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            move = true;
        }
    }
}

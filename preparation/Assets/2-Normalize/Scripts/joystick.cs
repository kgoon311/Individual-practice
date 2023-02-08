using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystick : MonoBehaviour
{
    [SerializeField] float limitePos;
    [SerializeField] float moveSpeed;

    private bool isMove;

    private Vector3 mousePos;
    private Vector3 originPos;

    [SerializeField] GameObject player;
    void Start()
    {
        originPos = transform.position;
    }

    void Update()
    {
        if(isMove == true)
        {
            MoveJoyCon();
            if (Input.GetMouseButtonUp(0)) 
            {
                isMove = false;
                transform.position = originPos;    
            }
        }
    }
    private void MoveJoyCon()
    {
        mousePos = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, -limitePos, limitePos),
            Mathf.Clamp(transform.localPosition.y, -limitePos, limitePos),
            0);

        player.transform.position += transform.localPosition.normalized * Time.deltaTime * moveSpeed;
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
            isMove = true;
    }
}

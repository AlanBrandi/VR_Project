using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 5;
    public float turnSpeed = 60;
    bool isFoward = false;
    bool isBack = false;
    bool isLeft = false;
    bool isRight = false;

    void FixedUpdate()
    {
        if (isFoward)
        {
            transform.Translate(0, 0, (speed * Time.fixedDeltaTime));
        }
        if (isBack)
        {
            transform.Translate(0, 0, (-speed * Time.fixedDeltaTime));
        }
        if (isLeft)
        {
            transform.Rotate(0, ((-speed-turnSpeed) * Time.fixedDeltaTime), 0);
        }
        if (isRight)
        {
            transform.Rotate(0, ((speed + turnSpeed) * Time.fixedDeltaTime), 0);
        }
    }

    void EnableMoveController(bool frente, bool tras, bool esqueda, bool direita)
    {
        if (frente)
            isFoward = frente;
        if (tras)
            isBack = tras;
        if (esqueda)
            isLeft = esqueda;
        if (direita)
            isRight = direita;

    }

    public void DisableMoveController()
    {
        isFoward = false;
        isBack = false;
        isLeft = false;
        isRight = false;
    }

    public void Foward()
    {
        EnableMoveController(true, false, false,false);
    }

    public void Back()
    {
        EnableMoveController(false,true,false,false);
    }

    public void Left()
    {
        EnableMoveController(false,false,true,false);
    }

    public void Right()
    {
        EnableMoveController(false,false,false,true);
    }
}

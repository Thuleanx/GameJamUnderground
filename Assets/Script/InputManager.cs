
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector2 rawInputDirection;
    public Vector2 InputDirection {
        get { return rawInputDirection; }
    }

    Vector2 fireInputDirection;
    public Vector2 FireInputDirection {
        get { return fireInputDirection; }
    }

    // Update is called once per frame
    void Update()
    {
        rawInputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        fireInputDirection = new Vector2(Input.GetAxis("HorizontalFire"), Input.GetAxis("VerticalFire"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursour : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        Vector2 cursourPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursourPos;
    }
}

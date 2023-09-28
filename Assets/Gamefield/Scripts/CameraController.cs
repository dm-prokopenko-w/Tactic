using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController
{
    private float _speed = 15f;

    public void Move(PointerEventData eventData)
    {
        int dirX = 0;
        int dirY = 0;

        if (eventData.delta.x > 0)
        {
            dirX = -1;
        }
        else if (eventData.delta.x < 0)
        {
            dirX = 1;
        }

        if (eventData.delta.y > 0)
        {
            dirY = -1;
        }
        else if (eventData.delta.y < 0)
        {
            dirY = 1;
        }

        Camera.main.transform.Translate(new Vector3(dirX, dirY, 0) * Time.deltaTime * _speed, Space.World);
        return;
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 0)
            {
                MoveCamera(new Vector3(-1, 0, 0));
            }
            else if (eventData.delta.x < 0)
            {
                MoveCamera(new Vector3(1, 0, 0));
            }
        }
        else
        {
            if (eventData.delta.y > 0)
            {
                MoveCamera(new Vector3(0, -1, 0));
            }
            else if (eventData.delta.y < 0)                         
            {
                MoveCamera(new Vector3(0, 1, 0));
            }
        }
    }

    private void MoveCamera(Vector3 dir)
    {
        Camera.main.transform.Translate(dir * Time.deltaTime * _speed, Space.World);
    }
}
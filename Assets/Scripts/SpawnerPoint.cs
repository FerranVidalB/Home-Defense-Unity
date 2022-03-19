using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerPoint : MonoBehaviour
{
    private Image img;
    private Canvas canvas;
    public Transform target;



    private void Start()    {
        img = Instantiate((Image)Resources.Load("Prefabs/SpawnerIndicator/Image", typeof(Image)));

        canvas = GameObject.FindObjectOfType<Canvas>();
        img.transform.SetParent(canvas.transform, false);
    }
    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);
        if((pos.x>minX && pos.x < maxX ) && (pos.y > minY && pos.y < maxY))
        {
            img.enabled = false;
        }
        else
        {
            img.enabled = true;
        }
        
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }
    public void SetTarget(Transform spawner)
    {
        target = spawner;
    }
}

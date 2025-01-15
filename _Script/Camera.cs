using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Camera : MonoBehaviour
{
    public GameObject player;
    public float startX, endX;
    public float startY, endY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float camX = transform.position.x;
        float camY = transform.position.y;
        
        // giới hạn di chuyển camera trên trục X

        if (playerX > startX && playerX < endX)
        {
            camX = playerX;
        }
        else
        {
            if (playerX < startX)
            {
                camX = startX;
            }
            if (playerX > endX)
            {
                camX = endX;
            }
        }
        // giới hạn di chuyển camera trên trục Y

        if (playerY > startY && playerY < endY)
        {
            camY = playerY;
        }
        else
        {
            if (playerY < startY)
            {
                camY = startY;
            }
            if (playerY > endY)
            {
                camY = endY;
            }
           
        }
        transform.position = new Vector3(camX, camY, transform.position.z);
    }
}

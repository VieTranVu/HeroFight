using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public int speed = 1;
    public Transform startPoint;
    public Transform endPoint;
    private Vector2 point;

    void Start()
    {
        point = startPoint.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, startPoint.position) < 0.1f)
        {
            point = endPoint.position;
        }
        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f)
        {
            point = startPoint.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}

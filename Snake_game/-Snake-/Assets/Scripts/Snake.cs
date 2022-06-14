using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segment;
    public Transform segmentPrefab;


    // Start is called before the first frame update
    void Start()
    {
        segment = new List<Transform>();
        segment.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }


    private void FixedUpdate()
    {

        for (int i= segment.Count-1; i>0; i--)
        {
            segment[i].position = segment[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );
    }

    private void grow()
    {
        Transform segment2 = Instantiate(this.segmentPrefab);
        segment2.position = segment[segment.Count - 1].position;

        segment.Add(segment2);

    }

    private void resetState()
    {
        for (int i = 1; i < segment.Count; i++)
        {
            Destroy(segment[i].gameObject);
        }
        segment.Clear();
        segment.Add(this.transform);
        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "food")
        {
            grow();
        }
        else if (collision.tag == "obstacle")
        {
            resetState();
        }
        
    }
}

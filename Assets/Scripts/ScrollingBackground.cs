using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour
{
    private float bottomOfScreen;
    private float heightOfSprite;
    public float speed;
    private GameObject[] planets;
    // Use this for initialization
    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        bottomOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y;
        heightOfSprite = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y + heightOfSprite <= bottomOfScreen)
        {
            transform.position = new Vector3(0.0f, 50.0f, 0.0f);
            
        }

        transform.Translate(new Vector3(0.0f, -1.0f) * speed * Time.deltaTime);
        
    }
}

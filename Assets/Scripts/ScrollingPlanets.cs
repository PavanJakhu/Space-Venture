using UnityEngine;
using System.Collections;

public class ScrollingPlanets : MonoBehaviour
{
    public float speed;

    private float bottomOfScreen;
    private Bounds heightOfSprite;
    private Vector2 minBounds, maxBounds;

    // Use this for initialization
    void Start()
    {
        bottomOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y;
        heightOfSprite = GetComponent<SpriteRenderer>().bounds;

        minBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        maxBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y + heightOfSprite.extents.y <= bottomOfScreen)
        {
            transform.position = new Vector3(Random.Range(minBounds.x, maxBounds.x - heightOfSprite.extents.x), 50.0f, 0.0f);

        }

        transform.Translate(new Vector3(0.0f, -1.0f) * speed * Time.deltaTime);

    }
}

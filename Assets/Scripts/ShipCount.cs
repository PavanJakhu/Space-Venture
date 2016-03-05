using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipCount : MonoBehaviour
{
    public Image supportShipImagePrefab;

    private Flocking mainShip;
    private int supportShipCount;
    private int oldSupportShipCount;

    // Use this for initialization
    void Start()
    {
        mainShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Flocking>();
    }

    // Update is called once per frame
    void Update()
    {
        oldSupportShipCount = supportShipCount;
        supportShipCount = mainShip.GetSupportShips().Count;

        if (supportShipCount != oldSupportShipCount)
        {
            foreach (RectTransform trans in GetComponent<RectTransform>())
            {
                Destroy(trans.gameObject);
            }
            for (int i = 0; i < supportShipCount; i++)
            {
                Image supportShipImage = Instantiate(supportShipImagePrefab);
                supportShipImage.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>());
                supportShipImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                31.5f + 35.0f * (2.0f * i), -8.0f);
            }
        }
    }
}

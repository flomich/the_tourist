using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGeneratorScript : MonoBehaviour
{
    public int sorting_order = 0;
    public string sorting_layer_name;
    public Vector3 offset;

    public float start_position = 0.0f;
    public float end_position = 100.0f;

    public int max_buildings = 30;

    public float min_distance = 5.0f;
    public float max_distance = 20.0f;

    public List<GameObject> buildings;

    // Start is called before the first frame update
    void Start()
    {
        float current_position = start_position;

        for (int i = 0; i < max_buildings; i++)
        {
            Vector3 position = new Vector3(current_position, 0.0f, 0.0f);

            Instantiate(buildings[0], position + offset, Quaternion.identity, this.transform);

            current_position += Random.Range(min_distance, max_distance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getSortingOrder()
    {
        return sorting_order;
    }

    public string getSortingLayerName()
    {
        return sorting_layer_name;
    }
}

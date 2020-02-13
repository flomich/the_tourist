using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderScript : MonoBehaviour
{
    private SpriteRenderer sprite_renderer;
    private BuildingGeneratorScript building_generator;

    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        building_generator = GetComponentInParent<BuildingGeneratorScript>();
    }

    void Update()
    {
        if (sprite_renderer && building_generator)
        {
            sprite_renderer.sortingOrder = building_generator.getSortingOrder();
            sprite_renderer.sortingLayerName = building_generator.getSortingLayerName();
        }
    }
}

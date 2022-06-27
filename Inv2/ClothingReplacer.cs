using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingReplacer
{
    private readonly Transform _transform;

    public ClothingReplacer(GameObject go)
    {
        _transform = go.transform;
    }

    public void Replace(GameObject clothes)
    {
        Object.Destroy(_transform.GetChild(0).gameObject);
        GameObject duds = GameObject.Instantiate(clothes, _transform);
    }
}
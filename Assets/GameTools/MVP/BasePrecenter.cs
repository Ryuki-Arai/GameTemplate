using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePrecenter : MonoBehaviour
{
    [SerializeField] private BaseView view;
    private BaseModel model;

    public void Initialize()
    {
        model = new BaseModel();
    }
}

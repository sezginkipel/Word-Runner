using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordController : MonoBehaviour
{
    [Header("Word Controls")] [SerializeField]
    private float wordMovementSpeed = 50f;
    
    void Update()
    {
        MoveWord();
    }
    
    void MoveWord()
    {
        transform.Translate(Vector3.back * (wordMovementSpeed * Time.deltaTime));
    }
}

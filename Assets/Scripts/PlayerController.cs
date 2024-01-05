using System;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] TMP_Text scoreText;

    public GameObject wordGenerator;
    public Joystick joystick;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    
    private void Update()
    {
        MovePlayerWithJoystick();
    }


    void MovePlayerWithJoystick()
    {
        var position = transform.position;
        position = new Vector3(Mathf.Clamp(position.x + joystick.Horizontal * Time.deltaTime * 5, -1.5f, 1.5f), position.y, position.z);
        transform.position = position;
    }

    void OnTriggerEnter(Collider triggerObj)
    {
        if (triggerObj.gameObject.CompareTag("CorrectObject") || triggerObj.gameObject.CompareTag("WrongObject"))
        {
            if (triggerObj.gameObject.CompareTag("CorrectObject"))
            {
                score += 10;
                audioSource.clip = audioClips[0];
                audioSource.Play();
                scoreText.text = score.ToString();
            }
            else
            {
                score -= 5;
                audioSource.clip = audioClips[1];
                audioSource.Play();
                scoreText.text = score.ToString();
            }

            Destroy(triggerObj.gameObject);
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("CorrectObject");
            foreach (var obj in allObjects)
            {
                if (obj != triggerObj.gameObject)
                {
                    Destroy(obj);
                }
            }

            allObjects = GameObject.FindGameObjectsWithTag("WrongObject");
            foreach (var obj in allObjects)
            {
                if (obj != triggerObj.gameObject)
                {
                    Destroy(obj);
                }
            }
            
            wordGenerator.gameObject.GetComponent<WordGenerator>().GenerateNewWordAndObjects();
        }
    }
}
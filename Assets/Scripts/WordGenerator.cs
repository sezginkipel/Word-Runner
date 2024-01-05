using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class WordGenerator : MonoBehaviour
{
    public TMP_Text wordText;
    public GameObject objectPrefab;
    public List<Sprite> objectSprites;

    private string _currentWord;
    private Dictionary<string, Sprite> _wordToSpriteMapping;

    private Vector3 _lastObjectPosition = Vector3.zero;
    private readonly float _minDistanceBetweenObjects = 2.5f;


    void Start()
    {
        _wordToSpriteMapping = new Dictionary<string, Sprite>();
        InitializeWordToSpriteMapping();
        GenerateNewWordAndObjects();
    }

    void InitializeWordToSpriteMapping()
    {
        _wordToSpriteMapping.Add("Apple", objectSprites[0]);
        _wordToSpriteMapping.Add("Banana", objectSprites[1]);
        _wordToSpriteMapping.Add("Orange", objectSprites[2]);
        _wordToSpriteMapping.Add("Pineapple", objectSprites[3]);
        _wordToSpriteMapping.Add("Pear", objectSprites[4]);
        _wordToSpriteMapping.Add("Kiwi", objectSprites[5]);
        _wordToSpriteMapping.Add("Lemon", objectSprites[6]);
    }

    internal void GenerateNewWordAndObjects()
    {
        _currentWord = GetRandomWord();
        wordText.text = _currentWord;

        Vector3 position1 = GetRandomPosition();
        Vector3 position2 = GetRandomPosition(position1);

        if (Random.Range(0, 2) == 0)
        {
            CreateObject(true, position1);
            CreateObject(false, position2);
        }
        else
        {
            CreateObject(false, position1);
            CreateObject(true, position2);
        }
    }

    string GetRandomWord()
    {
        int randomIndex = Random.Range(0, _wordToSpriteMapping.Count);
        return _wordToSpriteMapping.Keys.ToArray()[randomIndex];
    }

    void CreateObject(bool isCorrect, Vector3 position)
    {
        GameObject newObj = Instantiate(objectPrefab, position, Quaternion.identity);
        SpriteRenderer spriteRenderer = newObj.GetComponent<SpriteRenderer>();

        if (isCorrect)
        {
            spriteRenderer.sprite = _wordToSpriteMapping[_currentWord];
            newObj.tag = "CorrectObject";
        }
        else
        {
            string randomWord;
            do
            {
                randomWord = GetRandomWord();
            } while (randomWord == _currentWord);

            spriteRenderer.sprite = _wordToSpriteMapping[randomWord];
            newObj.tag = "WrongObject";
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 newPosition;
        do
        {
            newPosition = new Vector3(Random.Range(-1.7f, 1.7f), 1.5f, 35);
        } while (Vector3.Distance(newPosition, _lastObjectPosition) < _minDistanceBetweenObjects);

        _lastObjectPosition = newPosition;
        return newPosition;
    }

    Vector3 GetRandomPosition(Vector3 otherPosition)
    {
        Vector3 newPosition;
        do
        {
            newPosition = GetRandomPosition();
        } while (newPosition == otherPosition);

        return newPosition;
    }
}
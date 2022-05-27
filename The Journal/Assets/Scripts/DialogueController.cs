using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{

    public TextMeshProUGUI textBox;
    public string[] sentences;
    public float dialogueSpeed;
    private int index = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)){
            NextSentence();
        }
    }

    void NextSentence()
    {
        if(index <= sentences.Length - 1)
        {
            textBox.text = "";
            StartCoroutine(WriteSentence());
        }
    }

    IEnumerator WriteSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        index++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{

    public TextMeshProUGUI textBox;
    public TextAsset script;
    private bool sentenceWrote = true;
    private bool sentenceInterrupt = false;
    private float dialogueSpeed = 0.05f;
    private string[] sentences;
    private int index = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ParseDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)){
            if (sentenceWrote)
            {
                sentenceInterrupt = false;
                NextSentence();
            }
            else {
                sentenceInterrupt = true;
                StopCoroutine(WriteSentence());
                textBox.text = sentences[index];
                index++;
                sentenceWrote = true;
            }
        }
    }

    void NextSentence()
    {
        if (index <= sentences.Length - 1)
        {
            textBox.text = "";
            StartCoroutine(WriteSentence());
        }
    }

    void ParseDialogue()
    {
        string text = script.text;
        sentences = text.Split('\n');
    }

    IEnumerator WriteSentence()
    {
        sentenceWrote = false;
        foreach(char letter in sentences[index].ToCharArray())
        {
            if (sentenceInterrupt)
            {
                yield break;
            }
            textBox.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        index++;
        sentenceWrote = true;
    }
}

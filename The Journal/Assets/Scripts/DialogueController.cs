using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    public TextMeshProUGUI textBox;
    public TextAsset script;
    public Canvas townCanvas;
    public Canvas buttonCanvas;
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
    void OnEnable()
    {
        textBox.text = "";
        if (SceneManager.GetActiveScene().name == "Town")
        {
            foreach (var btn in buttonCanvas.GetComponentsInChildren<Button>(true))
            {
                btn.interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)){
            if (SceneManager.GetActiveScene().name == "Beginning" && index == sentences.Length - 1)
            {
                SceneManager.LoadScene("Town");
            }
            if (SceneManager.GetActiveScene().name == "End" && index == sentences.Length - 1)
            {
                SceneManager.LoadScene("Credits");
            }
            if (SceneManager.GetActiveScene().name == "Town" && index == sentences.Length-1)
            {
                townCanvas.gameObject.SetActive(false);
                foreach (var btn in buttonCanvas.GetComponentsInChildren<Button>(true))
                {
                    btn.interactable = true;
                }
                Destroy(this.gameObject);
            }
            if (SceneManager.GetActiveScene().name == "Beginning" && index == 78)
            {
                textBox.fontStyle = FontStyles.Italic;
                textBox.horizontalAlignment = HorizontalAlignmentOptions.Center;
                textBox.verticalAlignment = VerticalAlignmentOptions.Middle;
            }
            else
            {
                textBox.fontStyle = FontStyles.Normal;
                textBox.horizontalAlignment = HorizontalAlignmentOptions.Left;
                textBox.verticalAlignment = VerticalAlignmentOptions.Top;
            }
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

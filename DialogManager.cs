using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private Phrase[] phrases;
    [SerializeField]
    private TMP_Text nameCharacter;
    [SerializeField]
    private TMP_Text textPhraseCharacter;
    [SerializeField]
    private Image characterSprite;
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private CharacterScriptableObject thisCharacter;
    private int indexPhrase;
    private bool isEnd;
    //[SerializeField]
    //private string followUpPhrase;
    //[SerializeField]
    //private bool transitionNextScene;
    //[SerializeField]
    //private string nextScene;


    public bool isDialogEnd;

    private void OnEnable() 
    {
        BlockKeys.DialogOpened();
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Speed", 0);
        float vertical = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Vertical");
        float horizontal = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Horizontal");
        if (vertical != 0 && horizontal != 0)
        {
            GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Vertical", 0);
        }
        isDialogEnd = false;
        indexPhrase = 0;
        nameCharacter.text = "";
        textPhraseCharacter.text = "";
        characterSprite.enabled = true;
        nameCharacter.text = phrases[indexPhrase].characterScriptableObject.nameCharacter;
        characterSprite.sprite = phrases[indexPhrase].characterScriptableObject.characterSprite;
        StartCoroutine(OutputTextPhrase());
        indexPhrase++;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isEnd)
        {
            isEnd = false;
            NextPhrase();
        }
    }
    public void NextPhrase()
    {
        nameCharacter.text = "";
        textPhraseCharacter.text = "";
        characterSprite.enabled = false;
        if (indexPhrase < phrases.Length)
        {
            nameCharacter.text = phrases[indexPhrase].characterScriptableObject.nameCharacter;
            characterSprite.enabled = true;
            characterSprite.sprite = phrases[indexPhrase].characterScriptableObject.characterSprite;
            StartCoroutine(OutputTextPhrase());
            indexPhrase++;
        }
        else
        {
            dialoguePanel.SetActive(false);
            indexPhrase = 0;
            isDialogEnd = true;
            //sceneController.ChangingLogicScene();
            //if (transitionNextScene)
            //{
            //    SceneManager.LoadScene(nextScene);
            //}
            //if (phrases.Length > 1)
            //{
            //    phrases = null;
            //    phrases = new Phrase[1];
            //    phrases[0].textPhrase = followUpPhrase;
            //    phrases[0].characterScriptableObject = thisCharacter;
            //}
            BlockKeys.DialogClosed();
        }
    }

    IEnumerator OutputTextPhrase() 
    {
        foreach (char letters in phrases[indexPhrase].textPhrase.ToCharArray())
        {
            textPhraseCharacter.text += letters;
            yield return new WaitForSeconds(0.02f);
        }
        isEnd = true;
    }
}

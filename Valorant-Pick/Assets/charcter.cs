using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class charcter : MonoBehaviour
{
    public int input;
    private Coroutine currentFadeCoroutine;
    private Coroutine timeOutMenuCoroutine;
    public int i;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI Text2;
    public float Second = 60;
    public float time; 
    [Range(-1, 9)] public int testIndex = -1; 
    public GameObject Grid;
    public Image targetUiImage;
    public Image targetUiImage2;
    public Image role;
    public GameObject Pick;
    public GameObject Pick2;
    public GameObject Panel;
    public Sprite[] characterSprites = new Sprite[10];
    public Sprite[] Illustration = new Sprite[10];
    public Sprite[] sprites = new Sprite[4];

    private CanvasGroup gridCanvasGroup;
    private bool isPicked = false;

    void Start()
    {
        input = 0;
        Pick.SetActive(true);
        i = 1;
        Panel.SetActive(false);

        if (Grid != null)
        {
            gridCanvasGroup = Grid.GetComponent<CanvasGroup>();
            if (gridCanvasGroup == null)
            {
                gridCanvasGroup = Grid.AddComponent<CanvasGroup>();
            }
        }

        SetGridInteractable(true);
        UpdateUIState();
    }

    private void Update()
    {
        if (i == 1)
        {
            time += Time.deltaTime;
            if (time >= 1f)
            {
                Second--;
                Text2.text = $"{Second}";
                time = 0f;
            } 
        }

        if (Second <= 0 && i == 1)
        {
            i = 0;
            Pick2.SetActive(false);
            Pick.SetActive(false);
            SetGridInteractable(false);
            
            if (timeOutMenuCoroutine != null) StopCoroutine(timeOutMenuCoroutine);
            timeOutMenuCoroutine = StartCoroutine(OpenMenuDelayedRoutine(2f));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void SetIndexByName(string characterName)
    {
        if (i == 0 || isPicked) return;
        
        string cleanName = characterName.Trim().ToUpper();
        
        switch (cleanName)
        {
            case "J":  testIndex = 0; break;
            case "P":  testIndex = 1; break;
            case "R":  testIndex = 2; break;
            case "S":  testIndex = 3; break;
            case "B":  testIndex = 4; break;
            case "O":  testIndex = 5; break;
            case "V":  testIndex = 6; break;
            case "B1": testIndex = 7; break;
            case "S1": testIndex = 8; break;
            case "S2": testIndex = 9; break;
            default:   testIndex = -1; break;
        }

        UpdateUIState();
    }

    private void UpdateUIState()
    {
        switch (testIndex)
        {
            case 0:  Text.text = "Jett"; break;
            case 1:  Text.text = "Phoenix"; break;
            case 2:  Text.text = "Raze"; break;
            case 3:  Text.text = "Sova"; break;
            case 4:  Text.text = "Breach"; break;
            case 5:  Text.text = "Omen"; break;
            case 6:  Text.text = "Viper"; break;
            case 7:  Text.text = "Brimstone"; break;
            case 8:  Text.text = "Sage"; break;
            case 9:  Text.text = "Cypher"; break;
            default: Text.text = "Pick..."; break;
        }

        if (testIndex >= 0 && testIndex <= 2) role.sprite = sprites[0];
        else if (testIndex >= 3 && testIndex <= 4) role.sprite = sprites[1];
        else if (testIndex >= 5 && testIndex <= 7) role.sprite = sprites[2];
        else if (testIndex >= 8 && testIndex <= 9) role.sprite = sprites[3];

        if (testIndex > -1)
        {
            if (targetUiImage != null) targetUiImage.color = Color.white;
            if (targetUiImage2 != null) targetUiImage2.color = Color.white;
            if (role != null) role.color = Color.white;
            Text.color = new Color32(177, 255, 252, 255);
            RefreshImage(testIndex);
        }
        else
        {
            if (targetUiImage != null) targetUiImage.color = Color.clear;
            if (targetUiImage2 != null) targetUiImage2.color = Color.clear;
            if (role != null) role.color = Color.clear;
            Text.color = new Color32(203, 203, 203, 255);
        }
    }

    private void RefreshImage(int index)
    {
        if (targetUiImage == null || characterSprites == null || Illustration == null) return;
        if (index < 0 || index >= characterSprites.Length || index >= Illustration.Length) return;

        if (characterSprites[index] != null && Illustration[index] != null)
        {
            targetUiImage.sprite = characterSprites[index];
            targetUiImage2.sprite = Illustration[index];
        }
    }

    public void pick()
    {
        if (testIndex > -1 && i == 1 && !isPicked)
        {
            isPicked = true;

            SetGridInteractable(false);

            Image targetImage = Pick2.GetComponent<Image>();
            TextMeshProUGUI targetText = Pick2.GetComponentInChildren<TextMeshProUGUI>();

            if (targetImage != null) targetImage.color = new Color32(133, 245, 229, 255);
            if (targetText != null) targetText.color = new Color32(255, 255, 255, 255);

            if (currentFadeCoroutine != null) StopCoroutine(currentFadeCoroutine);
            currentFadeCoroutine = StartCoroutine(FadeOutRoutine());
        }
    }

    private IEnumerator FadeOutRoutine()
    {
        Image targetImage = Pick2.GetComponent<Image>();
        TextMeshProUGUI targetText = Pick2.GetComponentInChildren<TextMeshProUGUI>();
        float alpha = 1f;

        while (alpha > 0)
        {
            alpha -= Time.unscaledDeltaTime * 2f;
            if (alpha < 0) alpha = 0;

            if (targetImage != null)
            {
                Color c = targetImage.color;
                c.a = alpha;
                targetImage.color = c;
            }
            if (targetText != null)
            {
                Color c = targetText.color;
                c.a = alpha;
                targetText.color = c;
            }
            yield return null;
        }

        Pick2.GetComponent<RectTransform>().anchoredPosition = new Vector2(513.07f, -427.6f);
        currentFadeCoroutine = null;
    }

    private IEnumerator OpenMenuDelayedRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Panel.SetActive(true);
        timeOutMenuCoroutine = null;
    }

    public void ResetSelection()
    {
        Time.timeScale = 1;
        input = 0;
        i = 1;
        isPicked = false;
        Second = 60;
        time = 0f;
        testIndex = -1;

        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
            currentFadeCoroutine = null;
        }
        if (timeOutMenuCoroutine != null)
        {
            StopCoroutine(timeOutMenuCoroutine);
            timeOutMenuCoroutine = null;
        }

        Text2.text = $"{Second}"; 
        Pick.SetActive(true);
        Panel.SetActive(false);
        Pick2.SetActive(true);

        UpdateUIState();

        Image targetImage = Pick2.GetComponent<Image>();
        TextMeshProUGUI targetText = Pick2.GetComponentInChildren<TextMeshProUGUI>();
        if (targetImage != null) targetImage.color = new Color32(133, 245, 229, 190);
        if (targetText != null) targetText.color = new Color32(255, 255, 255, 255);

        Pick2.GetComponent<RectTransform>().anchoredPosition = new Vector2(513.07f, -427.6f);
        
        SetGridInteractable(true);
    }

    private void SetGridInteractable(bool state)
    {
        if (gridCanvasGroup != null)
        {
            gridCanvasGroup.blocksRaycasts = state;
        }
    }

    private void ToggleMenu()
    {
        if (input == 0)
        {
            Panel.SetActive(true);
            Time.timeScale = 0;
            input = 1;
        }
        else
        {
            Time.timeScale = 1;
            Panel.SetActive(false);
            input = 0;
        }
    }

    public void Quit()
    {
        Debug.Log("게임을 종료합니다.");
        Application.Quit();
    }
}
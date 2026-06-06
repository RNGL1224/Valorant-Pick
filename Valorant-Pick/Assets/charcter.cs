using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class charcter : MonoBehaviour
{
    private Coroutine currentFadeCoroutine;
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

    void Start()
    {
        Pick.SetActive(true);
        i = 1;
        Panel.SetActive(false);
        RefreshImage(testIndex);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1f && i == 1)
        {
            Second--;
            Text2.text = $"{Second}";
            time = 0f;
        } 

        switch (testIndex)
        {
            case 0:  Text.text = "제트"; break;
            case 1:  Text.text = "피닉스"; break;
            case 2:  Text.text = "레이즈"; break;
            case 3:  Text.text = "소바"; break;
            case 4:  Text.text = "브리치"; break;
            case 5:  Text.text = "오멘"; break;
            case 6:  Text.text = "바이퍼"; break;
            case 7:  Text.text = "브림스톤"; break;
            case 8:  Text.text = "세이지"; break;
            case 9:  Text.text = "사이퍼"; break;
            default: Text.text = "선택 중..."; break;
        }

        if (testIndex == 0 || testIndex == 1 || testIndex == 2)
        {
            role.sprite = sprites[0];
        }
        if (testIndex == 3 || testIndex == 4)
        {
            role.sprite = sprites[1];
        }
        if (testIndex == 5 || testIndex == 6 || testIndex == 7)
        {
            role.sprite = sprites[2];
        }
        if (testIndex == 8 || testIndex == 9)
        {
            role.sprite = sprites[3];
        }

        if (targetUiImage == null) return;

        if (i == 1)
        {
            if (testIndex > -1)
            {
                targetUiImage.color = Color.white;
                targetUiImage2.color = Color.white;
                role.color = Color.white;
                Text.color = new Color32(177, 255, 252, 255);
                RefreshImage(testIndex);
                Panel.SetActive(false);
            }
            else
            {
                targetUiImage2.color = Color.clear;
                targetUiImage.color = Color.clear;
                role.color = Color.clear;
                Text.color = new Color32(203, 203, 203, 255);
                Panel.SetActive(false);
            }
        }

        if (Second == 0 && testIndex > -1)
        {
            Pick2.SetActive(false);
            Pick.SetActive(false);
            Panel.SetActive(true);
            RefreshImage(testIndex);
            i = 0;
        }
        if (Second == 0 && testIndex >= -1)
        {
            Panel.SetActive(true);
            i = 0;
        }
    }

    private void RefreshImage(int index)
    {
        if (targetUiImage == null || characterSprites == null || Illustration == null) return;
        if (index < 0 || index >= characterSprites.Length || index >= Illustration.Length) return;

        if (characterSprites[index] && Illustration[index] != null)
        {
            targetUiImage.sprite = characterSprites[index];
            targetUiImage2.sprite = Illustration[index];
        }
    }

    public void SetIndexByName(string characterName)
    {
        if (i == 0) return;
        
        switch (characterName)
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
    }

    public void pick()
    {
        if (testIndex > -1 && i == 1)
        {
            i = 0;

            Image[] gridImages = Grid.GetComponentsInChildren<Image>();
            for (int index = 0; index < gridImages.Length; index++)
            {
                gridImages[index].raycastTarget = false;
            }

            Image pickImage = Pick.GetComponent<Image>();
            if (pickImage != null) pickImage.raycastTarget = false;

            Image targetImage = Pick2.GetComponent<Image>();
            TextMeshProUGUI targetText = Pick2.GetComponentInChildren<TextMeshProUGUI>();

            if (targetImage != null) targetImage.color = new Color32(133, 245, 229, 255);
            if (targetText != null) targetText.color = new Color32(255, 255, 255, 255);

            if (currentFadeCoroutine != null)
            {
                StopCoroutine(currentFadeCoroutine);
            }

            currentFadeCoroutine = StartCoroutine(FadeOutRoutine());
        }
    }

    private IEnumerator FadeOutRoutine()
    {
        Image targetImage = Pick2.GetComponent<Image>();
        TextMeshProUGUI targetText = Pick2.GetComponentInChildren<TextMeshProUGUI>();

        float alpha = 255f;

        while (alpha > 0)
        {
            yield return new WaitForSeconds(0.0001f);
            alpha -= 5f;

            if (alpha < 0) alpha = 0;

            byte byteAlpha = (byte)alpha;

            if (targetImage != null)
            {
                Color32 c = targetImage.color;
                c.a = byteAlpha;
                targetImage.color = c;
            }

            if (targetText != null)
            {
                Color32 c = targetText.color;
                c.a = byteAlpha;
                targetText.color = c;
            }
        }

        Pick2.GetComponent<RectTransform>().anchoredPosition = new Vector2(513.07f, -427.6f);
        currentFadeCoroutine = null;
    }

    public void ResetSelection()
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
            currentFadeCoroutine = null;
        }

        Second = 60;
        testIndex = -1;
        Text2.text = $"{Second}"; 
        Pick.SetActive(true);
        Panel.SetActive(false);
        RefreshImage(testIndex);
        
        Pick2.SetActive(true);
        Image targetImage = Pick2.GetComponent<Image>();
        TextMeshProUGUI targetText = Pick2.GetComponentInChildren<TextMeshProUGUI>();
        if (targetImage != null) targetImage.color = new Color32(133, 245, 229, 255);
        if (targetText != null) targetText.color = new Color32(255, 255, 255, 255);

        Pick2.GetComponent<RectTransform>().anchoredPosition = new Vector2(513.07f, -427.6f);
        
        i = 1;

        Image[] gridImages = Grid.GetComponentsInChildren<Image>();
        for (int index = 0; index < gridImages.Length; index++)
        {
            gridImages[index].raycastTarget = true;
        }

        Image pickImage = Pick.GetComponent<Image>();
        if (pickImage != null) pickImage.raycastTarget = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
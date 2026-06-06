using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class charcter : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public TextMeshProUGUI Text2;
    public float Second = 60;
    public float time; 
    [Range(-1, 9)] public int testIndex = -1; 
    public Image targetUiImage;
    public Image targetUiImage2;
    public Image role;
    public GameObject Pick;
    public GameObject Panel;
    public Sprite[] characterSprites = new Sprite[10];
    public Sprite[] Illustration = new Sprite[10];
    public Sprite[] sprites = new Sprite[4];

    void Start()
    {
        Panel.SetActive(false);
        RefreshImage(testIndex);
    }

    private void Update()
    {
        //타이머
        time += Time.deltaTime;
        if (time >= 1f)
        {
            Second--;
            Text2.text = $"{Second}";
            time = 0f;
        } 

        //캐릭터 감지
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
        //캐릭터 이미지
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

        //캐릭터 픽
        if (targetUiImage == null) return;

        if (testIndex > -1)
        {
            targetUiImage.color = Color.white;
            targetUiImage2.color = Color.white;
            role.color = Color.white;
            Text.color = new Color32(177, 255, 252, 255);
            RefreshImage(testIndex);
        }
        else
        {
            targetUiImage2.color = Color.clear;
            targetUiImage.color = Color.clear;
            role.color = Color.clear;
            Text.color = new Color32(203, 203, 203, 255);
        }

        if (time == 0 && testIndex > -1)
        {
            Pick.SetActive(false);
            Panel.SetActive(true);
            RefreshImage(testIndex);
        }
        if (time == 0 && testIndex <= -1)
        {
            Pick.SetActive(false);
            Panel.SetActive(true);
        }
    }

    //매개 변수index에 따라 이미지 변경
    private void RefreshImage(int index)
    {
        if (targetUiImage == null || characterSprites == null || Illustration == null) return;
        if (index < 0 || index >= characterSprites.Length || index >= Illustration.Length) return;

        if (characterSprites[index] && Illustration[index] != null)
        {
            targetUiImage.sprite = characterSprites[index];
            targetUiImage2.sprite = Illustration[index];
            Debug.Log("실행");
            //targetUiImage.sprite = Illustration[index];
        }
    }
    //캐릭터에 따라 i를 설정.
    public void SetIndexByName(string characterName)
    {
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
        //testIndex = i;
        Debug.Log($"현재 testIndex의 값: {testIndex} ({characterName} 선택됨)");
    }

    public void ResetSelection()
    {
        testIndex = -1;
        time = 60; // 타이머 초기화
        Text2.text = $"{Second}"; // 타이머 텍스트 초기화
        Pick.SetActive(true);
        Panel.SetActive(false);
        RefreshImage(testIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class charcter : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public int i = -1;
    [Range(-1, 9)] public int testIndex = -1; 
    public Image targetUiImage;
    public Image targetUiImage2;
    public Image role;
    public Sprite[] characterSprites = new Sprite[10];
    public Sprite[] Illustration = new Sprite[10];
    public Sprite[] sprites = new Sprite[4];

    void Start()
    {
        RefreshImage(testIndex);
    }

    private void Update()
    {
        switch (i)
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

        if (i == 0 || i == 1 || i == 2)
        {
            role.sprite = sprites[0];
        }
        if (i == 3 || i == 4)
        {
            role.sprite = sprites[1];
        }
        if (i == 5 || i == 6 || i == 7)
        {
            role.sprite = sprites[2];
        }
        if (i == 8 || i == 9)
        {
            role.sprite = sprites[3];
        }   
        Debug.Log($"현재 testIndex의 값: {testIndex} ({Text.text} 선택됨)");
        if (targetUiImage == null) return;

        if (testIndex > -1)
        {
            targetUiImage.color = Color.white;
            targetUiImage2.color = Color.white;
            role.color = Color.white;
            RefreshImage(testIndex);
        }
        else
        {
            targetUiImage2.color = Color.clear;
            targetUiImage.color = Color.clear;
            role.color = Color.clear;
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
            Debug.Log("실행");
            //targetUiImage.sprite = Illustration[index];
        }
    }
    public void SetIndexByName(string characterName)
    {
        switch (characterName)
        {
            case "J":  i = 0; break;
            case "P":  i = 1; break;
            case "R":  i = 2; break;
            case "S":  i = 3; break;
            case "B":  i = 4; break;
            case "O":  i = 5; break;
            case "V":  i = 6; break;
            case "B1": i = 7; break;
            case "S1": i = 8; break;
            case "S2": i = 9; break;
            default:   i = -1; break;
        }
        testIndex = i;
        Debug.Log($"현재 testIndex의 값: {testIndex} ({characterName} 선택됨)");
    }
}
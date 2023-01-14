using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 클릭되면 캐릭터 선택 씬으로 이동
public class SelectLoad : MonoBehaviour
{
    Button Button;

    private void Awake()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(SelectLoadScene);
    }

    private void SelectLoadScene()
    {
        SceneManager.LoadScene("CharactersSelect");
    }
}

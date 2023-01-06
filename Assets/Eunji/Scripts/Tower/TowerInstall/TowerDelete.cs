using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// 타워 삭제가 클릭이 되었는지 확인
public class TowerDelete : MonoBehaviour
{
    bool ok = false;    // 삭제 확인용 변수

    Button okButton;    // ok버튼

    public bool OK
    {
        get => ok;
        private set => ok = value;
    }

    public  void Awake()
    {
        okButton = transform.GetChild(2).GetComponent<Button>();
        okButton.onClick.AddListener(DeleteCheck);      // 오케이 버튼을 누르면 DeleteCheck함수 실행
    }

    void DeleteCheck()
    {
        OK = true;      // 삭제 승낙
    }

}

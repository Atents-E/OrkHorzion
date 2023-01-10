using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{
    // 기능들 정리 -----------------------------------------------------------------------------------------------------------------------------
    // 1. 라운드가 승리하면 리워드창을 띄운다.
    // 2. 아이템패널 두개를 선택할 수 있고 그 이상은 선택을 못한다. 같은 곳을 누르면 선택이 해제된다.
    // 3. 선택이 끝나면 보상 받기 버튼이 나온다.
    // 4. 보상 받기를 누르면 해당 아이템이 인벤토리에 추가된다.

    // 디테일하게 기능 정리 --------------------------------------------------------------------------------------------------------------------
    // 1. 라운드 승리를 어떻게 인식할 것인가?
            // 1.1 해당 라운드가 승리했을때의 대한 트리거를 따로 제공받아야한다.

    // 2. 아이템 선택의 로직을 어떻게 짤 것인가?
            // 2.1 5개의 아이템패널 중 하나를 누르면 선택상태로 바뀐다.(패널의 색깔도 변경한다)
            // 2.2 각 아이템 패널의 선택 상태를 체크하여 2개까지만 선택되도록 체크한다.
            // 2.3 이미 선택된 아이템 패널을 누르면 다시 선택상태가 해제된다.
            // 2.4 선택이 두개이상 되면 하단에 보상 받기 버튼이 생기고, 그 외에는 다시 안보이게 처리한다.


    // 3. 리워드 창안에 아이템들을 어떻게 구성할 것인가?
            // 3.1 아이템패널 스크립트를 만들고 배열을 구성한다.
            // 3.2 두번째와 네번째는 골드보상으로 고정하고 500에서 1000골드 사이로 100골드 단위로 랜덤 보상을 띄운다.
            // 3.3 나머지 패널에는 아이템보상을 구성하되, 다음과 같은 상황을 체크한다.
                // 3.3.1 보상할 아이템이 이미 가지고 있는 최대갯수이면 다른거로 바꾼다.
                // 3.3.2 만약 모든 아이템을 가지고 있다면 골드보상으로 대체한다.
    
    // 4. 만약 인벤토리에 슬롯이 모자라면 어떻게 할 것인가?
            // 4.1 일단 리워드창을 닫고 인벤토리 창을 띄운다.
                // 4.1.1 해당 인벤토리 창에서 창을 닫으려고 하면 팝업창을 띄운다.
                // "보상을 받기위해 충분한 슬롯을 마련하지 않았습니다. 보상을 다시 선택하시겠습니까?
                // "다시 선택"  --> 다시 선택을 누르면 리워드창이 다시 나타나고 인벤토리 창을 다시 닫는다.
                // "보상 포기"  --> 보상 포기를 누르면 인벤토리 창을 닫고 보상을 포기한다.                     
            // 4.2 부족한 슬롯만큼 인벤토리안에서 아이템을 버린다.
            // 4.3 보상을 받을 슬롯이 생기면 선택한 아이템이 인벤토리에 추가된다.

    // 추가해야할 변수들 --------------------------------------------------------------------------------------------------------------------
    // 1. 플레이어의 인벤토리
    // 2. 제어할 수 있는 아이템패널 클래스
    // 3. 확인 버튼
    // 4. 각 상황을 알려줄 텍스트

    InventoryUI invenUI;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemDescription;
    Sprite itemIcon;


    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            // 3.3.1 보상할 아이템이 이미 가지고 있는 아이템이고 그 아이템이 최대갯수이면 다른거로 바꾼다.
            // 3.3.2 가지고 있는 아이템이 최대갯수는 아닌데 보상 리스트에 아이템이 중복으로 나와 둘 다 고르면 최대 갯수가 넘는 경우를 제외시켜야함
            // 3.3.3 보상 리스트 아이템에 보상아이템의 최대갯수가 1인경우의 아이템은 다른 아이템패널에서 또 나오지않게 제외시켜야함
            // 3.3.4 만약 모든 아이템을 가지고 있다면 골드보상으로 대체한다.

    // 4. 만약 인벤토리에 슬롯이 모자라면 어떻게 할 것인가?
        // 4.1 일단 리워드창을 닫고 인벤토리 창을 띄운다.
            // 4.1.1 해당 인벤토리 창에서 창을 닫으려고 하면 팝업창을 띄운다.
            // "보상을 받기위해 충분한 슬롯을 마련하지 않았습니다. 보상을 다시 선택하시겠습니까?
            // "다시 선택"  --> 다시 선택을 누르면 리워드창이 다시 나타나고 인벤토리 창을 다시 닫는다.
            // "보상 포기"  --> 보상 포기를 누르면 인벤토리 창을 닫고 보상을 포기한다.                     
        // 4.2 부족한 슬롯만큼 인벤토리안에서 아이템을 버린다.
        // 4.3 보상을 받을 슬롯이 생기면 선택한 아이템이 인벤토리에 추가된다.

    // 5. 주의사항
    // 리워드창이 나오면 타워탭과 인벤토리탭을 누를 수 없게 제어해야함
    // 보상 처리가 끝나면 bool로 끝났다는 걸 알릴 수 있는 bool을 리턴하는 public 함수가 필요함

    // 추가해야할 변수들 --------------------------------------------------------------------------------------------------------------------
    // 1. 플레이어의 인벤토리
    // 2. 제어할 수 있는 아이템패널 클래스
    // 3. 확인 버튼
    // 4. 각 상황을 알려줄 텍스트

    Inventory inventory;
    ItemManager itemManager;
    ItemPanel[] itemPanels;

    TextMeshProUGUI rewardDescription;
    Button acceptButton;

    List<ItemData_Base> rewardItemList;
    ItemData_Base[] rewardItems;

    CanvasGroup canvasGroup;

    public Sprite goldImage;

    [HideInInspector]
    public bool isRoundEnd = false;

    int[] rewardGoldValue = { 500, 600, 700, 800, 900, 1000 };


    int selectCount = 0;

    int maxSelectCount = 2;

    int firstSelectIndex = -1;
    int secondSelectIndex = -1;

    public Action onSelect;

    private void Awake()
    {
        rewardItemList = new List<ItemData_Base>();
        canvasGroup = GetComponent<CanvasGroup>();

        Close();

        acceptButton = transform.GetChild(2).GetComponent<Button>();
        acceptButton.onClick.AddListener(OnAcceptButton);

        acceptButton.gameObject.SetActive(false);

        rewardDescription = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        itemPanels = GetComponentsInChildren<ItemPanel>();

        for (int i = 0; i < itemPanels.Length; i++)
        {
            itemPanels[i].onClick += OnClick;
        }
    }

    private void Start()
    {
        itemManager = GameManager.Inst.ItemData;
        rewardItems = itemManager.ItemDatas;
        //EnemySpawner enemySpawner = GameManager.Inst.EnemySpawner;
        //enemySpawner.onRoundEnd += 
    }

    public void GetPlayerInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public void Open()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        inventory.IncreaseSlot();
        SetReward();
    }

    bool a;
    public void Open(bool a)
    {
        if (a)
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            inventory.IncreaseSlot();
            SetReward(); 
        }
    }

    public void Close()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 각 아이템 패널에 보상세팅하는 함수
    /// </summary>
    public void SetReward()
    {
        List<ItemData_Base> rewardList = GetRewardItemList();
        
        selectCount = 0;
        firstSelectIndex = -1;
        secondSelectIndex = -1;

        for (int i = 0; i < itemPanels.Length; i++) 
        {
            itemPanels[i].isSelect = false; // 처음에 열릴 때 우선 모두 선택 해제
        }

        for (int i = 0; i < itemPanels.Length; i+=2)    // 1,3,5 번째에 있는 아이템 패널은 아이템 제공패널
        {
            if (rewardList.Count != 0)  
            {
                // 리워드 보상리스트가 있는 경우
                int randomIndex = UnityEngine.Random.Range(0, rewardList.Count);
                itemPanels[i].RefreshPanel(rewardList[randomIndex]);    // 랜덤으로 인덱스를 뽑아 보상 리스트에서 하나를 아이템패널에 표시한다
                rewardList.RemoveAt(randomIndex);   // 다음 번째 패널에는 이미 보상 리스트 중 썼던 인덱스는 안나오게 하기위해 리스트에서 제거한다.
            }
            else
            {
                // 리워드 보상리스트가 없는 경우(이미 모든 아이템을 가지고 있거나 등의 이유) -> 골드로 대체
                int randomIndex = UnityEngine.Random.Range(0, rewardGoldValue.Length);
                itemPanels[i].RefreshGoldPanel(rewardGoldValue[randomIndex]);
            }
        }
        for (int i = 1; i < itemPanels.Length - 1; i += 2)   // 1,3,5 번째에 있는 아이템 패널은 골드 제공패널
        {
            int randomIndex = UnityEngine.Random.Range(0, rewardGoldValue.Length);
            itemPanels[i].RefreshGoldPanel(rewardGoldValue[randomIndex]);
        }
    }

    /// <summary>
    /// 보상으로 올릴 아이템만 리스트로 반환하는 함수
    /// </summary>
    /// <returns>보상목록 아이템 리스트</returns>
    private List<ItemData_Base> GetRewardItemList()
    {
        rewardItemList.Clear();

        for (int i = 0; i < rewardItems.Length; i++)
        {
            rewardItemList.Add(rewardItems[i]); // 전체 아이템 데이터 배열을 리스트로 받아오기
        }

        List<ItemData_Base> maxCountItems = GetMaxCountItemList();  // 플레이어가 가지고 있는 아이템 중 최대갯수인 아이템 리스트 가져오기

        if (maxCountItems.Count != 0)
        {
            // 최대갯수인 아이템을 1개라도 가지고 있다면
            foreach (var item in maxCountItems)
            {
                rewardItemList.Remove(item);
            }
        }

        return rewardItemList;
    }

    /// <summary>
    /// 플레이어 인벤토리에서 가지고 있는 아이템 중 최대갯수인 아이템들을 리스트로 반환하는 함수
    /// </summary>
    /// <returns>플레이어가 가지고 있는 최대갯수인 아이템데이터 리스트</returns>
    private List<ItemData_Base> GetMaxCountItemList()
    {
        List<ItemData_Base> maxCountItem = new List<ItemData_Base>(); 

        maxCountItem.Clear();

        List<ItemSlot> playerInvenSlots = inventory.GetInventorySlotList();
        for (int i=0; i<playerInvenSlots.Count; i++)
        {
            if (!playerInvenSlots[i].IsEmpty)
            {
                if (playerInvenSlots[i].ItemCount == playerInvenSlots[i].ItemData.maxStackCount)
                {
                    maxCountItem.Add(playerInvenSlots[i].ItemData);
                }
            }
        }

        return maxCountItem;
    }

    /// <summary>
    /// 버튼을 누를 때 보상을 획득하는 함수
    /// </summary>
    private void OnAcceptButton()
    {
        // . 선택한 패널이 아이템류인지 골드인지 체크.
        // . 선택된 패널이 골드면 바로 보상 처리
        // . 선택된 패널이 아이템이면 이미가지고 있는 아이템인지 체크
            // . 이미가지고 있는 아이템이 아니면 빈 슬롯이 있는지 체크
            // 넣을 아이템이 1개인지 2개인지, 빈슬롯은 몇갠지 알아야함

        for (int i = 0; i < itemPanels.Length; i++)
        {
            if (itemPanels[i].isSelect)
            {
                if (itemPanels[i].itemData != null)
                {
                    // 아이템 추가 코드
                    if (inventory.FindEmptySlot() != null)
                    {
                        // 빈 슬롯이 있으면    
                        inventory.AddItem(itemPanels[i].itemData);
                    }
                    else
                    {
                        // 빈 슬롯이 없으면
                        // 골드 배열 중 첫번째꺼(가장 싼) 골드 지급
                        GameManager.Inst.PlayerGold.NowGold += rewardGoldValue[0];
                        Debug.Log($"{itemPanels[i].itemData.name}은 슬롯이 없어 {rewardGoldValue[0]}골드로 대체되었습니다.");
                    }
                }
                else
                {
                    // 골드 추가 코드
                    GameManager.Inst.PlayerGold.NowGold += itemPanels[i].goldValue; 
                    Debug.Log($"{itemPanels[i].goldValue}골드 만큼 골드가 추가되었습니다.");
                }
            }
        }
        Close();
        onSelect?.Invoke();
    }

    private void OnClick(ItemPanel itemPanel)
    {
        if (!itemPanel.isSelect)
        {
            if (selectCount < maxSelectCount)
            {
                switch (selectCount)
                {
                    case 0:
                        firstSelectIndex = SearchPanelIndex(itemPanel);
                        break;
                    case 1:
                        secondSelectIndex = SearchPanelIndex(itemPanel);
                        break;
                }

                itemPanel.isSelect = true;
                selectCount++;
                Debug.Log(selectCount);
            }
            else
            {
                itemPanels[firstSelectIndex].isSelect = false;
                itemPanels[firstSelectIndex].UnSelectPanel();
                firstSelectIndex = secondSelectIndex;
                itemPanel.isSelect = true;
                secondSelectIndex = SearchPanelIndex(itemPanel);
            }
        }
        else
        {
            itemPanel.isSelect = false;
            if (firstSelectIndex == SearchPanelIndex(itemPanel))
            {
                if (secondSelectIndex != -1)
                {
                    firstSelectIndex = secondSelectIndex;
                    secondSelectIndex = -1;
                }
                else
                {
                    firstSelectIndex = -1;
                    secondSelectIndex = -1;
                }
            }

            selectCount--;
            Debug.Log(selectCount);
        }
        RewardDescription();
    }

    private int SearchPanelIndex(ItemPanel itemPanel)
    {
        int result = -1;
        for (int i = 0; i < itemPanels.Length; i++)
        {
            if (itemPanels[i] == itemPanel)
            {
                result = i;
            }
        }

        return result;
    }

    private void RewardDescription()
    {
        switch (selectCount)
        {
            case 0: rewardDescription.text = "획득할 아이템을 2개 선택하세요.";
                    acceptButton.gameObject.SetActive(false);
                break;
            case 1: rewardDescription.text = "획득할 아이템을 1개 선택하세요.";
                    acceptButton.gameObject.SetActive(false);
                break;
            case 2: rewardDescription.text = "";
                acceptButton.gameObject.SetActive(true);
                break;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class TowerCreat : MonoBehaviour
{
    // 鸥况 橇府普 积己
    public GameObject tower;    // 积己 且 鸥况狼 橇府普

    public Button shortAttack;

    private void Awake()
    {
        tower = GetComponent<GameObject>();
    }

    private void Start()
    {
        shortAttack.onClick.AddListener(MakeTower);
    }

    void MakeTower()
    {
        tower = Instantiate(tower, tower.transform);
        Debug.Log("shortAttack 积己");
    }

}

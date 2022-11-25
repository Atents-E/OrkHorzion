using UnityEngine;
using UnityEngine.UI;

public class TowerCreat : MonoBehaviour
{
    // Ÿ�� ������ ����
    public GameObject tower;    // ���� �� Ÿ���� ������

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
        Debug.Log("shortAttack ����");
    }

}

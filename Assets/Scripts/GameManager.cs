using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int hp = 10;
    [SerializeField] private int coin = 100;
    [SerializeField] private int killedEnemy = 0;

    [SerializeField] private ShopUI shapUI;
    private bool isShopOpen = false;

    public int Hp { get; private set; }
    public int Coin { get; private set; }
    public int KilledEnemy { get; private set; }

    public event Action<int> UpdateHp;
    public event Action<int> UpdateCoin;
    public event Action<int> UpdateKilledEnemy;

    private void Start()
    {
        initialize();
    }
    private void Update()
    {
        OnShop();
    }
    private void initialize()
    {
        Hp = hp;
        Coin = coin;
        KilledEnemy = killedEnemy;
    }
    private void OnEnable()
    {
        EnemyMovement.OnPlayerDamaged += TakeDamage;
        Enemy.OnDropCoin += AddCoin;
        Enemy.OnDie += EnemyDie;
    }
    private void TakeDamage()
    {
        Hp--;
        UpdateHp?.Invoke(Hp);
    }
    private void AddCoin(int amount)
    {
        Coin += amount;
        UpdateCoin?.Invoke(Coin);
    }
    private void EnemyDie()
    {
        AddKilledCount(1);
    }
    private void AddKilledCount(int amount)
    {
        KilledEnemy += amount;
        UpdateKilledEnemy?.Invoke(KilledEnemy);
    }

    //public void OnShop(InputAction.CallbackContext context)
    //{
    //    if (!context.performed) return;
    //
    //    isShopOpen = !isShopOpen;
    //    shapUI.OpenShopUI(isShopOpen);
    //}

    private void OnShop()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            isShopOpen = !isShopOpen;
            shapUI.OpenShopUI(isShopOpen);
        }
    }
}

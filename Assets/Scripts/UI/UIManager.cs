using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text killedEnemyText;
    [SerializeField] GameManager gameManager;
    private void Start()
    {
        UpdateHpUI(gameManager.Hp);
        UpdateCoinUI(gameManager.Coin);
        UpdateKilledUI(gameManager.KilledEnemy);
    }
    private void OnEnable()
    {
        gameManager.UpdateHp += UpdateHpUI;
        gameManager.UpdateCoin += UpdateCoinUI;
        gameManager.UpdateKilledEnemy += UpdateKilledUI;
    }

    private void UpdateHpUI(int hp)
    {
        hpText.text = $"HP : {hp}";
    }
    private void UpdateCoinUI(int coin)
    {
        coinText.text = $"Coin : {coin}";
    }
    private void UpdateKilledUI(int killed)
    {
        killedEnemyText.text = $"Killed : {killed}";
    }

}

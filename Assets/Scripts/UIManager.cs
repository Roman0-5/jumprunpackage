using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinCounterText;
    [SerializeField] 
    private Character character;
    [SerializeField] 
    private Image healthBar;
    [SerializeField] private CanvasGroup hudCanvasGroup;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private float fadingTime = 2.0f;

    private bool isFadingInGameOver = false;
    private static UIManager instance = null;
    public static UIManager Instance => instance;

    private class PlayerStatistics
    {
        public int coinCounter = 0;
        public int deaths = 0;
        public int enemiesKilled = 0;
        public float time = 0;
    }
    private PlayerStatistics statistics;

    private void Awake()
    {
        instance = this;
        this.statistics = new PlayerStatistics()
        {
                coinCounter = 0,
                deaths = 0,
                enemiesKilled = 0,
                time = 0
        };
    }

    public void collectCoin()
    {
        this.statistics.coinCounter++;
        string coinText = $"{this.statistics.coinCounter}";
        this.coinCounterText.text = coinText;
    }

    private System.Collections.IEnumerator FadeInGameOver()
    {
        this.isFadingInGameOver = true;

        float timer = 0.0f;
        while (timer < this.fadingTime)
        {
            float percent = timer / this.fadingTime;
            this.hudCanvasGroup.alpha = 1.0f - percent;
            this.gameOverCanvasGroup.alpha = percent;

            yield return null;
            timer += Time.deltaTime;
        }

        this.hudCanvasGroup.alpha = 0.0f;
        this.gameOverCanvasGroup.alpha = 1.0f;
    }

    public void Update()
    {
        if (this.character == null)
        {
            Debug.LogError("UIManager: character-Referenz fehlt im Inspector!");
            return;
        }
        if (this.healthBar == null)
        {
            Debug.LogError("UIManager: healthBar-Referenz fehlt im Inspector!");
            return;
        }

        float maxHealth = this.character.getMaxHealth();
        float percent = maxHealth > 0.0f
            ? this.character.getCurrentHealth() / maxHealth
            : 0.0f;
        this.healthBar.fillAmount = Mathf.Clamp01(percent);
        
        if (percent <= 0.0f && !this.isFadingInGameOver)
        {
            this.StartCoroutine(this.FadeInGameOver());
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject normalObject;
    public GameObject hatObject;
    public GameObject restartButton;
    int score = 0;
    public EnemyAI enemy1;
    public EnemyAI enemy2;
    public EnemyAI enemy3;
    public EnemyAI enemy4;

    private bool isBoosted = false;
    public TextMeshProUGUI centerText;

    private void Start()
    {
        restartButton.SetActive(false);
        normalObject.SetActive(true);
        hatObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("collectable"))
        {
            score++;
            Destroy(collision.gameObject);
            scoreText.text = score.ToString();
        }
        if (collision.CompareTag("booster"))
        {
            isBoosted = true;
            Destroy(collision.gameObject);
            enemy1.currentState = EnemyAI.enemyState.afraid;
            enemy1.testing.ChooseCorner(enemy1,enemy1.baseColor);

            enemy2.currentState = EnemyAI.enemyState.afraid;
            enemy2.testing.ChooseCorner(enemy2,enemy2.baseColor);

            enemy3.currentState = EnemyAI.enemyState.afraid;
            enemy3.testing.ChooseCorner(enemy3 ,enemy3.baseColor);

            enemy4.currentState = EnemyAI.enemyState.afraid;
            enemy4.testing.ChooseCorner(enemy4 ,enemy4.baseColor);
            StartCoroutine(ReturnToChaseState());

            normalObject.SetActive(false);
            hatObject.SetActive(true);
        }
        if (collision.CompareTag("enemy"))
        {
            if (!isBoosted)
            {
                restartButton.SetActive(true);
                centerText.text = "You Lose";
                Destroy(enemy1.gameObject);
                Destroy(enemy2.gameObject);
                Destroy(enemy3.gameObject);
                Destroy(enemy4.gameObject);
                Destroy(gameObject);
                
            }
            else if(collision.GetComponent<EnemyAI>().currentState != EnemyAI.enemyState.dead)
            {
                collision.GetComponent<EnemyAI>().currentState = EnemyAI.enemyState.dead;
                collision.GetComponent<EnemyAI>().testing.GoToCenter(collision.GetComponent<EnemyAI>(), Color.black);
                score += 10;
            }
        }
    }
    private IEnumerator ReturnToChaseState()
    {
        yield return new WaitForSeconds(10f);
        if(enemy1.currentState == EnemyAI.enemyState.afraid)
            enemy1.currentState = EnemyAI.enemyState.chase;
        if (enemy2.currentState == EnemyAI.enemyState.afraid)
            enemy2.currentState = EnemyAI.enemyState.chase;
        if (enemy3.currentState == EnemyAI.enemyState.afraid)
            enemy3.currentState = EnemyAI.enemyState.chase;
        if (enemy4.currentState == EnemyAI.enemyState.afraid)
            enemy4.currentState = EnemyAI.enemyState.chase;

        isBoosted = false;

        normalObject.SetActive(true);
        hatObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private float _delay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        _scoreController.ResetScore();
        SceneManager.LoadScene(1);
    }

    public void LoadOver()
    {
        StartCoroutine(WaitSeconds());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(2);
    }
}

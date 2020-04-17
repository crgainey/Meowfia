using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;

public class GameManager : MonoBehaviour
{ 
    //for pause
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    //UI Lives
    public Image[] lives;
    public Sprite Life;
    public Sprite noLife;

    public float health = 9;
    bool _isInvincicible = false;

    //for hairballImage
    public Image hbCooldownImage;
    public float cooldown = 10;
    bool _isCooldown;

    //laser 
    public Image[] laserCharges;
    public Sprite laserFull;
    public Sprite laserUsed;

    //mice and catnip
    public Text miceText;
    int _numOfMice;
    public Text catnipText;
    public int numOfCatnip;
    public ParticleSystem catnipDashEffect;


    //scripts
    PlayerControllerRB _playerController;
    Laser _laser;

    bool _dialogueOpen = false;

    //Audio 
    public AudioSource lostLifeSound;
    public AudioSource gameoverSound;

    void Start()
    {
        PlayerPrefs.DeleteAll();

        _numOfMice = 0;
        numOfCatnip = 0;

        //Finds the player script
        GameObject laserObject = GameObject.FindWithTag("Laser");
        if (laserObject != null)
        {
            _laser = laserObject.GetComponent<Laser>();
        }
        if (laserObject == null)
        {
            Debug.Log("Cannot find 'Laser' script");
        }

        //Finds the laser script
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            _playerController = playerObject.GetComponent<PlayerControllerRB>();
        }
        if (playerObject == null)
        {
            Debug.Log("Cannot find 'Player' script");
        }

    }

    void Update()
    {
        HairballCooldown();
        LifeUpdate();
        LaserUI();

        //for catnip powerup
        if (numOfCatnip >=1)
        {
            Debug.Log("CatnipGOOOOOOD");
            if (Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                numOfCatnip--;
                catnipText.text = "" + numOfCatnip;
                StartCoroutine(CatnipPowerUp());
            }
        }

        //for pause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (FindObjectOfType<SayDialog>())
        {
            _dialogueOpen = true;
            PauseMovement();
        }
        
    }

    public void PauseMovement()
    {
        if (_dialogueOpen)
        {
            Debug.Log("Dialog going Movement Paused");
        }
        else
        {
            
        }
    }

    //General Buttons
    public void Resume()
    {
        //Debug.Log("Resume");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        //Debug.Log("Paused");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    //SCENES

    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ResetCurrentScene()
    {
        lostLifeSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//restarts current scene
    }


    //UI

    public void TakeDamage(float amount)
    {
        if (!_isInvincicible)
        {
            health -= amount;
            PlayerPrefs.SetFloat("Health", health);
            Debug.Log("PlayerPrefs health " + health);
            ResetCurrentScene();
        }

    }

    //lives UI
    public void LifeUpdate()
    {
        float newHealth = PlayerPrefs.GetFloat("Health", 9.0f);

        for(int i=0; i < lives.Length; i++)
        {
            if (i < newHealth)
            {
                lives[i].sprite = Life;
            }
            else
            {
                lives[i].sprite = noLife;
            }
        }

        if (newHealth == 0)
        {
            GameOver();
        }
    }

    //Hairball UI
    public void HairballCooldown()
    {
        
        if (Input.GetButton("Fire2"))
        {
            //Debug.Log("Hairball cooldown");
            _isCooldown = true;
        }

        if (_isCooldown)
        {
            hbCooldownImage.fillAmount += 1 / cooldown * Time.deltaTime;

            if(hbCooldownImage.fillAmount >= 1)
            {
                hbCooldownImage.fillAmount = 0;
                _isCooldown = false;
            }
        }
    }

    //laser UI
    void LaserUI()
    {
        for (int i = 0; i < laserCharges.Length; i++)
        {
            if (i < _laser.numOfCharges)
            {
                laserCharges[i].sprite = laserFull;
            }
            else
            {
                laserCharges[i].sprite = laserUsed;
            }
        }
    }


    //SCORE

    public void UpdateMice()
    {
        _numOfMice++;
        miceText.text = "" + _numOfMice;

    }

    public void UpdateCatnip()
    {
        numOfCatnip++;
        catnipText.text = "" + numOfCatnip;
    }

    IEnumerator CatnipPowerUp()
    {
        //Debug.Log("Catnip Used");
        _isInvincicible = true;
        _playerController.moveSpeed += 50f;
        catnipDashEffect.Play();

        yield return new WaitForSeconds(5f);

        //returns moveSpeed to normal
        _playerController.moveSpeed -= 50f;
    }

    //GAMEOVER

    //Gameover
    void GameOver()
    {
        gameoverSound.Play();
        SceneManager.LoadScene("GameOver");
    }

    //Win
    public void Win()
    {
        SceneManager.LoadScene("Win");
    }

    //SAVE STUFF

}

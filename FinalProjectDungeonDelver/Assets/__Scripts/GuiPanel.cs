using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiPanel : MonoBehaviour
{
    [Header ("Inscribed")]
    public Sprite healthEmpty;
    public Sprite healthHalf;
    public Sprite healthFull;

    Text        keyCountText;
    List<Image> healthImages;

    private float gameOverTime = 5;
    private float currentGameOverTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Game Over Panel").gameObject.SetActive(false) ;
        // Key Count
        Transform trans = transform.Find ("Key Count");
        keyCountText = trans.GetComponent<Text>();

        // Health Icons
        Transform healthPanel = transform.Find ("Health Panel");
        healthImages = new List<Image>();
        if (healthPanel != null)
        {
            for (int i = 0; i < 20; i++)
            {
                trans = healthPanel.Find ("H_" + i);
                if (trans == null) break;
                healthImages.Add (trans.GetComponent<Image>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Show keys
        keyCountText.text = Dray.NUM_KEYS.ToString();

        // Show health
        int healthDisp = Dray.HEALTH;
        for (int i = 0; i < healthImages.Count; i++)
        {
            if (healthDisp > 1)
                healthImages[i].sprite = healthFull;
            else if (healthDisp == 1)
                healthImages[i].sprite = healthHalf;
            else
                healthImages[i].sprite = healthEmpty;

            healthDisp -= 2;
        }

        if (Dray.HEALTH == 0 && currentGameOverTime == 0) 
        {
            Time.timeScale = 0;
            currentGameOverTime = Time.unscaledTime + gameOverTime;

            GameObject healthPanel = transform.Find ("Health Panel").gameObject;
            healthPanel.SetActive(false);
            transform.Find("Game Over Panel").gameObject.SetActive(true) ;
        };

        if (Time.unscaledTime >= currentGameOverTime && currentGameOverTime != 0)
        {
            Time.timeScale = 1;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}

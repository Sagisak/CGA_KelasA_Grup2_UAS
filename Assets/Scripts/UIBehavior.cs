using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBehavior : MonoBehaviour
{
    private TextMeshProUGUI textField;  
    private Game gameManager;

    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
       if(this.gameObject.name == "Total Lives")
        {
            textField.text = gameManager.lives.ToString();
        }

        if (this.gameObject.name == "Total Money")
        {
            textField.text = "$" + gameManager.money.ToString();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance { get; set; }

    [SerializeField] private List<GameObject> itemGroup = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }

    public void ItemSpawn(Transform pos)
    {
        Instantiate(itemGroup[Random.Range(0,itemGroup.Count)] , pos.position,Quaternion.Euler(0,0,0));
    }
    public void PushRestartButton()
    {
        SceneManager.LoadScene(0);
    }
    public void PushLeave()
    {
        Application.Quit();
    }
}

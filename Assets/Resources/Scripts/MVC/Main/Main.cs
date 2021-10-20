using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : Singleton<Main>
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<EventManager>();
        gameObject.AddComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using System.Collections;

public abstract class MyScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateDo();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        DestroyDo();
    }

    /// <summary>
    /// 创建的时候需要做的功能逻辑
    /// </summary>
    protected abstract void CreateDo();

    /// <summary>
    /// 销毁的时候需要做的功能逻辑
    /// </summary>
    protected abstract void DestroyDo();
}

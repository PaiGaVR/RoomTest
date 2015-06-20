using UnityEngine;
using System.Collections;

//================================================================================
/// 设置材质的类
///
/// 获取需要的材质，并注册到控制器中
//================================================================================
public class MyShaderRoot : MonoBehaviour {
    public Material transformMaterial;
    public Material lightMaterial;

	// Use this for initialization
	void Start () {
        MyController.setShaderController(this);
	}
}

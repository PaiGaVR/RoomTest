using UnityEngine;
using System.Collections;

//================================================================================
/// 改变模型材质所加载的类
///
/// 初始化编辑场景，创建编辑的菜单，加载编辑场景中的头瞄脚本
//================================================================================

public class ChangeMaterialsByRay : MyScripts {

    /// <summary>
    /// 碰撞体的包围盒.
    /// </summary>
    private Bounds bounds;

    /// <summary>
    /// 记录模型原先的材质列表.
    /// </summary>
    private ArrayList materialsArray;

    /// <summary>
    /// 要改变的模型材质.
    /// </summary>
    public Material materials;

    protected override void CreateDo()
    {
        // 获取碰撞体包围盒.
        bounds = GetComponent<Collider>().bounds;

        // 获取模型原先的材质列表，并改变模型到指定材质.
        materialsArray = MeshTools.changeAllMaterialsInModel(transform, materials);
    }

    /// <summary>
    /// 恢复状态.
    /// </summary>
    public void Recover()
    {
        // 恢复模型到原有材质.
        MeshTools.RecoverAllMaterials(transform, materialsArray);
        Destroy(this);
    }
	
	void Update () {
        if (!isLock && !bounds.IntersectRay(MyController.getHeadRay())) Recover();
	}

    // 锁定的控制变量.
    private bool isLock = false;

    /// <summary>
    /// 锁定模型.
    /// </summary>
    /// <param name="flag">锁定的控制变量.</param>
    public void Lock(bool flag)
    {
        this.isLock = flag;
    }

    protected override void DestroyDo()
    {
        Recover();
    }
}

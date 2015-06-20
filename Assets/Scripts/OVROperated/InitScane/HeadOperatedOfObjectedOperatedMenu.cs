using UnityEngine;
using System.Collections;

//================================================================================
/// 编辑场景中的头瞄菜单类
///
/// 在编辑场景中，通过头瞄碰撞目标物体，获取物体并显示操作菜单
//================================================================================

public class HeadOperatedOfObjectedOperatedMenu : MyScripts {

    /// <summary>
    /// 屏蔽图层.
    /// </summary>
    private LayerMask layerMask = 1 << 8;

    /// <summary>
    /// 菜单MyEnum.ScaneObjectType.ObjectOperatedMenu.
    /// </summary>
    private GameObject menuObject;

    /// <summary>
    /// 菜单的初始位置.
    /// </summary>
    private Vector3 menuPosition = new Vector3(0.05f, 0f, 0.44f);

    /// <summary>
    /// 头瞄的控制变量.
    /// </summary>
    private bool flag = true;

    protected override void CreateDo()
    {
        menuObject = MyController.CreatePrefabInTransform(transform, MyEnum.ScaneObjectType.ObjectOperatedMenu, menuPosition);
        menuObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        if (menuObject != null && menuObject.activeSelf == true) menuObject.SetActive(false);
    }

    /// <summary>
    /// 是否开启头瞄.
    /// </summary>
    /// 
    /// <param name="flag">头瞄的控制变量.</param>
    public void turn(bool flag)
    {
        // 如果菜单为空，返回.
        if (menuObject == null) return;

        // 通过判断头瞄的控制变量与菜单的显示状态，开启或关闭头瞄.
        if (!flag && menuObject.activeSelf)
        {
            menuObject.SetActive(false);
        }
        else if (flag && !menuObject.activeSelf)
        {
            if (isLock) menuObject.SetActive(true);
        }
        
        this.flag = flag;
    }

    /// <summary>
    /// 头瞄的锁定控制变量.
    /// </summary>
    private bool isLock = false;

    /// <summary>
    /// 头瞄是否锁定.
    /// </summary>
    /// 
    /// <param name="flag">头瞄的锁定控制变量.</param>
    public void Lock(bool flag)
    {
        this.isLock = flag;
        if (headRatcastHit.collider.GetComponent<ChangeMaterialsByRay>() != null)
        {
            headRatcastHit.collider.GetComponent<ChangeMaterialsByRay>().Lock(flag);
        }
    }

    /// <summary>
    /// 头瞄射线的碰撞返回信息.
    /// </summary>
    private RaycastHit headRatcastHit;
	void Update () {
        if (!isLock && flag)
        {
            if (Physics.Raycast(MyController.getHeadRay(), out headRatcastHit, 100f))
            {
                if (menuObject.activeSelf == false) menuObject.SetActive(true);
                if (headRatcastHit.collider.GetComponent<ChangeMaterialsByRay>() == null) headRatcastHit.collider.gameObject.AddComponent<ChangeMaterialsByRay>().materials = MyController.getMyShaderController().lightMaterial;
            }
            else
            {
                if (menuObject.activeSelf == true) menuObject.SetActive(false);
            }
        }
	}

    /// <summary>
    /// 获取头瞄射线的碰撞信息.
    /// </summary>
    /// 
    /// <returns>头瞄射线的碰撞信息.</returns>
    public RaycastHit getRaycastHit()
    {
        return headRatcastHit;
    }

    protected override void DestroyDo()
    {
        Destroy(menuObject);
    }
}

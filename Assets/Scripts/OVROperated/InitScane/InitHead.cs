using UnityEngine;
using System.Collections;

public class InitHead : MyScripts
{

    /// <summary>
    /// 菜单MyEnum.ScaneObjectType.ObjectOperatedMenu.
    /// </summary>
    private GameObject menuObject;

    /// <summary>
    /// 头瞄的控制变量.
    /// </summary>
    private bool flag = true;

    /// <summary>
    /// 头瞄准心.
    /// </summary>
    private Transform cursor;

    /// <summary>
    /// 准心进度条.
    /// </summary>
    private UISprite sprite;

    protected override void CreateDo()
    {

    }

    public void turn(bool flag)
    {
        this.flag = flag;
    }

    /// <summary>
    /// 头瞄射线的碰撞返回信息.
    /// </summary>
    private RaycastHit headRatcastHit;
    void Update()
    {
        if (flag)
        {
            if (Physics.Raycast(MyController.getHeadRay(), out headRatcastHit, 3f))
            {
                if (headRatcastHit.transform.FindChild("icon(Clone)") != null)
                {
                    Debug.Log("headRatcastHit.transform" + headRatcastHit.transform);
                    cursor = headRatcastHit.transform.FindChild("icon(Clone)").GetChild(1);
                    sprite = cursor.GetChild(0).GetComponent<UISprite>();
                    setMenuObjectHover(sprite);
                }
            }
        }
    }
 
    public void setMenuObjectHover(UISprite sprite)
    {
        sprite.fillAmount += Time.deltaTime;
 
        if (sprite.fillAmount >= 1f)
        {
            sprite.fillAmount = 0f;

            MyController.getRootController().ScaneSwitch(MyEnum.ScaneType.TheObjectBrowserScane);
            flag = false;
        }
        else
        {
            sprite.fillAmount = 0f;
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

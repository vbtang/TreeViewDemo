using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeViewNode : MonoBehaviour
{
    private bool bShowSelf = true;

    private TreeViewNode parentNode;
    private List<TreeViewNode> ChildNodeList = new List<TreeViewNode>();
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddChildNode(TreeViewNode childNode)
    {
        childNode.SetParent(this);
        ChildNodeList.Add(childNode);
        //Manager.UpdateTree();
    }

    private void SetParent(TreeViewNode _parentNode)
    {
        parentNode = _parentNode;
    }

    private bool IsShow()
    {
        if (parentNode != null)
        {
            return bShowSelf && parentNode.IsShow();
        }
        else
        {
            return true;
        }
    }

    public void RemoveChildNode(TreeViewNode childNode)
    {
        foreach (TreeViewNode node in ChildNodeList)
        {
            if (node.GetHashCode() == childNode.GetHashCode())
            {
                ChildNodeList.Remove(childNode);
                Destroy(childNode.gameObject);
                return;
            }
        }
    }

//     public void SetBackGroundColor(Color bgColor)
//     {
//         this.gameObject.GetComponent<Image>().color = bgColor;
//     }
// 
//     public void SetHeight(float fHeight)
//     {
//         this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, fHeight);
//     }

    public void ShowSelf(bool bShow)
    {
        bShowSelf = bShow;

        this.gameObject.SetActive(IsShow());
        ShowIterate(bShow);
    }

    public void ShowChild(bool bShow)
    {
        foreach(TreeViewNode node in ChildNodeList)
        {
            node.ShowSelf(bShow);
        }
    }

    private void ShowIterate(bool bShow)
    {
        this.gameObject.SetActive(IsShow());
        foreach (TreeViewNode node in ChildNodeList)
        {
            node.ShowIterate(bShow && bShowSelf);
        }
    }

    public void SortTreeIntoList(List<TreeViewNode> NodeList)
    {
        NodeList.Add(this);
        foreach (TreeViewNode node in ChildNodeList)
        {
            node.SortTreeIntoList(NodeList);
        }
    }

//     public void SetText(string strText)
//     {
//         this.gameObject.GetComponentInChildren<Text>().text = strText;
//     }
// 
//     public List<TreeViewNode> GetChilds()
//     {
//         return ChildNodeList;
//     }


    public void ClearChild()
    {
        foreach (TreeViewNode node in ChildNodeList)
        {
            node.ClearChild();
        }

        ChildNodeList.Clear();
    }

}

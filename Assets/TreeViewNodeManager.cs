using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeViewNodeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RootNode.ShowSelf(true);
        // 构建树状结构
        // 一级菜单
		for(int i=0; i<2; ++i)
        {
            string str = "一级：" + i;
            Color color = new Color(0, 0, 0);
            TreeViewNode node1 = InitNode(str, color);
            // 添加子节点
            RootNode.AddChildNode(node1);
            // 子节点的消隐
            node1.ShowSelf(true);
            // 节点的点击操作，这边用的是Toggle
            node1.gameObject.GetComponent<Toggle>().onValueChanged.AddListener((bool bValue) =>
            {
                node1.ShowChild(bValue);
            });

            // 二级菜单
            for (int j=0; j<3; ++j)
            {
                str = "一级：" + i + "/二级：" + j;
                color = new Color(0.5f, 0.5f, 0.5f);
                TreeViewNode node2 = InitNode(str, color);
                node1.AddChildNode(node2);
                node2.ShowSelf(false);
                node2.gameObject.GetComponent<Toggle>().onValueChanged.AddListener((bool bValue) =>
                {
                    node2.ShowChild(bValue);
                });

                // 三级菜单
                for (int k = 0; k < 4; ++k)
                {
                    str = "一级：" + i + "/二级：" + j + "/三级：" + k;
                    color = new Color(1.0f, 1.0f, 1.0f);
                    TreeViewNode node3 = InitNode(str, color);
                    node3.ShowSelf(false);
                    
                    node2.AddChildNode(node3);
                }

                node2.ShowChild(false);
            }

        }

        // 对树状结构进行深度遍历，然后将其展开成数组
        List<TreeViewNode> listNodes = new List<TreeViewNode>();
        // 展开成数组
        RootNode.SortTreeIntoList(listNodes);
        listNodes.Remove(RootNode);
     
        /*
        *  设置UI中的父子关系，首先清除掉父子关系
        *  不清除掉的话，重新设置父子关系无法重新排布顺序
        *  因为原先都是属于RootNode的子物体
        */
        foreach (TreeViewNode node in listNodes)
        {
            node.gameObject.transform.SetParent(null);
        }

        foreach (TreeViewNode node in listNodes)
        {
            node.gameObject.transform.SetParent(RootNode.gameObject.transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // 初始化节点
    TreeViewNode InitNode(string strLable, Color color)
    {
        GameObject obj = GameObject.Instantiate(NodeTemplete);
        obj.GetComponent<Image>().color = color;
        obj.GetComponentInChildren<Text>().text = strLable;

        return obj.GetComponent<TreeViewNode>();
    }

    // 所有的子节点都是从这个克隆出来的
    public GameObject NodeTemplete;

    // 根节点，根节点只是起管理作用，不需要实际显示
    public TreeViewNode RootNode;
}

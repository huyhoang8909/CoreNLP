using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTool
{
    //Class chứa Node của cây
    class Node
    {
        //phần khai báo thuộc tính
        public string name; //tên của node
        public List<Node> childs; //chứa các Childs của nó

        //phần khởi tạo
        public Node(ActRule ar)
        {
            if (ar != null)
            {
                this.name = ar.leftOfRule;
                if (ar.childs != null)
                {
                    this.childs = new List<Node>();
                    for (int i = 0; i < ar.childs.Count; i++)
                    {
                        if (ar.childs[i] != null)
                        {
                            Node newChild = new Node(ar.childs[i]);
                            this.childs.Add(newChild);
                        }
                    }
                }
            }
        }
        public Node(List<String> stringsOfTree)
        {
            if (stringsOfTree != null)
            {
                if (stringsOfTree.Count == 0) return;
                if ((stringsOfTree[0].Equals("(")) && 
                    (stringsOfTree[stringsOfTree.Count - 1].Equals(")")) &&
                    (stringsOfTree.Count > 2))
                {
                    //khởi tạo một số phần tử
                    this.name = stringsOfTree[1];
                    List<String> tempList = new List<string>();
                    List<List<String>> childs = new List<List<string>>();
                    #region tìm các string là node con của node đang xét
                    int count = 0;
                    for (int i = 2; i < stringsOfTree.Count - 1; i++)
                    {
                        if (stringsOfTree[i].Equals("("))
                        {
                            if ((tempList.Count > 0) &&(count == 0))
                            {
                                childs.Add(tempList);
                                tempList = new List<string>();
                            }
                            count++;
                            tempList.Add(stringsOfTree[i]);
                        }
                        else if (stringsOfTree[i].Equals(")"))
                        {
                            count--;
                            tempList.Add(stringsOfTree[i]);
                            if (count == 0)
                            {
                                childs.Add(tempList);
                                tempList = new List<string>();
                            }
                        }
                        else
                        {
                            tempList.Add(stringsOfTree[i]);
                        }
                    }
                    if (stringsOfTree[stringsOfTree.Count - 2].Equals(")") == false)
                    {
                        childs.Add(tempList);
                    }
                    #endregion
                    //gọi đệ quy tạo các node con
                    this.childs = new List<Node>();
                    foreach (List<string> ls in childs)
                    {
                        Node n = new Node(ls);
                        this.childs.Add(n);
                    }
                }

                else if (stringsOfTree.Count >= 0)
                {
                    string aChild = "";
                    for (int i = 0; i < stringsOfTree.Count; i++)
                    {
                        aChild += stringsOfTree[i] + " ";
                    }
                    this.name = aChild;
                  }

            }
            else this.name = null;
        }
        #region một số hàm chức năng
        public String ToString(string space) 
        {
            String sToReturn = "( ";
            sToReturn += this.name + "\n";
            if (this.childs != null)
            {
                foreach (Node aChild in this.childs)
                {
                    sToReturn += space + aChild.ToString(space + "\t") + "\n";
                }
            }
            sToReturn = sToReturn.Substring(0, sToReturn.LastIndexOf("\n"));
            sToReturn += " )";
            return sToReturn;
        }
        
        #endregion

    }
}

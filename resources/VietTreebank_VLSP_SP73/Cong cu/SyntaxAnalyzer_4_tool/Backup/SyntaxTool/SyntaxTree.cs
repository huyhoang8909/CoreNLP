using System;
using System.Collections.Generic;
using System.Text;
using Netron.Lithium;

namespace SyntaxTool
{
    //lớp SyntaxTree để biểu diễn một cây cú pháp kết quả nhằm phục vụ cho việc vẽ cây
    //Quy ước mỗi Node có một tên khác nhau
    class SyntaxTree
    {
        #region khai báo các thuộc tính private
        private NodeOfSyntaxTree root;
        //private string pathOfTree;
        private int numOfNodes;
        #endregion
        //__________________________________________________________________________________//
        #region khởi tạo
        public SyntaxTree(string nameOfRoot)
        {
            if (nameOfRoot != null)
            {
                this.root = new NodeOfSyntaxTree(nameOfRoot);
            }
            this.numOfNodes = 1;
        }
        public NodeOfSyntaxTree getRoot()
        {
            return this.root;
        }
        #endregion
        //_________________________________________________________________________________//
        #region một số thao tác với SyntaxTree
        public bool insertANode(string nameOfChild, string nameOfParent)
        {
            NodeOfSyntaxTree childNode = new NodeOfSyntaxTree(nameOfChild);
            //Gọi hàm thêm node này vào gôc
            if (this.insertANode(childNode, nameOfParent) == true)
                return true;
            else
                return false;

        }
        //Hàm thêm một Node vào một node gốc
        public bool insertANode(NodeOfSyntaxTree childNode, string nameOfparent)
        { 
            //đầu tiên là tìm kiếm trong Tree xem parent ở đâu
            NodeOfSyntaxTree parent = searchANode(nameOfparent, this.root);
            //kiểm tra xem có tìm thấy parent ko
            if (parent != null)
            { 
                //nếu tìm thấy thì add vào parent
                if (parent.addAChildToNode(childNode) == true)
                {
                    this.numOfNodes++;
                    return true; //add thành công thì thông  báo true
                }
                else
                    return false;
            }
            //ngược lại, trả về false
            return false;
        }
        //Hàm tìm kiếm một Node trong Tree
        private NodeOfSyntaxTree searchANode(string nameOfNodeWantToSearch, NodeOfSyntaxTree rootToSearch)
        {
            if (rootToSearch == null)
                return null;
            string s = rootToSearch.getNameOfNode();
            if ( s == nameOfNodeWantToSearch)
            {
                return rootToSearch;
            }
            if (rootToSearch.getNumOfChilds() > 0)
            {
                NodeOfSyntaxTree[] childs = rootToSearch.getChilds();
                for (int i = 0; i < rootToSearch.getNumOfChilds(); i++)
                {
                    NodeOfSyntaxTree snode = searchANode(nameOfNodeWantToSearch, childs[i]);
                    if(snode != null)
                        return snode;
                }
                return null;
            }
            else
                return null;

        }

        #endregion

    }
    //lớp Node để biểu diễn một Node con
    class NodeOfSyntaxTree
    {
        #region khai báo các thuộc tính private
        private string name;
        private NodeOfSyntaxTree parent;
        private NodeOfSyntaxTree[] childs;
        #endregion
        //____________________________________________________________________________________//
        #region các hàm khởi tạo
        public NodeOfSyntaxTree(string name)
        {
            this.name = name;
        }
        public NodeOfSyntaxTree(string name, NodeOfSyntaxTree[] childs)
        {
            this.name = name;
            if (childs.Length > 0)
            {
                this.childs = new NodeOfSyntaxTree[childs.Length];
                for (int i = 0; i < childs.Length; i++)
                {
                    this.childs[i] = childs[i];
                }
            }

        }
        public NodeOfSyntaxTree(string name, NodeOfSyntaxTree[] childs, NodeOfSyntaxTree parent)
        {
            this.name = name;
            if (childs.Length > 0)
            {
                this.childs = new NodeOfSyntaxTree[childs.Length];
                for (int i = 0; i < childs.Length; i++)
                {
                    this.childs[i] = childs[i];
                }
            }
            this.parent = parent;
        }
        #endregion
        //____________________________________________________________________________________//
        #region các hàm lấy giá trị thuộc tính
        //hàm lấy tên của Node đang xét
        public string getNameOfNode()
        {
            return this.name;
        }
        //Hàm tính số lượng của các Node con của Node đang xét
        public int getNumOfChilds()
        {
            if (this.childs != null)
                return this.childs.Length;
            else
                return 0;
        }
        //Hàm lấy địa chỉ của parent
        public NodeOfSyntaxTree getParent()
        {
            return this.parent;
        }
        //Hàm lấy địa chỉ của các Child
        public NodeOfSyntaxTree[] getChilds()
        {
            return this.childs;
        }
        //hàm thêm một child vào node
        public bool addAChildToNode(NodeOfSyntaxTree childToAdd)
        {
            try
            {
                //kiểm tra xem node đang xét đã có child hay chưa
                if (childs == null)
                {
                    //tạo mới mảng childs
                    this.childs = new NodeOfSyntaxTree[1];
                    this.childs[0] = childToAdd;
                }
                //nếu đã có rồi thì thêm tiếp vào
                else
                {
                    NodeOfSyntaxTree[] tam = new NodeOfSyntaxTree[this.getNumOfChilds() + 1];
                    for (int i = 0; i < this.getNumOfChilds(); i++)
                    {
                        tam[i] = this.childs[i];
                    }
                    tam[this.getNumOfChilds()] = childToAdd;
                    this.childs = tam;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

            
        }

        #endregion
    }
}

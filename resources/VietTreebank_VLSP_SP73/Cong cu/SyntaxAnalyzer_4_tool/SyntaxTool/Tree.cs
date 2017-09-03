using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTool
{
    class Tree
    {
        //phần khai báo thuộc tính
        public Node root = null; //thuộc tính là node gốc của cây
        
        //phần khởi tạo
        public Tree()
        { 
        }
/*
        public Tree(ActRule actRule)
        {
            if (actRule != null)
            {
                this.root = new Node(actRule);
            }
        }
 */
        public Tree(Node root)
        {
            this.root = root;
        }

        public void SetPos()
        {
         // Xac dinh cac nut rong
            root.SetNullElem();

         // Cho nut la
            int n = 0;
            root.SetPosForLeaves(ref n);

         // Cho nut trong
            root.SetPos();
        }

        // Thai viet
        // Tao cay phu thuoc ung voi cay phrase-structure hien thoi
        public Tree CreateDependencyTree()
        {
            // tao cay
            Tree depTree = new Tree();

            // tao nut goc
            depTree.root = this.root.CreateDependencyRoot();

            // tao nhanh
            this.root.CreateDependencyNode(ref depTree.root);
            
            return depTree;
        }
   }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTool
{
    class Tree
    {
        //phần khai báo thuộc tính
        public Node root; //thuộc tính là node gốc của cây
        
        //phần khởi tạo
        public Tree(ActRule actRule)
        {
            if (actRule != null)
            {
                this.root = new Node(actRule);
            }
        }
        public Tree(Node root)
        {
            this.root = root;
        }
   }
}

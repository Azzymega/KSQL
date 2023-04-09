using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSQL.Scripts
{
    public class Tree
    {
        private TreeView _tree;
        private TreeNode _root;
        private List<TreeNode> _parentChildernNodes;
        public Tree(TreeView treeView)
        {
            _tree = treeView;
            _root = new TreeNode("Таблицы");
            _parentChildernNodes = new List<TreeNode>();
            _tree.Nodes.Add(_root);
        }
        public void Initialize(List<string> tablesNames)
        {
            _root.Nodes.Clear();
            _parentChildernNodes = new List<TreeNode>();
            foreach (string table in tablesNames)
            {
                _parentChildernNodes.Add(new TreeNode(table));
            }
            foreach (TreeNode item in _parentChildernNodes)
            {
                _tree.Nodes[0].Nodes.Add(item);
            }
        }
        public void ClearTree()
        {
            _tree.Nodes.Clear();
        }
    }
}

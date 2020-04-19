using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public enum Side { Left, Right } 

    public class BinaryTreeNode<T> where T : IComparable 
    {
        public T Data { get; set; }
        public BinaryTreeNode<T> LeftNode { get; set; } 
        public BinaryTreeNode<T> RightNode { get; set; }      
        public BinaryTreeNode<T> ParentNode { get; set; } 

        public Side? NodeSide =>
        ParentNode == null ?
            (Side?)null : ParentNode.LeftNode == this ?
             Side.Left : Side.Right;
        public BinaryTreeNode(T data)
        {
            Data = data;
        }

    }
    class BinaryTree<T> where T : IComparable
    {
        public BinaryTreeNode<T> RootNode;

        public BinaryTreeNode<T> Add(BinaryTreeNode<T> node, 
                                     BinaryTreeNode<T> currentNode = null)
        {
            if(RootNode == null)
            {
                node.ParentNode = null;
                return RootNode = node;
            }

            currentNode = currentNode ?? RootNode;
            node.ParentNode = currentNode;

            int result;
            return (result = node.Data.CompareTo(currentNode.Data)) == 0
                ? currentNode
                : result < 0
                    ? currentNode.LeftNode == null
                        ? (currentNode.LeftNode = node)
                        : Add(node, currentNode.LeftNode)
                    : currentNode.RightNode == null
                        ? (currentNode.RightNode = node)
                        : Add(node, currentNode.RightNode);
        }

        public BinaryTreeNode<T> Add(T data)
        {
            return Add(new BinaryTreeNode<T>(data));
        }

        public BinaryTreeNode<T> FindNode(T data, 
                                         BinaryTreeNode<T> startWithNode = null)
        {
            startWithNode = startWithNode ?? RootNode;

            int result;
            return (result = data.CompareTo(startWithNode.Data)) == 0 ?
                startWithNode
                : result < 0 ? 
                     startWithNode.LeftNode == null ?                   
                         null : FindNode(data, startWithNode.LeftNode)
                :
                     startWithNode.RightNode == null ? 
                         null : FindNode(data, startWithNode.RightNode);
        }

        public void Remove(BinaryTreeNode<T> node)
        {

            if (node == null) return;

            Side? currentNodeSide = node.NodeSide;

            if(node.LeftNode == null && node.RightNode == null) // No children.
            {
                if(currentNodeSide == Side.Left)
                    node.ParentNode.LeftNode = null;
                else
                    node.ParentNode.RightNode = null;
            }
            else if (node.LeftNode == null) // Only right child.
            {
                if (currentNodeSide == Side.Left)
                    node.ParentNode.LeftNode = node.RightNode;
                else
                    node.ParentNode.RightNode = node.RightNode;
                node.RightNode.ParentNode = node.ParentNode; // Swich parent.
            }
            else if(node.RightNode == null) // Only left child.
            {
                if (currentNodeSide == Side.Left)
                    node.ParentNode.LeftNode = node.LeftNode;
                else
                    node.ParentNode.RightNode = node.LeftNode;
            }
            else // Two child.
                switch(currentNodeSide)
                {
                    case Side.Left:
                        node.ParentNode.LeftNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        break;
                    case Side.Right:
                        node.ParentNode.RightNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        break;
                    default: // Delete root.
                        var bufLeft = node.LeftNode;
                        var bufRightLeft = node.RightNode.LeftNode;
                        var bufRightRight = node.RightNode.RightNode;
                        node.Data = node.RightNode.Data;
                        node.RightNode = bufRightRight;
                        node.LeftNode = bufRightLeft;
                        Add(bufLeft, node);
                        break;
                }
        }

        public void Remove(T data)
        {
            var foundNode = FindNode(data);
            Remove(foundNode);
        }

        private void PrintTree(BinaryTreeNode<T> startNode,
                               string indent = "   ", Side? side = null)
        {
            if (startNode != null)
            {
                var nodeSide = side == null ? "Root" : side == Side.Left ? "L" : "R";

                Console.WriteLine(string.Format("{0} {1} {2}", indent, nodeSide, startNode.Data));
                indent += new string(' ', 3);

                PrintTree(startNode.LeftNode, indent, Side.Left);
                PrintTree(startNode.RightNode, indent, Side.Right);
            }
        }

        public void PrintTree()
        {
            PrintTree(RootNode);
        }
    }
}

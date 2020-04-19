using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
   
    class Program
    {
        public static void PrintTree(BinaryTree<int> tree)
        {
            tree.PrintTree();
            Console.WriteLine(new string('-', 50));
        }
        static void Main()
        {
            var binaryTree = new BinaryTree<int>();

            binaryTree.Add(10);
            binaryTree.Add(7);
            binaryTree.Add(20);
            binaryTree.Add(30);
            binaryTree.Add(3);
            binaryTree.Add(4);
            binaryTree.Add(25);
            binaryTree.Add(5);

            PrintTree(binaryTree);

            Console.WriteLine("Delete 7 element \n");
            binaryTree.Remove(7);
            PrintTree(binaryTree);

            Console.WriteLine("Delete Root element \n");
            binaryTree.Remove(10);
            PrintTree(binaryTree);

            if (binaryTree.FindNode(7) == null)
                Console.WriteLine("Element wich data 7 has not found ");

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace NeetCode150
{
    


    public class Program
    {
        static void Main(string[] args)
        {
            RunEvalRPN();


            Console.ReadLine();
        }

        #region InProgress

        public static void RunGenerateParenthesis()
        {
            var res = GenerateParenthesis(3);

        }

        public static List<string> GenerateParenthesis(int n)
        {



            return new List<string>();
        }





        #endregion

        public static void RunEvalRPN()
        {
            var input = new string[] { "10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+" };

            var result = EvalRPN(input);

        }

        public static int EvalRPN(string[] tokens)
        {
            var myStack = new Stack<int>();

            foreach (var s in tokens)
            {
                int val = 0;

                if (Int32.TryParse(s, out val))
                {
                    myStack.Push(val);
                }
                else
                {
                    var res = 0;
                    var secRes = myStack.Pop();
                    var firstRes = myStack.Pop();

                    if (s == "+")
                    {
                        res = firstRes + secRes;
                    }
                    else if (s == "-")
                    {
                        res = firstRes - secRes;
                    }
                    else if (s == "/")
                    {
                        res = firstRes / secRes;
                    }
                    else if (s == "*")
                    {
                        res = firstRes * secRes;
                    }
                    myStack.Push(res);
                }
            }
            return myStack.Pop();
        }

        public static void RunMinStack()
        {
            var lst = new List<int> { 6, 1, 7, 2, 0 };

            var myStack = new MinStack();

            myStack.Push(5);
            myStack.Push(0);
            myStack.Push(2);
            myStack.Push(4);
            myStack.GetMin();
            myStack.Pop();
            myStack.GetMin();
            myStack.Pop();
            myStack.GetMin();

        }

        public static void RunMaxSlidingWindow()
        {
            var nums = new int[] { 7, 2, 4 };

            var res = MaxSlidingWindow(nums, 2);
        }

        // Not optimal , i need to implement my own heap to remove a specific element...
        public static int[] MaxSlidingWindow(int[] nums, int k)
        {
            if (k == 1)
            {
                return nums;
            }
            int l = 0;

            var maxHeap = new PriorityQueue<int, int>();
            var outArr = new List<int>();

            while (true)
            {

                for (int i = 0; i < k; i++)
                {
                    maxHeap.Enqueue(nums[l + i], -1000 - nums[l + i]);
                }

                int max = maxHeap.Dequeue();

                l++;
                maxHeap.Clear();
                outArr.Add(max);

                if (l > nums.Length - k)
                {
                    break;
                }
            }

            return outArr.ToArray();

        }


        public static void RunCharacterReplacement()
        {
            string s = "xyyx";
            int k = 2;

            Console.WriteLine(CharacterReplacement(s, k));
        }

        public static int CharacterReplacement(string s, int k)
        {
            int l = 0;
            int maxLength = 0;
            int maxFreq = 0;

            var freqDict = new Dictionary<char, int>();

            for (int r = 0; r < s.Count(); r++)
            {
                // Add the current character to the frequency dictionary
                if (!freqDict.ContainsKey(s[r]))
                {
                    freqDict[s[r]] = 0;
                }
                freqDict[s[r]]++;

                maxFreq = Math.Max(maxFreq, freqDict[s[r]]);

                // Shrink the window by moving the left pointer
                while ((r - l + 1) - maxFreq > k)
                {
                    freqDict[s[l]]--;
                    l++;
                }

                // Update the maximum length of the valid window
                maxLength = Math.Max(maxLength, r - l + 1);
            }

            return maxLength;
        }

        public static void RunLengthOfLongestSubstring()
        {
            string inS = "dvdf";
            Console.WriteLine(LengthOfLongestSubstring(inS));
        }


        public static int LengthOfLongestSubstring(string s)
        {
            if (s == "")
            {
                return 0;
            }
            if (s.Count() < 2)
            {
                return 1;
            }
            int maxLen = 0;
            int l = 0;

            Dictionary<char, int> containsDict = new Dictionary<char, int>();

            for (int r = 0; r < s.Count(); r++)
            {
                while (true)
                {
                    if (containsDict.TryGetValue(s[r], out int val))
                    {
                        containsDict.Remove(s[l]);
                        l++;
                    }
                    else
                    {
                        break;
                    }
                }
                containsDict.Add(s[r], 1);
                maxLen = Math.Max(maxLen, containsDict.Count());
            }

            return maxLen;
        }

        public static void RunMaxProfit()
        {

            var input = new int[] { 2, 1, 4 };

            Console.WriteLine(MaxProfit(input));

        }
        public static int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length < 2)
            {
                return 0;
            }

            int maxProfit = 0;
            int min = prices[0];

            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] < min)
                {
                    min = prices[i];
                }
                else
                {
                    int curProfit = prices[i] - min;
                    maxProfit = Math.Max(maxProfit, curProfit);
                }
            }

            return maxProfit;
        }

        public static void IsValidBST()
        {
            var root = new TreeNode(5);
            root.left = new TreeNode(4);
            root.right = new TreeNode(6);

            root.right.left = new TreeNode(3);
            root.right.right = new TreeNode(7);

            var res = IsValidBST(root);
        }


        public static bool IsValidBST(TreeNode root)
        {
            var res = QueueTraversal(root);
            return res;
        }

        public static bool QueueTraversal(TreeNode root)
        {
            var queue = new Queue<(TreeNode node, int min, int max)>();
            queue.Enqueue((root, int.MinValue, int.MaxValue));

            while (queue.Count > 0)
            {

                int len = queue.Count;

                for (int i = 0; i < len; i++)
                {
                    var currNode = queue.Dequeue();

                    if (currNode.node.val <= currNode.min || currNode.node.val >= currNode.max)
                    {
                        return false;
                    }

                    if (currNode.node.left != null)
                    {

                        queue.Enqueue((currNode.node.left, currNode.min, currNode.node.val));
                        Console.WriteLine($"Left: {currNode.node.left.val} Min : {currNode.min} max: {currNode.node.val}");
                    }
                    if (currNode.node.right != null)
                    {
                        queue.Enqueue((currNode.node.right, currNode.node.val, currNode.max));
                        Console.WriteLine($"Right: {currNode.node.right.val}  Min : {currNode.node.val} max: {currNode.max}");
                    }
                }
            }
            return true;
        }

        #region Submitted
        public static void RunLevelOrderTraversal()
        {
            var root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            root.left.left = new TreeNode(4);
            root.left.right = new TreeNode(5);
            root.right.left = new TreeNode(6);
            root.right.right = new TreeNode(7);


            var res = LevelOrder(root);
        }

        public static List<List<int>> LevelOrder(TreeNode root)
        {

            if (root == null)
            {
                return new List<List<int>>();
            }

            return RecursiveLevelOrderTraversal(root, new List<List<int>>(), 0);
        }

        public static List<List<int>> RecursiveLevelOrderTraversal(TreeNode node, List<List<int>> res, int level)
        {
            if (node == null)
            {
                return null;
            }

            if (res.Count <= level)
            {
                res.Add(new List<int>());
            }

            res[level].Add(node.val);

            RecursiveLevelOrderTraversal(node.left, res, level + 1);
            RecursiveLevelOrderTraversal(node.right, res, level + 1);

            return res;
        }

        public static void RunBuildTree()
        {
            var preOrder = new int[] { 1, 2, 3, 4 };
            var inOrder = new int[] { 2, 1, 3, 4 };

            Console.WriteLine(BuildTree(preOrder, inOrder));
        }

        public static TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            // Get root position
            int rootPos = 0;
            for (int i = 0; i < inorder.Length; i++)
            {
                if (inorder[i] == preorder[0])
                {
                    rootPos = i;
                }
            }

            var root = new TreeNode(inorder[rootPos]);


            return new TreeNode();
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunKthSmallest()
        {
            var root = new TreeNode(4);
            root.left = new TreeNode(3);
            root.right = new TreeNode(5);
            root.left.left = new TreeNode(2);

            Console.WriteLine(KthSmallest(root, 4));

        }


        public static int KthSmallest(TreeNode root, int k)
        {

            var arr = new List<int>();

            var res = RecurseKSmallest(root, ref arr, k);

            int valRes = arr[k - 1];

            return valRes;
        }

        public static TreeNode RecurseKSmallest(TreeNode curNode, ref List<int> arr, int k)
        {

            if (curNode == null) return null;

            RecurseKSmallest(curNode.left, ref arr, k);

            arr.Add(curNode.val);

            RecurseKSmallest(curNode.right, ref arr, k);

            return curNode;

        }
        /////////////////////////////////////////////////////////////////////
        public static void RunGoodNodes()
        {
            var root = new TreeNode(3);
            root.left = new TreeNode(3);


            root.left.left = new TreeNode(4);
            root.left.right = new TreeNode(2);

            Console.WriteLine(GoodNodes(root));


        }

        public static int GoodNodes(TreeNode root)
        {
            int n = 0;

            if (root == null)
            {
                return 0;
            }

            RecursiveGoodNodes(root, -101, ref n);

            return n + 1;
        }

        public static void RecursiveGoodNodes(TreeNode curNode, int maxVal, ref int n)
        {

            if (curNode == null) return;

            if (curNode.val >= maxVal)
            {
                n++;
            }

            maxVal = Math.Max(maxVal, curNode.val);

            RecursiveGoodNodes(curNode.left, maxVal, ref n);
            RecursiveGoodNodes(curNode.right, maxVal, ref n);

            return;

        }

        /////////////////////////////////////////////////////////////////////
        public static void RunRightSideVisible()
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);

            root.left.left = null;
            root.left.right = new TreeNode(5);


            root.right.left = null;
            root.right.right = new TreeNode(4);

            Console.WriteLine(RightSideView(root));


        }
        public static List<int> RightSideView(TreeNode root)
        {
            if (root == null)
            {
                return new List<int>();
            }

            return BFSSearch(root);
        }

        public static List<int> BFSSearch(TreeNode rootNode)
        {
            var bfsQueue = new Queue<TreeNode>();
            var resList = new List<int>();

            int level = 0;
            bfsQueue.Enqueue(rootNode);


            while (bfsQueue.Count > 0)
            {

                int len = bfsQueue.Count;
                for (int i = 0; i < len; i++)
                {
                    var curNode = bfsQueue.Dequeue();

                    if (curNode.left != null)
                    {
                        bfsQueue.Enqueue(curNode.left);
                    }
                    if (curNode.right != null)
                    {
                        bfsQueue.Enqueue(curNode.right);
                    }

                    if (i == len - 1)
                    {
                        resList.Add(curNode.val);
                    }
                }

                level++;
            }


            return resList;
        }

        /////////////////////////////////////////////////////////////////////

        public static void RunLowestCommonAncestor()
        {
            var rootNode = new TreeNode();
            rootNode.val = 5;
            var node1 = new TreeNode();
            node1.val = 1;
            var node2 = new TreeNode();
            node2.val = 2;
            var node3 = new TreeNode();
            node3.val = 3;
            var node4 = new TreeNode();
            node4.val = 4;
            var node5 = new TreeNode();
            node5.val = 5;
            var node6 = new TreeNode();
            node6.val = 6;
            var node7 = new TreeNode();
            node7.val = 7;
            var node8 = new TreeNode();
            node8.val = 8;
            var node9 = new TreeNode();
            node9.val = 9;
            rootNode.left = node3;
            rootNode.right = node8;
            node3.left = node1;
            node3.right = node4;
            node1.right = node2;
            node8.left = node7;
            node8.right = node9;
            var res = LowestCommonAncestor(rootNode, node1, node9);

            Console.WriteLine(res.val);

        }

        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {

            var res = RecursionLCA(root, p, q);

            return res;
        }

        public static TreeNode RecursionLCA(TreeNode curNode, TreeNode p, TreeNode q)
        {
            if (curNode == null || p == null || q == null)
            {
                return null;
            }

            if (Math.Max(p.val, q.val) < curNode.val)
            {
                RecursionLCA(curNode.left, p, q);

            }
            else if (Math.Min(p.val, q.val) > curNode.val)
            {
                RecursionLCA(curNode.right, p, q);

            }
            else
            {
                return curNode;
            }

            return null;
        }

        /////////////////////////////////////////////////////////////////////
        public int LeetMaxDepth(TreeNode root)
        {
            if (root == null) return 0;

            var res = Math.Max(LeetMaxDepth(root.left), LeetMaxDepth(root.right)) + 1;

            return res;
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunIsBalancedTree()
        {
            var rootNode = new TreeNode();
            rootNode.val = 1;
            var node2 = new TreeNode();
            node2.val = 2;
            var node3 = new TreeNode();
            node3.val = 3;
            var node4 = new TreeNode();
            node4.val = 4;
            var node5 = new TreeNode();
            node5.val = 5;


            rootNode.left = node2;
            rootNode.right = node3;

            node3.left = node4;
            node4.left = node5;

            Console.Write(IsBalanced(rootNode));

        }

        public static bool IsBalanced(TreeNode root)
        {
            if (root == null) { return true; }


            var res = DfsRecurse(root, 0);

            if (res != -1)
            {
                return true;
            }

            return false;
        }


        public static int DfsRecurse(TreeNode curNode, int d)
        {
            if (curNode == null) return 0;

            int heightL = DfsRecurse(curNode.left, 0);
            int heightR = DfsRecurse(curNode.right, 0);

            if (heightL == -1)
            {
                return -1;
            }
            if (heightR == -1)
            {
                return -1;
            }

            if (Math.Abs(heightL - heightR) > 1)
            {
                return -1;
            }

            return Math.Max(heightL, heightR) + 1;

        }

        /////////////////////////////////////////////////////////////////////

        public static void RunDiameterOfBinaryTree()
        {
            var rootNode = new TreeNode();
            rootNode.val = 1;
            var node2 = new TreeNode();
            node2.val = 2;
            var node3 = new TreeNode();
            node3.val = 3;
            var node4 = new TreeNode();
            node4.val = 4;
            var node5 = new TreeNode();
            node5.val = 5;

            rootNode.right = node2;
            node2.left = node3;
            node2.right = node4;
            node3.left = node5;

            Console.WriteLine(DiameterOfBinaryTree(rootNode));

        }

        public static int DiameterOfBinaryTree(TreeNode root)
        {
            int n = 0;

            DfsDiameterRecur(root, ref n);

            return n;
        }

        public static int DfsDiameterRecur(TreeNode curNode, ref int n)
        {
            if (curNode == null) return 0;

            int NodeLeft = DfsDiameterRecur(curNode.left, ref n);
            int NodeRight = DfsDiameterRecur(curNode.right, ref n);

            n = Math.Max(n, NodeLeft + NodeRight);

            return Math.Max(NodeLeft, NodeRight) + 1;
        }


        /////////////////////////////////////////////////////////////////////
        public static void RunInvertTree()
        {

            var rootNode = new TreeNode();
            rootNode.val = 1;
            var node2 = new TreeNode();
            node2.val = 2;
            var node3 = new TreeNode();
            node3.val = 3;
            var node4 = new TreeNode();
            node4.val = 4;
            var node5 = new TreeNode();
            node5.val = 5;
            var node6 = new TreeNode();
            node6.val = 6;
            var node7 = new TreeNode();
            node7.val = 7;

            rootNode.left = node2;
            rootNode.right = node3;
            node2.left = node4;
            node2.right = node5;
            node3.left = node6;
            node3.right = node7;
            var nodeResult = InvertTree(rootNode);
        }


        public static TreeNode InvertTree(TreeNode root)
        {

            if (root == null) return null;

            var tempNode = new TreeNode(root.val);

            tempNode.right = InvertTree(root.left);
            tempNode.left = InvertTree(root.right);

            return tempNode;

        }
        /////////////////////////////////////////////////////////////////////
        public static void RunMaxDepth()
        {

            var rootNode = new TreeNode();
            rootNode.val = 1;
            var node2 = new TreeNode();
            node2.val = 2;
            var node3 = new TreeNode();
            node3.val = 3;
            var node4 = new TreeNode();
            node4.val = 4;
            rootNode.left = node2;
            rootNode.right = node3;
            node3.left = node4;

            Console.WriteLine(DFSMaxDepth(rootNode));
            Console.WriteLine(BFSMaxDepth(rootNode));
        }



        public static int DFSMaxDepth(TreeNode root)
        {
            // Perform DFS

            if (root == null) return 0;

            var depth = Math.Max(DFSMaxDepth(root.left), DFSMaxDepth(root.right));

            return depth + 1;

        }

        public static int BFSMaxDepth(TreeNode root)
        {

            Queue<TreeNode> queue = new Queue<TreeNode>();

            if (root != null)
            {
                queue.Enqueue(root);
            }
            int level = 0;

            while (queue.Count > 0)
            {
                int len = queue.Count;
                for (int i = 0; i < len; i++)
                {
                    var node = queue.Dequeue();

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }

                level++;
            }

            return level;

        }

        /////////////////////////////////////////////////////////////////////
        public static void RunMergeTwoLists()
        {
            var list1 = new List<int>() { 1, 2, 4 };
            var list2 = new List<int>() { 1, 3, 5 };

            var list1Head = new ListNode();
            var list2Head = new ListNode();

            list1Head.val = 1;
            list1Head.next = new ListNode() { val = 2 };
            list1Head.next.next = new ListNode() { val = 4 };


            list2Head.val = 1;
            list2Head.next = new ListNode() { val = 3 };
            list2Head.next.next = new ListNode() { val = 5 };


            var node = MergeTwoLists(list1Head, list2Head);

        }

        public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {

            if (list1 == null)
            {
                return list2;
            }
            else if (list2 == null)
            {
                return list1;
            }

            ListNode dummyNode = new ListNode();

            dummyNode.val = -100;
            dummyNode.next = list1;

            var currNode1 = dummyNode;
            var currNode2 = list2;

            while (currNode2 != null)
            {
                // Fetch the curr Node val for both list 1 and 2

                var nextNode1 = currNode1.next;
                var nextNode2 = currNode2;


                if (nextNode1 == null)
                {
                    currNode1.next = nextNode2;
                    currNode2 = nextNode1;

                }
                else if (currNode1.val <= nextNode2.val && nextNode1.val >= nextNode2.val)
                {
                    var temp = currNode1.next;
                    currNode1.next = nextNode2;

                    currNode2 = currNode2.next;
                    currNode1.next.next = temp;
                }

                currNode1 = currNode1.next;
            }

            return dummyNode.next;
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunReverseLinkedList()
        {
            var list = new List<int>() { 1, 2, 3, 4 };

            var nodes = new ListNode();

            nodes.val = 1;
            nodes.next = new ListNode() { val = 2 };
            nodes.next.next = new ListNode() { val = 3 };
            nodes.next.next.next = new ListNode() { val = 4 };

            Console.WriteLine(nodes.next.next.val);
            Console.WriteLine(nodes.next.next.next.val);

            var node = ReverseList(nodes);

        }

        public static ListNode ReverseList(ListNode head)
        {

            int n = 0;

            ListNode newHead = null;

            RecursiveUnwindList(head, ref n, ref newHead);

            return newHead;

        }

        public static ListNode RecursiveUnwindList(ListNode currNode, ref int n, ref ListNode newHead)
        {
            if (currNode == null)
            {
                return null;
            }

            n++;

            var prevNode = RecursiveUnwindList(currNode.next, ref n, ref newHead);

            n--;

            // Reverse the linked list here!

            if (currNode.next == null)
            {
                newHead = currNode;
                return currNode;
            }

            if (currNode != null && prevNode != null)
            {
                prevNode.next = currNode;
            }

            if (n == 0)
            {
                currNode.next = null;
                return null;
            }

            return currNode;
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunRemoveNthFromEnd()
        {
            var list = new List<int>() { 1, 2, 3, 4 };

            var nodes = new ListNode();
            nodes.val = 1;

            nodes.next = new ListNode() { val = 2 };
            nodes.next.next = new ListNode() { val = 3 };
            nodes.next.next.next = new ListNode() { val = 4 };

            Console.WriteLine(nodes.next.next.val);
            Console.WriteLine(nodes.next.next.next.val);

            Console.WriteLine(RemoveNthFromEnd(nodes, 2));

        }

        public static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            var values = head.NodesToList();

            return GetNextNode(head, ref n);
        }


        public static ListNode GetNextNode(ListNode currNode, ref int len)
        {
            if (currNode == null)
            {
                return null;
            }

            currNode.next = GetNextNode(currNode.next, ref len);

            len--;

            if (len == 0)
            {
                return currNode.next;
            }

            return currNode;
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunBinarySearch()
        {
            var input = new int[] { -1, 0, 3, 5, 9, 12 };

            Console.WriteLine(BinarySearch(input, 13));

        }
        public static int BinarySearch(int[] nums, int target)
        {
            var binarySearch = new BinarySearch();

            return binarySearch.Search(nums, target);
        }


        /////////////////////////////////////////////////////////////////////
        public static void RunParenthesesIsValid()
        {
            string input = "((";
            Console.Write(ParenthesesIsValid(input));

        }

        public static bool ParenthesesIsValid(string s)
        {
            Dictionary<char, char> charDict = new Dictionary<char, char> {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' }};

            if (s.Length == 0)
            {
                return true;
            }
            if (s.Length % 2 != 0)
            {
                return false;
            }

            var stack = new Stack<char>();
            char temp = '.';

            foreach (char c in s)
            {
                var isInStack = charDict.TryGetValue(c, out temp);

                if (!isInStack)
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Count > 0 && stack.Peek() == temp)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (stack.Count > 0)
            {
                return false;
            }

            return true;

        }

        public static void RunMinCostClimbingStairs()
        {
            var input = new int[] { 1, 2, 3 };

            Console.WriteLine(MinCostClimbingStairs(input));

        }
        public static int MinCostClimbingStairs(int[] cost)
        {

            var solver1 = new SolutionCostClimbingStairs();
            var cost1 = solver1.Solve(cost, 0);
            var cost2 = solver1.Solve(cost, 1);

            return Math.Min(cost1, cost2);
        }


        /////////////////////////////////////////////////////////////////////

        public static int RunClimbStairs_DP(int n)
        {
            // Dynamic programming
            int ways = 1;
            int waysPrev = 1;

            for (int i = 0; i < n - 1; i++)
            {
                var temp = waysPrev;
                waysPrev = ways;
                ways = ways + temp;
            }

            return ways;

        }


        /////////////////////////////////////////////////////////////////////
        public static void RunClimbStairs()
        {
            Console.WriteLine("Possible Ways = " + ClimbStairs(10));
        }
        public static int ClimbStairs(int n)
        {

            if (n == 1)
            {
                return 1;
            }
            if (n == 0)
            {
                return 0;
            }

            var solutionMemo = new SolutionStepCounterMemo(n);
            solutionMemo.Solve();

            return solutionMemo.Solve();
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunLastStoneWeight()
        {
            var maxHeap = new JacobsHeapMax();
            var nums = new List<int>() { 2, 3, 6, 2, 4 };

            foreach (var num in nums)
            {
                maxHeap.InsertNode(num);
            }


            while (nums.Count > 1)
            {

                var stoneX = maxHeap.RemoveLargest();
                var stoneY = maxHeap.RemoveLargest();

                // Should Implement peak here.
                // Do nothing, both removed
                if (stoneX == stoneY)
                {
                    break;
                }
                ;

                // If x < y , add new stone, y - x
                if (stoneX < stoneY)
                {
                    // Stone y  remains
                    maxHeap.InsertNode(stoneY - stoneX);
                }
                else
                {
                    // Stone X remains
                    maxHeap.InsertNode(stoneX - stoneY);
                }

                nums = maxHeap.GetHeapList();
            }
            maxHeap.DisplayHeap();
        }


        /////////////////////////////////////////////////////////////////////
        // Different from question, this is a custom heap implementation.

        public static void RunKthLargest()
        {
            /*
            var myHeap = new JacobsHeapMin();

            myHeap.InsertHeap(3);
            myHeap.InsertHeap(4);
            myHeap.InsertHeap(8);
            myHeap.InsertHeap(9);
            myHeap.InsertHeap(7);
            myHeap.InsertHeap(10);
            myHeap.InsertHeap(9);
            myHeap.InsertHeap(15);
            myHeap.InsertHeap(2);
            myHeap.InsertHeap(1);

            myHeap.RemoveSmallest();
            myHeap.RemoveSmallest();

            myHeap.DisplayHeap();

            */

            // Custom Heap implementation Min heap.
            var Heap = new KthLargest(4, new int[] { 10, 2, 3, 8, 1, 2 });
        }

        public class KthLargest
        {
            private JacobsHeapMin newHeap = new JacobsHeapMin();

            public KthLargest(int k, int[] nums)
            {

                foreach (var num in nums)
                {
                    newHeap.InsertHeap(num);
                }

                newHeap.DisplayHeap();
                var result = newHeap.GetKLargest(k);

                Console.WriteLine(result);
            }

            public int Add(int val)
            {
                return 0;
            }
        }
        /////////////////////////////////////////////////////////////////////
        public static void RunIsPalindrome()
        {
            string s = "0p";
            Console.Write(IsPalindrome(s));

        }
        public static bool IsPalindrome(string s)
        {

            string concatStr = "";
            s = s.ToLower();

            // Since a = 097 and z = 122 in utf8
            foreach (char c in s)
            {
                byte tempByte = (byte)c;

                if ((tempByte >= 97 && tempByte <= 122) || (tempByte >= 48 && tempByte <= 57))
                {
                    concatStr += c;
                }
            }

            // Some edge cases...
            if (concatStr == "")
            {
                return true;
            }
            if (concatStr.Length == 1)
            {
                return true;
            }

            var endPoint = concatStr.Length - 1;
            // Now start from both sides and go inwards 
            for (int i = 0; i < (concatStr.Length / 2); i++)
            {
                if (concatStr[i] != concatStr[endPoint])
                {
                    return false;
                }

                endPoint--;
            }

            return true;
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunIsValidSudoku()
        {
            /*
            char[][] board = new char[][]
            {
                new char[] { '1', '2', '.', '.', '3', '.', '.', '.', '.' },
                new char[] { '1', '.', '.', '5', '.', '.', '.', '.', '.' },
                new char[] { '.', '9', '8', '.', '.', '.', '.', '.', '3' },
                new char[] { '5', '.', '.', '.', '6', '.', '.', '.', '4' },
                new char[] { '.', '.', '.', '8', '.', '3', '.', '.', '5' },
                new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new char[] { '.', '.', '.', '.', '.', '.', '2', '.', '.' },
                new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '8' },
                new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };
            */
            char[][] board =
            {
                new char[] { '1', '2', '.', '.', '3', '.', '.', '.', '.' },
                new char[] { '4', '.', '.', '5', '.', '.', '.', '.', '.' },
                new char[] { '.', '9', '1', '.', '.', '.', '.', '.', '3' },
                new char[] { '5', '.', '.', '.', '6', '.', '.', '.', '4' },
                new char[] { '.', '.', '.', '8', '.', '3', '.', '.', '5' },
                new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new char[] { '.', '.', '.', '.', '.', '.', '2', '.', '.' },
                new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '8' },
                new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };



            Console.WriteLine("Is Valid = " + IsValidSudoku(board));
        }

        public static bool IsValidSudoku(char[][] board)
        {
            // Check each Col
            for (int col = 0; col < 9; col++)
            {
                HashSet<char> seen = new HashSet<char>();
                for (int i = 0; i < 9; i++)
                {
                    if (board[i][col] != '.')
                    {
                        if (seen.Contains(board[i][col]))
                        {
                            return false;
                        }
                        seen.Add(board[i][col]);
                    }
                }
            }

            // Check each row
            for (int row = 0; row < 9; row++)
            {
                HashSet<char> seen = new HashSet<char>();
                for (int i = 0; i < 9; i++)
                {
                    if (board[row][i] != '.')
                    {
                        if (seen.Contains(board[row][i]))
                        {
                            return false;
                        }
                        seen.Add(board[row][i]);
                    }
                }
            }


            // Use a dict to check that each 3x3 squares has duplicates.

            for (int square = 0; square < 9; square++)
            {
                HashSet<char> seen = new HashSet<char>();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int row = (square / 3) * 3 + i;
                        int col = (square % 3) * 3 + j;

                        char item = board[row][col];
                        if (item != '.')
                        {
                            if (seen.Contains(item))
                            {
                                return false;
                            }
                            seen.Add(item);
                        }

                    }
                }
            }

            return true;
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunProductExceptSelf()
        {
            var input = new int[] { -1, 1, 0, -3, 3 };
            var result = ProductExceptSelf(input);

            foreach (var x in result)
            {
                Console.WriteLine(x);
            }
        }
        // O(n) solution !!
        public static int[] ProductExceptSelf(int[] nums)
        {

            var totalMul = 1;
            var negCount = 0;
            int zeroCount = 0;

            List<int> result = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    totalMul *= nums[i];
                }
                else
                {
                    zeroCount++;
                }
                if (nums[i] < 0)
                {
                    negCount++;
                }
            }

            foreach (var num in nums)
            {
                // If the number is larger than 0 but there is a zero elsewhere, then just 0 as result.
                if (num > 0)
                {
                    if (zeroCount == 0)
                    {
                        result.Add(totalMul / num);
                    }
                    else
                    {
                        result.Add(0);
                    }
                }
                else
                {
                    // If there is only 1 zero in the array and we take out a zero, then no change.
                    if (num == 0)
                    {
                        if (zeroCount == 1)
                        {
                            result.Add(totalMul);
                        }
                        else
                        {
                            result.Add(0);
                        }

                    }

                    // Dealing with neg, is balanced, then result should be pos, else neg , of course we need to check if there is a zero too. 
                    if (num < 0)
                    {
                        if (zeroCount > 0)
                        {
                            result.Add(0);
                        }
                        else
                        {
                            var count = negCount - 1;

                            if ((count % 2) == 0)
                            {
                                result.Add(Math.Abs(totalMul / Math.Abs(num)));
                            }
                            else
                            {
                                result.Add(-(totalMul / Math.Abs(num)));
                            }
                        }
                    }
                }
            }

            return result.ToArray();
        }

        /////////////////////////////////////////////////////////////////////

        // Convert each char to Byte, then we can denode the starting char of each word as a value larger than UTF8 (255). 
        // This implementation do not require any additional characters to be added. More complex though! 

        public static void RunEncodeDecode()
        {
            List<string> strs = new List<string>() { "testing", "abs", "xyz" };

            var str = Q_Encode(strs);
            var strsDecoded = Q_Decode(str);

            foreach (var word in strsDecoded)
            {
                Console.WriteLine(word);
            }
        }
        public static string Q_Encode(IList<string> strs)
        {
            string result = "";
            int addition = 256;
            bool isFirstChar = false;

            if (strs.Count == 0)
            {
                return "";
            }
            if (strs.Count == 1)
            {
                return strs[0];
            }

            foreach (var s in strs)
            {
                isFirstChar = true;
                foreach (var c in s)
                {
                    // Since a is 97, if we add 3 then a will be 100, so we can work with 3s for each charactor in decode.
                    int cByte = c + 3;
                    string encodedC = "";

                    if (isFirstChar)
                    {
                        cByte += addition;
                        isFirstChar = false;
                    }

                    encodedC = cByte.ToString();
                    result += encodedC;
                }
            }

            return result;
        }

        public static List<string> Q_Decode(string s)
        {
            int len = s.Length;

            string word = "";
            List<string> words = new List<string>();
            bool inWord = true;

            if (s.Length == 0)
            {
                return new List<string>() { };
            }
            for (int i = 0; i < len; i += 3)
            {
                int intC = Int32.Parse((s[i].ToString() + s[i + 1].ToString() + s[i + 2].ToString())) - 3;

                if (intC > 256)
                {
                    intC -= 256;

                    if (word != "")
                    {
                        words.Add(word);
                        word = "";
                        inWord = true;
                    }
                }
                if (inWord)
                {
                    word += (char)intC;
                }
            }

            words.Add(word);

            return words;
        }

        /////////////////////////////////////////////////////////////////////
        // Use a reverse dict , bucket sort.
        public static void RunTopKFrequent()
        {
            var input = new int[] { 1, 2, 2, 3, 3, 3 };
            var k = 2;

            var result = TopKFrequent(input, k);

            foreach (var item in result)
            {
                Console.Write(" " + item + " ");
            }

        }
        public static int[] TopKFrequent(int[] nums, int k)
        {
            var freqDict = new Dictionary<int, int>();
            var revDict = new Dictionary<int, List<int>>();
            var result = new List<int>();

            // Add numbers to dict
            foreach (int i in nums)
            {
                if (freqDict.ContainsKey(i))
                {
                    freqDict[i]++;
                }
                else
                {
                    freqDict.Add(i, 1);
                }
            }

            // Build a reverse dict ! 
            foreach (var item in freqDict)
            {
                if (revDict.ContainsKey(item.Value))
                {
                    revDict[item.Value].Add(item.Key);

                }
                else
                {
                    revDict.Add(item.Value, new List<int> { item.Key });
                }
            }

            int k_Tracker = 0;
            // Since max possible occurancce is len of initial array, we can start from n and counting down
            for (int i = nums.Length; i >= 0; i--)
            {
                // Add if neccessory
                if (revDict.ContainsKey(i))
                {
                    foreach (var x in revDict[i])
                    {
                        if (k_Tracker > k - 1)
                        {
                            return result.ToArray();
                        }
                        else
                        {
                            result.Add(x);
                        }
                        k_Tracker++;
                    }
                }

            }

            return result.ToArray();
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunGroupAnagrams()
        {
            var input = new string[] { "bdddddddddd", "cbbbbbbbbbb" };
            var result = GroupAnagrams(input);

            foreach (var str in result)
            {
                Console.Write("[ ");

                foreach (var subStr in str)
                {
                    Console.Write(" " + subStr + " ");
                }
                Console.WriteLine(" ]");
            }

        }
        public static List<List<string>> GroupAnagrams(string[] strs)
        {
            var result = new List<List<string>>();
            // If one , then just return right away
            if (strs.Length == 1)
            {
                return new List<List<string>> { new List<string> { strs[0] } };
            }

            var occurLst = new List<int[]>();
            foreach (string str in strs)
            {
                Console.WriteLine("str " + str);
                // Count Occurance
                int[] count = new int[26];
                foreach (char c in str)
                {
                    count[c - 'a']++;
                }
                occurLst.Add(count);
            }

            // Now map the array into a dict key so we can lookup unique values, add that match to sublist.
            var occurDictLst = new List<Dictionary<string, List<int>>>();

            int inputPos = 0;
            int outputPos = 0;

            var occurDict = new Dictionary<string, int>();
            foreach (var occur in occurLst)
            {
                var str = "";
                foreach (var x in occur)
                {
                    str += x.ToString() + "|";
                }

                Console.WriteLine(str);

                if (occurDict.ContainsKey(str))
                {
                    // If the current string has a occurance match, add that match to sublist.

                    var value = occurDict[str];
                    var strAdd = strs[inputPos];
                    result[value].Add(strAdd);

                }
                else
                {
                    // If the string is new, add the new string to the list

                    occurDict.Add(str, outputPos);
                    result.Add(new List<string> { strs[inputPos] });
                    outputPos++;
                }
                inputPos++;
            }

            return result;
        }


        /////////////////////////////////////////////////////////////////////
        public static void RunTwoSum()
        {
            var result = TwoSum(new int[] { 4, 5, 6 }, 10);

            foreach (var i in result)
            {
                Console.WriteLine(i);
            }


        }
        public static int[] TwoSum(int[] nums, int target)
        {
            var intDict = new Dictionary<int, int>();
            var diffPos = 0;
            int i = 0;
            foreach (int val in nums)
            {
                if (intDict.ContainsKey(val))
                {
                    intDict[val] = i;
                }
                else
                {
                    intDict.Add(val, i);
                }

                i++;
            }
            int j = 0;
            foreach (int val in nums)
            {
                var diff = intDict.TryGetValue(target - val, out diffPos);
                if (diff && diffPos != j)
                {
                    return new int[] { j, diffPos };
                }
                j++;
            }

            return new int[] { 0, 0 };
        }
        /////////////////////////////////////////////////////////////////////
        public static void RunIsAnagram()
        {
            //string s = "racecar";
            //string t = "carrace";

            string s = "jam";
            string t = "jar";

            var result = IsAnagram(s, t);
            Console.WriteLine(result.ToString());
        }
        public static bool IsAnagram(string s, string t)
        {
            // Check is both strings is the same length else false
            if (s.Length != t.Length)
            {
                return false;
            }

            // Add string to a dict
            var dicS = AddStringToDict(s);
            var dicT = AddStringToDict(t);

            foreach (var charValuePair in dicS)
            {
                var dicTValuePair = dicT.TryGetValue(charValuePair.Key, out int val);
                // If the char is not found in first string...
                if (!dicTValuePair)
                {
                    return false;
                }
                // If the number of char is different from the first..
                if (val != charValuePair.Value)
                {
                    return false;
                }

            }

            return true;
        }
        public static Dictionary<char, int> AddStringToDict(string s)
        {
            var dicS = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (dicS.ContainsKey(c))
                {
                    dicS[c]++;
                }
                else
                {
                    dicS.Add(c, 1);
                }
            }
            return dicS;
        }

        /////////////////////////////////////////////////////////////////////
        public static void RunContainsDuplicate()
        {
            int[] testInput = new int[] { 1, 2, 3, 3 };
            var result = hasDuplicate(testInput);
            Console.WriteLine(result.ToString());
        }
        public static bool hasDuplicate(int[] nums)
        {
            var dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    return true;
                }
                else
                {
                    dic.Add(nums[i], 1);
                }
            }
            return false;
        }
        /////////////////////////////////////////////////////////////////////
        #endregion
    }
}



#region Utils
public class MinStack
{
    Stack<int> stack = new Stack<int>();
    Stack<int> minStack = new Stack<int>();

    int currMin = -1;

    public MinStack()
    {

    }

    public void Push(int val)
    {
        stack.Push(val);

        if (minStack.Count > 0)
        {
            var valMin = minStack.Peek();

            if (valMin < val)
            {
                minStack.Push(valMin);
            }
            else
            {
                minStack.Push(val);
            }
        }
        else
        {
            minStack.Push(val);
        }


    }

    public void Pop()
    {
        if (stack.Count > 0)
        {
            stack.Pop();
            minStack.Pop();
        }


    }

    public int Top()
    {
        if (stack.Count > 0)
        {
            return stack.Peek();
        }
        else
        {
            return -int.MaxValue;
        }

    }

    public int GetMin()
    {
        if (minStack.Count > 0)
        {
            return minStack.Peek();
        }
        else
        {
            return -int.MaxValue;
        }

    }
}
public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }

}


public class ListNode
{

    public int val;
    public ListNode next;
    private List<int> nodeList = new List<int>();

    public ListNode(int val = 0, ListNode next = null)
    {

        this.val = val;
        this.next = next;
    }

    public List<int> NodesToList()
    {

        GetNextNode(this);

        return nodeList;
    }


    private ListNode GetNextNode(ListNode currNode)
    {
        nodeList.Add(currNode.val);

        if (currNode.next != null)
        {
            return GetNextNode(currNode.next);
        }

        return null;
    }
}


public class BinarySearch
{

    private int[] nums;

    public BinarySearch() { }

    public int Search(int[] nums, int target)
    {
        this.nums = nums;
        int len = nums.Length;

        var result1 = SearchPartition(0, len - 1, target);

        return result1;
    }

    public int SearchPartition(int lowerBound, int upperBound, int target)
    {

        int mid = lowerBound + (upperBound - lowerBound) / 2;

        if (lowerBound > upperBound)
        { return -1; }

        if (nums[mid] == target)
        { return mid; }

        if (target > nums[mid])
        {
            // Search right partition
            return SearchPartition(mid + 1, upperBound, target);
        }
        else
        {
            return SearchPartition(lowerBound, mid - 1, target);
        }
    }
}


public class SolutionCostClimbingStairs
{
    public SolutionCostClimbingStairs() { }

    private Dictionary<int, int> memoDict = new Dictionary<int, int>();

    public int Solve(int[] costs, int initalStep)
    {
        int n = costs.Length - initalStep;

        var result = RecursiveSolve(initalStep, 0, costs);

        return result;
    }

    public int RecursiveSolve(int currStep, int currCost, int[] costs)
    {

        var memoCost = -1;
        var oneStepCost = -1;
        var twoStepCost = -1;

        if (currStep >= costs.Length)
        {
            return 0;
        }

        bool isSearched = memoDict.TryGetValue(currStep, out memoCost);

        if (isSearched)
        {
            currCost = memoDict[currStep];
            return currCost;
        }

        oneStepCost = RecursiveSolve(currStep + 1, currCost, costs);
        twoStepCost = RecursiveSolve(currStep + 2, currCost, costs);

        currCost = costs[currStep] + Math.Min(oneStepCost, twoStepCost);

        memoDict[currStep] = currCost;
        return currCost;
    }

}


// Used for the stairs climbing problem
public class SolutionStepCounterMemo
{
    private Dictionary<int, int> memo = new Dictionary<int, int>();
    int n = 0;
    public SolutionStepCounterMemo(int n)
    {
        this.n = n;
    }

    public int Solve()
    {
        return ClimbRecursive(0, 0, this.n);
    }

    public int ClimbRecursive(int currVal, int distinctWays, int target)
    {
        if (currVal == target)
        {
            distinctWays++;
            return distinctWays;
        }

        if (currVal > target)
        {
            return distinctWays;
        }
        else
        {
            int distinctWaysMemo = 0;
            bool isSearched = memo.TryGetValue(currVal, out distinctWaysMemo);

            if (isSearched)
            {
                return distinctWays + distinctWaysMemo;
            }
            else
            {
                distinctWays = ClimbRecursive(currVal + 1, distinctWays, target);
                distinctWays = ClimbRecursive(currVal + 2, distinctWays, target);
            }
        }

        // Remember the solution !
        memo[currVal] = distinctWays;
        return distinctWays;
    }
}



public class JacobsHeapMax
{

    List<int> heapList = new List<int>();

    public JacobsHeapMax() { }

    public int GetParentPos(int currPos)
    {
        // Denote -1 as no parent node
        int pos = (currPos - 1) / 2;

        if (pos < 0)
        {
            return -1;
        }
        return pos;
    }

    public int GetLeftChildPos(int currPos)
    {

        return (2 * currPos) + 1;
    }

    public int GetRightChildPos(int currPos)
    {
        return (2 * currPos) + 2;
    }

    public int GetLargest()
    {

        return heapList[0];
    }

    public int InsertNode(int currVal)
    {
        heapList.Add(currVal);
        int currNodePos = heapList.Count - 1;

        // If the parent node is smaller, then swap

        for (int i = 0; i < currNodePos + 1; i++)
        {
            int parentPos = GetParentPos(currNodePos);

            if (parentPos == -1)
            {
                return currNodePos;
            }

            int parentVal = heapList[parentPos];

            if (parentVal < currVal)
            {
                heapList[currNodePos] = parentVal;
                heapList[parentPos] = currVal;

                currNodePos = parentPos;
            }
        }
        return currNodePos;
    }

    public int RemoveLargest()
    {
        // Remove top node then recursive heapify.
        var largestVal = heapList[0];

        heapList.RemoveAt(0);

        SwapMinOfBothChild(0);

        return largestVal;
    }

    public List<int> GetHeapList()
    {
        return heapList;
    }

    public void SwapMinOfBothChild(int parentPos)
    {

        int leftChildPos = GetLeftChildPos(parentPos);
        int rightChildPos = GetRightChildPos(parentPos);

        int lenHeap = heapList.Count - 1;

        if (leftChildPos > lenHeap)
        {
            leftChildPos = -1;
        }
        if (rightChildPos > lenHeap)
        {
            rightChildPos = -1;
        }

        // If leftchild is missing... Dont swap
        if (leftChildPos == -1)
        {
            return;
        }

        // If right is missing , then check left exists then swap if child is smaller
        if (rightChildPos == -1 && leftChildPos > 0)
        {
            var leftChildVal = heapList[leftChildPos];
            var parentVal = heapList[parentPos];
            if (parentVal < leftChildVal)
            {
                heapList[parentPos] = leftChildVal;
                heapList[leftChildPos] = parentVal;
                parentPos = leftChildPos;
            }
            else { return; }

        }
        else if (leftChildPos != -1 && rightChildPos != -1)
        {
            // If both left and right nodes are present, then choose largest and swap
            var leftChildVal = heapList[leftChildPos];
            var rightChildVal = heapList[rightChildPos];
            var parentVal = heapList[parentPos];

            if ((parentVal < rightChildVal) && (rightChildVal > leftChildVal))
            {
                heapList[parentPos] = rightChildVal;
                heapList[rightChildPos] = parentVal;
                parentPos = rightChildPos;

            }
            else if ((parentVal < leftChildVal) && (leftChildVal > rightChildVal))
            {
                heapList[parentPos] = leftChildVal;
                heapList[leftChildPos] = parentVal;
                parentPos = leftChildPos;
            }
            else
            {
                return;
            }
        }
        // Recursive Bubble down.
        if (parentPos != -1 || parentPos == 0)
        {
            SwapMinOfBothChild(parentPos);
        }
    }

    public void DisplayHeap()
    {
        Console.WriteLine("------------");
        foreach (var num in heapList)
        {
            Console.WriteLine(num);
        }

        Console.WriteLine("------------");
    }
}

public class JacobsHeapMin
{
    List<int> heapList = new List<int>();

    public JacobsHeapMin()
    {
    }

    public int GetParentPos(int pos)
    {
        return (pos - 1) / 2;
    }

    public int GetLeftChildPos(int pos)
    {
        return (2 * pos) + 1;
    }
    public int GetRightChildPos(int pos)
    {
        return (2 * pos) + 2;
    }

    public int Swap(int curPos)
    {
        if (curPos == 0)
        {
            return -1;
        }

        int parentPos = GetParentPos(curPos);
        int parentVal = heapList[parentPos];
        int currentVal = heapList[curPos];

        // If the parent node is larger then swap with child
        if (parentVal > currentVal)
        {
            heapList[parentPos] = currentVal;
            heapList[curPos] = parentVal;

            return parentPos;
        }
        return -1;
    }

    public void InsertHeap(int val)
    {
        heapList.Add(val);

        int lastPos = heapList.Count - 1;
        var swapPos = lastPos;

        while (swapPos != -1)
        {
            swapPos = Swap(swapPos);
        }
    }

    public void RemoveSmallest()
    {
        if (heapList.Count == 1)
        {
            heapList.RemoveAt(0);
            return;
        }

        // Remove root node, and take the last node to root.
        var lastNodePos = heapList.Count - 1;
        var lastNodeVal = heapList[lastNodePos];

        heapList[0] = lastNodeVal;
        heapList.RemoveAt(lastNodePos);

        // Recursive swap
        SwapPosSmallerChild(0);

    }

    public void SwapPosSmallerChild(int parentPos)
    {
        int heapLen = heapList.Count;

        // Get parent value
        int parentVal = heapList[parentPos];

        // Get positions of left and right children
        int leftChildPos = GetLeftChildPos(parentPos);
        int rightChildPos = GetRightChildPos(parentPos);

        // Determine the smallest child
        int smallestChildPos = -1;

        if (leftChildPos < heapLen && heapList[leftChildPos] < parentVal)
        {
            smallestChildPos = leftChildPos;
        }

        if (rightChildPos < heapLen && heapList[rightChildPos] <
            (smallestChildPos != -1 ? heapList[smallestChildPos] : parentVal))
        {
            smallestChildPos = rightChildPos;
        }

        // Swap with the smallest child if needed
        if (smallestChildPos != -1)
        {
            heapList[parentPos] = heapList[smallestChildPos];
            heapList[smallestChildPos] = parentVal;

            // Recursively heapify the affected subtree
            SwapPosSmallerChild(smallestChildPos);
        }
    }

    public int GetKLargest(int k)
    {
        int removeCount = heapList.Count - k;

        for (int i = 0; i <= removeCount; i++)
        {
            RemoveSmallest();
        }

        return GetSmallest();
    }
    public int GetSmallest()
    {
        return heapList[0];
    }

    public void DisplayHeap()
    {
        foreach (var val in heapList)
        {
            Console.WriteLine(val.ToString());
        }
    }

}

#endregion
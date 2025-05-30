﻿using System;
using System.Data;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;

namespace NeetCode150
{
    public class NodeGraph
    {
        public int val;
        public IList<NodeGraph> neighbors;

        public NodeGraph()
        {
            val = 0;
            neighbors = new List<NodeGraph>();
        }

        public NodeGraph(int _val)
        {
            val = _val;
            neighbors = new List<NodeGraph>();
        }

        public NodeGraph(int _val, List<NodeGraph> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            RunHasCycle();


            Console.ReadLine();
        }

        #region InProgress


    
       


        public static void RunCloneGraph()
        {
            var node1 = new NodeGraph(1);
            var node2 = new NodeGraph(2);
            var node3 = new NodeGraph(3);

            node1.neighbors.Add(node2);
            node2.neighbors.Add(node1);
            node2.neighbors.Add(node3);
            node3.neighbors.Add(node2);

            var newNodes = CloneGraph(node1);

        }



        public static NodeGraph CloneGraph(NodeGraph node)
        {
            var newNode1 = new NodeGraph(1);
            var nodeDict = new HashSet<NodeGraph>();

            NodeGraphSearch(node, nodeDict, newNode1);

            return newNode1;
        }

        public static void NodeGraphSearch(NodeGraph node, HashSet<NodeGraph> dict, NodeGraph newNode)
        {
            if(node == null)
            {
                return;
            }

            Console.WriteLine("Node: " + node.val);
            if (!dict.Contains(node))
            {
                dict.Add(node);
            } else
            {
                return;
            }

            foreach (var n in node.neighbors)
            {
                var clone = new NodeGraph(n.val);
                newNode.neighbors.Add(clone);
                NodeGraphSearch(n, dict, clone);
            }

        }






        // Too much brain .... Will come back to finish this....
        public static void RunSearchMatrix()
        {

            int[][] input = new int[][]
            {
                new int[] { 1,2,4,8 },
                new int[] { 10,11,12,13 },
                new int[] { 14, 20, 30, 40 }
            };



            var res = SearchMatrix(input, 3);

        }

        public static bool SearchMatrix(int[][] matrix, int target)
        {
            int size = matrix.Length * matrix[0].Length;

            DFSSearchMat(matrix, target, size, 0, 0, matrix.Length - 1, matrix[0].Length - 1);

            return false;
        }

        public static bool DFSSearchMat(int[][] matrix, int target, int size, int r1, int c1, int r4, int c4)
        {

            if(r1 ==  r4 && c1 == c4)
            {
                return false;
            }

            // Partition
            int p1 = size / 2;
            int p2 = size - p1;

            int newR2 = r1 + (p1 / matrix[0].Length);
            int newC2 = c1 + (p1 % matrix[0].Length) - 1;

            int newR3 = r1 + ((p1 + 1) / matrix[0].Length);
            int newC3 = c1 + ((p1 + 1) % matrix[0].Length) - 1;

            DFSSearchMat(matrix, target, p1, r1, c1, newR2, newC2);
            DFSSearchMat(matrix, target, p2, newR3, newC3, r4, c4);


            return false;
        }




        #endregion


        public static void RunHasCycle()
        {

            var list1Head = new ListNode();

            list1Head.val = 1;
            list1Head.next = new ListNode() { val = 2 };
            list1Head.next.next = new ListNode() { val = 3 };
            list1Head.next.next.next = new ListNode() { val = 4 };

            list1Head.next.next.next = list1Head;

            var res = HasCycle(list1Head);

        }


        public static bool HasCycle(ListNode head)
        {
            var visited = new HashSet<ListNode>();
            var res = HasCycleDFS(head, visited, false);

            return res;
        }


        public static bool HasCycleDFS(ListNode node, HashSet<ListNode> visited, bool res)
        {
            if (node == null)
            {
                return res;
            }

            if (!visited.Contains(node))
            {
                visited.Add(node);
            }
            else
            {
                return true;
            }

            res = HasCycleDFS(node.next, visited, res);

            return res;
        }

        public static void RunMaxAreaOfIsland()
        {

            var input = new int[][]
            {
                new int[] { 0, 1, 1, 0, 1 },
                new int[] { 1, 0, 1, 0, 1 },
                new int[] { 0, 1, 1, 0, 1 },
                new int[] { 0, 1, 0, 0, 1 }
            };

            var res = MaxAreaOfIsland(input);

        }
        public static int MaxAreaOfIsland(int[][] grid)
        {
            int maxArea = 0;
            var visited = new Dictionary<string, int>();

            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[0].Length; c++)
                {
                    if (visited.ContainsKey($"{r} | {c}"))
                    {
                        continue;
                    }

                    if (grid[r][c] == 1)
                    {
                        int curArea = DFSMaxAreaOfIsland(grid, r, c, visited, 0);
                        maxArea = Math.Max(curArea, maxArea);

                    }
                }
            }

            return maxArea;
        }

        public static int DFSMaxAreaOfIsland(int[][] grid, int r, int c, Dictionary<string, int> visited, int curArea)
        {
            // Bounding Function
            if (r < 0 || c < 0 || r >= grid.Length || c >= grid[0].Length)
            {
                return curArea;
            }

            if (grid[r][c] == 0)
            {
                return curArea;
            }

            if (!visited.ContainsKey($"{r} | {c}") && grid[r][c] == 1)
            {
                visited.Add($"{r} | {c}", 1);
            }
            else
            {
                return curArea;
            }

            curArea += 1;

            // DFS Section
            curArea = DFSMaxAreaOfIsland(grid, r + 1, c, visited, curArea);
            // DFS Down
            curArea = DFSMaxAreaOfIsland(grid, r - 1, c, visited, curArea);
            // DFS Right
            curArea = DFSMaxAreaOfIsland(grid, r, c + 1, visited, curArea);
            // DFS Left
            curArea = DFSMaxAreaOfIsland(grid, r, c - 1, visited, curArea);

            return curArea;
        }

        public static void RunNumberOfIslands()
        {

            var input = new char[][]
            {
                new char[] { '0', '1', '1', '1', '0' },
                new char[] { '0', '1', '0', '1', '0' },
                new char[] { '1', '1', '0', '0', '0' },
                new char[] { '0', '0', '0', '0', '0' }
            };


            var input2 = new char[][]
            {
                new char[] { '1', '1', '0', '0', '1' },
                new char[] { '1', '1', '0', '0', '1' },
                new char[] { '0', '0', '1', '0', '0' },
                new char[] { '0', '0', '0', '1', '1' }

            };


            var res = NumIslands(input2);

        }

        public static int NumIslands(char[][] grid)
        {
            var visited = new Dictionary<string, char>();
            int islandCount = 0;

            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[0].Length; c++)
                {
                    // If we already visited the cell, then skip
                    if (visited.ContainsKey($"{r} | {c}"))
                    {
                        continue;
                    }

                    if (grid[r][c] == '1')
                    {
                        // DFS 
                        DFSNumOfIslands(grid, r, c, visited);

                        islandCount++;
                    }
                }
            }

            return islandCount;
        }

        public static void DFSNumOfIslands(char[][] grid, int r, int c, Dictionary<string, char> visited)
        {

            if (r < 0 || c < 0 || r >= grid.Length || c >= grid[0].Length || grid[r][c] == '0')
            {
                return;
            }

            if (!visited.ContainsKey($"{r} | {c}") && grid[r][c] == '1')
            {
                visited.Add($"{r} | {c}", grid[r][c]);
            }
            else
            {
                return;
            }

            if (grid[r][c] == '0')
            {
                return;
            }

            // DFS Up
            DFSNumOfIslands(grid, r + 1, c, visited);
            // DFS Down
            DFSNumOfIslands(grid, r - 1, c, visited);
            // DFS Right
            DFSNumOfIslands(grid, r, c + 1, visited);
            // DFS Left
            DFSNumOfIslands(grid, r, c - 1, visited);

        }

        public static void RunSearch2DMat()
        {

            var input = new int[][] {
                new int[] { 1, 3, 4, 8 },
                new int[] { 10, 11, 12, 13 },
                new int[] { 14, 20, 30, 40 },
            };

            int[][] input2 = new int[][]
            {
                new int[] { 1 },
                new int[] { 3 }
            };

            var res = SearchMatrix2D(input2, 3);
        }


        public static bool SearchMatrix2D(int[][] matrix, int target)
        {
            if (matrix.Length < 2)
            {
                // Refactor this out to separate binary search function...
                foreach (var num in matrix.First())
                {
                    if (target == num)
                    {
                        return true;
                    }
                    if (num > target)
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < matrix.Length - 1; i++)
            {
                var arr = matrix[i];
                var nextArr = matrix[i + 1];


                if (target > arr.Last() && target <= nextArr.Last() && target >= nextArr.First())
                {
                    // Select Second Arry
                    arr = nextArr;
                }

                if (target > arr.Last() && target < nextArr.First())
                {
                    return false;
                }

                // Refactor this out to separate binary search function...
                foreach (var num in arr)
                {
                    if (target == num)
                    {
                        return true;
                    }
                    if (num > target || target > arr.Last())
                    {
                        break;
                    }
                }
            }

            return false;
        }

        public static void RunNextGreaterElement()
        {
            var nums1 = new[] { 2, 4 };
            var nums2 = new[] { 1, 2, 3, 4 };

            var res = NextGreaterElement(nums1, nums2);
        }

        public static int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            if (nums2.Length == 0) return new int[nums1.Length];

            // Create dic to store is greater dict
            var dict = new Dictionary<int, int>();
            int greater = nums2[nums2.Length - 1];
            dict.Add(nums2[nums2.Length - 1], -1);

            for (int i = nums2.Length - 2; i >= 0; i--)
            {
                if (nums2[i] < nums2[i + 1])
                {

                    dict.Add(nums2[i], nums2[i + 1]);
                }
                else if (nums2[i] < greater)
                {
                    dict.Add(nums2[i], greater);
                }
                else
                {
                    dict.Add(nums2[i], -1);
                }

                if (nums2[i] > greater)
                {
                    greater = nums2[i];
                }
            }

            var res = new int[nums1.Length];

            for (int i = 0; i < nums1.Length; i++)
            {
                int temp = -1;
                dict.TryGetValue(nums1[i], out temp);
                res[i] = temp;
            }

            return res;
        }









        public static void RunNonRepeatingChar()
        {
            string input1 = "aabbcc"; // -1

            Console.WriteLine("Answer is : " + NoNRepeatedChar(input1).ToString());
        }

        public static int NoNRepeatedChar(string input)
        {

            var dict = new Dictionary<char, int>();

            // Subproblem 1 . Creating the dict and are populating the count for each charactor
            foreach (char c in input)
            {
                if (dict.ContainsKey(c))
                {
                    dict[c] += 1;
                } else
                {
                    dict[c] = 1;
                }
            }

            // Subproblem 2 . Iterate through each char , find the first char where value == 1

            for(int i= 0; i < input.Count(); i++)
            {
                // Lookup the dict
                if (dict.ContainsKey(input[i]))
                {
                    // Retrives the value from dict
                    int value = dict[ input[i]];
                    if(value == 1)
                    {
                        return i;
                    }
                }
            }

            // If non if the char is unique , return -1
            return -1;
        }






        public static void RunExistWordSearch()
        {
            char[][] board = new char[][]
            {
                new char[] { 'A', 'B', 'C', 'D' },
                new char[] { 'S', 'A', 'A', 'T' },
                new char[] { 'A', 'C', 'A', 'E' }
            };

            char[][] board2 = new char[][]
            {
                new char[] { 'A', 'B', 'C', 'E' },
                new char[] { 'S', 'F', 'C', 'S' },
                new char[] { 'A', 'D', 'E', 'E' }
            };

            char[][] board3 = new char[][]
            {
                new char[] { 'a', 'b' },
                new char[] { 'c', 'd'},
            };

            char[][] board4 = new char[][]
            {
                new char[] { 'C', 'A', 'A' },
                new char[] { 'A', 'A', 'A' },
                new char[] { 'B', 'C', 'D' },
            };

            char[][] board5 = new char[][]
            {
                new char[] { 'A', 'B', 'C', 'E' },
                new char[] { 'S', 'F', 'E', 'S' },
                new char[] { 'A', 'D', 'E', 'E' },
            };

            var res = WordSearchExist(board5, "ABCESEEEFS");
        }

        public static bool WordSearchExist(char[][] board, string word)
        {

            int r = board.Length;
            int c = board[0].Length;

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (board[i][j] == word[0])
                    {
                        if (WordSearchBk(board, word, 0, i, j, new HashSet<string>()))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool WordSearchBk(char[][] board, string word, int wordIdx, int r, int c, HashSet<string> memo)
        {

            // Check Bounds of position
            if (r < 0 || r >= board.Length || c < 0 || c >= board[0].Length || wordIdx >= word.Length)
            {
                return false;
            }

            var curVal = board[r][c];
            string currentPos = r + "," + c;

            // Check if path is visited
            if (memo.Contains(currentPos))
            {
                return false;
            }

            if (word[wordIdx] == curVal)
            {
                if (wordIdx == word.Length - 1)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            memo.Add(currentPos);

            var res = WordSearchBk(board, word, wordIdx + 1, r + 1, c, memo) ||
                  WordSearchBk(board, word, wordIdx + 1, r - 1, c, memo) ||
                  WordSearchBk(board, word, wordIdx + 1, r, c + 1, memo) ||
                  WordSearchBk(board, word, wordIdx + 1, r, c - 1, memo);

            memo.Remove(currentPos);

            return res;
        }

        public static void RunSubsetsWithDup()
        {
            var input = new int[] { 1, 2, 1 };
            SubsetsWithDup(input);
        }

        public static List<List<int>> SubsetsWithDup(int[] nums)
        {
            var res = new List<List<int>>();
            var resNum = new List<List<int>>();
            var memo = new HashSet<string>();

            Array.Sort(nums);
            // Map num to index
            var convertedNum = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            { convertedNum.Add(i); }

            BackTkSubsets2(res, convertedNum, new List<int>(), 0);

            // Convert index back to Nums and check for duplicates
            foreach (var lst in res)
            {
                var temp = new List<int>();
                string str = "";
                foreach (var i in lst)
                {
                    str += nums[i].ToString();
                    temp.Add(nums[i]);
                }
                if (!memo.Contains(str))
                {
                    resNum.Add(temp);
                    memo.Add(str);
                }
            }
            return resNum;
        }

        public static void BackTkSubsets2(List<List<int>> res, List<int> nums, List<int> subArr, int choice)
        {
            // Bounding Function.
            if (choice > nums.Count) { return; }
            res.Add(new List<int>(subArr));

            // Choices , only get numbers to the right.
            var choices = new List<int>();
            for (int i = choice; i < nums.Count; i++) { choices.Add(i); }

            // Recurse and backtrack.
            foreach (var c in choices)
            {
                subArr.Add(nums[c]);
                BackTkSubsets2(res, nums, subArr, c + 1);

                subArr.Remove(nums[c]);

            }
        }

        public static void RunPermute()
        {
            var input = new int[] { 1, 3, 5, 7, 9, 10, 15 };
            var res = Permute(input);
        }

        public static List<List<int>> Permute(int[] nums)
        {
            var res = new List<List<int>>();

            PermuteBackTrk(nums, new List<int>(), res);

            return res;
        }

        public static void PermuteBackTrk(int[] nums, List<int> subAry, List<List<int>> res)
        {
            // Bounding
            if (subAry.Count == nums.Length)
            {
                res.Add(new List<int>(subAry));
            }

            // DFS Down
            foreach (var choice in PermuteGetChoice(nums, subAry))
            {
                subAry.Add(choice);

                PermuteBackTrk(nums, subAry, res); // Recurse

                subAry.Remove(choice); // BackTrack Rm
            }
        }

        public static List<int> PermuteGetChoice(int[] arr, List<int> subArr)
        {
            var res = new List<int>();
            foreach (var i in arr)
            {
                if (!subArr.Contains(i)) { res.Add(i); }
            }
            return res;
        }

        public static void RunCombinationSum2()
        {
            var input = new int[] { 1, 2, 3, 4, 5 };
            var res = CombinationSum2(input, 7);

        }
        public static List<List<int>> CombinationSum2(int[] candidates, int target)
        {
            Array.Sort(candidates);

            var res = CombiSum2BackTrack(candidates, target, 0, new List<int>(), 0, new List<List<int>>());

            return res;
        }


        public static List<List<int>> CombiSum2BackTrack(int[] candi, int target, int curEle, List<int> subAry, int sum, List<List<int>> res)
        {
            if (curEle > candi.Length - 1 || sum + candi[curEle] > target)
            {
                return res;
            }

            if (sum + candi[curEle] == target)
            {
                var temp = new List<int>(subAry);
                temp.Add(candi[curEle]);
                res.Add(temp);

                return res;
            }

            subAry.Add(candi[curEle]);
            CombiSum2BackTrack(candi, target, curEle + 1, subAry, sum + candi[curEle], res);

            // Backtrack !
            subAry.RemoveAt(subAry.Count - 1);
            sum -= candi[curEle];

            // Skip Pass Duplicates
            int next = curEle + 1;
            while (next < candi.Length && candi[next] == candi[curEle])
            {
                next++;
            }

            CombiSum2BackTrack(candi, target, next, subAry, sum + candi[curEle], res);

            return res;
        }

        public static void RunCombinationSum()
        {
            var input = new int[] { 8, 7, 4, 3 };
            var res = CombinationSum(input, 11);
        }

        public static List<List<int>> CombinationSum(int[] nums, int target)
        {

            Array.Sort(nums);
            var res = BackTrackCombiSum(nums, new List<int>(), 0, 0, target, new List<List<int>>());

            return res;
        }

        public static List<List<int>> BackTrackCombiSum(int[] nums, List<int> subAry, int ele, int curSum, int target, List<List<int>> res)
        {
            // The target 
            if (curSum == target)
            {
                res.Add(new List<int>(subAry));
                return res;
            }

            // Boundinf Function 
            if (ele >= nums.Length || curSum + nums[ele] > target)
            {
                return res;
            }

            // Select to add element
            subAry.Add(nums[ele]);
            curSum += nums[ele];
            BackTrackCombiSum(nums, subAry, ele, curSum, target, res);

            // Backtrack
            subAry.RemoveAt(subAry.Count - 1);
            curSum -= nums[ele];

            BackTrackCombiSum(nums, subAry, ele + 1, curSum, target, res);

            return res;
        }

        public static void RunSubsets()
        {
            var input = new int[] { 1, 2, 3 };
            Subsets(input);

        }
        public static List<List<int>> Subsets(int[] nums)
        {
            var res = new List<List<int>>();
            BacktrackSubset(nums, 0, new List<int>(), res);
            return res;
        }

        private static void BacktrackSubset(int[] nums, int index, List<int> current, List<List<int>> res)
        {

            if (index > nums.Length - 1)
            {
                res.Add(new List<int>(current));
                return;
            }

            current.Add(nums[index]);
            BacktrackSubset(nums, index + 1, current, res);
            current.RemoveAt(current.Count - 1);
            BacktrackSubset(nums, index + 1, current, res);

        }

        public static void RunWordDictionary()
        {
            var myTrie = new WordDictionary();
            myTrie.AddWord("Day");
            myTrie.AddWord("Bay");
            myTrie.AddWord("May");
            myTrie.AddWord("startsWith");

            Console.WriteLine(myTrie.Search(".ay").ToString());
            Console.WriteLine(myTrie.Search("M..").ToString());
            Console.WriteLine(myTrie.Search("Mayi").ToString());

            Console.WriteLine(myTrie.Search("..With").ToString());
        }


        public static void RunImplementTrie()
        {
            var myTrie = new PrefixTree();
            myTrie.Insert("Dog");
            myTrie.Insert("Dig");
            myTrie.Insert("search");
            myTrie.Insert("startsWith");

            Console.WriteLine("Start With " + myTrie.Search("Digf"));
            Console.WriteLine("Start With " + myTrie.StartsWith("stars"));

        }


        public static void RunCarFleet()
        {

            int target = 10;
            int[] pos = new int[] { 4, 1, 0, 7 };
            int[] speed = new int[] { 2, 2, 1, 1 };

            var res = CarFleet(target, pos, speed);

        }

        public static int CarFleet(int target, int[] position, int[] speed)
        {
            int[][] pair = new int[position.Length][];

            var iterStack = new Stack<double>();

            for (int i = 0; i < position.Count(); i++)
            {
                pair[i] = new int[] { position[i], speed[i] };
            }

            Array.Sort(pair, (a, b) => b[0].CompareTo(a[0]));

            for (int i = 0; i < position.Count(); i++)
            {
                double time = (double)(target - pair[i][0]) / pair[i][1];

                if (iterStack.Count == 0 || time > iterStack.Peek())
                {
                    iterStack.Push(time);
                }
            }

            return iterStack.Count();
        }

        public static bool RunIsSameTree()
        {
            // Tree 1
            var root = new TreeNode(4);
            root.left = new TreeNode(7);

            // Tree 2
            var root2 = new TreeNode(4);
            root2.right = new TreeNode(7);

            var listRes1 = new List<int>();
            var listRes2 = new List<int>();

            DFS(root, listRes1);
            DFS(root2, listRes2);

            // Subproblem 2 , compare both list to make sure they are same. 
            var res = listRes1.SequenceEqual(listRes2);

            return res;
        }

        public static void DFS(TreeNode node, List<int> res)
        {
            // Bounding functions 
            if (node == null)
            {
                res.Add(-100);
                return;
            } else
            {
                res.Add(node.val);
            }

            DFS(node.left, res);
            DFS(node.right, res);
        }

        public static bool IsSameTree(TreeNode p, TreeNode q)
        {
            bool res = true;

            DfsTreeCheck(p, q, ref res);

            return res;
        }

        public static void DfsTreeCheck(TreeNode p, TreeNode q, ref bool res)
        {

            if (p == null && q != null)
            {
                res = false;
            }

            if (p != null && q == null)
            {
                res = false;
            }

            if (p == null || q == null || res == false)
            {
                return;
            }

            if (p.val != q.val)
            {
                res = false;
                return;
            }

            DfsTreeCheck(p.left, q.left, ref res);
            DfsTreeCheck(p.right, q.right, ref res);

            return;

        }

        public static void RunDailyTemperatures()
        {
            var input = new int[] { 89, 62, 70, 58, 47, 47, 46, 76, 100, 70 };

            var res = DailyTemperatures(input);

        }

        public static int[] DailyTemperatures(int[] temperatures)
        {

            // Can be improved by i starting from the temp.Len , so we dont need to reverse array.

            int[] res = new int[temperatures.Length];
            Stack<int[]> stack = new Stack<int[]>(); // pair: [temp, index]

            Array.Reverse(temperatures);
            stack.Push(new int[] { temperatures[0], 0 });
            res[0] = 0;

            for (int i = 1; i < temperatures.Count(); i++)
            {
                var currTemp = temperatures[i];

                while (stack.Count > 0)
                {
                    if (currTemp >= stack.Peek()[0])
                    {
                        stack.Pop();
                    }
                    else { break; }
                }

                if (stack.Count == 0)
                {
                    res[i] = 0;
                }
                else
                {
                    res[i] = i - stack.Peek()[1];
                }

                stack.Push(new int[] { currTemp, i });
            }

            Array.Reverse(res);

            return res;
        }

        public static void RunGenerateParenthesis()
        {
            var res = GenerateParenthesis(3);
        }

        public static List<string> GenerateParenthesis(int n)
        {
            var res = new List<string>();
            DecisionTreeParenthesis("(", 1, 0, n, res);

            return res;
        }


        public static void DecisionTreeParenthesis(string s, int open, int close, int n, List<string> res)
        {
            if (open < close)
            {
                return;
            }

            if ((open + close) > n * 2)
            {
                return;
            }

            if (open == close && open == n)
            {
                res.Add(s);
            }

            DecisionTreeParenthesis(s + "(", open + 1, close, n, res);
            DecisionTreeParenthesis(s + ")", open, close + 1, n, res);

            return;
        }

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


public class PrefixNode
{
    public Dictionary<char, PrefixNode> ChildrenNodes { get; set; }
    public bool IsWord { get; set; }
    public char c { get; set; }

    public PrefixNode()
    {
        IsWord = false;
        ChildrenNodes = new Dictionary<char, PrefixNode>();
    }
}

public class PrefixTree
{
    private PrefixNode root;

    public PrefixTree()
    {
        root = new PrefixNode();
    }

    public bool StartsWith(string prefix)
    {
        if (prefix.Length < 1)
        {
            return true;
        }

        PrefixNode curNode = root;

        for (int i = 0; i < prefix.Length; i++)
        {
            if (!curNode.ChildrenNodes.TryGetValue(prefix[i], out curNode))
            {
                return false;
            }
        }
        return true;
    }

    public bool Search(string word)
    {
        if (word.Length < 1)
        {
            return true;
        }

        PrefixNode curNode = root;

        for (int i = 0; i < word.Length; i++)
        {
            if (!curNode.ChildrenNodes.TryGetValue(word[i], out curNode))
            {
                return false;
            }
            if (i == word.Length - 1)
            {
                if (!curNode.IsWord)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Insert(string word)
    {
        if (word.Length < 1)
        {
            return;
        }

        PrefixNode curNode = root;
        PrefixNode nextNode;

        for (int i = 0; i < word.Length; i++)
        {
            // If the node is aready present 
            if (!curNode.ChildrenNodes.TryGetValue(word[i], out nextNode))
            {
                nextNode = new PrefixNode();
                nextNode.c = word[i];
                curNode.ChildrenNodes.Add(word[i], nextNode);
            }

            curNode = nextNode;

            if (i == word.Length - 1)
            {
                curNode.IsWord = true;
            }

        }

    }

}

public class WordDictNode
{
    public Dictionary<char, WordDictNode> childNode;

    public WordDictNode prevNode;
    public bool isWord;
    public char c;

    public WordDictNode()
    {
        childNode = new Dictionary<char, WordDictNode>();
        isWord = false;
    }

}


public class WordDictionary
{
    private WordDictNode root;
    public List<WordDictNode> edgeNodes;

    public WordDictionary()
    {
        root = new WordDictNode();
        edgeNodes = new List<WordDictNode>();
    }

    public void AddWord(string word)
    {
        var currNode = root;
        WordDictNode nextNode = new WordDictNode();
        WordDictNode prevNode = new WordDictNode();

        for (int i = 0; i < word.Length; i++)
        {
            if (!currNode.childNode.TryGetValue(word[i], out nextNode))
            {
                nextNode = new WordDictNode();
                nextNode.c = word[i];
                currNode.childNode.Add(word[i], nextNode);
            }

            nextNode.prevNode = currNode;
            prevNode = currNode;

            currNode = nextNode;

            if (i == word.Length - 1)
            {
                currNode.isWord = true;
                currNode.c = word[i];
                edgeNodes.Add(currNode);
            }
        }
    }

    public bool Search(string word)
    {
        var res = false;
        var edges = new List<WordDictNode>();

        if (word[0] == 46)
        {
            var lastC = word[word.Length - 1];
            foreach (var e in edgeNodes)
            {
                if (e.c == lastC)
                {
                    res = findWordFromEdge(e, word, 0);
                    if (res == true)
                    {
                        return res;
                    }

                }
            }
        }
        else
        {
            res = findWordFromRoot(root, word, 0);
            if (res == true)
            {
                return res;
            }
        }

        return res;
    }


    private bool findWordFromRoot(WordDictNode node, string word, int counter)
    {
        bool res = false; ;
        if (counter > word.Length - 1)
        {
            return true;
        }

        var c = word[counter];

        if (node.childNode.TryGetValue(c, out node))
        {
            if (node.c == c || c == 46)
            {
                res = findWordFromEdge(node, word, counter + 1);
            }
        }
        else
        {
            return false;
        }

        return res;
    }

    private bool findWordFromEdge(WordDictNode node, string word, int counter)
    {
        bool res = false;
        if (node.prevNode == root || counter > word.Length - 1)
        {
            return true;
        }

        var c = word[word.Length - counter - 1];

        if (node.c == c || c == 46)
        {
            res = findWordFromEdge(node.prevNode, word, counter + 1);
        }

        return res;
    }
}
#endregion
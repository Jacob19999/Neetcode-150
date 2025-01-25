﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeetCode75
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunIsPalindrome();



            Console.ReadLine();
        }

        #region InProgress



        #endregion


        #region Submitted
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

            var result = IsAnagram(s,t);
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

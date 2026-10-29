using System;
using System.Collections.Generic;
using System.Text;

namespace TransportSystemsQueue
{
    public class MyStack
    {
        private Queue<int> queue;

        public MyStack()
        {
            queue = new Queue<int>();
        }

        public void Push(int x)
        {
            if (x < 1 || x > 9)
                throw new ArgumentException("Елемент має бути в межах [1, 9].");

            queue.Enqueue(x);
            int size = queue.Count;

            for (int i = 0; i < size - 1; i++)
            {
                queue.Enqueue(queue.Dequeue());
            }
        }

        public int Pop()
        {
            return queue.Dequeue();
        }

        public int Top()
        {
            return queue.Peek();
        }

        public bool Empty()
        {
            return queue.Count == 0;
        }
    }

    public class RecentCounter
    {
        private Queue<int> queue;

        public RecentCounter()
        {
            queue = new Queue<int>();
        }

        public int Ping(int t)
        {
            if (t < 1 || t > 1_000_000_000)
                throw new ArgumentException("Час t має бути в межах [1, 10^9].");

            queue.Enqueue(t);

            while (queue.Peek() < t - 3000)
            {
                queue.Dequeue();
            }
            return queue.Count;
        }
    }

    public class MyCircularDeque
    {
        private int[] data;
        private int front;
        private int rear;
        private int count;
        private int capacity;

        public MyCircularDeque(int k)
        {
            if (k < 1 || k > 1000)
                throw new ArgumentException("Розмір черги k має бути в межах [1, 1000].");
            capacity = k;
            data = new int[k];
            front = 0;
            rear = 0;
            count = 0;
        }

        private void ValidateValue(int value)
        {
            if (value < 0 || value > 1000)
                throw new ArgumentException("Значення має бути в межах [0, 1000].");
        }

        public bool InsertFront(int value)
        {
            if (IsFull()) return false;
            ValidateValue(value);
            front = (front - 1 + capacity) % capacity;
            data[front] = value;
            count++;
            return true;
        }

        public bool InsertLast(int value)
        {
            if (IsFull()) return false;
            ValidateValue(value);
            data[rear] = value;
            rear = (rear + 1) % capacity;
            count++;
            return true;
        }

        public bool DeleteFront()
        {
            if (IsEmpty()) return false;
            front = (front + 1) % capacity;
            count--;
            return true;
        }

        public bool DeleteLast()
        {
            if (IsEmpty()) return false;
            rear = (rear - 1 + capacity) % capacity;
            count--;
            return true;
        }

        public int GetFront()
        {
            if (IsEmpty()) return -1;
            return data[front];
        }

        public int GetRear()
        {
            if (IsEmpty()) return -1;
            return data[(rear - 1 + capacity) % capacity];
        }

        public bool IsEmpty() => count == 0;
        public bool IsFull() => count == capacity;
    }

    public class MyCircularQueue
    {
        private int[] data;
        private int front;
        private int rear;
        private int count;
        private int capacity;

        public MyCircularQueue(int k)
        {
            if (k < 1 || k > 1000)
                throw new ArgumentException("Розмір черги k має бути в межах [1, 1000].");
            capacity = k;
            data = new int[k];
            front = 0;
            rear = 0;
            count = 0;
        }

        public bool EnQueue(int value)
        {
            if (IsFull()) return false;
            if (value < 0 || value > 1000)
                throw new ArgumentException("Значення має бути в межах [0, 1000].");

            data[rear] = value;
            rear = (rear + 1) % capacity;
            count++;
            return true;
        }

        public bool DeQueue()
        {
            if (IsEmpty()) return false;
            front = (front + 1) % capacity;
            count--;
            return true;
        }

        public int Front()
        {
            if (IsEmpty()) return -1;
            return data[front];
        }

        public int Rear()
        {
            if (IsEmpty()) return -1;
            return data[(rear - 1 + capacity) % capacity];
        }

        public bool IsEmpty() => count == 0;
        public bool IsFull() => count == capacity;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== 1 ===");
            {
                Console.WriteLine($"1: Input: s = \"leopard\" -> Output: {FirstUniqChar("leopard")}");
                Console.WriteLine($"2: Input: s = \"loveleopard\" -> Output: {FirstUniqChar("loveleopard")}");
                Console.WriteLine($"3: Input: s = \"aabb\" -> Output: {FirstUniqChar("aabb")}\n");
            }

            Console.WriteLine("=== 2 ===");
            {
                MyStack myStack = new MyStack();
                myStack.Push(1);
                myStack.Push(2);
                int top = myStack.Top();
                int pop = myStack.Pop();
                bool empty = myStack.Empty();
                Console.WriteLine($"1: Input [\"MyStack\", \"push\", \"push\", \"top\", \"pop\", \"empty\"] [[], [1], [2], [], [], []]");
                Console.WriteLine($"Output [null, null, null, {top}, {pop}, {empty.ToString().ToLower()}]\n");
            }

            Console.WriteLine("=== 3 ===");
            {
                RecentCounter rc = new RecentCounter();
                int p1 = rc.Ping(1);
                int p2 = rc.Ping(100);
                int p3 = rc.Ping(3001);
                int p4 = rc.Ping(3002);
                Console.WriteLine($"1: Input [\"RecentCounter\", \"ping\", \"ping\", \"ping\", \"ping\"] [[], [1], [100], [3001], [3002]]");
                Console.WriteLine($"Output [null, {p1}, {p2}, {p3}, {p4}]\n");
            }

            Console.WriteLine("=== 4 ===");
            {
                MyCircularDeque deque = new MyCircularDeque(3);
                bool i1 = deque.InsertLast(1);
                bool i2 = deque.InsertLast(2);
                bool i3 = deque.InsertFront(3);
                bool i4 = deque.InsertFront(4);
                int r1 = deque.GetRear();
                bool isf = deque.IsFull();
                bool dl = deque.DeleteLast();
                bool i5 = deque.InsertFront(4);
                int f1 = deque.GetFront();
                Console.WriteLine("1: Input [\"MyCircularDeque\", \"insertLast\", \"insertLast\", \"insertFront\", \"insertFront\", \"getRear\", \"isFull\", \"deleteLast\", \"insertFront\", \"getFront\"]");
                Console.WriteLine("[[3], [1], [2], [3], [4], [], [], [], [4], []]");
                Console.WriteLine($"Output [null, {i1.ToString().ToLower()}, {i2.ToString().ToLower()}, {i3.ToString().ToLower()}, {i4.ToString().ToLower()}, {r1}, {isf.ToString().ToLower()}, {dl.ToString().ToLower()}, {i5.ToString().ToLower()}, {f1}]\n");
            }

            Console.WriteLine("=== 5 ===");
            {
                MyCircularQueue q = new MyCircularQueue(3);
                bool e1 = q.EnQueue(1);
                bool e2 = q.EnQueue(2);
                bool e3 = q.EnQueue(3);
                bool e4 = q.EnQueue(4);
                int r1 = q.Rear();
                bool isf = q.IsFull();
                bool d1 = q.DeQueue();
                bool e5 = q.EnQueue(4);
                int r2 = q.Rear();
                Console.WriteLine("1: Input [\"MyCircularQueue\", \"enQueue\", \"enQueue\", \"enQueue\", \"enQueue\", \"Rear\", \"isFull\", \"deQueue\", \"enQueue\", \"Rear\"]");
                Console.WriteLine("[[3], [1], [2], [3], [4], [], [], [], [4], []]");
                Console.WriteLine($"Output [null, {e1.ToString().ToLower()}, {e2.ToString().ToLower()}, {e3.ToString().ToLower()}, {e4.ToString().ToLower()}, {r1}, {isf.ToString().ToLower()}, {d1.ToString().ToLower()}, {e5.ToString().ToLower()}, {r2}]\n");
            }

            Console.WriteLine("=== 6 ===");
            {
                Console.WriteLine($"1: Input: stamp = \"abc\", target = \"ababc\" -> Output: [{string.Join(",", MovesToStamp("abc", "ababc"))}]");
                Console.WriteLine($"2: Input: stamp = \"abca\", target = \"aabcaca\" -> Output: [{string.Join(",", MovesToStamp("abca", "aabcaca"))}]\n");
            }

            Console.WriteLine("=== 7 ===");
            {
                int[] nums1 = { 1, 3, -1, -3, 5, 3, 6, 7 };
                Console.WriteLine($"1: Input: nums = [1,3,-1,-3,5,3,6,7], k = 3 -> Output: [{string.Join(",", MaxSlidingWindow(nums1, 3))}]");
                int[] nums2 = { 1 };
                Console.WriteLine($"2: Input: nums = [1], k = 1 -> Output: [{string.Join(",", MaxSlidingWindow(nums2, 1))}]\n");
            }

            Console.WriteLine("=== 8 ===");
            {
                int[] nums1 = { 10, 2, -10, 5, 20 };
                Console.WriteLine($"1: Input: nums = [10,2,-10,5,20], k = 2 -> Output: {ConstrainedSubsetSum(nums1, 2)}");

                int[] nums2 = { -1, -2, -3 };
                Console.WriteLine($"2: Input: nums = [-1,-2,-3], k = 1 -> Output: {ConstrainedSubsetSum(nums2, 1)}");

                int[] nums3 = { 10, -2, -10, -5, 20 };
                Console.WriteLine($"3: Input: nums = [10,-2,-10,-5,20], k = 2 -> Output: {ConstrainedSubsetSum(nums3, 2)}");
            }
        }

        public static int FirstUniqChar(string s)
        {
            if (s.Length < 1 || s.Length > 100_000)
                throw new ArgumentException("Довжина рядка має бути в межах [1, 100000].");

            int[] counts = new int[26];
            foreach (char c in s)
            {
                if (c < 'a' || c > 'z') throw new ArgumentException("Дозволені лише малі літери англійського алфавіту.");
                counts[c - 'a']++;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (counts[s[i] - 'a'] == 1)
                    return i;
            }
            return -1;
        }

        public static int[] MovesToStamp(string stamp, string target)
        {
            if (stamp.Length < 1 || target.Length > 1000 || stamp.Length > target.Length)
                throw new ArgumentException("Порушено обмеження довжини рядків.");

            char[] t = target.ToCharArray();
            char[] s = stamp.ToCharArray();
            List<int> res = new List<int>();
            bool[] visited = new bool[t.Length];
            int stars = 0;

            while (stars < t.Length)
            {
                bool replaced = false;
                for (int i = 0; i <= t.Length - s.Length; i++)
                {
                    if (!visited[i] && CanReplace(t, i, s))
                    {
                        stars = Replace(t, i, s.Length, stars);
                        replaced = true;
                        visited[i] = true;
                        res.Add(i);
                        if (stars == t.Length) break;
                    }
                }
                if (!replaced) return Array.Empty<int>();
            }

            res.Reverse();
            return res.ToArray();
        }

        private static bool CanReplace(char[] t, int p, char[] s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (t[p + i] != '?' && t[p + i] != s[i])
                    return false;
            }
            return true;
        }

        private static int Replace(char[] t, int p, int len, int stars)
        {
            for (int i = 0; i < len; i++)
            {
                if (t[p + i] != '?')
                {
                    t[p + i] = '?';
                    stars++;
                }
            }
            return stars;
        }

        public static int[] MaxSlidingWindow(int[] nums, int k)
        {
            if (nums.Length < 1 || nums.Length > 100_000 || k < 1 || k > nums.Length)
                throw new ArgumentException("Порушено обмеження масиву або вікна k.");

            int[] result = new int[nums.Length - k + 1];
            LinkedList<int> deque = new LinkedList<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < -10000 || nums[i] > 10000)
                    throw new ArgumentException("Значення елемента виходить за межі [-10000, 10000].");

                if (deque.Count > 0 && deque.First.Value < i - k + 1)
                {
                    deque.RemoveFirst();
                }

                while (deque.Count > 0 && nums[deque.Last.Value] < nums[i])
                {
                    deque.RemoveLast();
                }

                deque.AddLast(i);

                if (i >= k - 1)
                {
                    result[i - k + 1] = nums[deque.First.Value];
                }
            }
            return result;
        }

        public static int ConstrainedSubsetSum(int[] nums, int k)
        {
            if (nums.Length < 1 || nums.Length > 100_000 || k < 1 || k > nums.Length)
                throw new ArgumentException("Порушено обмеження масиву або відстані k.");

            int[] dp = new int[nums.Length];
            LinkedList<int> deque = new LinkedList<int>();
            int maxSum = int.MinValue;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < -10000 || nums[i] > 10000)
                    throw new ArgumentException("Значення елемента виходить за межі [-10000, 10000].");

                if (deque.Count > 0 && deque.First.Value < i - k)
                {
                    deque.RemoveFirst();
                }

                dp[i] = nums[i];
                if (deque.Count > 0 && dp[deque.First.Value] > 0)
                {
                    dp[i] += dp[deque.First.Value];
                }

                maxSum = Math.Max(maxSum, dp[i]);

                while (deque.Count > 0 && dp[deque.Last.Value] <= dp[i])
                {
                    deque.RemoveLast();
                }
                deque.AddLast(i);
            }

            return maxSum;
        }
    }
}
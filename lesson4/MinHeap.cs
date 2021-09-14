using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson4
{
    public class MinHeap<T>
    {

        private Vertice<T>[] minHeap = new Vertice<T>[100];
        private int size = 0;//
        private int maxSize = 100;//

        public int GetSize()
        {
            return size;
        }

        //מחזירה את האינדקס של האבא  
        private int Parent(int i)
        {
            if (i == 0)
                return 0;
            if (i % 2 == 0)
                return i / 2 - 1;
            return i / 2;
        }

        //מחזירה את האינדקס של הילד השמאלי 
        private int LeftChild(int i)
        {
            if (i == 0)
                return 1;
            return i * 2 + 1;
        }

        //מחזירה את האינדקס של הילד הימני
        private int RightChild(int i)
        {
            if (i == 0)
                return 2;
            return i * 2 + 2;
        }

        //מוסיפה מספר לעץ
        public void Insert(Vertice<T> num)
        {
            if (size == maxSize)
            {
                Vertice<T>[] temp = new Vertice<T>[maxSize * 2];
                for (int i = 0; i < minHeap.Length; i++)
                {
                    temp[i] = minHeap[i];
                }
                minHeap = temp;
            }
            size++;
            minHeap[size] = num;
            SiftUp(size);
        }

        //מוחקת את השורש - האיבר המקסימלי
        public Vertice<T> ExtractMin()
        {
            Vertice<T> result = minHeap[0];
            minHeap[0] = minHeap[size - 1];
            minHeap[size] = null;
            size--;
            SiftDown(0);
            return result;
        }

        //מוחקת איבר במיקום מסוים - i
        public void Remove(int i)
        {
            minHeap[i] = null;
            SiftUp(i);
            ExtractMin();
        }
        public void ChangePriority()
        {
            BuildHeap(this.minHeap, this.size);
        }

        //מחליפה איבר מסוים בערך חדש 
        public void ChangePriority(int i, Vertice<T> newNum)
        {
            Vertice<T> oldnum = minHeap[i];
            minHeap[i] = newNum;
            if (newNum.Dist < oldnum.Dist)
                SiftUp(i);
            else
                SiftDown(i);
        }

        //האם האיבר שנמצא באינדקס מסוים הוא עלה 
        public bool IsLeaf(int i)
        {
            if (i < size / 2)
                return false;
            return true;
        }

        //בודקת האם העץ שלם
        public bool IsComplete()
        {
            int temp_size = size;
            int count = 0;
            while (temp_size > 0)//ספירת הקומות בעץ
            {
                temp_size /= 2;
                count++;
            }
            //אם המקום האחרון בעץ הוא מחזקות 2 - סימן שהעץ הינו שלם
            //ההורדה של 2: 1 בגלל שהמקום האחרון בקומה הינו תמיד בחזקת 2 פחות 1  
            //                   c# - ו1 בגלל שסופרים החל מהמקום ה0 
            if (minHeap[(int)Math.Pow(2, count) - 2].Dist != 0)
                return true;
            return false;
        }

        //מחזירה גובה של העץ
        public int High(int i)
        {
            if (i > size)
                return 0;
            return Math.Max(High(LeftChild(i)), High(RightChild(i))) + 1;
        }
        //בודקת האם העץ מאוזן או לא
        public bool IsBalance(int i)
        {
            if (minHeap == null)
                return true;
            int hl = High(i);
            int hr = High(i);
            if (Math.Abs(hl - hr) <= 1)
                return IsBalance(LeftChild(i)) && IsBalance(RightChild(i));
            return false;
        }

        //מסדרת את המערך מהקטן לגדול
        public void HeapSort(Vertice<T>[] arr, int n)
        {
            BuildHeap(arr, n);
            Vertice<T> temp;
            for (int i = 0; i < n; i++)
            {
                temp = arr[0];
                arr[0] = arr[size];
                arr[size] = temp;
                size--;
                SiftDown(0);
            }
            foreach (var i in arr)//מדפיסה את המערך המסודר
            {
                Console.Write(i + " ");
            }
        }

        //סידור המערך מהגדול לקטן
        public void HeapSortUp(Vertice<T>[] arr, int n)
        {
            BuildHeap(arr, n);
            arr = new Vertice<T>[n + 1];
            for (int i = 0; i < n; i++)
            {
                arr[i] = ExtractMin();
            }
            arr[n] = minHeap[0];
            foreach (Vertice<T> i in arr)//מדפיסה את המערך המסודר
            {
                Console.Write(i + " ");
            }
        }

        //מקבלת מערך שהוא לא ערימה מקסמילת ומסדרת אותו לערימה
        public void BuildHeap(Vertice<T>[] arr, int n)
        {
            minHeap = new Vertice<T>[100];
            for (int i = 0; i < n; i++)
            {
                minHeap[i] = arr[i];
            }
            size = n;
            for (int i = Parent(n); i >= 0; i--)
            {
                SiftDown(i);
            }
        }

        //מגלגלת איבר כלפי מטה
        private void SiftDown(int i)//O(logn)
        {
            Vertice<T> temp;
            int maxIndex = 0;
            while (i < size && ((LeftChild(i) < size && minHeap[i].Dist > minHeap[LeftChild(i)].Dist) || (RightChild(i) < size && minHeap[i].Dist > minHeap[RightChild(i)].Dist)))
            {
                if (LeftChild(i) < size && minHeap[i].Dist > minHeap[LeftChild(i)].Dist)
                    maxIndex = LeftChild(i);
                if (RightChild(i) < size && minHeap[RightChild(i)].Dist < minHeap[maxIndex].Dist)
                    maxIndex = RightChild(i);
                temp = minHeap[i];
                minHeap[i] = minHeap[maxIndex];
                minHeap[maxIndex] = temp;
                i = maxIndex;
            }
        }
        public void Swap(int i, int j, int[] vec)
        {
            int x = vec[i];
            vec[i] = vec[j];
            vec[j] = x;
        }

        //מגלגלת איבר כלפי מעלה
        private void SiftUp(int i)
        {
            while (Parent(i) >= 0 && (minHeap[i] == null || minHeap[Parent(i)].Dist > minHeap[i].Dist))
            {
                Vertice<T> temp = minHeap[Parent(i)];
                minHeap[Parent(i)] = minHeap[i];
                minHeap[i] = temp;
                i = Parent(i);
            }
        }

        //מדפיסה את האיברים במערך
        public void Print()
        {
            for (int i = 0; i <= size; i++)
            {
                Console.Write(minHeap[i] + " ");
            }
            Console.WriteLine();
        }

        //מחזירה האם הערימה ריקה או לא
        public bool IsEmpty()
        {
            return this.size == 0;
        }
    }
}

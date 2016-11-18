using System;

namespace Assets.Scripts.Tools
{
    public class Heap<T> where T : IHeapItem<T>
    {
        private readonly T[] _items;

        public Heap(int maxHeapSize)
        {
            _items = new T[maxHeapSize];
        }

        public void Add(T item)
        {
            item.HeapIndex = Count;
            _items[Count] = item;
            SortUp(item);
            Count++;
        }

        private void SortUp(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;

            while (true)
            {
                T parentItem = _items[parentIndex];
                if (item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                {
                    break;
                }
                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }

        public T RemoveFirst()
        {
            T firstItem = _items[0];
            Count--;
            _items[0] = _items[Count];
            _items[0].HeapIndex = 0;
            SortDown(_items[0]);
            return firstItem;
        }

        public int Count { get; private set; }

        //Pour le pathfindinf pas besoin de caller SortDown ici, mais dans un autre cas faudrait
        public void UpdateItem(T item)
        {
            SortUp(item);
        }

        public bool Contains(T item)
        {
            return Equals(_items[item.HeapIndex], item);
        }

        /// <summary>
        /// Cette méthode ne supprime pas les gameObjects en jeu, mais les 
        /// libères en mémoire logique. Il faut donc supprimer du tableau,
        /// puis faire un Delete(_monGameObject) pour le supprime complètement.
        /// </summary>
        public void Clear()
        {
            Array.Clear(_items, 0, _items.Length);
            Count = 0;
        }

        private void SortDown(T item)
        {
            while (true)
            {
                int childIndexLeft = item.HeapIndex * 2 + 1;
                int childIndexRight = item.HeapIndex * 2 + 2;
                int swapIndex = 0;

                if (childIndexLeft < Count)
                {
                    swapIndex = childIndexLeft;
                    if (childIndexRight < Count)
                    {
                        if (_items[childIndexLeft].CompareTo(_items[childIndexRight]) < 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }

                    if (item.CompareTo(_items[swapIndex]) < 0)
                    {
                        Swap(item, _items[swapIndex]);
                    }
                    else
                    {
                        return;
                    }
                }

                else
                {
                    return;

                }

            }

        }

        private void Swap(T itemA, T itemB)
        {
            _items[itemA.HeapIndex] = itemB;
            _items[itemB.HeapIndex] = itemA;
            int itemAIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }
    }
}
//public interface IHeapitem<T>: IComparable<T>
//    {
//        int HeapIndex
//        {
//            get; set;
//        }
//    }

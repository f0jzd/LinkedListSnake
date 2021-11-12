public class LinkList<T>
{
    public ListNode<T> head;
    public ListNode<T> LLtail;
    public int count;
    private T[] _items;


    public LinkList()
    {
        //_items = new T[capacity];
        LLtail = null;
        head = null;
        count = 0;
    }

    public class ListNode<T>
    {
        public T data;
        public ListNode<T> nextNode;
    }

    public int Count => count;

    public void Add(T item)
    {
        var newNode = new ListNode<T>();
        if (head == null && LLtail == null)
        {
            newNode.data = item;
            head = newNode;
            LLtail = newNode;
            count++;
        }
        else
        {
            newNode.data = item;
            LLtail.nextNode = newNode;
            LLtail = newNode;
            count++;
        }
    }

    public void Insert(int index, T item)
    {
        int indexChecker = 0;
        var temphead = head;
        var newNode = new ListNode<T>();
        newNode.data = item;

        while (temphead != null)
        {
            if (index == 0)
            {
                count++;
                newNode.nextNode = head;
                head = newNode;

                if (newNode.nextNode == null)
                {
                    LLtail = newNode;
                }

                return;
            }

            indexChecker++;
            temphead = temphead.nextNode;

            if (index == indexChecker)
            {
                count++;
                newNode.nextNode = temphead.nextNode;
                temphead.nextNode = newNode;

                if (newNode.nextNode == null)
                {
                    LLtail = newNode;
                }

                return;
            }

            indexChecker++;
            temphead = temphead.nextNode;
        }
    }

    public void RemoveAt(int index)
    {
        int iterations = 0;
        var temphead = head;
        while (temphead != null)
        {
            if (index == 0)
            {
                head = head.nextNode;
                count--;
                return;
            }

            iterations++;
            if (iterations == index)
            {
                if (temphead.nextNode.nextNode == null)
                {
                    LLtail = temphead;
                    temphead.nextNode = null;
                    count--;
                    return;
                }
                else
                {
                    temphead.nextNode = temphead.nextNode.nextNode;
                }
            }

            temphead = temphead.nextNode;
        }
    }
}

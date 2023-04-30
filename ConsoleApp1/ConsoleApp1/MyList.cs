public class MyList
{
    public int capacity = 4;
    public int count;
    private int[] datas = new int[4];
    public int this[int idx]
    {
        get { return datas[idx]; } 
        set { datas[idx] = value;}
    }

    public void Add(int idx)
    {
        datas[count] = idx;
        if (++count >= capacity)
        {
            capacity = capacity * 2;

            int[] temp = new int[capacity];
            for(int i = 0; i < capacity /2;i++)
                temp[i] = datas[i];
            datas = temp;
        }
    }

    public void Clear()
    {
        datas = new int[capacity];
        count = 0;
    }

    public void RemoveAt(int idx)
    {
        for(int i = idx;i < count;i++)
            datas[i] = datas[i + 1];

        count--; 
    }

    public void PrintData()
    {
        if(count == 0)
        {
            Console.WriteLine("비어있습니다.");
        }
        for(int i = 0; i < count;i++)
        {
            Console.WriteLine(datas[i]);
        }
    }
}


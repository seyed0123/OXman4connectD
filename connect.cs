using System;
public class Connect
{
    private class Point
    {
        public int x;
        public int y;
        public Point(int x , int y)
        {
            this.x=x;
            this.y=y;
        }public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }
            Point other = (Point)obj;
            return x == other.x && y == other.y;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }
    }
    private int [,] matrix;
    HashSet<Point> person1 , person2;
    private int h , w ;
    private int maxNumber = 4 ;
    private string[] colors;
    string[] colorCodes = new string[]
    {
        "\u001b[31m", // Red
        "\u001b[32m", // Green
        "\u001b[34m", // Blue
        "\u001b[33m", // Yellow
        "\u001b[35m", // Purple
        "\u001b[38;5;208m", // Orange
        "\u001b[38;5;218m", // Pink
        "\u001b[38;5;246m", // Gray
        "\u001b[38;5;130m", // Brown
        "\u001b[36m", // Cyan
    };
    public Connect(int x,int y)
    {
        matrix = new int[x+2,y+2];
        h=y;
        w=x;
        person1 = new HashSet<Point>();
        person2 = new HashSet<Point>();
        colors = new string [3];
    }
    public void play()
    {
        string[] colorNames = new string[]
        {
            "Red",
            "Green",
            "Blue",
            "Yellow",
            "Purple",
            "Orange",
            "Pink",
            "Gray",
            "Brown",
            "Cyan",
        };
        string[] names = new string [3];
        Console.WriteLine("enter your names :\nperson1 :");
        names[0] = Console.ReadLine() ?? "person 1";
        Console.WriteLine("person 2 :");
        names[1] = Console.ReadLine() ?? "person 2";
        for (int i = 0; i < colorCodes.Length; i++)
        {
            Console.Write(i+"- "+colorCodes[i] + colorNames[i]+" ");
            Console.Write("\u001b[0m"); // Reset color to default
        }
        Console.WriteLine("");
        Console.WriteLine("chosse a number of color for your team. first {0}",names[0]);
        try
        {
            colors[0] = colorCodes[int.Parse(Console.ReadLine() ?? "0")];
        }catch{
            colors[0]=colorCodes[0];
        }
        Console.WriteLine("and {0} chosse a color different of {1}color\u001b[0m.",names[1],colors[0]);
        string def = "1";
        if(colors[0]==colorCodes[1])
            def = "0";
        try{
            colors[1] = colorCodes[int.Parse(Console.ReadLine() ?? def)];
        }catch
        {
            colors[1] = colorCodes[int.Parse(def)];
        }
        if(colors[1]==colors[0])
        {
            if(colorCodes[0]!=colors[0])
                colors[1]=colorCodes[0];
            else
                colors[1]=colorCodes[1];
        }
        Console.Clear();
        int round = 0;
        print();
        while(true)
        {
            Console.WriteLine("\u001b[33mnow it's your turn {1}{0}\u001b[33m , chosse a column:\u001b[0m",names[round],colors[round]);
            int column = 0;
            try{
                column = int.Parse(Console.ReadLine() ?? "-2");
                Console.Clear();
            if(column < 1 || column > w+1)
                throw new Exception();
            column--;
            }catch
            {
                Console.WriteLine("enter a valid number");
                print();
                continue;
            }
            printAnim(round+1,column,calDes(column));
            int checkNum = check();
            if(checkNum==1)
            {
                Console.WriteLine("\u001b[33m"+names[0]+" I implore you to accept my congratulations on rubbing the nose of your treacherous and powerful opponent.\n\u001b[35mYou have won this fascinating competition.\n");
                break;
            }else if(checkNum==2)
            {
                Console.WriteLine("\u001b[33m"+names[1]+" I offer my heartfelt congratulations on your stunning victory over your formidable and deceitful adversary.\n\u001b[35mYou have emerged triumphant in this captivating contest.\n");
                break;
            }else if(checkNum==3)
            {
                Console.WriteLine("I regret to announce that this breathtaking and alluring competition has not seen anyone worthy of winning.");
                break;
            }
            round = ((round+1)%2);
        }
    }
    private int calDes(int column)
    {
        if(matrix[w,column]==0)
            return h;
        for(int i = 0 ; i <= h; i++)
        {
            if(matrix[i,column]!=0)
            {
                return i-1;
            }
        }
        return -1;
    }
    private void printAnim(int person , int column , int des)
    {
        if(des==-1)
        {
            print();
        }
        for(int i = 0 ; i <= des ; i++)
        {
            Console.Clear();
            matrix[i,column]=person;
            print();
            if(i != des)
                matrix[i,column]=0;
            Thread.Sleep(150);
        }
        if(person==1)
            person1.Add(new Point(des,column));
        else
            person2.Add(new Point(des,column));
    }
    private void print()
    {
        for(int i = 0 ; i <= w ; i++)
        {
            for(int j = 0 ; j <= h ;j++)
            {
                if(matrix[i,j]==0)
                    Console.Write("\u001b[38;5;75m◦  \u001b[0m");
                else if(matrix[i,j]==1)
                    Console.Write("{0}●  \u001b[0m",colors[0]);
                else
                    Console.Write("{0}●  \u001b[0m",colors[1]);
            }
            Console.WriteLine("");
        }
        for(int i = 0 ; i <= h ; i++)
        {
            Console.Write((i+1).ToString().PadRight(3));
        }
        Console.WriteLine("");
    }
    private int check()
    {
        if(person1.Count+person2.Count==(w+1)*(w+1))
            return 3;
        bool [,] visited1 = new bool [w+2,h+2];
        bool [,] visited2 = new bool [w+2,h+2];
        foreach(Point p in person1)
        {
            if(true)
                if(DFS(visited1 , p.x , p.y , 1))
                {
                    return 1;
                }
        }
        foreach(Point p in person2)
        {
            if(true)
                if(DFS(visited2 , p.x , p.y , 2))
                {
                    return 2;
                }
        }
        return 0;
    }
    private bool DFS(bool[,] visited, int x, int y , int person)
    {
        visited[x, y] = true; // mark cell as visited
        // explore neighboring cells
        if (x > 0 && y > 0 &&  matrix[x-1, y-1] == person) {
            if(DfsLine(x-1,y-1 , new Point(-1,-1) , 1 ,person))
                return true;
            if(!visited[x-1, y-1])
                DFS(  visited, x-1, y-1 , person); // explore upper-left neighbor
        }
        if (x > 0 &&  matrix[x-1, y] == person ) {
            if(DfsLine(x-1,y , new Point(-1,0) , 1 ,person))
                return true;
            if( !visited[x-1, y])
                DFS(  visited, x-1, y , person); // explore upper neighbor
        }
        if (x > 0 && y < h &&  matrix[x-1, y+1] == person ) {
            if(DfsLine(x-1,y+1 , new Point(-1,1) , 1 ,person))
                return true;
            if(!visited[x-1, y+1])
                DFS(  visited, x-1, y+1 , person); // explore upper-right neighbor
        }
        if (y > 0 &&  matrix[x, y-1] == person ) {
            if(DfsLine(x,y-1 , new Point(0,-1) , 1 ,person))
                return true;
            if(!visited[x, y-1])
                DFS(  visited, x, y-1 , person); // explore left neighbor
        }
        if (y < h &&  matrix[x, y+1] == person ) {
            if(DfsLine(x,y+1 , new Point(0,1) , 1 ,person))
                return true;
            if(!visited[x, y+1])
                DFS(  visited, x, y+1 , person); // explore right neighbor
        }
        if (x < w && y > 0 &&  matrix[x+1, y-1] == person) {
            if(DfsLine(x+1,y-1 , new Point(+1,-1) , 1 ,person))
                return true;
            if( !visited[x+1, y-1])
                DFS(  visited, x+1, y-1 , person); // explore lower-left neighbor
        }
        if (x < w &&  matrix[x+1, y] == person ) {
            if(DfsLine(x+1,y , new Point(+1,0) , 1 ,person))
                return true;
            if( !visited[x+1, y])
                DFS(  visited, x+1, y , person); // explore lower neighbor
        }
        if (x < w  && y < h &&  matrix[x+1, y+1] == 1 ) {
            if(DfsLine(x+1,y+1 , new Point(+1,+1) , 1 ,person))
                return true;
            if( !visited[x+1, y+1])
                DFS(  visited, x+1, y+1 , person); // explore lower-right neighbor
        }
        return false;
    }
    private bool DfsLine(int x, int y, Point dir , int number , int person)
    {
        if(number+1 == maxNumber)
            return true;
        int x1 = x+dir.x , y1 = y+dir.y;
        if(x1>-1 && x1 <= w && y1>-1 && y1 <= h)
            if(matrix[x1,y1] == person)
                return DfsLine( x1 , y1 , dir , number+1 , person);
        return false;
    }
}
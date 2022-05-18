
using MersenneTwister;
using System;
using System.Collections.Generic;
using System.Linq;

//GeneratorMatricsWorld newGen = new GeneratorMatricsWorld(5,5);
//GeneratorMatricsWorld newGen1 = new GeneratorMatricsWorld(123);
//Console.Write(newGen.resMT());

//newGen.ResultGeneration();
public class GeneratorMatricsWorld
{
    private int[,] gen ;
    private int ResMT;
    private int rows;
    private int columns ;
    private static int square;

    public GeneratorMatricsWorld(uint seed, int squareMap)
    {
        /*
        0 - void
        1 - defolt room
        2 - Boss room
        3..7 - key room
        8 - enemy-binding room
        */
        square = squareMap;
        gen = new int[square, square];
        rows = gen.GetUpperBound(0) + 1;
        columns = gen.Length / rows;
        MTRandom MT = new MTRandom(seed);
        ResMT = Math.Abs(MT.genrand_int32SignedInt());

        int[] BossRoom = GenerateBossRooms();
        int[] StartRoom = GenerateStartRoom();
        List<int[]> ListKeyRooms = GenerateKeysRooms();
        
        pathFindingAlgorithm(StartRoom, BossRoom, 2);
        
        pathFindingAlgorithm(StartRoom, ListKeyRooms[0], 3);
        pathFindingAlgorithm(StartRoom, ListKeyRooms[1], 4);
        pathFindingAlgorithm(StartRoom, ListKeyRooms[2], 5);
        pathFindingAlgorithm(StartRoom, ListKeyRooms[3], 6);
    }

    public int[] GenerateBossRooms()
    {
        int[] Last2numbs = Next2numbs(1);
        //num 0-9
        
        
        int RowBoss = Last2numbs[0];
        int ColBoss = Last2numbs[1];

        if (ColBoss >= rows)
            ColBoss %= square;

        RowBoss = 0;

        gen[RowBoss, ColBoss] = 2;
        return new int[] { RowBoss, ColBoss };
    }
    public int[] GenerateStartRoom()
    {
        int StartRoomRow_1 = square-1;
        int StartRoomCol_1 = Next2numbs(3)[1];
        
        return CreateCords(StartRoomRow_1, StartRoomCol_1, gen, 1);
    }
    public List<int[]> GenerateKeysRooms()
    {
        
        
        int KRoomRow_1 = Next2numbs(3)[0];
        int KRoomCol_1 = Next2numbs(3)[1];

        int KRoomRow_2 = Next2numbs(4)[0];
        int KRoomCol_2 = Next2numbs(4)[1];

        int KRoomRow_3 = Next2numbs(5)[0];
        int KRoomCol_3 = Next2numbs(5)[1];

        int KRoomRow_4 = Next2numbs(6)[0];
        int KRoomCol_4 = Next2numbs(6)[1];

        int[] room1 = CreateCords(KRoomRow_1, KRoomCol_1, gen, 3);
        int[] room2 = CreateCords(KRoomRow_2, KRoomCol_2, gen, 4);
        int[] room3 = CreateCords(KRoomRow_3, KRoomCol_3, gen, 5);
        int[] room4 = CreateCords(KRoomRow_4, KRoomCol_4, gen, 6);
        
        return new List<int[]> {room1, room2, room3, room4 };
        
        //pathFindingAlgorithm(room1, room2, 4);
        
    }
    
    public void ResultGeneration()
    {
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write($"{gen[i, j]} \t");
            }
            Console.WriteLine();
        }
    }
    
    public int[] Next2numbs(int numFromEnd)
    {
        int K = 1;

        int count2NumMT = 1;
        int res = ResMT;
        for (int i = 0;res > 1 ;i++)
        {
            res /= 10;
            count2NumMT = i/2;
        }
        count2NumMT+=2;
        numFromEnd %= count2NumMT;
        
        for (int i = 1; i < numFromEnd; i++)
        {
            K *= 100;
        }

        int Next2numbs = ResMT / K % 100;

        

        return new int[2] {Next2numbs / 10 % 10, Next2numbs % 10};
    }
    public int[] CreateCords(int row, int column, int[,] genArr, int key)
    {
        
        if (row >= rows)
            row %= square;
        if (column >= columns)
            column %= square;

        while (genArr[row, column] != 0)
        {
            column++;
            if (column >= columns)
                column %= square;
        }
            
        
        genArr[row, column] = key;
        int[] trueCords = { row, column };

        return trueCords;
    }

    public int resMT()
    {
        
        return ResMT;
    }

    class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;

    }
    static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Math.Abs(targetX - x) + Math.Abs(targetY - y);
    }

    static List<Location> GetWalkableAdjacentSquares(int x, int y,int endPos, int[,] map)
    {
        List<Location> ListResult = new List<Location>();

        int CordDown = y - 1;
        int CordUp = y + 1;
        int CordLeft = x - 1;
        int CordRight = x + 1;

        var proposedLocations = new List<Location>()
        {
            new Location { X = x, Y = CordDown },
            new Location { X = x, Y = CordUp },
            new Location { X = CordLeft, Y = y },
            new Location { X = CordRight, Y = y },
        };
        proposedLocations = proposedLocations.Where(p => ( p.X < square && p.X >= 0) && (p.Y < square && p.Y >= 0 )).ToList();
        for (int i=0; i<proposedLocations.Count; i++)
        {
            int n = map[proposedLocations[i].Y, proposedLocations[i].X];
            if (n >= 0 )
                ListResult.Add(proposedLocations[i]);

        }

        return ListResult;
        /*return proposedLocations.Where(
            l => map[l.Y,l.X] == 0 || map[l.Y,l.X] == endPos).ToList();*/
    }

    private void pathFindingAlgorithm(int[] CordRoomStart,int[] CordRoomEnd,int endPos)
    {
        Location current = null;
        var start = new Location { X = CordRoomStart[1], Y = CordRoomStart[0] };
        var target = new Location { X = CordRoomEnd[1], Y = CordRoomEnd[0] };
        var openList = new List<Location>();
        var closedList = new List<Location>();
        int g = 0;

        // start by adding the original position to the open list
        openList.Add(start);
        while (openList.Count > 0)
        {
            // algorithm's logic goes here

            // get the square with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);
            if (gen[current.Y, current.X] == 0)
            gen[current.Y, current.X] = 8;

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, endPos, gen);
            g++;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X,
                        adjacentSquare.Y, target.X, target.Y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentSquare.H < adjacentSquare.F)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;
                    }
                }
            }
        }
    }
}


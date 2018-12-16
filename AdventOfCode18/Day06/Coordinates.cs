using System;
using System.Drawing;

namespace Day06
{
    class Coordinates
    {
        static void Main(string[] args)
        {
            String[] locations = System.IO.File.ReadAllLines("input.txt");
            Coord[] coordinates = new Coord[locations.Length];
            short maxX = 0, maxY = 0;
            short tempX, tempY;
            //create the coordinates and find the dimensions needed for a map
            for (short i = 0; i < locations.Length; i++)
            {
                tempX = short.Parse(locations[i].Split(',')[0]);
                tempY = short.Parse(locations[i].Split(' ')[1]);
                coordinates[i] = new Coord { X = tempX, Y = tempY, Id = (short)(i), area = 0 };
                maxX = Math.Max(maxX, tempX);
                maxY = Math.Max(maxY, tempY);
            }
            //create a map
            maxX++;
            maxY++;
            short[,] map = new short[maxX,maxY];
            //mark closest coordinate for each point on the map
            short closestPt;
            for (short x = 0; x < maxX; x++)
            {
                for (short y = 0; y < maxY; y++)
                {
                    closestPt = Closest(ref coordinates, x, y);
                    map[x, y] = closestPt;
                    if (closestPt >= 0)
                    {
                        coordinates[closestPt].area++;
                      
                    }
                }
            }
            //identify edge coordinates
            short edgeVal;
            for (short x = 0; x < maxX; x++)
            {
                edgeVal = map[x, 0];
                if (edgeVal >= 0)
                {
                    coordinates[edgeVal].isOnEdge = true;
                }
                edgeVal = map[x, maxY-1];
                if (edgeVal >= 0)
                {
                    coordinates[edgeVal].isOnEdge = true;
                }
            }
            for (short y = 1; y < maxY-1; y++)
            {
                edgeVal = map[0,y];
                if (edgeVal >= 0)
                {
                    coordinates[edgeVal].isOnEdge = true;
                }
                edgeVal = map[maxX-1, y];
                if (edgeVal >= 0)
                {
                    coordinates[edgeVal].isOnEdge = true;
                }
            }
            //find largest area for acceptable coordinates (not on edge)
            int largestArea = 0;
            foreach (Coord c in coordinates)
            {
                if (!c.isOnEdge)
                {
                    largestArea = Math.Max(c.area, largestArea);
                }
            }
            Console.WriteLine("First answer: " + largestArea); 






            //TODO: second part





            Console.WriteLine("Second answer: " + 0); 
            Console.Read();
        }

        /// <summary>
        /// Returns closest coordinate for a given point on the map
        /// </summary>
        /// <param name="coords">array of coordinates to consider</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>ID of the closest coordinate or -1 if there are multiple</returns>
        public static short Closest(ref Coord[] coords, short x, short y)
        {
            short minDist=short.MaxValue, closestPtId=-1,dist;
            foreach (Coord pt in coords)
            {
                dist = (short)(Math.Abs(pt.X - x) + Math.Abs(pt.Y - y));
                if (dist == 0)
                {
                    return pt.Id;
                }
                else if (dist < minDist)
                {
                    minDist = dist;
                    closestPtId = pt.Id;
                }
                else if (dist == minDist)
                {
                    closestPtId = -1;
                }
            }
            return closestPtId;
        }

        public struct Coord
        {
            public short X; 
            public short Y;
            public short Id;
            public int area;
            public bool isOnEdge;
        }
    }
}

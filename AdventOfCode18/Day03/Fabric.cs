using System;
using System.Text.RegularExpressions;
namespace Day03
{
    class Fabric
    {
        static void Main(string[] args)
        {
            int maxLength = 1000;
            Regex claimRegex = new Regex(@"#(\d*) @ (\d*),(\d*): (\d*)x(\d*)");
            MatchCollection parsedClaims = claimRegex.Matches(System.IO.File.ReadAllText("input.txt"));
            int x, y, offsetX, offsetY;
            bool[,] fabricClaims = new bool[maxLength, maxLength];
            bool[,] multipleClaims = new bool[maxLength, maxLength];
            foreach (Match claim in parsedClaims)
            {
                offsetX = int.Parse(claim.Groups[2].Value);
                offsetY = int.Parse(claim.Groups[3].Value);
                x = int.Parse(claim.Groups[4].Value);
                y = int.Parse(claim.Groups[5].Value);
                for (int i = offsetX ; i < offsetX + x; i++)
                {
                    for (int j = offsetY ; j < offsetY + y; j++)
                    {
                        if (fabricClaims[i, j])
                        {
                            multipleClaims[i, j] = true;
                        }
                        else
                        {
                            fabricClaims[i, j] = true;
                        }
                    }
                }
            }
            int area = 0;
            for (int i = 0; i < maxLength; i++)
            {
                for (int j = 0; j < maxLength; j++)
                {
                    if (multipleClaims[i, j])
                    {
                        area++;
                    }
                }
            }
            Console.WriteLine("Disputed area: " + area);
            bool noConfilct;
            int noConflictID = 0;
            foreach (Match claim in parsedClaims)
            {
                offsetX = int.Parse(claim.Groups[2].Value);
                offsetY = int.Parse(claim.Groups[3].Value);
                x = int.Parse(claim.Groups[4].Value);
                y = int.Parse(claim.Groups[5].Value);
                noConfilct = true;
                for (int i = offsetX; i < offsetX + x; i++)
                {
                    for (int j = offsetY; j < offsetY + y; j++)
                    {
                        if (multipleClaims[i, j])
                        {
                            noConfilct = false;
                        }
                    }
                }
                if (noConfilct)
                {
                    noConflictID = int.Parse(claim.Groups[1].Value);
                }
            }
            Console.WriteLine("Best claim ID: " + noConflictID);
            Console.Read();
        }
    }
}

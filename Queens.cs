using System;
using System.Diagnostics;
using System.Collections.Generic;

public class QueenSolve
{
    private bool[] invDiagonal, diagonal;
    private bool state;
    private Random rand;

    public QueenSolve(){
        rand = new Random();
    }

    private void printArray(int[] pArr)
    {
        String array = "[";
        foreach(int i in pArr)
        {
            array+= i + ", ";
        }
        array = array.Remove(array.Length-2);
        array+= "]";
        Console.WriteLine(array);
    }

    private void swap(int[] pArray, int pX, int pY)
    {
        int temp = pArray[pX];
        pArray[pX] = pArray[pY];
        pArray[pY] = temp;
    }
    
    public void BackQueens(int pSize)
    {
        int invDiagSize = (2*pSize)+1;
        invDiagonal = new bool[invDiagSize];
        for (int i = 0; i < invDiagSize; i++)
        {
            invDiagonal[i] = true;
        }

        int diagSize = (2*pSize)-1;
        diagonal = new bool[diagSize];
        for (int i = 0; i < diagSize; i++)
        {
            diagonal[i] = true;
        }
        
        int[] array = new int[pSize+1];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }

        state = false;
        BackAux(array, pSize, pSize-1);
    }

    private void BackAux(int[] pArray, int pSize, int pAdjust)
    {
        if (pSize == 0)
        {
            printArray(pArray);
            state = true;
            return;
        }
        else
        {
            for (int i = pSize; i > 0; i--)
            {
                if (invDiagonal[pArray[i]+pSize] && diagonal[(pArray[i]-pSize) + pAdjust] && !state)
                {
                    invDiagonal[pArray[i]+pSize] = false;
                    diagonal[(pArray[i]-pSize) + pAdjust] = false;
                    swap(pArray, pSize, i);
                    BackAux(pArray, pSize-1, pAdjust);
                    swap(pArray, pSize, i);
                    invDiagonal[pArray[i]+pSize] = true;
                    diagonal[(pArray[i]-pSize) + pAdjust] = true;
                }
            }
            return;
        }
    }

    private void ProbQueens(int pSize)
    {   
        int[][] board = GetMatrix(pSize);
        int[] available;
        int j;
        bool done = false;
        
        while (!done)
        {
            for (int i = 0; i < pSize; i++)
            {
                available = GetAvailable(board, i);

                if (available.Length == 0)
                {
                    board = GetMatrix(pSize);
                    break;
                }

                if (i == pSize-1) done = true;
                j = available[rand.Next(available.Length)];
                board[i][j] = 1;
            }
        }

        foreach(int[] a in board)
        {
            printArray(a);
        }
    }
    private int[][] GetMatrix(int pSize)
    {
        int[][] board = new int[pSize][];
        for (int i = 0; i < pSize; i++)
        {
            board[i] = new int[pSize];
            for (int j = 0; j < pSize; j++)
            {
                board[i][j] = 0;
            }
        }
        return board;
    }

    private int[] GetAvailable(int[][] board, int pI)
    {
        List<int> available = new List<int>();
        int[] row = board[pI];
        for (int j = 0; j < row.Length; j++)
        {
            if(IsAvailable(board, pI, j))
            {
                available.Add(j);
            }
        }
        return available.ToArray();
    }

    private bool IsAvailable(int[][] board, int pI, int pJ)
    {
        
        for (int i = 0; i < pI; i++)
        {
            if (board[i][pJ] == 1)
            {
                return false;
            }
        }

        int diagI = pI;
        int diagJ = pJ;
        while(diagI != 0 & diagJ != 0)
        {
            diagI--;
            diagJ--;
            if (board[diagI][diagJ] == 1)
            {
                return false;
            }
        } 

        int size = board[pI].Length - 1;
        int invI = pI;
        int invJ = pJ;
        while (invI != 0 & invJ < size)
        {
            invI--;
            invJ++;
            if (board[invI][invJ] == 1)
            {
                return false;
            }
        }

        return true;
    }

    public static void Main(string[] args)
    {
        QueenSolve q = new QueenSolve();
        Stopwatch stopwatch = new Stopwatch();

        stopwatch = new Stopwatch();
        stopwatch.Start();
        q.BackQueens(35);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString() + ", ");

        stopwatch = new Stopwatch();
        stopwatch.Start();
        q.ProbQueens(35);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString());
    }
}
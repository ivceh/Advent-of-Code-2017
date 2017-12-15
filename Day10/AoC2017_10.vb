Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.IO

Module AoC2017

    Sub ReverseCircularList(li As List(Of Byte), pos As Integer, length As Integer)

        Dim i, t As Integer
        For i = 0 To length \ 2 - 1
            t = li((pos + i) Mod li.Count)
            li((pos + i) Mod li.Count) = li((pos + length - 1 - i) Mod li.Count)
            li((pos + length - 1 - i) Mod li.Count) = t
        Next

    End Sub

    Sub Main()

        Dim cnt As Integer
        Dim pos As Integer = 0
        Dim skip_size As Integer = 0
        Dim round As Integer

        Dim sr As New StreamReader("../../input.txt")

        Dim inp As String = sr.ReadLine()
        sr.Close()

        ' creating initial list
        Dim li As New List(Of Byte)
        For cnt = 0 To 255
            li.Add(cnt)
        Next

        ' get lengths from input
        Dim spl_str As String() = inp.Split(",")

        For Each length In spl_str
            ReverseCircularList(li, pos, length)
            pos += length + skip_size
            skip_size += 1
        Next

        Console.WriteLine("First part: " & (Convert.ToInt32(li(0)) * Convert.ToInt32(li(1))))

        ' restore initial state for the second part
        pos = 0
        skip_size = 0
        li.Clear()
        For cnt = 0 To 255
            li.Add(cnt)
        Next

        Dim spl As New List(Of Byte)(Encoding.ASCII.GetBytes(inp))
        spl.Add(17)
        spl.Add(31)
        spl.Add(73)
        spl.Add(47)
        spl.Add(23)

        For round = 0 To 63
            For Each length In spl
                ReverseCircularList(li, pos, length)
                pos = (pos + length + skip_size) Mod li.Count
                skip_size += 1
            Next
        Next

        Dim out As String = ""
        For i = 0 To 15
            Dim xoresult As Byte = 0
            For j = 0 To 15
                xoresult = xoresult Xor li(16 * i + j)
            Next
            out &= xoresult.ToString("x2")
        Next

        Console.WriteLine("Second part: " + out)

    End Sub

End Module
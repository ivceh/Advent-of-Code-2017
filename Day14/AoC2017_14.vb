Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.IO

Module AoC2017

    Sub Swap(ByRef a As Integer, ByRef b As Integer)
        Dim t As Integer = a
        a = b
        b = t
    End Sub

    Sub ReverseCircularList(li As List(Of Byte), pos As Integer, length As Integer)

        Dim i, t As Integer
        For i = 0 To length \ 2 - 1
            Swap(li((pos + i) Mod li.Count), li((pos + length - 1 - i) Mod li.Count))
        Next

    End Sub

    Function KnotHash(inp As String) As String

        Dim cnt As Integer
        Dim pos As Integer = 0
        Dim skip_size As Integer = 0
        Dim round As Integer

        Dim li As New List(Of Byte)
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

        Return out

    End Function

    Function NumberOfBinaryOnes(n As Integer) As Integer

        Dim cnt As Integer = 0
        While n > 0
            cnt += n Mod 2
            n \= 2
        End While
        Return cnt

    End Function

    Sub VisitRegion(grid As BitArray(), visited As BitArray(), i As Integer, j As Integer)
        If i >= 0 AndAlso i < 128 AndAlso j >= 0 AndAlso j < 128 AndAlso grid(i)(j) AndAlso Not visited(i)(j) Then
            visited(i).Set(j, True)
            VisitRegion(grid, visited, i - 1, j)
            VisitRegion(grid, visited, i + 1, j)
            VisitRegion(grid, visited, i, j - 1)
            VisitRegion(grid, visited, i, j + 1)
        End If
    End Sub

    Sub Main()

        Dim sr As New StreamReader("../../input.txt")
        Dim inp As String = sr.ReadLine()
        sr.Close()
        Dim ones As Integer = 0
        Dim grid(127) As BitArray
        Dim i, j, number As Integer

        For i = 0 To 127
            grid(i) = New BitArray(128)
            j = 0
            For Each c In KnotHash(inp & "-" & i)
                number = Convert.ToInt32(c, 16)
                ones += NumberOfBinaryOnes(number)

                If number \ 8 = 1 Then
                    grid(i).Set(j, True)
                End If
                If number \ 4 Mod 2 = 1 Then
                    grid(i).Set(j + 1, True)
                End If
                If number \ 2 Mod 2 = 1 Then
                    grid(i).Set(j + 2, True)
                End If
                If number Mod 2 = 1 Then
                    grid(i).Set(j + 3, True)
                End If
                j += 4
            Next
        Next

        Console.WriteLine("First part: " & ones)

        Dim visited(127) As BitArray
        For i = 0 To 127
            visited(i) = New BitArray(128)
        Next

        Dim regions_counter As Integer = 0
        For i = 0 To 127
            For j = 0 To 127
                If grid(i)(j) AndAlso Not visited(i)(j) Then
                    VisitRegion(grid, visited, i, j)
                    regions_counter += 1
                End If
            Next
        Next

        Console.WriteLine("Second part: " & regions_counter)

    End Sub

End Module
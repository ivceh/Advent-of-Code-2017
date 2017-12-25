Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.IO

Module AoC2017

    Function Min(ByVal a As Integer, ByVal b As Integer) As Integer
        If a <= b Then
            Return a
        Else
            Return b
        End If
    End Function

    Public Class ListComparer : Implements IComparer(Of List(Of String))
        Public Function Compare(x As List(Of String), y As List(Of String)) As Integer Implements IComparer(Of List(Of String)).Compare
            Dim comp = x.Count.CompareTo(y.Count)
            If comp <> 0 Then
                Return comp
            End If
            For i = 0 To Min(x.Count, y.Count) - 1
                comp = x(i).CompareTo(y(i))
                If comp <> 0 Then
                    Return comp
                End If
            Next
            Return 0
        End Function
    End Class

    Dim transf As New SortedDictionary(Of List(Of String), List(Of String))(New ListComparer)

    Sub Swap(ByRef a As Integer, ByRef b As Integer)
        Dim t As Integer = a
        a = b
        b = t
    End Sub

    Sub AddTransformations(ByVal key As List(Of String), ByVal value As List(Of String))
        For Each horizontal_flip In ({False, True})
            For Each vertical_flip In ({False, True})
                For Each diagonal_flip In ({False, True})
                    Dim key2 As New List(Of String)
                    For i = 0 To key.Count - 1
                        Dim s As String = ""
                        For j = 0 To key.Count - 1
                            Dim i2 = i, j2 = j
                            If horizontal_flip Then
                                j2 = key.Count - 1 - j2
                            End If
                            If vertical_flip Then
                                i2 = key.Count - 1 - i2
                            End If
                            If diagonal_flip Then
                                Swap(i2, j2)
                            End If
                            s &= key(i2)(j2)
                        Next
                        key2.Add(s)
                    Next

                    If Not transf.ContainsKey(key2) Then
                        transf.Add(key2, value)
                    End If
                Next
            Next
        Next
    End Sub

    Function Transform(ByVal key As List(Of String)) As List(Of String)
        If key.Count = 2 OrElse key.Count = 3 Then
            If transf.ContainsKey(key) Then
                Return transf(key)
            Else
                Throw New Exception("Transformation not found!")
            End If
        Else
            Dim retval As New List(Of String)
            If key.Count Mod 2 = 0 Then
                For i = 0 To key.Count \ 2 - 1
                    For k = 0 To 2
                        retval.Add("")
                    Next
                    For j = 0 To key.Count \ 2 - 1
                        Dim key2 As New List(Of String)({key(2 * i).Substring(2 * j, 2), key(2 * i + 1).Substring(2 * j, 2)})
                        Dim block_transf As List(Of String) = Transform(key2)
                        For k = 0 To 2
                            retval(3 * i + k) &= block_transf(k)
                        Next
                    Next
                Next
            ElseIf key.Count Mod 3 = 0 Then
                For i = 0 To key.Count \ 3 - 1
                    For k = 0 To 3
                        retval.Add("")
                    Next
                    For j = 0 To key.Count \ 3 - 1
                        Dim key2 As New List(Of String)({key(3 * i).Substring(3 * j, 3), key(3 * i + 1).Substring(3 * j, 3), key(3 * i + 2).Substring(3 * j, 3)})
                        Dim block_transf As List(Of String) = Transform(key2)
                        For k = 0 To 3
                            retval(4 * i + k) &= block_transf(k)
                        Next
                    Next
                Next
            Else
                Throw New Exception("Size is not divisible neither by 2 nor by 3!")
            End If

            Return retval
        End If
    End Function

    Sub Main()

        Dim sr As New StreamReader("../../input.txt")

        Dim line As String = sr.ReadLine()
        While Not (line Is Nothing)
            If line.Trim <> "" Then
                Dim spl As New List(Of List(Of String))
                For Each el1 In line.Split({"=>", " "}, StringSplitOptions.RemoveEmptyEntries)
                    Dim spl2 As New List(Of String)
                    For Each el2 In el1.Split("/")
                        spl2.Add(el2)
                    Next
                    spl.Add(spl2)
                Next
                AddTransformations(spl(0), spl(1))

            End If
            line = sr.ReadLine()
        End While
        sr.Close()

        Dim image As New List(Of String)({".#.", "..#", "###"})

        Dim solution As Integer = 0

        For i = 0 To 4
            image = Transform(image)
        Next
        solution = image.Sum(Function(row) row.Count(Function(c) c = "#"))
        Console.WriteLine("First part: " & solution)

        For i = 5 To 17
            image = Transform(image)
        Next
        solution = image.Sum(Function(row) row.Count(Function(c) c = "#"))
        Console.WriteLine("Second part: " & solution)

    End Sub

End Module
' If size of image is not divisible by 2 it must be divisible by 3.
' Then image is separated into 3×3 blocks which will then transform independently.
' I used this property and optimized this by dynamic programming.

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

    Public Class TupleIntegerListComparer : Implements IComparer(Of Tuple(Of Integer, List(Of String)))
        Public Function Compare(x As Tuple(Of Integer, List(Of String)), y As Tuple(Of Integer, List(Of String))) As Integer Implements IComparer(Of Tuple(Of Integer, List(Of String))).Compare
            Dim comp = x.Item1.CompareTo(y.Item1)
            If comp <> 0 Then
                Return comp
            Else
                Return (New ListComparer).Compare(x.Item2, y.Item2)
            End If
        End Function
    End Class

    Dim transf As New SortedDictionary(Of List(Of String), List(Of String))(New ListComparer)

    ' contains results of OnPixelsAfterNIterations function when key is 3×3 list
    Dim cache As New SortedDictionary(Of Tuple(Of Integer, List(Of String)), Integer)(New TupleIntegerListComparer)

    Sub Swap(ByRef a As Integer, ByRef b As Integer)
        Dim t As Integer = a
        a = b
        b = t
    End Sub

    Sub AddTransformations(key As List(Of String), value As List(Of String))
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

    Sub AddToCache(key As List(Of String), n As Integer, value As Integer)
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

                    Dim keytuple = New Tuple(Of Integer, List(Of String))(n, key2)
                    If Not cache.ContainsKey(keytuple) Then
                        cache.Add(keytuple, value)
                    End If
                Next
            Next
        Next
    End Sub

    Function Transform(key As List(Of String)) As List(Of String)
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

    Function OnPixelsAfterNIterations(key As List(Of String), n As Integer) As Integer
        If key.Count = 3 Then
            Dim keytuple As New Tuple(Of Integer, List(Of String))(n, key)
            If cache.ContainsKey(keytuple) Then
                Return cache(keytuple)
            Else
                Dim retval As Integer
                If n = 0 Then
                    retval = key.Sum(Function(row) row.Count(Function(c) c = "#"))
                Else
                    retval = OnPixelsAfterNIterations(Transform(key), n - 1)
                End If
                AddToCache(key, n, retval)
                Return retval
            End If

        ElseIf key.Count Mod 2 = 0 Then
            Dim retval As Integer
            If n = 0 Then
                retval = key.Sum(Function(row) row.Count(Function(c) c = "#"))
            Else
                retval = OnPixelsAfterNIterations(Transform(key), n - 1)
            End If
            Return retval

        ElseIf key.Count Mod 3 = 0 Then
            Dim retval As Integer = 0
            For i = 0 To key.Count \ 3 - 1
                For j = 0 To key.Count \ 3 - 1
                    Dim key2 As New List(Of String)({key(3 * i).Substring(3 * j, 3), key(3 * i + 1).Substring(3 * j, 3), key(3 * i + 2).Substring(3 * j, 3)})
                    retval += OnPixelsAfterNIterations(key2, n)
                Next
            Next
            Return retval

        Else
            Throw New Exception("Size is not divisible neither by 2 nor by 3!")
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

        Console.WriteLine("First part: " & OnPixelsAfterNIterations(image, 5))
        Console.WriteLine("Second part: " & OnPixelsAfterNIterations(image, 18))

    End Sub

End Module
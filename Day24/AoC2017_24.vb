Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.IO

Module AoC2017

    Dim dict As New Dictionary(Of Integer, Dictionary(Of Integer, Integer))

    Dim maxw As Integer = 0

    Dim maxl As Integer = 0
    Dim maxlw As Integer = 0

    Sub AddComponent(a As Integer, b As Integer)

        If dict.ContainsKey(a) Then
            If dict(a).ContainsKey(b) Then
                dict(a)(b) += 1
            Else
                dict(a).Add(b, 1)
            End If
        Else
            dict.Add(a, New Dictionary(Of Integer, Integer) From {{b, 1}})
        End If

        If dict.ContainsKey(b) Then
            If dict(b).ContainsKey(a) Then
                dict(b)(a) += 1
            Else
                dict(b).Add(a, 1)
            End If
        Else
            dict.Add(b, New Dictionary(Of Integer, Integer) From {{a, 1}})
        End If

    End Sub

    Sub RemoveComponent(a As Integer, b As Integer)
        dict(a)(b) -= 1
        dict(b)(a) -= 1
    End Sub

    Sub SearchBridges(currweight As Integer, number_expected As Integer, current_length As Integer)

        If currweight > maxw Then
            maxw = currweight
        End If

        If current_length > maxl OrElse (current_length = maxl AndAlso currweight > maxlw) Then
            maxl = current_length
            maxlw = currweight
        End If

        If dict.ContainsKey(number_expected) Then
            Dim other_sides As New HashSet(Of Integer)(dict(number_expected).Keys)

            For Each other_side In other_sides
                If dict(number_expected)(other_side) > 0 Then
                    RemoveComponent(number_expected, other_side)
                    SearchBridges(currweight + number_expected + other_side, other_side, current_length + 1)
                    AddComponent(number_expected, other_side)
                End If
            Next
        End If

    End Sub

    Sub Main()

        Dim sr As New StreamReader("../../input.txt")

        Dim line As String = sr.ReadLine()
        While Not (line Is Nothing)
            If line.Trim <> "" Then
                Dim spl = line.Split("/")
                AddComponent(spl(0), spl(1))
            End If
            line = sr.ReadLine()
        End While
        sr.Close()

        SearchBridges(0, 0, 0)

        Console.WriteLine("First part: " & maxw)
        Console.WriteLine("Second part: " & maxlw)

    End Sub

End Module
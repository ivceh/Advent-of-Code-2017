Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.IO

Module AoC2017_5

    Sub Main()

        Dim sr As New StreamReader("../../input.txt")
        Dim li As New List(Of Integer)
        Dim n As Integer = 0
        Dim pcnt As Integer = 0

        Dim second_part As Boolean = True

        Dim line As String = sr.ReadLine()
        While Not (line Is Nothing)
            li.Add(Convert.ToInt32(line))
            line = sr.ReadLine()
        End While

        While pcnt >= 0 AndAlso pcnt < li.Count
            If second_part AndAlso li(pcnt) >= 3 Then
                li(pcnt) -= 1
                pcnt += li(pcnt) + 1
            Else
                li(pcnt) += 1
                pcnt += li(pcnt) - 1
            End If

            n += 1
        End While

        System.Console.WriteLine(n)

    End Sub

End Module

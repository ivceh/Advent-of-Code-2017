Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.IO

Module AoC2017

    Dim registers As New Dictionary(Of String, Long)

    Function GetValue(s As String) As Long
        If (s(0) >= "0" AndAlso s(0) <= "9") OrElse s(0) = "-" Then
            Return Convert.ToInt32(s)
        Else
            If Not registers.ContainsKey(s) Then
                Return 0
            Else
                Return registers(s)
            End If
        End If
    End Function

    Sub SetValue(s As String, n As Long)
        If registers.ContainsKey(s) Then
            registers(s) = n
        Else
            registers.Add(s, n)
        End If
    End Sub

    Sub Main()

        Dim sr As New StreamReader("../../input.txt")

        Dim li As New List(Of String)

        Dim line As String = sr.ReadLine()
        While Not (line Is Nothing)
            If line.Trim <> "" Then
                li.Add(line)
            End If
            line = sr.ReadLine()
        End While
        sr.Close()

        Dim cnt As Integer = 0
        Dim current_sound As Integer = -1
        Do
            Dim spl As String() = li(cnt).Split()

            Select Case spl(0)
                Case "snd"
                    current_sound = GetValue(spl(1))
                Case "set"
                    SetValue(spl(1), GetValue(spl(2)))
                Case "add"
                    SetValue(spl(1), GetValue(spl(1)) + GetValue(spl(2)))
                Case "mul"
                    SetValue(spl(1), GetValue(spl(1)) * GetValue(spl(2)))
                Case "mod"
                    SetValue(spl(1), GetValue(spl(1)) Mod GetValue(spl(2)))
                Case "rcv"
                    If GetValue(spl(1)) <> 0 Then
                        Exit Do
                    End If
                Case "jgz"
                    If GetValue(spl(1)) > 0 Then
                        cnt += GetValue(spl(2)) - 1
                    End If
                Case Else
                    Console.WriteLine("Unknown instruction!")
                    Exit Sub
            End Select
            cnt += 1
        Loop

        System.Console.WriteLine(current_sound)

    End Sub

End Module
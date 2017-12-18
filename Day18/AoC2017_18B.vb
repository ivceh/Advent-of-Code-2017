Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.IO

Module AoC2017

    Dim registers(1) As Dictionary(Of String, Long)
    Dim cnt() As Integer = {0, 0}
    Dim id As Integer = 0
    Dim message_queue(1) As Queue(Of Long)

    Function GetValue(s As String) As Long
        If (s(0) >= "0" AndAlso s(0) <= "9") OrElse s(0) = "-" Then
            Return Convert.ToInt32(s)
        Else
            If Not registers(id).ContainsKey(s) Then
                Return 0
            Else
                Return registers(id)(s)
            End If
        End If
    End Function

    Sub SetValue(s As String, n As Long)
        If registers(id).ContainsKey(s) Then
            registers(id)(s) = n
        Else
            registers(id).Add(s, n)
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

        registers(0) = New Dictionary(Of String, Long)
        registers(1) = New Dictionary(Of String, Long) From {{"p", 1}}
        message_queue(0) = New Queue(Of Long)
        message_queue(1) = New Queue(Of Long)
        Dim answer As Integer = 0
        Do
            Dim spl As String() = li(cnt(id)).Split()

            Select Case spl(0)
                Case "snd"
                    If id = 1 Then
                        answer += 1
                    End If
                    message_queue(1 - id).Enqueue(GetValue(spl(1)))
                Case "set"
                    SetValue(spl(1), GetValue(spl(2)))
                Case "add"
                    SetValue(spl(1), GetValue(spl(1)) + GetValue(spl(2)))
                Case "mul"
                    SetValue(spl(1), GetValue(spl(1)) * GetValue(spl(2)))
                Case "mod"
                    SetValue(spl(1), GetValue(spl(1)) Mod GetValue(spl(2)))
                Case "rcv"
                    If message_queue(id).Any() Then
                        SetValue(spl(1), message_queue(id).Dequeue)
                    Else
                        id = 1 - id
                        If li(cnt(id)).Split()(0) = "rcv" AndAlso Not message_queue(id).Any Then
                            Exit Do
                        Else
                            Continue Do
                        End If
                    End If
                Case "jgz"
                    If GetValue(spl(1)) > 0 Then
                        cnt(id) += GetValue(spl(2)) - 1
                    End If
                Case Else
                    Console.WriteLine("Unknown instruction!")
                    Exit Sub
            End Select
            cnt(id) += 1
        Loop

        Console.WriteLine(answer)

    End Sub

End Module
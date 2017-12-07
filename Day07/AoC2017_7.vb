Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.IO

Public Module AoC2017

    Public Const undefined As Integer = -1

    Class NodeInfo

        Public node_weight As Integer = undefined
        Public children As New List(Of String)
        Private sub_weight As Integer = undefined

        Public Function Subtree_weight() As Integer

            ' calculate it only if it is not yet calculated
            If sub_weight = undefined Then
                sub_weight = node_weight
                For Each child In children
                    sub_weight += nodes(child).Subtree_weight()
                Next
            End If

            Return sub_weight
        End Function

    End Class

    Dim nodes As New Dictionary(Of String, NodeInfo)

    Sub Main()

        Dim sr As New StreamReader("../../input.txt")
        Dim spl_line As String()
        Dim line As String = sr.ReadLine()
        Dim node_name As String
        Dim nodes_with_parent As New HashSet(Of String)
        Dim root As String = ""

        While Not (line Is Nothing)
            If line.Trim <> "" Then
                spl_line = line.Split()
                node_name = spl_line(0)
                Dim info As New NodeInfo
                info.node_weight = Convert.ToInt32(spl_line(1).Replace("(", "").Replace(")", ""))

                If spl_line.Length > 2 Then
                    For i = 3 To spl_line.Length - 1
                        info.children.Add(spl_line(i).Replace(",", ""))
                        nodes_with_parent.Add(spl_line(i).Replace(",", ""))
                    Next
                End If

                nodes.Add(node_name, info)
            End If

            line = sr.ReadLine()
        End While
        sr.Close()

        For Each node In nodes
            node_name = node.Key
            If Not nodes_with_parent.Contains(node_name) Then
                If root = "" Then
                    root = node_name
                Else
                    Console.WriteLine("Wrong input! There is more than one node without parent.")
                    Exit Sub
                End If
            End If
        Next
        If root = "" Then
            Console.WriteLine("Wrong input! All nodes have parents.")
            Exit Sub
        Else
            Console.WriteLine("Part 1: " & root)
        End If

        If nodes(root).children.Count <= 2 Then
            Console.WriteLine("I did not cover this case. I could but the code would be more complicated.")
            Exit Sub
        Else
            Dim subtree_weights As New Dictionary(Of Integer, Integer)
            ' keys: subtree weight
            ' values: number of subtrees with that weight

            For Each node In nodes(root).children
                Dim subtree_weight As Integer = nodes(node).Subtree_weight()
                If subtree_weights.ContainsKey(nodes(node).Subtree_weight()) Then
                    subtree_weights(subtree_weight) += 1
                Else
                    subtree_weights.Add(subtree_weight, 1)
                End If
            Next

            If subtree_weights.Count <> 2 Then
                Console.WriteLine("Wrong input! All root's subtrees must have the same weight except one.")
                Exit Sub
            Else
                If subtree_weights.ElementAt(0).Value = 1 Then
                    Dim part2 = FindUnbalanced(root, subtree_weights.ElementAt(0).Key - subtree_weights.ElementAt(1).Key)
                    Console.WriteLine("Part 2: " & part2.Item1 & " should be changed from " & part2.Item2 & " to " & part2.Item3 & ".")
                ElseIf subtree_weights.ElementAt(1).Value = 1 Then
                    Dim part2 = FindUnbalanced(root, subtree_weights.ElementAt(1).Key - subtree_weights.ElementAt(0).Key)
                    Console.WriteLine("Part 2: " & part2.Item1 & " should be changed from " & part2.Item2 & " to " & part2.Item3 & ".")
                Else
                    Console.WriteLine("Wrong input! All root's subtrees must have the same weight except one.")
                    Exit Sub
                End If
            End If
        End If

    End Sub

    ' returns name of a node that has to be changed, its previous weight and weight after the change
    Function FindUnbalanced(ByVal s As String, ByVal diff As Integer) As Tuple(Of String, Integer, Integer)
        Dim max As String
        Dim min As String
        If nodes(s).children.Any Then
            max = nodes(s).children.First
            min = nodes(s).children.First
            For Each child In nodes(s).children
                If (nodes(child).Subtree_weight() > nodes(max).Subtree_weight()) Then
                    max = child
                End If
                If (nodes(child).Subtree_weight() < nodes(min).Subtree_weight()) Then
                    min = child
                End If
            Next

            If (nodes(max).Subtree_weight() > nodes(min).Subtree_weight()) Then
                If diff > 0 Then
                    Return FindUnbalanced(max, diff)
                Else
                    Return FindUnbalanced(min, diff)
                End If
            Else
                Return New Tuple(Of String, Integer, Integer)(s, nodes(s).node_weight, nodes(s).node_weight - diff)
            End If
        Else
            Return New Tuple(Of String, Integer, Integer)(s, nodes(s).node_weight, nodes(s).node_weight - diff)
        End If
    End Function

End Module
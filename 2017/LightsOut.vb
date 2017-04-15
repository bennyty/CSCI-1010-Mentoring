Option Strict On
Option Explicit On
Public Class Form1
    ' The board is square so this should be a perfect square number. (it will be rounded down to the nearest perfect square
    Const sizeOfBoard As Integer = 25
    Dim sizeOfBoardEdge As Integer = CInt(Math.Floor(Math.Sqrt(sizeOfBoard)))


    ' PART 1: Make a custom structure with two variables inside. A Boolean named boolLightOn and an Integer named intClicksRemaining
    ' Put your structure declaration here ↓

    ' Put your structure declaration here ↑
    ' When you make your custom structure, change the type of this array. It should end with:    As YourStructureName
    Private boardArray(sizeOfBoard - 1) As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Lights Out!"
        ' If you want to setup the board (maybe randomize it?) you can do that here
        ' PART 1: Write a loop that makes all the lights start ON (Right now they start off because Booleans are false by default)
        '         and start with 5 clicks remaining

        RedrawBoard() ' This will draw the board for the first time
    End Sub

    Public Sub ButtonGotClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        ' You can use intButtonID to get the number of the button clicked
        Dim intButtonID As Integer = CInt(CType(sender, Button).Tag)
        ' Since each row is sizeOfBoardEdge long, you can divide and round down to find the row number
        Dim intRowCoordinate As Integer = intButtonID \ sizeOfBoardEdge
        ' Since each row is sizeOfBoardEdge long, you can divide and get the remainder to find the column number
        Dim intColumnCoordinate As Integer = intButtonID Mod sizeOfBoardEdge

        ' ============================================================================================================
        ' Your code goes here in this area

        ' PART 1: When you add your custom structure edit the line below to use boolLightOn
        boardArray(intButtonID) = Not boardArray(intButtonID) ' This updates boardArray to change the button you clicked
        ' PART 1: When you click a button, you should subtract 1 from the clicks remaining.

        ' PART 2: When you click a button it should toggle the buttons adjacent to it in a cross shape

        ' You can delete this when you understand what is going on
        MessageBox.Show("You clicked button number: " + Convert.ToString(intButtonID) +
                        " Coordinate: (" + Convert.ToString(intRowCoordinate) + ", " + Convert.ToString(intColumnCoordinate) + ")")

        ' Leave everything below this line alone
        ' ============================================================================================================
        RedrawBoard() ' Now that you have update boardArray we have to redraw the board
    End Sub

    ' You don't need to look at this function. Just call RedrawBoard(boardArray) when you want to update the display
    Sub RedrawBoard()
        Me.Size = New Size(sizeOfBoardEdge * 66, sizeOfBoardEdge * 66)
        Me.ClientSize = New Size(sizeOfBoardEdge * 66, sizeOfBoardEdge * 66)
        Dim Btns(sizeOfBoardEdge * sizeOfBoardEdge - 1) As Button
        Dim table As TableLayoutPanel = New TableLayoutPanel
        Dim type = boardArray.GetType()
        Dim stateList As List(Of Boolean) = New List(Of Boolean)
        Dim scoreList As List(Of Integer) = New List(Of Integer)
        Console.WriteLine("Type was: " + type.ToString)
        Console.WriteLine(type.IsValueType)
        Console.WriteLine(type.IsPrimitive)
        Console.WriteLine(type.Namespace.StartsWith("System"))
        Console.WriteLine(type.IsEnum)
        table.Size = Me.ClientSize


        For i As Integer = 0 To Btns.Length - 1
            Btns(i) = New Button
            Btns(i).Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
            If Not type.IsValueType And Not type.IsPrimitive And Not type.Namespace.StartsWith("System") And Not type.IsEnum Then
                ' This is for boardArray As Tile
                Console.WriteLine("Struct: " + (i).ToString)
                Dim item = boardArray(i)
                Try
                    Dim clicksRemaining = CInt(CallByName(item, "intClicksRemaining", CallType.Get, Nothing))
                    Btns(i).Text = (clicksRemaining).ToString
                    scoreList.Add(clicksRemaining)
                    Console.WriteLine("    intClicksRemaining: " + (clicksRemaining).ToString)
                Catch ex As MissingMemberException
                    MessageBox.Show("Your custom struct did not contain a Integer named intClicksRemaining. Array index: " + (i).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End
                End Try
                Try
                    Dim lightOn = CallByName(item, "boolLightOn", CallType.Get, Nothing)
                    Dim enabled = CBool(lightOn)
                    stateList.Add(enabled)
                    Btns(i).ForeColor = If(enabled, Color.Black, Color.White)
                    Btns(i).BackColor = If(enabled, Color.White, Color.Black)
                    Console.WriteLine("    boolLightOn: " + (enabled).ToString)
                Catch ex As MissingMemberException
                    MessageBox.Show("Your custom struct did not contain a Boolean named boolLightOn. Array index: " + (i).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End
                End Try
            Else
                ' This is for boardArray As Boolean
                Dim enabled = CBool(CObj(boardArray(i)))
                stateList.Add(enabled)
                scoreList.Add(-1)
                Btns(i).BackColor = If(enabled, Color.White, Color.Black)
            End If
            Btns(i).Tag = i
            Btns(i).Size = New Size(60, 60)
            table.Controls.Add(Btns(i), i Mod sizeOfBoardEdge, i \ sizeOfBoardEdge)
            AddHandler Btns(i).Click, AddressOf ButtonGotClicked
        Next
        Me.Controls.Clear()
        Me.Controls.Add(table)

        Dim wonTheGame As Boolean = Not stateList.Contains(True)
        Dim lostTheGame As Boolean = True
        For Each x In scoreList
            If x <> 0 Then
                lostTheGame = False
                Exit For
            End If
        Next

        If lostTheGame Then
            For Each btn In Btns
                btn.Enabled = False
            Next
            MessageBox.Show("Sorry you ran out of moves.", "You lost", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End
        End If
        If wonTheGame Then
            Dim score = 0
            For Each btn In Btns
                btn.Enabled = False
                Try
                    Dim clicksRemaining = CInt(CallByName(btn, "intClicksRemaining", CallType.Get, Nothing))
                    score += clicksRemaining
                Catch ex As Exception
                End Try
            Next
            MessageBox.Show("WINNER: Your score was: " + score.ToString, "You won", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Task.Run(Sub() DiscoParty(Btns))
        End If
    End Sub

    ' Thank you Devansh for this excellnt disco code
    Sub DiscoParty(btns() As Button)
        While (True)
            For Each btn In btns
                btn.BackColor = getRandomColor()
            Next
            Threading.Thread.Sleep(200)
        End While
    End Sub
    ' Thank you Devansh for this excellnt disco code
    Function getRandomColor() As Color
        Return Color.FromArgb(getRandomColorChannel(), getRandomColorChannel(), getRandomColorChannel())
    End Function

    ' Thank you Devansh for this excellnt disco code
    ' Generate Random number in range 0 ... 255
    ' https://msdn.microsoft.com/en-us/library/f7s023d2(v=vs.90).aspx
    Function getRandomColorChannel() As Integer
        Const upperbound = 255
        Const lowerbound = 0
        Return CInt(Math.Floor((upperbound - lowerbound + 1) * Rnd())) + lowerbound
    End Function
End Class
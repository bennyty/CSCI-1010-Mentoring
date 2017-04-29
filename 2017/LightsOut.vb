Option Strict On
Option Explicit On
Public Class Form1
    ' The board is square so this should be a perfect square number. (it will be rounded down to the nearest perfect square)
    Const sizeOfBoard As Integer = 25
    Dim sizeOfBoardEdge As Integer = CInt(Math.Floor(Math.Sqrt(sizeOfBoard)))

	' Hey team. This code has been worked on a lot to make the game look pretty good already, you just need to extend the functionality
	' Please read through the code here up till the warning line. Then follow the steps in order 1,2,3.


    ' STEP 2: Make a custom structure with two variables inside. A Boolean named boolLightOn and an Integer named intClicksRemaining
    ' Put your structure declaration here ↓

    ' Put your structure declaration here ↑
    ' When you make your custom structure, change the type of this array to be an array of your structures instead of an array of booleans
    Private boardArray(sizeOfBoard - 1) As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Lights Out!"
        ' If you want to setup the board (maybe randomize it?) you can do that here

		' STEP 1: Right now, when you start the game you will instantly win!
		' Right now all the lights start off because Booleans are false by
		' default. Write a loop that makes all the lights start ON

		' STEP 2: When you start the game after adding you custom structure,
		' you will instantly lose! The game tells you that you ran out of
		' moves. Modify your loop so that you start with 5 clicks remaining

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

		' STEP 2: When you add your custom structure, boardArray is now not an
		' array of booleans, now its an array of structures that CONTAIN a
		' boolean. Edit the line below to use boolLightOn instead.
        boardArray(intButtonID) = Not boardArray(intButtonID) ' This updates boardArray to change the button you clicked
        ' STEP 3: When you click a button, you should also subtract 1 from the clicks remaining, just on that button.

		' STEP 3: When you click a button it should toggle the buttons adjacent
		' to it in a cross shape please add code that will toggle the buttons
		' adjacent to the clicked button. There are some variables created for
		' you that might help. Make sure you check the edge cases.

        ' You can delete this when you understand what is going on (its kind of annoying)
        MessageBox.Show("You clicked button number: " + Convert.ToString(intButtonID) +
                        " Coordinate: (" + Convert.ToString(intRowCoordinate) + ", " + Convert.ToString(intColumnCoordinate) + ")")

        ' Leave everything below this line alone
        ' ============================================================================================================
        RedrawBoard() ' Now that you have updated boardArray we have to redraw the board
    End Sub



'   Only confusion and misery await you below
'   ==================================================================================================================
'                      __      ___   ___ _  _ ___ _  _  ___   ___   ___    _  _  ___ _____
'                      \ \    / /_\ | _ \ \| |_ _| \| |/ __| |   \ / _ \  | \| |/ _ \_   _|
'                       \ \/\/ / _ \|   / .` || || .` | (_ | | |) | (_) | | .` | (_) || |
'                        \_/\_/_/ \_\_|_\_|\_|___|_|\_|\___| |___/ \___/  |_|\_|\___/ |_|
'                         ___ ___  ___  ___ ___   _____ _  _ ___ ___   _    ___ _  _ ___
'                        / __| _ \/ _ \/ __/ __| |_   _| || |_ _/ __| | |  |_ _| \| | __|
'                       | (__|   / (_) \__ \__ \   | | | __ || |\__ \ | |__ | || .` | _|
'                        \___|_|_\\___/|___/___/   |_| |_||_|___|___/ |____|___|_|\_|___|
'   ==================================================================================================================

    Dim Btns(sizeOfBoardEdge * sizeOfBoardEdge - 1) As Button
    Sub SetupBoard()
        Me.Size = New Size(sizeOfBoardEdge * 66, sizeOfBoardEdge * 66)
        Me.ClientSize = New Size(sizeOfBoardEdge * 66, sizeOfBoardEdge * 66)
        Dim table As TableLayoutPanel = New TableLayoutPanel
        Dim type = boardArray.GetType()
        table.Size = Me.ClientSize
        Console.WriteLine("Type was: " + type.ToString)
        Console.WriteLine(type.IsValueType)
        Console.WriteLine(type.IsPrimitive)
        Console.WriteLine(type.Namespace.StartsWith("System"))
        Console.WriteLine(type.IsEnum)

        For i As Integer = 0 To Btns.Length - 1
            Btns(i) = New Button
            Btns(i).Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
            If Not type.IsValueType And Not type.IsPrimitive And Not type.Namespace.StartsWith("System") And Not type.IsEnum Then
                ' This is for boardArray As Tile
                Console.WriteLine("Struct: " + i.ToString)
                Dim item = boardArray(i)
                Try
                    Dim clicksRemaining = CInt(CallByName(item, "intClicksRemaining", CallType.Get, Nothing))
                    Btns(i).Text = clicksRemaining.ToString
                    Console.WriteLine("    intClicksRemaining: " + clicksRemaining.ToString)
                Catch ex As MissingMemberException
                    MessageBox.Show("Your custom struct did not contain a Integer named intClicksRemaining. Array index: " + i.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End
                End Try
                Try
                    Dim lightOn = CBool(CallByName(item, "boolLightOn", CallType.Get, Nothing))
                    Btns(i).ForeColor = If(lightOn, Color.Black, Color.White)
                    Btns(i).BackColor = If(lightOn, Color.White, Color.Black)
                    Console.WriteLine("    boolLightOn: " + enabled.ToString)
                Catch ex As MissingMemberException
                    MessageBox.Show("Your custom struct did not contain a Boolean named boolLightOn. Array index: " + i.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End
                End Try
            Else
                ' This is for boardArray As Boolean
                Dim enabled = CBool(CObj(boardArray(i)))
                Btns(i).BackColor = If(enabled, Color.White, Color.Black)
            End If
            Btns(i).Tag = i
            Btns(i).Size = New Size(60, 60)
            table.Controls.Add(Btns(i), i Mod sizeOfBoardEdge, i \ sizeOfBoardEdge)
            AddHandler Btns(i).Click, AddressOf ButtonGotClicked
        Next
        Me.Controls.Clear()
        Me.Controls.Add(table)
        RedrawBoard() ' After setup draw the board once
    End Sub

    ' You don't need to look at this function. Just call RedrawBoard() when you need to update the display
    Sub RedrawBoard()
        Dim stateList As IEnumerable(Of Boolean)
        Dim scoreList As IEnumerable(Of Integer)
        Dim type = boardArray.GetType()
        Dim wonTheGame As Boolean = False
        Dim lostTheGame As Boolean = False
        Dim score = 0

        If Not type.IsValueType And Not type.IsPrimitive And Not type.Namespace.StartsWith("System") And Not type.IsEnum Then
            ' This is for boardArray As Tile
            For i As Integer = 0 To Btns.Length - 1
                Dim item = boardArray(i)
                Try
                    Dim clicksRemaining = CInt(CallByName(item, "intClicksRemaining", CallType.Get, Nothing))
                    Btns(i).Text = clicksRemaining.ToString
                Catch ex As MissingMemberException
                    MessageBox.Show("Your custom struct did not contain a Integer named intClicksRemaining. Array index: " + i.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End
                End Try
                Try
                    Dim lightOn = CBool(CallByName(item, "boolLightOn", CallType.Get, Nothing))
                    Btns(i).ForeColor = If(lightOn, Color.Black, Color.White)
                    Btns(i).BackColor = If(lightOn, Color.White, Color.Black)
                Catch ex As MissingMemberException
                    MessageBox.Show("Your custom struct did not contain a Boolean named boolLightOn. Array index: " + i.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End
                End Try
            Next
            stateList = boardArray.Select((Function(x) CBool(CallByName(x, "boolLightOn", CallType.Get, Nothing))))
            scoreList = boardArray.Select((Function(x) CInt(CallByName(x, "intClicksRemaining", CallType.Get, Nothing))))
            lostTheGame = scoreList.All(Function(x) x = 0)
            score = scoreList.Sum()
        Else
            For i As Integer = 0 To Btns.Length - 1
                ' This is for boardArray As Boolean
                Dim enabled = CBool(CObj(boardArray(i)))
                Btns(i).BackColor = If(enabled, Color.White, Color.Black)
            Next
            stateList = CType(boardArray, IEnumerable(Of Boolean))
        End If

        wonTheGame = Not stateList.Contains(True)

        If lostTheGame Then
            For Each btn In Btns
                btn.Enabled = False
            Next
            MessageBox.Show("Sorry you ran out of moves.", "You lost", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End
        End If
        If wonTheGame Then
            For Each btn In Btns
                btn.Enabled = False
            Next
            MessageBox.Show("WINNER: Your score was: " + score.ToString, "You won", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Task.Run(Sub() DiscoParty(Btns))
        End If
    End Sub

    ' Thank you Lucien for this excellent disco code
    Sub DiscoParty(btns As IEnumerable(Of Button))
        While (True)
            For Each btn In btns
                btn.BackColor = getRandomColor()
            Next
            Threading.Thread.Sleep(200)
        End While
    End Sub
    ' Thank you Lucien for this excellent disco code
    Function getRandomColor() As Color
        Return Color.FromArgb(getRandomColorChannel(), getRandomColorChannel(), getRandomColorChannel())
    End Function

    ' Thank you Lucien for this excellent disco code
    ' Generate Random number in range 0 ... 255
    ' https://msdn.microsoft.com/en-us/library/f7s023d2(v=vs.90).aspx
    Function getRandomColorChannel() As Integer
        Const upperbound = 255
        Const lowerbound = 0
        Return CInt(Math.Floor((upperbound - lowerbound + 1) * Rnd())) + lowerbound
    End Function
End Class

Option Explicit On
Option Strict On
Public Class Form1

	' This function takes a word as input and returns an array of length 26
	' Example: input the word "Hello" will return the following array
	' Array : [ 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 ]
	' Index :   0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25
	' Letter:   a  b  c  d  e  f  g  h  i  j  k  l  m  n  o  p  q  r  s  t  u  v  w  x  y  z
	Function StringToCharacterTotals(ByVal strWord As String) As Integer()
		Dim wordLetters(25) As Integer

		For characterNumber As Integer = 0 To strWord.Length - 1
			Dim character As String = strWord.Substring(characterNumber, 1)
			Dim charValue As Integer = Asc(character) - 65
			wordLetters(charValue) += 1
		Next

		Return wordLetters
	End Function

	'This function take two string and returns true is they are anagrams and false otherwise
	Function checkAnagram(ByVal strWord1 As String, ByVal strWord2 As String) As Boolean
		If strWord1.Length <> strWord2.Length Then
			'MessageBox.Show("The two strings must have the same length")
			Return False
		End If

		Dim word1Letters() As Integer = StringToCharacterTotals(strWord1)
		Dim word2Letters() As Integer = StringToCharacterTotals(strWord2)

		For intVariable As Integer = 0 To 25
			If word1Letters(intVariable) <> word2Letters(intVariable) Then
				'MessageBox.Show("Not anagrams")
				Return False
			End If
		Next
		'MessageBox.Show("Is an anagram")
		Return True
	End Function

	'This function take two string and returns true is they are anagrams and false otherwise
	Function checkAnagramWithSorting(ByVal strWord1 As String, ByVal strWord2 As String) As Boolean
		If strWord1.Length <> strWord2.Length Then
			Return False
		End If

		Dim word1AsArray() As Char = strWord1.ToCharArray
		Dim word2AsArray() As Char = strWord2.ToCharArray

		Array.Sort(word1AsArray)
		Array.Sort(word2AsArray)

		'Check if each element is the same
		For intVariable As Integer = 0 To Math.Min(word1AsArray.GetUpperBound(0), word2AsArray.GetUpperBound(0))
			If word1AsArray(intVariable) <> word2AsArray(intVariable) Then
				Return False
			End If
		Next
		Return True
	End Function

	'*NOTE*: This program will only work for single words and alphabetic input. No spaces, numbers, or special characters allowed.
	Private Sub btnCheckAnagram_Click(sender As Object, e As EventArgs) Handles btnCheckAnagram.Click
		'Get inputs and put them in variables
		'	Note the inline conversion to UPPERCASE words
		Dim strWord1 As String = txtWord1.Text.ToUpper
		Dim strWord2 As String = txtWord2.Text.ToUpper

		' These are and-ed together to check if both functions say it is an anagram
		If checkAnagram(strWord1, strWord2) And checkAnagramWithSorting(strWord1, strWord2) Then
			MessageBox.Show("Are anagrams")
		Else
			MessageBox.Show("Not anagrams")
		End If
	End Sub

	Function CSCI1010TryParse(ByVal strInput As String) As Integer
		If IsNumeric(strInput) = False Then
			MessageBox.Show("Not a number")
			' Instructions say to return 0 if there is an error
			Return 0
		Else
			MessageBox.Show("Is a number")
			'Should NOT Exit Function
		End If

		If strInput.Contains(".") Then
			MessageBox.Show("Not an integer")
			' Instructions say to return 0 if there is an error
			Return 0
		End If


		Dim intOutput As Integer = 0
		intOutput = CInt(strInput)
		Return intOutput
	End Function

End Class


Public Class Form1
    Dim MyFiles() As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MyFiles(0) = "" Then MsgBox("No data, drag & drop some csv") : Exit Sub
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(My.Computer.FileSystem.SpecialDirectories.Desktop & "\Qb.csv", False)
        file.WriteLine("Date,Description,Amount")
        file.WriteLine(TextBox1.Text)
        file.Close()

        'update prev file
        My.Computer.FileSystem.WriteAllText(My.Computer.FileSystem.SpecialDirectories.Desktop & "\QbBankHistory.csv", TextBox1.Text, True)
        'My.Computer.FileSystem.WriteAllText("s:\company\figures\QbBankHistory.csv", TextBox1.Text, True)

    End Sub



    Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            Dim mdate As String
            Dim mdesc As String
            Dim mamount As String

            'read previous transactions so we can avoid them
            Dim prev As String = My.Computer.FileSystem.ReadAllText(My.Computer.FileSystem.SpecialDirectories.Desktop & "\QbBankHistory.csv")
            'Dim prev As String = My.Computer.FileSystem.ReadAllText("s:\company\figures\QbBankHistory.csv")

            ' Assign the files to an array.
            MyFiles = e.Data.GetData(DataFormats.FileDrop)

            Dim tfp As New Microsoft.VisualBasic.FileIO.TextFieldParser(MyFiles(0))
            tfp.Delimiters = New String() {","}
            tfp.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited

            Dim ln As String = ""
            Dim op As String
            Do
                ln = tfp.ReadLine() ' skip header
            Loop Until instr(ln, "Date,Description,") <> 0

            TextBox1.Text = ""

            While tfp.EndOfData = False
                Dim fields = tfp.ReadFields()
                mdate = fields(0)
                mdesc = fields(1) & " " & fields(3) & " " & fields(2)
                mamount = "0"
                If (fields(4) <> "") Then mamount = fields(4)
                If (fields(5) <> "") Then mamount = "-" & fields(5)
                If Len(mdate) > 6 And mdate <> "Transactions" Then
                    op = String.Format("{0},{1},{2}", mdate, mdesc, mamount)
                    If InStr(prev, op) = 0 Then
                        TextBox1.Text = TextBox1.Text & op & vbCrLf
                    End If
                End If
            End While

            Exit Sub

            ' Display the file Name
            'TextBoxDrop.Text = MyFiles(0)
            ' Display the file contents
            TextBox1.Text = My.Computer.FileSystem.ReadAllText(MyFiles(0))

            'strip the headers
            Dim p = InStr(TextBox1.Text, "Date,Description,")
            Dim st As String = Mid(TextBox1.Text, p)
            Dim sdat() As String = st.Split(",")
        End If
    End Sub

    Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox1.DragEnter
        'SET THE PROPER ACTION FOR FILE DROP....BY USING THE FILEDROP METHOD TO COPY
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = My.Computer.FileSystem.ReadAllText(My.Computer.FileSystem.SpecialDirectories.Desktop & "\QbBankHistory.csv")
        'TextBox1.Text = My.Computer.FileSystem.ReadAllText("s:\company\figures\QbBankHistory.csv")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class

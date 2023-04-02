<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.Title = New System.Windows.Forms.Label()
        Me.Status = New System.Windows.Forms.Label()
        Me._Exit = New System.Windows.Forms.Label()
        Me.Proc_Name = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.DLLs = New System.Windows.Forms.ListBox()
        Me.Clear_Lst = New System.Windows.Forms.Button()
        Me.Remove = New System.Windows.Forms.Button()
        Me.Browse = New System.Windows.Forms.Button()
        Me._Inject = New System.Windows.Forms.Button()
        Me._Quit = New System.Windows.Forms.Button()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me._About = New System.Windows.Forms.Button()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Title
        '
        Me.Title.AutoSize = True
        Me.Title.ForeColor = System.Drawing.Color.DarkRed
        Me.Title.Location = New System.Drawing.Point(29, 9)
        Me.Title.Name = "Title"
        Me.Title.Size = New System.Drawing.Size(192, 21)
        Me.Title.TabIndex = 0
        Me.Title.Text = "CO4ER INJECTOR"
        '
        'Status
        '
        Me.Status.AutoSize = True
        Me.Status.Font = New System.Drawing.Font("Lucida Console", 10.0!, System.Drawing.FontStyle.Italic)
        Me.Status.Location = New System.Drawing.Point(224, 16)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(175, 14)
        Me.Status.TabIndex = 1
        Me.Status.Text = "- Waiting for process"
        '
        '_Exit
        '
        Me._Exit.AutoSize = True
        Me._Exit.Font = New System.Drawing.Font("Lucida Console", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Exit.Location = New System.Drawing.Point(496, 9)
        Me._Exit.Name = "_Exit"
        Me._Exit.Size = New System.Drawing.Size(23, 21)
        Me._Exit.TabIndex = 2
        Me._Exit.Text = "X"
        '
        'Proc_Name
        '
        Me.Proc_Name.AutoSize = True
        Me.Proc_Name.Font = New System.Drawing.Font("Lucida Console", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Proc_Name.Location = New System.Drawing.Point(12, 48)
        Me.Proc_Name.Name = "Proc_Name"
        Me.Proc_Name.Size = New System.Drawing.Size(148, 16)
        Me.Proc_Name.TabIndex = 3
        Me.Proc_Name.Text = "Process Name :"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Black
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Lucida Console", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(15, 67)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(189, 23)
        Me.TextBox1.TabIndex = 4
        '
        'DLLs
        '
        Me.DLLs.BackColor = System.Drawing.Color.Black
        Me.DLLs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DLLs.Font = New System.Drawing.Font("Lucida Console", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DLLs.ForeColor = System.Drawing.Color.White
        Me.DLLs.FormattingEnabled = True
        Me.DLLs.Location = New System.Drawing.Point(15, 97)
        Me.DLLs.Name = "DLLs"
        Me.DLLs.Size = New System.Drawing.Size(384, 210)
        Me.DLLs.TabIndex = 5
        '
        'Clear_Lst
        '
        Me.Clear_Lst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Clear_Lst.Location = New System.Drawing.Point(15, 313)
        Me.Clear_Lst.Name = "Clear_Lst"
        Me.Clear_Lst.Size = New System.Drawing.Size(189, 39)
        Me.Clear_Lst.TabIndex = 6
        Me.Clear_Lst.Text = "Clear List"
        Me.Clear_Lst.UseVisualStyleBackColor = True
        '
        'Remove
        '
        Me.Remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Remove.Location = New System.Drawing.Point(15, 358)
        Me.Remove.Name = "Remove"
        Me.Remove.Size = New System.Drawing.Size(189, 39)
        Me.Remove.TabIndex = 7
        Me.Remove.Text = "Remove"
        Me.Remove.UseVisualStyleBackColor = True
        '
        'Browse
        '
        Me.Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Browse.Location = New System.Drawing.Point(210, 313)
        Me.Browse.Name = "Browse"
        Me.Browse.Size = New System.Drawing.Size(189, 39)
        Me.Browse.TabIndex = 8
        Me.Browse.Text = "Browse"
        Me.Browse.UseVisualStyleBackColor = True
        '
        '_Inject
        '
        Me._Inject.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me._Inject.Location = New System.Drawing.Point(210, 356)
        Me._Inject.Name = "_Inject"
        Me._Inject.Size = New System.Drawing.Size(189, 39)
        Me._Inject.TabIndex = 9
        Me._Inject.Text = "Inject"
        Me._Inject.UseVisualStyleBackColor = True
        '
        '_Quit
        '
        Me._Quit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me._Quit.Location = New System.Drawing.Point(406, 313)
        Me._Quit.Name = "_Quit"
        Me._Quit.Size = New System.Drawing.Size(108, 82)
        Me._Quit.TabIndex = 10
        Me._Quit.Text = "Quit"
        Me._Quit.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Font = New System.Drawing.Font("Lucida Console", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(406, 97)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(73, 18)
        Me.RadioButton1.TabIndex = 11
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Manual"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Font = New System.Drawing.Font("Lucida Console", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(406, 135)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(97, 18)
        Me.RadioButton2.TabIndex = 12
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Automatic"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Lucida Console", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(405, 178)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(114, 18)
        Me.CheckBox1.TabIndex = 13
        Me.CheckBox1.Text = "Close After"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        '_About
        '
        Me._About.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me._About.Font = New System.Drawing.Font("Lucida Console", 15.0!, System.Drawing.FontStyle.Italic)
        Me._About.Location = New System.Drawing.Point(405, 202)
        Me._About.Name = "_About"
        Me._About.Size = New System.Drawing.Size(109, 105)
        Me._About.TabIndex = 14
        Me._About.Text = "About"
        Me._About.UseVisualStyleBackColor = True
        '
        'Timer
        '
        '
        'ofd
        '
        Me.ofd.FileName = "OpenFileDialog1"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.CO4ER.My.Resources.Resources.DLL__1_
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(2, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(21, 21)
        Me.PictureBox1.TabIndex = 15
        Me.PictureBox1.TabStop = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(524, 407)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me._About)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me._Quit)
        Me.Controls.Add(Me._Inject)
        Me.Controls.Add(Me.Browse)
        Me.Controls.Add(Me.Remove)
        Me.Controls.Add(Me.Clear_Lst)
        Me.Controls.Add(Me.DLLs)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Proc_Name)
        Me.Controls.Add(Me._Exit)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.Title)
        Me.Font = New System.Drawing.Font("Lucida Console", 15.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(7, 5, 7, 5)
        Me.Name = "Main"
        Me.Text = "Form1"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Title As Label
    Friend WithEvents Status As Label
    Friend WithEvents _Exit As Label
    Friend WithEvents Proc_Name As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents DLLs As ListBox
    Friend WithEvents Clear_Lst As Button
    Friend WithEvents Remove As Button
    Friend WithEvents Browse As Button
    Friend WithEvents _Inject As Button
    Friend WithEvents _Quit As Button
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents _About As Button
    Friend WithEvents Timer As Timer
    Friend WithEvents ofd As OpenFileDialog
    Friend WithEvents PictureBox1 As PictureBox
End Class

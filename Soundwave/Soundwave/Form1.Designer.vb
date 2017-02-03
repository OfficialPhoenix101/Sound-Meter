<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.VPB_OUT_Right = New Soundwave.VerticalProgressBar()
        Me.VPB_OUT_MASTER = New Soundwave.VerticalProgressBar()
        Me.VPB_OUT_Left = New Soundwave.VerticalProgressBar()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(250, 27)
        Me.Label1.TabIndex = 20
        '
        'VPB_OUT_Right
        '
        Me.VPB_OUT_Right.Location = New System.Drawing.Point(189, 29)
        Me.VPB_OUT_Right.Name = "VPB_OUT_Right"
        Me.VPB_OUT_Right.Size = New System.Drawing.Size(50, 415)
        Me.VPB_OUT_Right.TabIndex = 19
        '
        'VPB_OUT_MASTER
        '
        Me.VPB_OUT_MASTER.Location = New System.Drawing.Point(59, 29)
        Me.VPB_OUT_MASTER.Name = "VPB_OUT_MASTER"
        Me.VPB_OUT_MASTER.Size = New System.Drawing.Size(124, 415)
        Me.VPB_OUT_MASTER.TabIndex = 18
        '
        'VPB_OUT_Left
        '
        Me.VPB_OUT_Left.Location = New System.Drawing.Point(3, 29)
        Me.VPB_OUT_Left.Name = "VPB_OUT_Left"
        Me.VPB_OUT_Left.Size = New System.Drawing.Size(50, 415)
        Me.VPB_OUT_Left.TabIndex = 17
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(247, 447)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.VPB_OUT_Right)
        Me.Controls.Add(Me.VPB_OUT_MASTER)
        Me.Controls.Add(Me.VPB_OUT_Left)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents VPB_OUT_Left As VerticalProgressBar
    Friend WithEvents VPB_OUT_MASTER As VerticalProgressBar
    Friend WithEvents VPB_OUT_Right As VerticalProgressBar
    Friend WithEvents Label1 As Label
End Class

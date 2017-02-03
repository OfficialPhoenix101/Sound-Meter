Option Explicit On
Option Strict On


Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports VUMeter.SampleAudioAPI

Public Class Form1
    Private AudioAPI As New SampleAudioAPI()
    Private WithEvents Observer As New Timer() With {.Interval = 10, .Enabled = True}

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        VPB_OUT_MASTER.Maximum = 100

    End Sub
    Private Sub Start(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        IN_MasterScalarValue = AudioAPI.IN_MasterScalar
        RaiseEvent MicrophoneValueChanged(IN_MasterScalarValue)
        OUT_MasterScalarValue = AudioAPI.OUT_MasterScalar
        RaiseEvent SpeakersValueChanged(OUT_MasterScalarValue)

    End Sub

    <DllImportAttribute("user32.dll")>
    Public Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As Integer, lParam As Integer) As Integer
    End Function

    <DllImportAttribute("user32.dll")> Public Shared Function ReleaseCapture() As Boolean
    End Function

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, Label1.MouseDown
        Const WM_NCLBUTTONDOWN As Integer = &HA1
        Const HT_CAPTION As Integer = &H2

        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
        End If

    End Sub

    Private Sub Observer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Observer.Tick



        If Not (OUT_MasterScalarValue = AudioAPI.OUT_MasterScalar) Then RaiseEvent SpeakersValueChanged(AudioAPI.OUT_MasterScalar)
        If Not (IN_MasterScalarValue = AudioAPI.IN_MasterScalar) Then RaiseEvent MicrophoneValueChanged(AudioAPI.IN_MasterScalar)

        Refresher()

    End Sub
    Private IN_MasterScalarValue As Single = 0.0F
    Private OUT_MasterScalarValue As Single = 0.0F
    Private Event MicrophoneValueChanged(ByVal value As Single)
    Private Event SpeakersValueChanged(ByVal value As Single)
    Private Sub Speakers_ValueChanged(ByVal value As Single) Handles Me.SpeakersValueChanged
        OUT_MasterScalarValue = value
        VPB_OUT_MASTER.Value = (SampleAudioAPI.SingleToIntPercentage(value))
    End Sub

    Dim _SMOOTHTRANSITION As Boolean = True
    Private Sub Refresher()


        'speakers
        VPB_OUT_MASTER.Value = SampleAudioAPI.SingleToIntPercentage(AudioAPI.Value(SampleAudioAPI.Channels.OUT_Master, _SMOOTHTRANSITION))
        VPB_OUT_Left.Value = SampleAudioAPI.SingleToIntPercentage(AudioAPI.Value(SampleAudioAPI.Channels.OUT_LeftPeak, _SMOOTHTRANSITION))
        VPB_OUT_Right.Value = SampleAudioAPI.SingleToIntPercentage(AudioAPI.Value(SampleAudioAPI.Channels.OUT_RightPeak, _SMOOTHTRANSITION))

    End Sub





End Class
<BrowsableAttribute(True), DescriptionAttribute("Vertical ProgressBar.")>
Public NotInheritable Class VerticalProgressBar
    Inherits ProgressBar

    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.Style = cp.Style Or &H4
            Return cp
        End Get
    End Property
End Class


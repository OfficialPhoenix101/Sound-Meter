Option Explicit On
Option Strict On

Imports CoreAudioApi
Imports System.ComponentModel

<BrowsableAttribute(True), DescriptionAttribute("Inherits simple functions provided by CoreAudioAPI.dll")>
Public NotInheritable Class SampleAudioAPI

    Friend deviceCapture As MMDevice
    Friend deviceRender As MMDevice
    Friend devEnum As MMDeviceEnumerator
    Public Sub New()

        If System.Environment.OSVersion.Version.Major >= 6 Then
            devEnum = New MMDeviceEnumerator()
            deviceCapture = devEnum.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eMultimedia)
            deviceRender = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia)
        Else
            Throw New InvalidEnumArgumentException("Minimum Windows Vista requiered.")
        End If

    End Sub
    <BrowsableAttribute(True), DescriptionAttribute("Audio Channels.")>
    Public Enum Channels As Int32
        IN_LeftPeak = 1
        IN_RightPeak = 2
        IN_Master = 3
        OUT_LeftPeak = 4
        OUT_RightPeak = 5
        OUT_Master = 6
    End Enum
    <BrowsableAttribute(True), DescriptionAttribute("Returns the value of the associated audio channel.")>
    Public Shadows Function Value(ByVal channel As Channels, Optional ByVal smooth As Boolean = False) As Single

        Dim in_state As Integer = CInt(deviceCapture.State)
        Dim out_state As Integer = CInt(deviceRender.State)

        Select Case channel

            'MICROPHONE

            Case Channels.IN_Master
                If (in_state = 1) AndAlso (Not (deviceCapture.AudioMeterInformation.PeakValues.Count = 0)) Then
                    Dim peak As Single = deviceCapture.AudioMeterInformation.MasterPeakValue
                    Smooth_IN_MASTER.Input = SingleToIntPercentage(peak)
                    Return If(smooth = True, IntToSinglePercentage(Smooth_IN_MASTER.SmoothedValue), peak)
                End If

            Case Channels.IN_LeftPeak
                If (in_state = 1) AndAlso ((deviceCapture.AudioMeterInformation.PeakValues.Count >= 1)) Then
                    Dim peak As Single = deviceCapture.AudioMeterInformation.PeakValues(0)
                    Smooth_IN_LEFT.Input = SingleToIntPercentage(peak)
                    Return If(smooth = True, IntToSinglePercentage(Smooth_IN_LEFT.SmoothedValue), peak)
                End If

            Case Channels.IN_RightPeak
                If (in_state = 1) AndAlso ((deviceCapture.AudioMeterInformation.PeakValues.Count >= 2)) Then
                    Dim peak As Single = deviceCapture.AudioMeterInformation.PeakValues(1)
                    Smooth_IN_RIGHT.Input = SingleToIntPercentage(peak)
                    Return If(smooth = True, IntToSinglePercentage(Smooth_IN_RIGHT.SmoothedValue), peak)
                End If


                'SPEAKERS
            Case Channels.OUT_Master
                If (out_state = 1) AndAlso (Not (deviceRender.AudioMeterInformation.PeakValues.Count = 0)) Then
                    Dim peak As Single = deviceRender.AudioMeterInformation.MasterPeakValue
                    Smooth_OUT_MASTER.Input = SingleToIntPercentage(peak)
                    Return If(smooth = True, IntToSinglePercentage(Smooth_OUT_MASTER.SmoothedValue), peak)
                End If

            Case Channels.OUT_LeftPeak
                If (out_state = 1) AndAlso (deviceRender.AudioMeterInformation.PeakValues.Count >= 1) Then
                    Dim peak As Single = deviceRender.AudioMeterInformation.PeakValues(0)
                    Smooth_OUT_LEFT.Input = SingleToIntPercentage(peak)
                    Return If(smooth = True, IntToSinglePercentage(Smooth_OUT_LEFT.SmoothedValue), peak)
                End If

            Case Channels.OUT_RightPeak
                If (out_state = 1) AndAlso (deviceRender.AudioMeterInformation.PeakValues.Count >= 2) Then
                    Dim peak As Single = deviceRender.AudioMeterInformation.PeakValues(1)
                    Smooth_OUT_RIGHT.Input = SingleToIntPercentage(peak)
                    Return If(smooth = True, IntToSinglePercentage(Smooth_OUT_RIGHT.SmoothedValue), peak)
                End If

        End Select

        'ON ERROR RETURN 0
        Return 0!

    End Function
    Private Smooth_IN_MASTER As New AudioSmoother()
    Private Smooth_IN_LEFT As New AudioSmoother()
    Private Smooth_IN_RIGHT As New AudioSmoother()
    Private Smooth_OUT_MASTER As New AudioSmoother()
    Private Smooth_OUT_LEFT As New AudioSmoother()
    Private Smooth_OUT_RIGHT As New AudioSmoother()
    Public Property IN_MasterScalar As Single
        Get
            Return deviceCapture.AudioEndpointVolume.MasterVolumeLevelScalar
        End Get
        Set(ByVal value As Single)
            deviceCapture.AudioEndpointVolume.MasterVolumeLevelScalar = value
        End Set
    End Property
    Public Property OUT_MasterScalar As Single
        Get
            Return deviceRender.AudioEndpointVolume.MasterVolumeLevelScalar
        End Get
        Set(ByVal value As Single)
            deviceRender.AudioEndpointVolume.MasterVolumeLevelScalar = value
        End Set
    End Property
    Public Property IN_MUTE As Boolean
        Get
            Return deviceCapture.AudioEndpointVolume.Mute
        End Get
        Set(ByVal value As Boolean)
            deviceCapture.AudioEndpointVolume.Mute = value
        End Set
    End Property
    Public Property OUT_MUTE As Boolean
        Get
            Return deviceRender.AudioEndpointVolume.Mute
        End Get
        Set(ByVal value As Boolean)
            deviceRender.AudioEndpointVolume.Mute = value
        End Set
    End Property

    Public Shared Function SingleToIntPercentage(ByVal value As Single) As Int32
        'SINGLE PERCENTAGE = 0.01 TO 1.00
        Dim IntegerPercentage As UInt32 = CUInt(Math.Min(1.0!, value) * 100)
        Return CInt(IntegerPercentage)
    End Function
    Public Shared Function IntToSinglePercentage(ByVal value As Integer) As Single
        'INTEGER PERCENTAGE = 1 TO 100
        Dim SinglePercentage As Single = CSng(Math.Min(100, value) / 100)
        Return SinglePercentage
    End Function
    Private NotInheritable Class AudioSmoother
        Public WriteOnly Property Input As Integer
            Set(value As Integer)
                Me.Value = value
            End Set
        End Property
        Private Shadows Value As Integer
        Protected SmoothValue As Integer
        Private Const Decrement As Integer = 2
        Public ReadOnly Property SmoothedValue As Int32
            Get
                Return Fader()
            End Get
        End Property
        Private Function Fader() As Int32
            If (Value = 0) Then Return 0
            'create an average of last/current value
            Dim average As Integer = Math.Min(((Value + SmoothValue) \ 2), 100)
            SmoothValue = Math.Max(Math.Max(average, SmoothValue - Decrement), 0)
            Return SmoothValue
        End Function
    End Class
End Class
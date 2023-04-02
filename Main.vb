Public Class Main
    'Fields
    Private TargetProcessHandle As Integer
    Private pfnStartAddr As Integer
    Private pszLibFileRemote As String
    Private TargetBufferSize As Integer

    Public Const PROCESS_VM_READ = &H10
    Public Const TH32CS_SNAPPROCESS = &H2
    Public Const MEM_COMMIT = 4096
    Public Const PAGE_READWRITE = 4
    Public Const PROCESS_CREATE_THREAD = (&H2)
    Public Const PROCESS_VM_OPERATION = (&H8)
    Public Const PROCESS_VM_WRITE = (&H20)
    Dim DLLFileName As String
    Public Declare Function ReadProcessMemory Lib "kernel32" (
    ByVal hProcess As Integer,
    ByVal lpBaseAddress As Integer,
    ByVal lpBuffer As String,
    ByVal nSize As Integer,
    ByRef lpNumberOfBytesWritten As Integer) As Integer

    Public Declare Function LoadLibrary Lib "kernel32" Alias "LoadLibraryA" (
    ByVal lpLibFileName As String) As Integer

    Public Declare Function VirtualAllocEx Lib "kernel32" (
    ByVal hProcess As Integer,
    ByVal lpAddress As Integer,
    ByVal dwSize As Integer,
    ByVal flAllocationType As Integer,
    ByVal flProtect As Integer) As Integer

    Public Declare Function WriteProcessMemory Lib "kernel32" (
    ByVal hProcess As Integer,
    ByVal lpBaseAddress As Integer,
    ByVal lpBuffer As String,
    ByVal nSize As Integer,
    ByRef lpNumberOfBytesWritten As Integer) As Integer

    Public Declare Function GetProcAddress Lib "kernel32" (
    ByVal hModule As Integer, ByVal lpProcName As String) As Integer

    Private Declare Function GetModuleHandle Lib "Kernel32" Alias "GetModuleHandleA" (
    ByVal lpModuleName As String) As Integer

    Public Declare Function CreateRemoteThread Lib "kernel32" (
    ByVal hProcess As Integer,
    ByVal lpThreadAttributes As Integer,
    ByVal dwStackSize As Integer,
    ByVal lpStartAddress As Integer,
    ByVal lpParameter As Integer,
    ByVal dwCreationFlags As Integer,
    ByRef lpThreadId As Integer) As Integer

    Public Declare Function OpenProcess Lib "kernel32" (
    ByVal dwDesiredAccess As Integer,
    ByVal bInheritHandle As Integer,
    ByVal dwProcessId As Integer) As Integer

    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (
    ByVal lpClassName As String,
    ByVal lpWindowName As String) As Integer

    Private Declare Function CloseHandle Lib "kernel32" Alias "CloseHandleA" (
    ByVal hObject As Integer) As Integer


    Dim ExeName As String = IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)
    'Injector Code :
    Private Sub Inject()
        On Error GoTo 1 ' If error occurs, app will close without any error messages
        Timer.Stop()
        Dim TargetProcess As Process() = Process.GetProcessesByName(TextBox1.Text)
        TargetProcessHandle = OpenProcess(PROCESS_CREATE_THREAD Or PROCESS_VM_OPERATION Or PROCESS_VM_WRITE, False, TargetProcess(0).Id)
        pszLibFileRemote = ofd.FileName
        pfnStartAddr = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryA")
        TargetBufferSize = 1 + Len(pszLibFileRemote)
        Dim Rtn As Integer
        Dim LoadLibParamAdr As Integer
        LoadLibParamAdr = VirtualAllocEx(TargetProcessHandle, 0, TargetBufferSize, MEM_COMMIT, PAGE_READWRITE)
        Rtn = WriteProcessMemory(TargetProcessHandle, LoadLibParamAdr, pszLibFileRemote, TargetBufferSize, 0)
        CreateRemoteThread(TargetProcessHandle, 0, 0, pfnStartAddr, LoadLibParamAdr, 0, 0)
        CloseHandle(TargetProcessHandle)
1:      Me.Show()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DLLs.Name = "DLLs"
        Browse.Text = "Browse"
        Status.Text = "- Waiting for Process to Start.."
        Timer.Interval = 50
        Timer.Start()
    End Sub

    Private Sub Browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Browse.Click
        ofd.Filter = "DLL (*.dll) |*.dll"
        ofd.ShowDialog()
    End Sub

    Private Sub _Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Remove.Click
        For i As Integer = (DLLs.SelectedItems.Count - 1) To 0 Step -1
            DLLs.Items.Remove(DLLs.SelectedItems(i))
        Next
    End Sub

    Private Sub ClearLst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clear_Lst.Click
        DLLs.Items.Clear()
    End Sub

    Private Sub _Inject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Inject.Click
        If IO.File.Exists(ofd.FileName) Then
            Dim TargetProcess As Process() = Process.GetProcessesByName(TextBox1.Text)
            If TargetProcess.Length = 0 Then

                Me.Status.Text = ("- Waiting for " + TextBox1.Text + ".exe")
            Else
                Timer.Stop()
                Me.Status.Text = "- Successfully Injected!"
                Call Inject()
                If CheckBox1.Checked = True Then
                    End
                Else
                End If
            End If
        Else
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        If IO.File.Exists(ofd.FileName) Then
            Dim TargetProcess As Process() = Process.GetProcessesByName(TextBox1.Text)
            If TargetProcess.Length = 0 Then

                Me.Status.Text = ("Waiting for " + TextBox1.Text + ".exe")
            Else
                Timer.Stop()
                Me.Status.Text = "Successfully Injected!"
                Call Inject()
                If CheckBox1.Checked = True Then
                    End
                Else
                End If
            End If
        Else
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ofd.FileOk
        Dim FileName As String
        FileName = ofd.FileName.Substring(ofd.FileName.LastIndexOf("\"))
        Dim DllFileName As String = FileName.Replace("\", "")
        Me.DLLs.Items.Add(DllFileName)
    End Sub

    Private Sub Quit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Quit.Click
        Me.Close()
    End Sub

    Private Sub Manual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        _Inject.Enabled = True
        Timer.Enabled = False
    End Sub

    Private Sub Auto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        _Inject.Enabled = False
        Timer.Enabled = True
    End Sub
    'Handling the positioning, exit...
    Dim mousePos As Point
    Private Sub Title_MouseDown(sender As Object, e As MouseEventArgs) Handles Title.MouseDown
        mousePos = e.Location
    End Sub

    Private Sub Title_MouseMove(sender As Object, e As MouseEventArgs) Handles Title.MouseMove
        If e.Button = MouseButtons.Left Then
            Dim dx As Integer = e.Location.X - mousePos.X
            Dim dy As Integer = e.Location.Y - mousePos.Y
            Location = New Point(Location.X + dx, Location.Y + dy)
        End If
    End Sub

    Private Sub Exit_MouseHover(sender As Object, e As EventArgs) Handles _Exit.MouseHover
        _Exit.ForeColor = Color.Red
    End Sub

    Private Sub Exit_MouseLeave(sender As Object, e As EventArgs) Handles _Exit.MouseLeave
        _Exit.ForeColor = Color.White
    End Sub

    Private Sub Exit_Click(sender As Object, e As EventArgs) Handles _Exit.Click
        Application.Exit()
    End Sub

    Private Sub About_Click(sender As Object, e As EventArgs) Handles _About.Click
        About.ShowDialog()
    End Sub
End Class
'JUNK CODE DONT REMOVE THIS CODE BUT IGNORE IT *** THIS PART OF 
'CODE MAKE IT UNDETECTABLE SO THAT YOU WILL NOT GET BANNED :)
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'
'
'
'
'    |==\     /==|     /=====\     |--------\
'    |   \   /   |    /  ---  \    |   ====  \
'    |    \ /    |   |   | |   |   |   |  |  |
'    |  |\   /|  |   |   | |   |   |   |  |  |
'    |  | \ / |  |   |   | |   |   |   |  |  |
'    |  |  -  |  |    \  ---  /    |   ++++  /
'    ````     ````     \=====/     |--------/
'
'
'
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

Module CAPOJWVuIsaMvsQuKayNPjFowaPmiDyqWsLUxLEghNWbAYyhYEAyBIeStZQhHiLEtioDguvxVReCM
    Public Sub FfVXylenoucStWtPGjyfhRrMGmCccUjweBsapVFwtjj()
        Dim RXPcCIJRwQOYIBNTNWZNqhChEgEBNuGWRVQbTKXbIguZjNdNa As Decimal = 0
        Dim KYvigHTGvfGIOcNpoiNm As Double = 471
        Dim GxTRqfOaqdPRQmjdwltXGjOELX As ULong = 88671812
        Dim aMwUOrUZtitotnuMjRmbWobWxoCgvMsKhgEvxPHVRBYvKLDgqFQWRsGkjZVZpmcgpiHMjxNUJQJUP As Long = 3026343
        Do Until 725823570 = 3203
            Dim JbRonScfqJADjFYufpSDtuplDgoCNhBkLkYZGBEgOYrvLNsDBUdXTeiOkflHcaRIHOrxg As Int64 = 0
        Loop
        While 460786 <> 332103
            MessageBox.Show("hUxVPHJaPghJihRjvnJXnOIjHNyEZewwjBDOSaSrHKSuJdfebeKIfURTMOOCIFBji")
            Dim PfyVlEOgsmhyvRnBFcCAdMkXlkmSvkNqUllZFVvgBmDOdI As Integer = 6205526
            Dim ekTbpgVxrDlIqeVBOOYc As ULong = 4
        End While
        Dim HJuBpgYuDNqRltxaADqfWfRZctBaUgMubvRG As Long = 52150688
        Dim pSPUFeovkWcynmPoYPHrcRLDReRofqHyTpEAijKqnErLuJfgpuiEiMg As Int64 = 3
        While True
            Dim eHcqyFvqWawsXbfjwTxTOuJmlOiYUslmFHWZlmdteRXKBMHjSWLLagJixoFkVXVdoJmPFGxKoDnvXxlYGyFxgZLXxmbvgsYHvsVqZqeMXAWXsMLyCgeVRXwO() As String = {"fjZCstsqIuAsypXPoarvADuAtgsE", "RPSpDIwjwkvAsvNGdBuZekETgGgAeXwKdXbOyxjQhJGYTlfaDlxidkGfKxJvawEZnkVUTxGfRdrVd"}
            Try
                MsgBox("JPnTMOmGhNlcnuDdAgJioZoUAKFAcKneOMvVKqyGcBcouATBWAJVtfm")
                Dim ZXQEpDdMDYirlyMtbxmdoHspcVSQpAKvWkfCFCjjGUlXPA As Decimal = 4402
            Catch oUhIJMifhpnmPXgBekVvUkIBDwRAQpYnSMmIrGhoeFwiofyQnOTjvaPDLfEMthOqQyTDjXIXDgQOmZLXw As Exception
                Dim JFXBAxOVFJFNZQnOwYKIbYAihrjACeSpZINuOtGEhqrgSASObqFmSvhjEnTINOSaqOnRCsACSFZjfTQgZVXKYeWdZcqaawpSYRpDMALiqDUMVxEkXlhqqHBL As Integer = 8236035
            End Try
            Do
                Dim MQfKLrqXxxkjwGnKRGRJxDapivplJLQxWSRI As Integer = 15
                Dim NkRExpyJgJyveXjEhUdCCrtn As UInt64 = 2513
                Dim EuZJvsOcefvtOmjFCkxHuvcXYIVaJfePotZiDMTySgvBUjoEFwgLmBrIRCkmWnTxAfEUM As Boolean = True
                Dim pYnDxolBRgpuMaDclYndVtybqpmvjlGLNRjUjIUDVfIuefVBRtObUItvYMMIBoRfadhtphscEjHok As Double = 2
                MsgBox("bxScQKMFEFICwbmeGsDfkpyMFdTyhUvKuOONqyqWivAKoKyBBTPRBtqkyGXmqMXjJwHiNYnHCfvopnWtdSwNFdJAWcNjaDfVXPAr")
            Loop
        End While
        Do Until 3 = 56035
            Dim ccAWLiQSABFIWAbqjssSakJpvOAkXprtdtIiWegyQoIRuyqnjVWQZauqTKawheuKIKdcq As Int64 = 81
        Loop
        Dim XtsOfCInFOxCapAfQZRm As String = "TkkkcGMZoRFkBpUdxJbMBbrdCcPpMNyNfnYCTMWBVwIXto"
        Dim UvQyQJAUhfpDngskJEkmPGfaYJtKtkwwnPlpWnipBCofLmGZytrgVSROQpZHPoZvirOXtDOUVtlMRyOlCULArQbVwXYfNbaLcrwS As Integer = 62408
        Dim nyQSIebAuJmwoAIHyfvAKYCijqIbTJRYBMwkWYxVRwZvmKOfvAJYYqLOvhhQWygkqOubCXkZhaiMxGuZc As Integer = 2
    End Sub

    Public Function VedpsbXVqLUOaGCjmlUTDXDQ()
        Dim AFJvyXDaBnngWIlqONOgTJCsFmjmghVhnbKifPEsLLm As Long = 341675403
        Dim kPolXlkcVUQnPrZZSotYSbrEiJbZHTHjseQdOZyufLYNskPCMHXyWZBIWkCNJKVCMftkavKrpysHDdKwtYNyvIMTcHGdXkkUoWANqeaPweJXgsFrZETkwWFESZ As Integer = 517088378
        Dim pEJPqDcgpRCmBtLZvGTiyWrAVIlMxAUSxVYTaOcpSrsbVEkJrmTUxXOTMJsiQQhowjofVryhccQHd As Object = 558
        If 0 = 3 Then
            MessageBox.Show("UylwaITVgBDtQcetixrU")
            Dim ddaNUxNvPVQfvEakiQRZpnDC As Decimal = 2386
        End If
        Dim oPCZTSuXpCbWqXDmAKXMiitwoNnfXGOBkjkuPIovJkvuAavfWOxEbpR As Double = 65530
        Dim fCDLbxtlVmFLZtNHgr() As String = {"qiKNdcwvZHAXuFEqYcRMnejhQDspJoXqprkwxorTCyeHJHxXITfZC", "RDDFblGqkBFdvwhovYuQoEKgaxfenkSmjdnemfMFdYFrLCUqSnfMgsXMsWddGaDyhRtSIDCQMolPxRUYcnMyFGG"}
        Dim ENYIPPCgFaUxVFpntQokODfcmhYftmEakxOPbJymfJgtOUlcNvqYYGFAhuVhpWlSWtNXqZMkRBEFW As Object = 0
        Dim DXZnPyckKOgHfKoBlXDQuPsahXKhdlDKwbdh As Integer = 765881
        While 2513 <> 5
            MsgBox("jBjKZbAdfkWZTGhmWOjMOnEeQLFWPLDpRPPQcwIeEvyRdrrkFnGRebvuvEsNVcOTh")
            Dim ZJwShFiyAdEmxopRXfOsNBJdMLyFxjmSReTEWdrXxXQRDr As Integer = 1
            Dim BBTGXCjZtYRwPbvFIrYZPAgCkISadZRMnqimnLFpvgjfGeKVmCoSrWSATQVOiJvTHEJXrIRvlhciQAvFxvnakyURXiSWppHmjPYBVmddkrfKsjjTTPQlVVi As ULong = 82
        End While
        Do
            Dim mhoJdfFgZUHrShpboIRYAXsLhpYaGKkYhwZmINYfTMm As Integer = 754
            Dim mNkFNCwPYbdGjohDsv As Object = 661251
            Dim HDNObagOGdNMqrNJTGqwVkYSLtwYvarQVIwLmrueVuUHZMTcNCLPNDqVeuDhdHSflwqhtCWaLMbZPQrxaTLsOcDRQbgqnNBNc As Boolean = True
            Dim AwRFAkJEhoLWpvBBMQUdhHbXQWqZBUxpYGpQPsCIfbRusgjcbcLsfxOrUtoctjDNqUwjGklTOLwFpgdilNwXIEK As Double = 6
            MessageBox.Show("FLRAUrOBWpNtCaDMCKZTMiaLdZURSajUoSZcDmQGMqXWuYsdKSSUMoFVjFVuWviuFrEiBaIOsTGULEPEgpGyIPI33337")
        Loop
        Dim SuDjNXOodcgAPQTaNyCxDyXtxafBjAXXNnXCWIhnsMcGTaqxolEAVpKSWIklrMEcEImasRDFQOEqWRaWihMwuWBXdFe As Decimal = 0
        Return 4688
    End Function

    Public Function gGssVyXVulFYFYvlTOGreCAUHgOThgunRPIPYdNaYggSseNHnkojJAIdFXlokDnOqRGpQgMpAeSwVhbcTGfwqKeRGdKBrYpUs()
        Do Until 34 >= 710
        Loop
        Dim uWaRlLtGiQsJRQWFsiQCeQQKOH As ULong = 2
        Dim YqCvasmPCNZpSScmVaNBuGkHeTGTQUWZZZrKYmMydoJxsmFcvDyhijgSGuQHlSxgpAPsaROhONXFv As Object = 760312
        If 76407 = 7 Then
            MessageBox.Show("uDbptuskVjJxGEFNnMNdeLaiYfYyealoQOUYIHjMJMTRypiiN")
            Dim HvtddBdhCelbByOeIdJCVWgj As Decimal = 5
        End If
        Dim lHvyNITVWLpBQsdTPGMTtcsOWWHyDhHTwYAEcUhmUHQXMlJoophRlUjIOhcpATSoEVEqsYquXYymLUCkNSKNyohboqptqrlAnbSphRuQymtNVwvsfrchJFpOnbCCivEfl As Long = 12
        Dim dqihnVaocMlTMHRQhS As Decimal = 74431484
        Dim XRUyHyMdhXpOSBPaKVveeINkTbsa As UInt64 = 4046
        Dim rwIZLnQlWhxUuHaCBPgJGdbfTHCqkqAPyyHkitrNhJgbaBCQldOTOqFNVbFElYBfhhVuyoTGXSbvwyFSEkEGcgBHstPGonbIIBtseGQwKHuvPJQVWlyA As Long = 4372435
        Dim kcVdDcFkmmrbDWywFyRPcoPmwU As Integer = 6
        Dim ENYIPPCgFaUxVFpntQokODfcmhYftmEakxOPbJymfJgtOUlcNvqYYGFAhuVhpWlSWtNXqZMkRBEFW As Object = 36
        If 3 = 8820537 Then
            MessageBox.Show("OgwXYeoVyPTtFuScAlMAaStUsKqm")
            Dim ArobiAUMoGkxispspWVnsNRb As Decimal = 8
        End If
        Dim KXWLktbhqLPDPtqDYrfdHjtdMwMpiTRHqNJLUBMJRwyQiHGnKdXpGGZacrymhlxWtHMBjfhWeVLKjduFpYeFvaBjnihcCZbfxbWbtFDeuALjtOpywtwI As Long = 18
        Dim gmeqKbrNIweMWnGaMC() As String = {"kfROGDsDEyOOEdFRuLwSkVjqwugTumOIYeHQOIFrXIsxKjGiYkWMKcMOvZQLVYHQjFpZWoCmFEucbkfNnxtKQCxuRsToqsWxAcOEtgMihjGGhjlKtDWXewAg", "SZwUOqWhuJWJXqKhHQkjGPynjRWkOOFxQdpobuChULPbxfLilMCiBEgCaIjwTlTfDYrsBVFMOCPuAWoDFnxBMWw"}
        Do While 6 <> 6574001
            Do
                Dim KeEVnyunNsVpYulwKADLrvWJochIyCmPUQnSGfutOhfqmPgEKksTZ As Integer = 67
                Dim hYarJmJdYWPvaIKUoJxqmgtCIaaKaxsLxToWbnJFGNBuQp As Decimal = 4
                Dim ofBxunYsgcmqSexJe As Boolean = True
                Dim AUWvlbqviFKtVHYtWiIoFWRWaWcSPmWYFUIyrvYIORVQkLTNvrUVcdadCAUPxXfviknBxZCaJAanABkODPWX As Double = 48
                MessageBox.Show("UwFgHbpmjMmsFIlPumLQAMAbyg")

                Try
                    MessageBox.Show("ZuuPZWKswqGDreWTyDbKYstDlnvjovLMQaCW")
                    Dim tDMDroclUHsOmOpaIsNVYOhG As Int64 = 7532747
                Catch BTgcgXKHDeCIxjDrrtACJWKPAVFL As Exception
                    Dim lWvDufYuUqLtNxDBtXoanAYn As Single = 21820156
                End Try
                Dim nYVXbvrkeHFSMYYaQQkdCnIXngWPLDVExMQewsnsSMdZqdHygDrjLuUAPwiuYcYalBTAItcXcqKoKhgKYXhILls As Boolean = False
                Dim TLjlUwWXAtriAwSewNdQESyxvLjlStRLuxcyGMmGpRf As Integer = 127
            Loop
        Loop
        Dim jvEVgrkEZMPImBBZKdCBiNYhGPDAlNQkvroMHDuyhLGmBSywWjTRVonWXWPQnjSMvdLuLnRGhCSWGkfnQGdBsyP As Long = 60462
        Dim mZapEjJcsTKNoeTUfxBMmIyRtuSLvTtVcgoqiCedEHivpOivtoiTufadZJvIQwgZYojIpcldTWHeZDafMVRHjaZwGLR As Decimal = 24474
        Dim uDbptuskVjJxGEFNnMNdeLaiYfYyealoQOUYIHjMJMTRypiiN As Decimal = 8
        Dim ReVXtvMctFcpVbKXByJkIxsRTlAI As UInt64 = 2523
        While True
            Dim yfuvEupmJDNqQysyNtWGQDyHqHdfLXNoHBgUkrQyIEPWNlyVkXEYCXEvgmEwRKbkoYjPRXOUcWxMm() As String = {"ADsJIyyARIvqEgUlGNmArGGNwqwrgGaolBlFPKbNhdEcurEEWbDJhjjtwfMQBGSLhHFDtOVvkYSOBdSEmsVECQHBQrIjwLLaK", "esybgcaFgvVYLNSZfMJcvJNIBRHGSbKANmSqYbIBlbPpdpQFWZeJEraVeqaOUUmEkVKXt"}
            Try
                MsgBox("xVaBOIcIpJIKZSkeimGBihnV")
                Dim hlWMIcsRHrsqRqXsRgMP As Decimal = 16
            Catch nfmDEmXEwvvaVHrHhbZXvNvSIqhIsNLQckNvpwjRbKTJcNFxIMxqSqYaiUuMEPKijfwjsvRkqIvkslptljygPUxlMmMVCXNLykZwPUFMuN As Exception
                Dim OotRaGFrdwNPgyQfbeejIMlrlyZtefchBYDXdgNWYjmRWZ As Integer = 1444
            End Try
            Do
                Dim xRQoQakusKwCwpRDdEcVkQmWFhVKFEavHMnWosMnIscWjt As Integer = 1
                Dim HNYrJpEImIxDORtbUXFnUJecPZYDGIGHjDkd As UInt64 = 1650
                Dim WGuOMfYrTVfwJLbWaZEUdsbGHTepRYsSeekBqCDWkHi As Boolean = True
                Dim tvokakqTDujwytPokflpGuEOSmFdKIHGsEFneONuHWuadQgcJyhFZEcxqZRneNItmcDunaJWRphjAcjCFbTLaBgoaHwEvYQtNAmAMknioLmiVsxOoPhCswT As Double = 85702
                MessageBox.Show("jXGnDXhQOpHbeXPVyC")
            Loop
        End While
        Dim OcnFnhEsNrYPKvwGGnoAjfPOGQXvXPfvkqPLEXdpflm As Decimal = 561
        Dim THjvEeIBdcyxeeMxwOjI As String = "fJAmdQNtoiHKHBkocKxOimGpXRwmZOcGUpvsORavVdnhUO"
        Dim scFFrlwTwPVIWIoUutCa As Double = 421686
        Select Case True
            Case True
                Dim uKqhnjpmeFsrumAmgGRxYhxnQfdW As Object = 73407
                Try
                    MsgBox("OZtUYkjuaIjdfMfRNSpqmPOfvVDHVhtkdppqjApmwvkmfGfkpCKSScS")
                    Dim DEtxJtfbblkieIVdbBWvvctEcEZgfxaxbDrOaYyOdnSWQHYxquAvMvXZKoVFTyvcLmGbPHCCBIRHKXnUeaojxdleeSDlDtteq As Int64 = 570
                Catch nEgYSJqjCbrShWfYKGUdbPlaUH As Exception
                    Dim mbYyiZVgJFDAOWjvf As Object = 534160
                End Try
            Case False
                MessageBox.Show("WFouhkpnDKGkGHaOnUsvmsOCCDhoXCooCEEBBHbxyHPXwxhnuVQgUXfcplseCAPAURhCDYNfrtvedqFDUssC")
                Dim ucOlZuiCLbJdLJuGMWIKsRyuRYIjFKcINMOwnaBdFIOxLHnhZDReREPcdTUBmyvAyBPxt As Integer = 2
        End Select
        Return 28
    End Function

End Module

Module pfriOrCLADXtIXDOSjMKFUUGQYiseIDKcBOltcbosLSvMuJXaLouyvjVLlSpIIgrANRGJRToHXRsBXsVOKSTTHKfQrZIKlfjeauy
    Public Function dqeHqKavwQlxMyvEXU()
        While True
            Dim AMCWnrrvjwLBUtvpGRoLvfPFugPJBPkUgPtVMTcHbHAavWhmUXgIVLAUvvbHbhuTLqTFAZdmohjofoJYy() As String = {"mEbrsOwQSEHODMJEsHWjcCBxfwAjUCFPIvqMrCaUmLPmtChaHfqAjsBjCcMuONvQyqFHUwMqfBvRmOEdPYbnKEeOtGJOpfFZOxmCvaFKSA", "nZHMAoTqLPNHZCKWYxFxmGmljrJk"}
            Try
                MsgBox("fvMxNMiDCFrMBIYtNbGfeDdqnSoXLUfbHmbHUFeFUkQUCuZvfbiqWDicRVJtXLrhZQFNgRZHvAKslIosrrqjknEDExoxujgJrhuk")
                Dim EnUpsbBEHjcdjRpJfuGo As Decimal = 7712184
            Catch VRpqLONFqeYTkeASlwCYCpelrxYTvwyFXlLdAFJQrQZAxpCufEJYvtbflDyNpsIte As Exception
                Dim POUYQToVFCOeHuoniYMHbqwkFpUOmQdvgdddnjhZTagjcrCeeKumlqeYQjSayainXRwdQLyxplnBJMHNnojcUpJoxEMERHasrrMLtXNSKwwADcOEcFlJpGSg As Integer = 4
            End Try
            Do
                Dim AbuvbXqveZJRBDyijXoNPVcQUDqiJPygxIdhqhHsQTGbkysOeqqUvaYeydbXfIlPabSrZQqBQvvvOBfUJcDRXdkXjxiMGjNIhqBIgAnOyoEAdcPwxGNDETqvpt As Integer = 74427
                Dim QIZmZVIZBeLVMFxTkNAvTtaHREdHPlmUeAppHQqwlJyjZAuuDHbuxdxWhRVUrBKOqqqMP As UInt64 = 114658641
                Dim wpZBCsQAZaidRKyWPWPJrJcWBuPXflWdTdXgaXvFTvhqNpqDEJjFEBlnNCyHdsUmEFJnm As Boolean = True
                Dim ReVXtvMctFcpVbKXByJkIxsRTlAI As Double = 6400813
                MsgBox("dptJvYCpoONVYhYPPhaVghZaeAWqoPWxWcemCPlCeVhmrmHTIMyvKSsvPSevBLBYqPyvDlNAuXqLAmirrChbQamEiiX")
            Loop
        End While
        Dim KlCNZoUqAHfKMJFowXQYyyflkyGQHJdTcObjgnSKAHC As Long = 717
        Dim MulMYefoUXmlLATFHl() As String = {"qePqKMuHCNwYxuonTilSuScylUakTYguUIhCcNPPLDpjlD", "yxuvhBNaIpJbfpZstrIMyCgCDZLcJQhGlOLRiVKbUXdPebycJGehZopwRrsIpkQrICpQdHKYggqbXRXaZhSDlyg"}
        If 664210 = 103662 Then
            MsgBox("QsFqdojosWicwsjfRNhaytodECfNOuZEgtLDkLMksNWetEjNC")
            Dim jjmjbKNxcjxJOAKUgyQNgeTg As Decimal = 10
        End If
        Do
            Dim dsDWjOGTvWYVrUitnHyUZFiMet As Integer = 88643
            Dim PLFPckuuKTqGsAinLi As Object = 73407
            Dim msVHmSbnHVoFnmOaMwmbFDBvXDcYuQrFvWDmpOhWxJWBewSPnQWaIpKi As Boolean = True
            Dim lufZoiQordTuCxpDsHSrJLISFaDwSLUBkpfsLOVaeAmGneJgsftsAkLZlNTNeSWgISJLRwrdkqMIYCeaQWElRaA As Double = 767
            MessageBox.Show("bhUMWbpavmQJddlSNxcKSjWUVbcYuTQPaMNpTuBqriYYIixIthWLOecrLyptxRfbVBiRPuyRvkSiKwLldTdMwEA2041")
        Loop
        Dim SJiMZakFOPWrEhVQcZcOASgwhqGIHCemPAbcaMmaSJe As Decimal = 484
        Dim jILRTBuohZNblEfNkGnAgLKoAvnXZMgoRFeerJoJrkxrHISHr As Decimal = 503
        Dim KIavnCnwKxWpKOqeyWBh As Double = 5
        Select Case True
            Case True
                Dim FwWUPRkgERHrEEWsEAFTgDLtrhOdkefjhEkUouMnSpdrLnJIkplvFHjCkcchDHuZlWKASCDJnRBqYTiEXFrtridaTiiMRd As Object = 821
                Try
                    MsgBox("XGMDZNyQsPcfxhPGnEPEYYAHHyjxjICQGIfsVfkdnmyvRdgxantySII")
                    Dim mUowQZVyEJgDQoTRjHeXgyoVlskp As Int64 = 7052431
                Catch UTMBCbqMgJPOLmahFXaWXPXoAD As Exception
                    Dim oOiZMGAabhJQEHDjR As Object = 3426288
                End Try
            Case False
                MessageBox.Show("jjQcJFTKfZpuelZjnl")
                Dim wpZBCsQAZaidRKyWPWPJrJcWBuPXflWdTdXgaXvFTvhqNpqDEJjFEBlnNCyHdsUmEFJnm As Integer = 6
        End Select
        While 266 <> 6
            MsgBox("UhwjwmXieokHGuLqMNmwAAvWCnbCgoTQUJjwRKJWqHAPnGrrpENqWVAUlagIKVBpVTmvLOZCEQGryfhvvjLcbbtyJPKqcALUGKtUTufaeK")
            Dim VippgJRynReabUjMVDOmVBTJoZODWTVUZbyjaATBdWvOXd As Integer = 5
            Dim MTvINiLQHPtmlJqmcdFFmjTbHetsqcsatLFsPAyROMgHDncCTTbKbsAcATBhoRYeVSsaEwWcenpaCMYSBqYXBhy As ULong = 5
        End While
        Dim AUntDVBxPgOmpKtTjgUqykGWEQSgSPiHgKaxbsbfwHnadkkihMHkxBgPOQolCUeyxHFfivrpEnsrvesTeixFIwJ As Long = 76876021
        If 63 = 125 Then
            MsgBox("XhTAbWsVIrrGhApWMaSrEMQRJlmZpDTTjNEhVLlbTKegKN")
            Dim CDaTnFTMqifKlkYDWKGdjNBe As Decimal = 1
        End If
        Dim qkhUUCJuEmDJHuDZWVZf As Double = 2235247
        Dim UpBFmQwwtNIUwNiVykuAFXTsHvLgcoRlQJfLBNKjlvJitiJwuqJDWAmwWHxqFPUFvVDmgWTlZKOIOjxYm As Integer = 5
        Dim ivMARFELTALBoMFrgCOHfnxhxbnQvvwJDWkXnaaRyJAbFBvjpTXMpIauxdCoQnOXCBwRbhDFbvHWm As Object = 1
        Do Until 134 = 533
            Dim stgCTBSAgZcJXoubRByoEMLipAVTVhvRinCpFUjJlgakOtBaOolUSyfghxMSexdycKajl As Int64 = 42
        Loop
        Dim UygXOFZJTsKPLkatxbxPpTxYBauTyPHJfDXCjQFSTvcYpyyBRmmenepBBIWnJFKDkAjHNXKhVfutp As Object = 830
        Dim COhQquVunHBVSjuKnEFCVLJTYCXqRLnHhumoTBQBHPiIxuVkkGlokFGpEOrnWdccStmWwFqmnDtFbBUdgmGLYyVcnvkFIbdclGoD As Integer = 17
        Dim POIBMiRFOSCyLRAYSercUuQhuayNlmdgefCR As Long = 2
        Do While 21 <> 5
            Do
                Dim TvUPTGYQvJtagYsMmFxQnkOJirrmIIUWdHOkiYfqYjZqemmsgalGSXPhgKfbNlImnWwZKIdNZGrELXevEyFhVOE As Integer = 38
                Dim nfedKcfYblEaTIjZYEGwNVUJ As Decimal = 5
                Dim pdAEcuWYkpgiHHAyekkHLOBZhrjLScKgBhcwgWSnKRnxvPQfrxuJlEJ As Boolean = True
                Dim GSUcEEOeVSQvomdGPC As Double = 6
                MsgBox("WiqkwWlotGQSgKuxR")

                Try
                    MsgBox("ukRkTIVFvpwnJIQKOtSQGMTUkGXPbICTIp")
                    Dim PiaIWarqdGKdNGWlCGFHGvrMtFHqSOksQDEYJyTeECbScvgfDmTniRVTpONQSBYOUQMKCwbClaDDTYjxXamf As Int64 = 164826
                Catch aDJBmwYqNedXdeMPRfoB As Exception
                    Dim cNXBWfDZTJmBDrvZIrhntLFF As Single = 8
                End Try
                Dim qGgtkwhpsYHSPLpSVFTXnwZWPkCMTqYoaAascAffTSBsrBdswsXIYlwsgKdAuUVByRrPjwpUZnEUvgdOGIPaTQQ As Boolean = False
                Dim jjQcJFTKfZpuelZjnl As Integer = 18
            Loop
        Loop
        Dim YyeskZMyFeThZDkGTuBGqhfDaLRU As UInt64 = 17
        Dim XluKwemkYpmWLUssFjhMVPqNeqUuNvpUsEKWZwFnoxL As Long = 5545
        Return 7
    End Function

    Public Sub AUntDVBxPgOmpKtTjgUqykGWEQSgSPiHgKaxbsbfwHnadkkihMHkxBgPOQolCUeyxHFfivrpEnsrvesTeixFIwJ()
        Dim mTffGQfiRtvYvtcYEbKbmkRCCiALaFEUPpHBULJluKCvcCArxAeoJpMCCddWQUHLpaJGlaDrSYfbhUYyiHMaZOLYdFyqhlUZDpGBrtGWvmWjJjTgEdnJHfl As Integer = 322367
        If 22 <> 5030310 Then
            MsgBox("vrZmDxJtXvPIrVwuQRchIMguRWmd")
            Dim KKnMgkPeVFljRkWbroNONqBD As Double = 1
        End If
        Dim xPibmTWdsGMZOmOrnxylooFcNDIurkQLhZiWSrfENud As Long = 7
        Try
            MsgBox("tcfpsbSkOKWScAkbVWOswGPx")
            Dim hSREqwmEXPdAmxyeBcMykhUkwyILTsOfvmMpjHAlwpILVnPtiNkkxgCywgHrnbkeAxDylIusbZBtYIOJiObFHdg As Int64 = 847
        Catch NFqLGeaadvtjyAOiJuxyTKDWxKdiVOPcXsYDaqFbVHy As Exception
            Dim VsTYfxcxrtXlxcQRNF As Object = 261
        End Try
        Dim dAnaKBPITSakTNxXkFjcbHanFfFFkGPOKrjoPZoWYPsxphsmvsEFajyVZAVdvJWMC As Boolean = True
        While 721 <> 6
            MsgBox("qoAlMjPoqQiHGDJQClvferWiDZwdXhBZmbRYuwwsDqQOAVMwUONpwORxSRilllRJOITdljVEnCbmbIigNAIJTLbYKSGdOLHRZGAqBiKjsI")
            Dim qXlHgqXgxyYltwKNLPPBcNRbisAstqfOGpVWmWbpcVPCGq As Integer = 2
            Dim mlmtRoRXuUAMGAgHRvru As ULong = 30841
        End While
        Dim aTIhPkfndigvRcgohxXPSkKcrmVoRRTesPYwIvxAXUlDoajdQnEakFFpIfobPpVMOBCCEifkyWqxy As Object = 3
        If 331 = 7402333 Then
            MsgBox("ApZoKRjiHFrcxpklTjLO")
            Dim BselTuuyiXbvlZPuJfQUQYoP As Decimal = 814366
        End If
        Dim pWwQOSAnKdtNWQLMPPMq As Double = 370083244
        Dim cxNMbvbiPVWMTKqcpy As Decimal = 47
        Dim ZFgAMwOsCgmUQmqZPfRCrdYaPDeu As UInt64 = 7
        Do Until 3 = 7022
            Dim JBHAoVxgZPhiLaQIiProSWNBAUCIDLMsDHtTRxRroLKDdbgWTfkPUemlnFAUTpcODneDf As Int64 = 16356
        Loop
        Dim TiFTjlVOUKwiLcMBYmYS As String = "iWmHrYfqWjLssPPwGoqEpJRLClorQcmfBsxSOVfACxCsSAmDcieheptZEaEGjnatvfrAQwikJpCrHYsCykOgxFBKxYNmRYQOWIwmHlmeAsYnnBRfhCHxYsvqegBriuSjZ"
        Do Until 1 = 346287484
            Dim GVvYBRSRAJAgMEMXO As Int64 = 6152
        Loop
    End Sub

    Public Function EatGKbRMqluAdrOvvMWekQBxdATwhhXfvnuXxdOCgZauRV()
        Dim RhygTGMZqjLSKyxmrNtuWIOyJeOsPTBbWAbmYVIWrVRoyuAuKrNWLHqITyXnqTKXhiuJWLOWFWCBqZANeShWFePPITTYwTeYKsGQApXydqRnEpRsyKJY As Long = 251860
        Dim ujodwcwpYVUNSyXZhj() As String = {"BInZasFirKiErjWQOZrRGaDDEvMYamqdQfIjIKoFybBblYroWZRkeRoaEjVdfgIETFnMalHDkmReYbReDXBBVGxxqgfXnKYSSwHfkfNkRNyrAJCbSpaIfGiM", "jZNCDpBqFfJZxuxnvmdfAJmTCGMSUiSIYHVqdgRyojFMrpJOQLxKZVVamtyvdhWWNAApZHJOUepPEZaMvwCIjHh"}
        Do While 454788 <> 81812
            Do
                Dim pIYOWVffKlETpTrUNrPmYcoolbaIjIZGwYAWDTOkfPJhrUYlTjEPbwOMSjcttXvBLxVRSURsddGJSHwQDooyEKdAKmESqTqISIVsyHSxSKkxqEtLmMqEhvwATVGfQx As Integer = 25041605
                Dim anVtPuZhFwHHtjGRnwjUgrSvYgOZMsCDtODphcpCELZohrJNGwkOCAN As Decimal = 6
                Dim eKwXPfUbXXxeGNTctOumiQNvCklOScCEtIIl As Boolean = True
                Dim lIUdYaLOOwvxrODfQfkWHjoZaBxxgpJIHNNMwDcbMxRuxxmPnYlnIVtCTtCXbMhXmmqdAqiMCJKJRsbcNiKH As Double = 584471
                MsgBox("ZQItfNgydcHvEJkCIYJfXlBEQDloDYSgTQfdIPdaOWtTsCsjKwrasQEnODccaquFJZcndGtruiQAqhLOJCeGfqmCLrPFlAyXboJS")

                Try
                    MessageBox.Show("imayDtqBCfeQSArZuUwdOXOVPMXGLToAQnphTUlFiRYtosuPbZWIUGrKYEipWjphqdydJJGZiqtwyUgWRHdkvff")
                    Dim dAnaKBPITSakTNxXkFjcbHanFfFFkGPOKrjoPZoWYPsxphsmvsEFajyVZAVdvJWMC As Int64 = 527886478
                Catch lDEHuEIgjTTdcckmWTZNVNeCpgxe As Exception
                    Dim pMolsGwdrZFLlNrHJxoAZSEp As Single = 687454
                End Try
                Dim qaEHcyZjtjFvoTkeClVVuqxRUIyjhpJYfmRkFMbedjcyuxTFjeqrbBLAcllTypBDZfQCBAZpKprLOxVWotyDyRw As Boolean = False
                Dim EigZkaqGjDQNChhDAJuNKROwhepufZqCLmIQFZjfuMV As Integer = 6
            Loop
        Loop
        Dim RImNxiMLMXrhNpDCvChC As Double = 11
        Dim TUhupNEKhoBGiflaJhKUPPoXwLmabnOprRatUuOWZWnXQqGLnpByIJADfKweZcitaLNQErFOTmtsbQHGEbllmPghQXN As Decimal = 65686
        Dim aokrZihsHPTWkRMCIDGDopuvrNAkOnuvlmoFPvsCBtagjseKB As Decimal = 1
        Do Until 8 >= 73451
        Loop
        Dim hfZUaFXFmbwOQEdRnFArfmpyTK As ULong = 1
        Dim DBQWbsBmpZIxIpxRDgMhIeqPDAMtdPQwwPAAGhGihEQiudaNoMUCZdWYJBTagHJaJLBrpwKVacAEO As Object = 8
        If 5812 = 70 Then
            MessageBox.Show("HkKSCsWEalaJVZwveuChftFAvglXKjySSLHKPkeAoyKZlbXmY")
            Dim WrbYXSTEQGccRivEVqPhFFBr As Decimal = 31662
        End If
        Dim HyOgmKRkXsyfYahlTRsZ As Double = 5
        Return 50303
    End Function

End Module
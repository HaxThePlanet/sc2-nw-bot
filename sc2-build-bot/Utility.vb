﻿Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Threading
Imports System.Security.Cryptography
Imports XnaFan.ImageComparison
Imports Constants
Imports System.Text
Imports Microsoft
Imports IronOcr

Module Utility
    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Private Function VkKeyScanEx(ch As Char, dwhkl As IntPtr) As Short
    End Function

    Public Declare Function SetCursorPos Lib "user32" (
          ByVal x As Long,
          ByVal y As Long) As Long

    Friend Function GetKeyCode(ByVal c As Char, Optional ByVal KeyboardLayout As UIntPtr = Nothing) As Short
    End Function
    <DllImport("user32.dll")>
    Private Sub mouse_event(ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)
    End Sub

    <DllImport("User32.dll", SetLastError:=False, CharSet:=CharSet.Auto)>
    Public Function MapVirtualKey(ByVal uCode As UInt32, ByVal uMapType As UInt32) As UInt32
    End Function

    Public Const MOUSEEVENTF_MOVE As Integer = &H15
    Private Declare Function GetCursorPos Lib "user32.dll" (ByRef lpPoint As Point) As Boolean
    Private Declare Sub keybd_event Lib "user32.dll" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
    Public Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)
    Private Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Integer, ByVal uParam As Integer,
    ByRef lpvParam As Integer, ByVal fuWinIni As Integer) As Integer

    Const KEYEVENTF_EXTENDEDKEY = &H1
    Const KEYEVENTF_KEYDOWN = &H0
    Const KEYEVENTF_KEYUP = &H2
    Const VK_Shift = &HA0
    Public theProb As Integer
    Public xmin As Integer
    Public ymin As Integer
    Public xmax As Integer
    Public ymax As Integer
    Public theCenterline As Integer
    Dim objectCenterline As Integer
    Dim lastGlobalWidth As Integer = -1
    Dim highestProb As Integer = -1
    Dim lastObjectCenterline
    Public Sub MakeUnit(firstKey As String, secondKey As String)
        My.Computer.Keyboard.SendKeys(Keys.Escape, False)
        My.Computer.Keyboard.SendKeys(firstKey, True)
        ResponsiveSleep(250)
        My.Computer.Keyboard.SendKeys(secondKey, True)
        LeftMouseClick()
        ResponsiveSleep(250)
        My.Computer.Keyboard.SendKeys(Keys.Escape, False)
    End Sub

    Public Sub BlinkSCV(x As Integer, y As Integer)
        My.Computer.Keyboard.SendKeys(Keys.Escape, False)

        My.Computer.Keyboard.SendKeys("2", True)
        ResponsiveSleep(50)
        My.Computer.Keyboard.SendKeys("G", True)
        ResponsiveSleep(50)
        SetCursorPos(x, y)
        LeftMouseClick()
        My.Computer.Keyboard.SendKeys(Keys.Escape, False)
        ResponsiveSleep(50)
    End Sub
    Public Sub MoveToBlackTop()
        Dim mousePos = GetGlobalMousePosition()
        SetCursorPos(mousePos.X, -500)

        ResponsiveSleep(1000)
    End Sub
    Public Sub MoveMouseToCenter()
        SetCursorPos(960, 540)
    End Sub


    Public Function OcrScreen() As Integer
        Dim convertedResult As Integer

        'take screen        
        'TakeAreaScreenshot("processme.tiff", 80, 60, 1490, 0, 0, 0)
        TakeAreaScreenshot("processme.tiff", 80, 60, 1490, 0, 0, 0)

        Dim Result As String = (New IronTesseract()).Read("processme.tiff").Text
        Result = LTrim(RTrim(Result.Replace("=", "")))

        WriteMessageToGlobalChat("Minerals:" & Result)

        'if no result call again
        If Integer.TryParse(Result, convertedResult) = False Then
            ResponsiveSleep(1000)
            OcrScreen()
        End If

        Return convertedResult
    End Function

    Public Sub TakeScreenShotAreaRec(file As String, width As Integer, height As Integer, sourceX As Integer, sourceY As Integer, destinationX As Integer, destinationY As Integer)
        WriteMessageToGlobalChat("taking screenshot area")

        Dim printscreen As Bitmap = New Bitmap(width, height)
        Dim graphics As Graphics = Graphics.FromImage(CType(printscreen, Image))
        graphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, printscreen.Size)
        printscreen.Save(file, ImageFormat.Tiff)

waitagain:
        If IsFileUnavailable(file) Then
            ResponsiveSleep(100)
            GoTo waitagain
        End If

        WriteMessageToGlobalChat("done taking screenshot area")
    End Sub

    Public Sub MoveToNexus()
        My.Computer.Keyboard.SendKeys("0", False)
        ResponsiveSleep(10)
        My.Computer.Keyboard.SendKeys("0", False)
        ResponsiveSleep(10)
    End Sub


    Public Sub MoveToScv()
        My.Computer.Keyboard.SendKeys("1", False)
        ResponsiveSleep(10)
        My.Computer.Keyboard.SendKeys("1", False)
        ResponsiveSleep(10)
    End Sub

    Public Function WriteMessageToGlobalChat(msg As String)
        'Dim I As Integer

        'msg = msg.Replace(":  ", Nothing)
        'msg = msg.Replace(",", Nothing)
        'msg = msg.Replace("!", Nothing)
        'msg = msg.Replace(".", Nothing)

        'msg = msg.ToLower()

        Debug.Print(msg)

        ''open chat
        'KeyDownUp(Keys.T, False, 10, False)
        'ResponsiveSleep(500)

        'For I = 1 To Len(msg)
        '    keybd_event(VkKeyScanEx((Mid(msg, I, 1)), 1), MapVirtualKey(VkKeyScanEx((Mid(msg, I, 1)), 1), 0), 0, 0)
        '    keybd_event(VkKeyScanEx((Mid(msg, I, 1)), 1), MapVirtualKey(VkKeyScanEx((Mid(msg, I, 1)), 1), 0), 2, 0)
        '    ResponsiveSleep(10)
        'Next I

        'KeyDownUp(Keys.Enter, False, 10, False)
        'ResponsiveSleep(1000)
    End Function
    Public Function GetLoByte(ByVal value As Short) As Byte
        Return BitConverter.GetBytes(value).First
    End Function
    Public Function GetGlobalMousePosition() As Point
        Dim pt As New Point
        GetCursorPos(pt)

        Return pt
    End Function




    Public Function sendMessage(msg As String)
        Return Nothing

        msg = msg.ToLower

        'bring up chat window
        KeyDownUp(Keys.T, False, 1, False)
        ResponsiveSleep(250)

        For Each c As Char In msg
            If c = " " Then
                KeyDownUp(Keys.Space, False, 1, False)
            End If
            If c = "." Then
                KeyDownUp(Keys.OemPeriod, False, 1, False)
            End If
            If c = "," Then
                KeyDownUp(Keys.Oemcomma, False, 1, False)
            End If

            If c = "0" Then
                KeyDownUp(Keys.NumPad0, False, 1, False)
            End If
            If c = "1" Then
                KeyDownUp(Keys.NumPad1, False, 1, False)
            End If
            If c = "2" Then
                KeyDownUp(Keys.NumPad2, False, 1, False)
            End If
            If c = "3" Then
                KeyDownUp(Keys.NumPad3, False, 1, False)
            End If
            If c = "4" Then
                KeyDownUp(Keys.NumPad4, False, 1, False)
            End If
            If c = "5" Then
                KeyDownUp(Keys.NumPad5, False, 1, False)
            End If
            If c = "6" Then
                KeyDownUp(Keys.NumPad6, False, 1, False)
            End If
            If c = "7" Then
                KeyDownUp(Keys.NumPad7, False, 1, False)
            End If
            If c = "8" Then
                KeyDownUp(Keys.NumPad8, False, 1, False)
            End If
            If c = "9" Then
                KeyDownUp(Keys.NumPad9, False, 1, False)
            End If

            If c = "a" Then
                KeyDownUp(Keys.A, False, 1, False)
            End If
            If c = "b" Then
                KeyDownUp(Keys.B, False, 1, False)
            End If
            If c = "c" Then
                KeyDownUp(Keys.C, False, 1, False)
            End If
            If c = "d" Then
                KeyDownUp(Keys.D, False, 1, False)
            End If
            If c = "e" Then
                KeyDownUp(Keys.E, False, 1, False)
            End If
            If c = "f" Then
                KeyDownUp(Keys.F, False, 1, False)
            End If
            If c = "g" Then
                KeyDownUp(Keys.G, False, 1, False)
            End If
            If c = "h" Then
                KeyDownUp(Keys.H, False, 1, False)
            End If
            If c = "i" Then
                KeyDownUp(Keys.I, False, 1, False)
            End If
            If c = "j" Then
                KeyDownUp(Keys.J, False, 1, False)
            End If
            If c = "k" Then
                KeyDownUp(Keys.K, False, 1, False)
            End If
            If c = "l" Then
                KeyDownUp(Keys.L, False, 1, False)
            End If
            If c = "m" Then
                KeyDownUp(Keys.M, False, 1, False)
            End If
            If c = "n" Then
                KeyDownUp(Keys.N, False, 1, False)
            End If
            If c = "o" Then
                KeyDownUp(Keys.O, False, 1, False)
            End If
            If c = "p" Then
                KeyDownUp(Keys.P, False, 1, False)
            End If
            If c = "q" Then
                KeyDownUp(Keys.Q, False, 1, False)
            End If
            If c = "r" Then
                KeyDownUp(Keys.R, False, 1, False)
            End If
            If c = "s" Then
                KeyDownUp(Keys.S, False, 1, False)
            End If
            If c = "t" Then
                KeyDownUp(Keys.T, False, 1, False)
            End If
            If c = "u" Then
                KeyDownUp(Keys.U, False, 1, False)
            End If
            If c = "v" Then
                KeyDownUp(Keys.V, False, 1, False)
            End If
            If c = "w" Then
                KeyDownUp(Keys.W, False, 1, False)
            End If
            If c = "x" Then
                KeyDownUp(Keys.X, False, 1, False)
            End If
            If c = "y" Then
                KeyDownUp(Keys.Y, False, 1, False)
            End If
            If c = "z" Then
                KeyDownUp(Keys.Z, False, 1, False)
            End If

        Next

        'close chat
        KeyDownUp(Keys.Enter, False, 1, False)
        ResponsiveSleep(250)

    End Function

    Public Function makeDayTime()
        ShiftUP()

        'bring up console
        KeyDownUp(Keys.F1, False, 1, False)
        ResponsiveSleep(500)

        'kill
        KeyDownUp(Keys.E, False, 1, False)
        KeyDownUp(Keys.N, False, 1, False)
        KeyDownUp(Keys.V, False, 1, False)
        KeyDownUp(Keys.OemPeriod, False, 1, False)
        KeyDownUp(Keys.T, False, 1, False)
        KeyDownUp(Keys.I, False, 1, False)
        KeyDownUp(Keys.M, False, 1, False)
        KeyDownUp(Keys.E, False, 1, False)
        KeyDownUp(Keys.Space, False, 1, False)
        KeyDownUp(Keys.NumPad1, False, 1, False)
        KeyDownUp(Keys.NumPad2, False, 1, False)
        KeyDownUp(Keys.Enter, False, 1, False)
        KeyDownUp(Keys.F1, False, 1, False)
    End Function

    Public Sub downSampleImage(path As String)
        'resize
        Dim psi As New ProcessStartInfo("C:\Users\bob\source\repos\RustBot\RustBot\bin\Debug\utils\downsample.exe", path & " -resize 4000x4000 " & path)
        Dim p As New Process
        p.StartInfo = psi
        psi.WindowStyle = ProcessWindowStyle.Hidden
        p.Start()
        p.WaitForExit()
    End Sub

    Public Sub Run(ByVal key As Byte, shift As Boolean, ByVal durationInMilli As Integer, jumping As Boolean)
        Dim targetTime As DateTime = DateTime.Now().AddMilliseconds(durationInMilli)
        Dim kb_delay As Integer
        Dim kb_speed As Integer

        SystemParametersInfo(Constants.SPI_GETKEYBOARDDELAY, 0, kb_delay, 0)
        SystemParametersInfo(Constants.SPI_GETKEYBOARDSPEED, 0, kb_speed, 0)

        keybd_event(key, MapVirtualKey(key, 0), 0, 0) ' key pressed      

        'shift?
        If shift Then
            'yes
            ShiftDwn()
        End If

        While targetTime.Subtract(DateTime.Now()).TotalMilliseconds > 0
            Application.DoEvents()
            System.Threading.Thread.Sleep(kb_delay + kb_speed)
            If jumping Then
                If targetTime.Subtract(DateTime.Now()).TotalMilliseconds.ToString.Contains("00") Then
                    Jump()
                End If
            End If
        End While

        keybd_event(key, MapVirtualKey(key, 0), 2, 0) ' key released                

        'was shift down?
        If shift Then
            'yes, pull up
            ShiftUP()
        End If

        'ResponsiveSleep(500)
    End Sub

    Public Sub KeyDownUp(ByVal key As Byte, shift As Boolean, ByVal durationInMilli As Integer, jumping As Boolean)
        Dim targetTime As DateTime = DateTime.Now().AddMilliseconds(durationInMilli)
        Dim kb_delay As Integer
        Dim kb_speed As Integer

        SystemParametersInfo(Constants.SPI_GETKEYBOARDDELAY, 0, kb_delay, 0)
        SystemParametersInfo(Constants.SPI_GETKEYBOARDSPEED, 0, kb_speed, 0)

        keybd_event(key, MapVirtualKey(key, 0), 0, 0) ' key pressed      

        While targetTime.Subtract(DateTime.Now()).TotalMilliseconds > 0
            System.Threading.Thread.Sleep(kb_delay + kb_speed)
        End While

        keybd_event(key, MapVirtualKey(key, 0), 2, 0) ' key released              
    End Sub

    Public Sub KeyDownOnly(ByVal key As Byte, shift As Boolean, ByVal durationInMilli As Integer, jumping As Boolean)
        Dim targetTime As DateTime = DateTime.Now().AddMilliseconds(durationInMilli)
        Dim kb_delay As Integer
        Dim kb_speed As Integer

        SystemParametersInfo(Constants.SPI_GETKEYBOARDDELAY, 0, kb_delay, 0)
        SystemParametersInfo(Constants.SPI_GETKEYBOARDSPEED, 0, kb_speed, 0)

        If shift Then
            ShiftDwn()
        End If

        keybd_event(key, MapVirtualKey(key, 0), 0, 0) ' key pressed    

        ResponsiveSleep(durationInMilli)
    End Sub

    Public Sub KeyUpOnly(ByVal key As Byte, shift As Boolean, ByVal durationInMilli As Integer, jumping As Boolean)
        Dim targetTime As DateTime = DateTime.Now().AddMilliseconds(durationInMilli)
        Dim kb_delay As Integer
        Dim kb_speed As Integer

        SystemParametersInfo(Constants.SPI_GETKEYBOARDDELAY, 0, kb_delay, 0)
        SystemParametersInfo(Constants.SPI_GETKEYBOARDSPEED, 0, kb_speed, 0)

        keybd_event(key, MapVirtualKey(key, 0), 2, 0) ' key released        

        If shift Then
            ShiftUP()
        End If
    End Sub

    Public Sub LeftMouseClick()
        mouse_event(Constants.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        ResponsiveSleep(250)
        mouse_event(Constants.MOUSEEVENTF_LEFTUP, 6, 0, 0, 0)
        ResponsiveSleep(250)
    End Sub

    Public Sub LeftMouseHold()
        mouse_event(Constants.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        Application.DoEvents()
    End Sub

    Public Sub LeftMouseRelease()
        mouse_event(Constants.MOUSEEVENTF_LEFTUP, 6, 0, 0, 0)
        Application.DoEvents()
    End Sub


    Public Sub RightMouseClick()
        mouse_event(Constants.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
        Application.DoEvents()
        mouse_event(Constants.MOUSEEVENTF_RIGHTUP, 6, 0, 0, 0)
        Application.DoEvents()
    End Sub

    Public Sub LeftMouseClick(timeInMilli As Integer)
        'down
        mouse_event(Constants.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        Application.DoEvents()
        'wait
        ResponsiveSleep(timeInMilli)
        'up
        mouse_event(Constants.MOUSEEVENTF_LEFTUP, 6, 0, 0, 0)
        Application.DoEvents()
    End Sub

    Private Sub ShiftDwn()
        keybd_event(Keys.ShiftKey, MapVirtualKey(Keys.ShiftKey, 0), 0, 0)
    End Sub

    Public Sub Jump()
        keybd_event(Keys.Space, MapVirtualKey(Keys.Space, 0), 0, 0)
        keybd_event(Keys.Space, MapVirtualKey(Keys.Space, 0), Constants.KEYEVENTF_KEYUP, 0)
    End Sub

    Public Sub ShiftUP()
        keybd_event(Keys.ShiftKey, MapVirtualKey(Keys.ShiftKey, 0), Constants.KEYEVENTF_KEYUP, 0)
    End Sub

    Public Sub TakeAreaScreenshot(file As String, width As Integer, height As Integer, sourceX As Integer, sourceY As Integer, destinationX As Integer, destinationY As Integer)
        WriteMessageToGlobalChat("taking screenshot area")

        Dim printscreen As Bitmap = New Bitmap(width, height)
        Dim graphics As Graphics = Graphics.FromImage(CType(printscreen, Image))
        graphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, printscreen.Size)
        printscreen.Save(file, ImageFormat.Png)

waitagain:
        If IsFileUnavailable(file) Then
            ResponsiveSleep(100)
            GoTo waitagain
        End If

        WriteMessageToGlobalChat("done taking screenshot area")
    End Sub

    Public Sub TakeScreenShotWhole(file As String, Optional moveFile As Boolean = True)
        On Error Resume Next


        WriteMessageToGlobalChat("start " + GetTime())

        Dim sc = New ScreenCapturer()
        Using bitmap = sc.Capture()
            bitmap.Save(file, ImageFormat.Tiff)
            bitmap.Dispose()
        End Using
        sc = Nothing

        WriteMessageToGlobalChat("end   " + GetTime())

        'wait for available file lock
waitagain:
        If IsFileUnavailable(file) Then
            Thread.Sleep(100)
            GoTo waitagain
        End If



        If moveFile Then
            If IO.File.Exists(file) Then
                'done
            End If
        End If
    End Sub

    Public Function GetTime() As String
        Dim epoch As Date = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        Dim ms As Long = (Date.UtcNow - epoch).TotalMilliseconds
        Return ms.ToString
    End Function

    Public Function IsFileUnavailable(ByVal path As String) As Boolean
        ' if file doesn't exist, return true
        If Not System.IO.File.Exists(path) Then
            Return True
        End If

        Dim file As FileInfo = New FileInfo(path)
        Dim stream As FileStream = Nothing
        Try
            stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None)
        Catch ex As IOException
            'the file is unavailable because it is:
            'still being written to
            'or being processed by another thread
            'or does not exist (has already been processed)
            Return True
        Finally
            If (Not (stream) Is Nothing) Then
                stream.Close()
            End If

        End Try

        'file is not locked
        Return False
    End Function


    Public Sub ResponsiveSleep(ByRef iMilliSeconds As Integer)
        Dim i As Integer, iHalfSeconds As Integer = iMilliSeconds / 50
        For i = 1 To iHalfSeconds
            Application.DoEvents()
            Threading.Thread.Sleep(50)
            Application.DoEvents()
        Next i
    End Sub

    Public Function GetProcessHandle() As String
        Dim processlist() As Process = Process.GetProcesses
        For Each process As Process In processlist
            If process.ProcessName = "RustClient" Then
                Try
                    Return process.MainWindowHandle
                Catch ex As Exception
                    Return ""
                End Try
            End If
        Next

    End Function

    Public Sub ResizeImageOnDisk(path As String)
        Dim strFileName = System.IO.Path.GetFileName(path)
        Try
            Dim original As Image = Image.FromFile(path)
            Dim resized As Image = ResizeImage(original, New Size(800, 800))
            Dim memStream As MemoryStream = New MemoryStream()
            resized.Save(memStream, ImageFormat.Jpeg)

            Dim file As New FileStream(path, FileMode.Create, FileAccess.Write)

            memStream.WriteTo(file)
            file.Close()
            memStream.Close()
        Catch ex As Exception
        End Try

    End Sub

    Public Function ResizeImage(img As Image, size As Size) As Image
        Return ResizeImage(img, size.Width, size.Height)
    End Function

    Public Function ResizeImage(bmp As Bitmap, width As Integer, height As Integer) As Image
        Return ResizeImage(DirectCast(bmp, Image), width, height)
    End Function

    Public Function ResizeImage(bmp As Bitmap, size As Size) As Image
        Return ResizeImage(DirectCast(bmp, Image), size.Width, size.Height)
    End Function

    Public Function fastCompareImages(path1 As String, path2 As String) As Double
        downSampleImage(path1)
        downSampleImage(path2)

        Dim img1 As Bitmap = New Bitmap(path1)
        Dim img2 As Bitmap = New Bitmap(path2)
        If (img1.Size <> img2.Size) Then
            Console.Error.WriteLine("Images are of different sizes")
            Return 0.0
        End If

        Dim diff As Single = 0
        Dim y As Integer = 0
        Do While (y < img1.Height)
            Dim x As Integer = 0
            Do While (x < img1.Width)
                Dim pixel1 As Color = img1.GetPixel(x, y)
                Dim pixel2 As Color = img2.GetPixel(x, y)
                diff = (diff + Math.Abs((pixel1.R - pixel2.R)))
                diff = (diff + Math.Abs((pixel1.G - pixel2.G)))
                Try
                    diff = (diff + Math.Abs((pixel1.B - pixel2.B)))
                Catch
                End Try
                x = (x + 1)
            Loop
            y = (y + 1)
        Loop

        Console.WriteLine("diff: {0} %", (100 * ((diff / 255) / (img1.Width * (img1.Height * 3)))))
    End Function


    'Public Function compareImages(path1 As String, path2 As String) As Double
    '    WriteMessageToGlobalChat("compareImages, starting downsample")
    '    'downSampleImage(path1)
    '    'downSampleImage(path2)

    '    WriteMessageToGlobalChat("compareImages, starting comparison")

    '    'compare the two
    '    Console.WriteLine(("Comparing: " + (bmp1 + (" and " _
    '                    + (bmp2 + (", with a threshold of " + threshold))))))
    '    Dim firstBmp As Bitmap = CType(Image.FromFile(path1), Bitmap)
    '    Dim secondBmp As Bitmap = CType(Image.FromFile(path2), Bitmap)
    '    'get the difference as a bitmap
    '    firstBmp.GetDifferenceImage(secondBmp, True).Save((path1 + "_diff.png"))
    '    Console.WriteLine(String.Format("Difference: {0:0.0} %", (firstBmp.PercentageDifference(secondBmp, threshold) * 100)))
    '    Console.WriteLine(String.Format("BhattacharyyaDifference: {0:0.0} %", (firstBmp.BhattacharyyaDifference(secondBmp) * 100)))


    '    WriteMessageToGlobalChat("compareImages, done")

    '    Return Math.Round((100 * ((diff / 255) / (img1.Width * (img1.Height * 3)))))
    'End Function

    Private processOutput As StringBuilder = Nothing

    Public detectedBuffer As New Collection

    Public Sub startChromeAudio(link As String)
        Shell("C:\Program Files\internet explorer\iexplore.exe " & link, AppWinStyle.Hide)
    End Sub

    Public Sub StartPythonBackend()

        WriteMessageToGlobalChat("killing python")
        Shell("taskkill /f /im python.exe", AppWinStyle.Hide)

        'wait for video mem to clear up
        ResponsiveSleep(5000)

        Dim processOptions As ProcessStartInfo = New ProcessStartInfo
        processOptions.FileName = "python.exe"
        processOptions.Arguments = "-u C:\Users\bob\yolov5\detect.py --conf-thres 0.40 --source test.png --weights weights\last.pt --output output --img-size 608"
        processOptions.WorkingDirectory = "C:\Users\bob\yolov5"
        processOptions.UseShellExecute = False
        processOptions.CreateNoWindow = True
        processOptions.RedirectStandardOutput = True
        processOptions.RedirectStandardError = False

        Dim process As Process
        process = Process.Start(processOptions)

        Dim sr As StreamReader = process.StandardOutput

        Dim strLine As String = Nothing
        Dim strLineAdd As String = Nothing

        Try
            Do
                'read a line
                strLine = sr.ReadLine()
                strLineAdd = strLineAdd & strLine

                'empty?g
                If strLine <> Nothing Then
                    'rec item?                                        
                    'dont add this
                    If strLineAdd.Contains("Done.") Then
                        WriteMessageToGlobalChat("adding rec result to global")
                        'Image 1 / 1 test.png
                        'rustbotresult 23 1241 445 1404 731 
                        'rustbotresult 8 1245 598 1267 633 

                        Dim thesplit = Split(strLineAdd, ":")
                        Dim thesplit2 = Split(thesplit(2), "|")

                        For i = 0 To thesplit2.Count - 2
                            'rec data only
                            If detectedBuffer.Contains(LTrim(RTrim(thesplit2(i)))) = False Then
                                detectedBuffer.Add(LTrim(RTrim(thesplit2(i))), LTrim(RTrim(thesplit2(i))))
                            End If
                        Next

                        'clear buffers
                        strLine = Nothing
                        strLineAdd = Nothing
                    End If

                    'add all log messages
                    WriteMessageToGlobalChat(strLine)
                End If
            Loop
        Catch ex As Exception
            WriteMessageToGlobalChat("Python crashed!")
        End Try
    End Sub


    'Public Function GetGenericTagging()
    '    Dim Output As String

    '    Using info As Process = New Process()
    '        info.StartInfo.FileName = "python.exe"
    '        info.StartInfo.Arguments = "generictagging.py"
    '        info.StartInfo.UseShellExecute = False
    '        info.StartInfo.RedirectStandardOutput = True
    '        info.StartInfo.CreateNoWindow = True
    '        info.Start()
    '        Output = info.StandardOutput.ReadToEnd()
    '        info.WaitForExit()
    '    End Using

    '    Dim json As JObject = JObject.Parse(Output)
    '    Dim converted As String = json.ToString.Replace(" ", "").Replace(vbCrLf, "").Replace(Chr(34), "").Replace("{", "").Replace("[", "").Replace("}", "").Replace("]", "")

    '    Dim TheSplit = Split(converted, "tags:")
    '    Dim TheSplit2 = Split(TheSplit(1), "task_id")
    '    Dim TheSplit3 = Split(TheSplit2(0), ",")

    '    'drop blank
    '    Array.Resize(TheSplit3, TheSplit3.Length - 1)

    '    'return array
    '    Return TheSplit3
    'End Function

    'Public Function DetectWater() As Boolean
    '    TakeScreenShotWhole("processme.png")

    '    Dim tags As Array = GetGenericTagging()

    '    For I = 0 To tags.Length - 1
    '        If tags(I).ToString.Contains("sea") Or tags(I).ToString.Contains("ocean") Or tags(I).ToString.Contains("water") Or tags(I).ToString.Contains("lake") Or tags(I).ToString.Contains("stream") Then
    '            Return True
    '        End If
    '    Next

    '    Return False
    'End Function

    'Public Function DoGenericTagging() As Collection
    '    TakeScreenShotWhole("processmedone.bmp")
    '    Dim collection As New Collection

    '    Dim tags As Array = GetGenericTagging()

    '    'do we have wood?
    '    For I = 0 To tags.Length - 1
    '        If tags(I).ToString.Contains("wood") Or tags(I).ToString.Contains("tree") Or tags(I).ToString.Contains("forest") Then
    '            'yes, return highest probability
    '            collection.Add(tags(I), tags(I - 1))
    '        End If
    '    Next

    'End Function

    Public Function Distance3D(ByVal x1 As Integer, ByVal y1 As Integer, ByVal z1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer, ByVal z2 As Integer) As Integer
        '     __________________________________note

        'd = √ (x2-x1)^2 + (y2-y1)^2 + (z2-z1)^2
        '
        'Our end result
        Dim result As Integer = 0
        'Take x2-x1, then square it
        Dim part1 As Double = Math.Pow(x2 - x1, 2)
        'Take y2-y1, then sqaure it
        Dim part2 As Double = Math.Pow(y2 - y1, 2)
        'Take z2-z1, then square it
        Dim part3 As Double = Math.Pow(z2 - z1, 2)
        'Add both of the parts together
        Dim underRadical As Double = part1 + part2 + part3
        'Get the square root of the parts
        result = CInt(Math.Sqrt(underRadical))
        'Return our result
        Return result
    End Function
    Public Function IsNullOrEmpty(ByVal myStringArray() As String) As Boolean
        Try
            If myStringArray.Count = 0 Then
                Return True
            Else
                Return False
            End If
        Catch
            Return True
        End Try
    End Function

    Public Function AreWeStuck() As Boolean
tryAgain:

        Dim postBefore As Array = GetCurrentPosition()

        If IsNullOrEmpty(postBefore) Then
            WriteMessageToGlobalChat("FAILED TO GET POSITION, TRYING AGAIN")
            GoTo tryAgain
        End If

        Dim tempadd1 As Double
        For I = 0 To postBefore.Length - 1
            tempadd1 = tempadd1 + LTrim(RTrim(Double.Parse(postBefore.GetValue(I))))
        Next

        WriteMessageToGlobalChat("Starting `run")

        'run see if we moved
        Run(Keys.W, False, 500, False)

        Dim tempadd2 As Double

        WriteMessageToGlobalChat("Done stuck run")
        'after
        Dim posAfter As Array = GetCurrentPosition()

        If IsNullOrEmpty(posAfter) Then
            WriteMessageToGlobalChat("FAILED TO GET POSITION, TRYING AGAIN")
            GoTo tryAgain
        End If

        For I = 0 To posAfter.Length - 1
            tempadd2 = tempadd2 + LTrim(RTrim(Double.Parse(posAfter.GetValue(I))))
        Next

        If tempadd2 <> tempadd1 Then
            'bump me                        
            WriteMessageToGlobalChat("We have moved")
            Return False
        Else
            WriteMessageToGlobalChat("We have NOT moved")
            Return True
        End If
    End Function

    Public Function GetCurrentPosition() As Array

        WriteMessageToGlobalChat("Getting Position")

        ResponsiveSleep(250)

        'bring up console
        KeyDownUp(Keys.F1, False, 1, False)
        'min
        ResponsiveSleep(250)

        ShiftUP()

        'get position
        KeyDownUp(Keys.C, False, 1, False)
        KeyDownUp(Keys.L, False, 1, False)
        KeyDownUp(Keys.I, False, 1, False)
        KeyDownUp(Keys.E, False, 1, False)
        KeyDownUp(Keys.N, False, 1, False)
        KeyDownUp(Keys.T, False, 1, False)
        KeyDownUp(Keys.OemPeriod, False, 1, False)
        KeyDownUp(Keys.P, False, 1, False)
        KeyDownUp(Keys.R, False, 1, False)
        KeyDownUp(Keys.I, False, 1, False)
        KeyDownUp(Keys.N, False, 1, False)
        KeyDownUp(Keys.T, False, 1, False)
        KeyDownUp(Keys.P, False, 1, False)
        KeyDownUp(Keys.O, False, 1, False)
        KeyDownUp(Keys.S, False, 1, False)
        KeyDownUp(Keys.Enter, False, 1, False)
        ResponsiveSleep(250)
        KeyDownUp(Keys.F1, False, 1, False)

        ResponsiveSleep(250)
        'min    

        Dim TheSplit2

        Try
            'read log
            Dim fs As FileStream = New FileStream("C:\Program Files (x86)\Steam\steamapps\common\Rust\output_log.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim sr As StreamReader = New StreamReader(fs)
            Dim lines = Split(sr.ReadToEnd, vbCr)
            Dim output As String = lines(lines.Count - 2).Replace(")", "")
            Dim TheSplit = Split(output, "(")
            TheSplit2 = Split(TheSplit(1), ",")

            Dim distance As Integer = 0

            Try
                distance = Distance3D(TheSplit2(0), TheSplit2(1), TheSplit2(2), Constants.homex1, Constants.homey1, Constants.homez1)
            Catch
            End Try

            TheSplit(1) = TheSplit(1) & "," & distance
            TheSplit2 = Split(TheSplit(1), ",")

            fs.Close()
            sr.Close()

            Return TheSplit2
        Catch
        End Try

        Return TheSplit2
    End Function

    'Public Function GetCurrentEyePos() As Array
    '    On Error Resume Next

    '    WriteMessageToGlobalChat("Getting Position")

    '    'bring up console
    '    KeyDownUp(Keys.F1, False, 1, False)
    '    'min
    '    ResponsiveSleep(500)

    '    ShiftUP()

    '    'get position
    '    KeyDownUp(Keys.C, False, 1, False)
    '    KeyDownUp(Keys.L, False, 1, False)
    '    KeyDownUp(Keys.I, False, 1, False)
    '    KeyDownUp(Keys.E, False, 1, False)
    '    KeyDownUp(Keys.N, False, 1, False)
    '    KeyDownUp(Keys.T, False, 1, False)
    '    KeyDownUp(Keys.OemPeriod, False, 1, False)
    '    KeyDownUp(Keys.P, False, 1, False)
    '    KeyDownUp(Keys.R, False, 1, False)
    '    KeyDownUp(Keys.I, False, 1, False)
    '    KeyDownUp(Keys.N, False, 1, False)
    '    KeyDownUp(Keys.T, False, 1, False)
    '    KeyDownUp(Keys.E, False, 1, False)
    '    KeyDownUp(Keys.Y, False, 1, False)
    '    KeyDownUp(Keys.E, False, 1, False)
    '    KeyDownUp(Keys.S, False, 1, False)
    '    KeyDownUp(Keys.Enter, False, 1, False)
    '    ResponsiveSleep(250)
    '    KeyDownUp(Keysp, False, 1, False)

    '    ResponsiveSleep(500)
    '    'min    

    '    'read log
    '    Dim fs As FileStream = New FileStream("C:\Program Files (x86)\Steam\steamapps\common\Rust\output_log.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '    Dim sr As StreamReader = New StreamReader(fs)
    '    Dim lines = Split(sr.ReadToEnd, vbCr)
    '    Dim output As String = lines(lines.Count - 2).Replace(")", "")
    '    Dim TheSplit = Split(output, "(")
    '    Dim TheSplit2 = Split(TheSplit(1), ",")

    '    Dim distance As Integer = Distance3D(TheSplit2(0), TheSplit2(1), TheSplit2(2), Constants.homex1, Constants.homey1, Constants.homez1)

    '    TheSplit(1) = TheSplit(1) & "," & distance
    '    TheSplit2 = Split(TheSplit(1), ",")

    '    fs.Close()
    '    sr.Close()

    '    Return TheSplit2
    'End Function

    ' Function to obtain the desired hash of a file
    Function hash_generator(ByVal hash_type As String, ByVal file_name As String)

        ' We declare the variable : hash
        Dim hash
        If hash_type.ToLower = "md5" Then
            ' Initializes a md5 hash object
            hash = MD5.Create
        ElseIf hash_type.ToLower = "sha1" Then
            ' Initializes a SHA-1 hash object
            hash = SHA1.Create()
        ElseIf hash_type.ToLower = "sha256" Then
            ' Initializes a SHA-256 hash object
            hash = SHA256.Create()
        Else
            MsgBox("Unknown type of hash : " & hash_type, MsgBoxStyle.Critical)
            Return False
        End If

        ' We declare a variable to be an array of bytes
        Dim hashValue() As Byte

        ' We create a FileStream for the file passed as a parameter
        'Dim fileStream As FileStream = File.OpenRead(file_name)

        Dim fileStream As FileStream = New FileStream(file_name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)

        ' We position the cursor at the beginning of stream
        fileStream.Position = 0
        ' We calculate the hash of the file
        hashValue = hash.ComputeHash(fileStream)
        ' The array of bytes is converted into hexadecimal before it can be read easily
        Dim hash_hex = PrintByteArray(hashValue)

        ' We close the open file
        fileStream.Close()

        ' The hash is returned
        Return hash_hex

    End Function

    ' We traverse the array of bytes and converting each byte in hexadecimal
    Public Function PrintByteArray(ByVal array() As Byte)
        Dim hex_value As String = ""

        ' We traverse the array of bytes
        Dim i As Integer
        For i = 0 To array.Length - 1

            ' We convert each byte in hexadecimal
            hex_value += array(i).ToString("X2")

        Next i

        ' We return the string in lowercase
        Return hex_value.ToLower

    End Function

    ' md5 is a reserved name, so we named the function : md5_hash
    Function md5_hash(ByVal file_name As String)
        Return hash_generator("md5", file_name)
    End Function

    Function sha_1(ByVal file_name As String)
        Return hash_generator("sha1", file_name)
    End Function

    Function sha_256(ByVal file_name As String)
        Return hash_generator("sha256", file_name)
    End Function
End Module

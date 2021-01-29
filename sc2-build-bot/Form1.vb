Public Class Form1

    Dim screenIndex As Integer = 1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'wait for game
        ResponsiveSleep(5000)

        'test jig
        'Do
        '    OcrScreen()
        '    ResponsiveSleep(5000)
        'Loop

        MoveToScv()
        MoveMouseToCenter()

        ResponsiveSleep(50)
        MoveToScv()
        ResponsiveSleep(50)

        'start off at nexus
        MoveToNexus()

        '0 make first unit = sentry
        BlinkSCV(1825, 390)
        MakeUnit("B", "W")

        '1 make second unit
        waitForMinerals(50)
        MoveToNextBuildPosition()
        MakeUnit("B", "W")

        '2 make ling
        waitForMinerals(50)
        MoveToNextBuildPosition()
        MakeUnit("V", "Q")

        '3 make roach
        waitForMinerals(75)
        MoveToNextBuildPosition()
        MakeUnit("V", "A")

        '4 make raveger
        waitForMinerals(150)
        MoveToNextBuildPosition()
        MakeUnit("V", "T")

        '5 make raveger
        waitForMinerals(150)
        MoveToNextBuildPosition()
        MakeUnit("V", "T")


        '6 make infestor
        waitForMinerals(150)
        MoveToNextBuildPosition()
        MakeUnit("V", "R")

        '7 make infestor
        waitForMinerals(150)
        MoveToNextBuildPosition()
        MakeUnit("V", "R")

        'NEXT ROW

        '8 make infestor
        waitForMinerals(150)
        MoveToNextBuildPosition()
        MakeUnit("V", "R")

        '9 make infestor
        waitForMinerals(150)
        MoveToNextBuildPosition()
        MakeUnit("V", "R")

        '10 unit, hydra
        waitForMinerals(100)
        MoveToNextBuildPosition()
        MakeUnit("V", "S")

        '11 unit, muta
        waitForMinerals(100)
        MoveToNextBuildPosition()
        MakeUnit("V", "Z")

        '12 unit, viper
        waitForMinerals(200)
        MoveToNextBuildPosition()
        MakeUnit("V", "G")

        '13 unit, viper
        waitForMinerals(200)
        MoveToNextBuildPosition()
        MakeUnit("V", "G")

        '14 unit, viper
        waitForMinerals(200)
        MoveToNextBuildPosition()
        MakeUnit("V", "G")

        '15 unit, viper
        waitForMinerals(200)
        MoveToNextBuildPosition()
        MakeUnit("V", "G")

        'done!
        ResponsiveSleep(2000)

        'NEXT ROW
    End Sub

    Dim onUnit As Integer = 0
    Sub MoveToNextBuildPosition()
        If onUnit = 0 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1691, 400)

            Return
        End If

        If onUnit = 1 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1566, 400)

            Return
        End If

        If onUnit = 2 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1441, 400)

            Return
        End If

        If onUnit = 3 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1316, 400)

            Return
        End If

        If onUnit = 4 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1191, 400)
            Return
        End If

        If onUnit = 5 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1066, 400)
            Return
        End If

        If onUnit = 6 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(959, 390)
            Return
        End If

        If onUnit = 7 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(838, 390)
            Return
        End If

        '2nd row

        If onUnit = 8 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1089, 498)
            Return
        End If

        If onUnit = 9 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1214, 500)
            Return
        End If

        If onUnit = 10 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1342, 501)
            Return
        End If

        If onUnit = 11 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1467, 501)
            Return
        End If

        If onUnit = 12 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1592, 501)
            Return
        End If

        If onUnit = 13 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1717, 501)
            Return
        End If

        If onUnit = 14 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1842, 501)
            Return
        End If

        If onUnit = 15 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1967, 501)
            Return
        End If

    End Sub

    'wait for >= mineral count param
    Function waitForMinerals(count As Integer)
        MoveToBlackTop()

        MoveMouseToCenter()

TryAgain:
        ResponsiveSleep(1000)

        Dim minerals As Integer = OcrScreen()

        'probably.
        If minerals >= count Then
            WriteMessageToGlobalChat("we have " & minerals & " mineral(s) needed, exiting wait")
            MoveToScv()

            Exit Function
        Else
            'start again
            GoTo TryAgain
        End If

    End Function
End Class

Public Class Form1

    Dim screenIndex As Integer = 1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'wait for game
        ResponsiveSleep(5000)

        MoveToScv()
        MoveMouseToCenter()

        ResponsiveSleep(50)
        MoveToScv()
        ResponsiveSleep(50)

        'start off at nexus
        MoveToNexus()

        '0 make first unit = sentry
        BlinkSCV(838, 390)
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

        '10 unit, next row
        waitForMinerals(150)
        MoveToNextBuildPosition()
        MakeUnit("V", "R")


    End Sub

    Dim onUnit As Integer = 0
    Sub MoveToNextBuildPosition()
        If onUnit = 0 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(959, 390)

            Return
        End If

        If onUnit = 1 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1081, 390)

            Return
        End If

        If onUnit = 2 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1203, 390)

            Return
        End If

        If onUnit = 3 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1325, 390)

            Return
        End If

        If onUnit = 4 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1450, 390)
            Return
        End If

        If onUnit = 5 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1575, 390)
            Return
        End If

        If onUnit = 6 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1700, 390)
            Return
        End If

        If onUnit = 7 Then
            onUnit += 1
            MoveToNexus()
            BlinkSCV(1825, 390)
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


    End Sub

    'wait for >= mineral count param
    Function waitForMinerals(count As Integer)
        MoveToBlackTop()

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

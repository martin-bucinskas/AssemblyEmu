Module Module1

    Dim memory(32) As String
    Dim PC As Integer = 0
    Dim MAR, ACCUMULATOR, MOVETO As Integer
    Dim MBR, CIR, OPCODE, OPERAND As String
    Dim menuInput As String
    Dim addressOfFunc As Integer
    Dim memoryHolder As Integer

    Sub Main()

        Menu()
        Console.ReadLine()

    End Sub

    Sub Menu()
        TitleTop()
        Console.WriteLine("1 - Program Editor")
        Console.WriteLine("2 - Load Program")
        Console.WriteLine("3 - See Code")
        Console.WriteLine("x - Close Emulator")
        Console.WriteLine()
        Console.BackgroundColor = ConsoleColor.DarkBlue
        Console.ForegroundColor = ConsoleColor.Red
        Console.Write(" >> ")
        menuInput = Console.ReadLine()
        Console.ResetColor()
        Console.WriteLine()

        Select Case menuInput
            Case "1"
                NEWPROGRAM()
            Case "2"
                LOADPROGRAM()
            Case "3"
                PrintCode()
            Case "H"

            Case "x"
                Close()
        End Select
    End Sub

    Sub NEWPROGRAM()
        Dim pass As Boolean
        Console.Clear()
        TitleTop()
        addressOfFunc = 0
        Console.Write("How many locations in the memory are used? (0 - 32) >> ")
        Console.BackgroundColor = ConsoleColor.DarkBlue
        Console.ForegroundColor = ConsoleColor.Red
        memoryHolder = Console.ReadLine()
        Console.ResetColor()
        Console.WriteLine()
        Console.WriteLine("Create a new program, after every line, press enter.")
        Console.WriteLine()

        For counter As Integer = 0 To memoryHolder - 1
            Highlight()
            Console.Write(counter & ". ")
            memory(counter) = Console.ReadLine()
            StopHighlight()
        Next
        For counter As Integer = 0 To memoryHolder - 1
            If memory(counter) <> "HLT" Then
                pass = False
            ElseIf memory(counter) = "HLT" Then
                pass = True
            End If
        Next

        If pass Then
            Console.WriteLine()
            Console.WriteLine("Done! Please wait...")
            Threading.Thread.Sleep(3000)
            Console.Clear()
        Else
            Console.WriteLine()
            Console.BackgroundColor = ConsoleColor.Red
            Console.ForegroundColor = ConsoleColor.Black
            Console.WriteLine("There is no HALT (HLT) opcode...")
            Console.ResetColor()
            Console.ReadLine()
            Console.Clear()
        End If

        Menu()

    End Sub

    Sub LOADPROGRAM()

        Do
            FETCH()
            DECODE()
            EXECUTE()
        Loop Until CIR = "HLT"

        PrintMemory()
        Menu()

    End Sub

    Sub FETCH()
        MAR = PC
        MBR = memory(MAR)
        PC = PC + 1
        CIR = MBR

        Console.WriteLine()
        Console.Write("     ")
        Console.BackgroundColor = ConsoleColor.DarkBlue
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.Write("CIR: " + CIR)
        Console.ResetColor()
    End Sub

    Sub DECODE()

        OPCODE = Mid(CIR, 1, 3)
        OPERAND = Mid(CIR, 5, 10)
        Console.WriteLine()
        Console.WriteLine("OPCODE: " + OPCODE)
        Console.WriteLine("OPERAND: " + OPERAND)

    End Sub

    Sub EXECUTE()

        Select Case OPCODE
            Case "LDA"
                If OPERAND.StartsWith("#") Then
                    ACCUMULATOR = Mid(OPERAND, 2, 10)
                Else
                    ACCUMULATOR = memory(OPERAND)
                End If
            Case "STO"
                memory(OPERAND) = ACCUMULATOR
            Case "MOV"
                MOVETO = Mid(OPERAND, 3)
                memory(MOVETO) = memory(Mid(OPERAND, 1, 2))
            Case "ADD"
                ALU("ADD")
            Case "SUB"
                ALU("SUBTRACT")
            Case "DIV"
                ALU("DIVIDE")
            Case "MUL"
                ALU("MULTIPLY")
            Case "INC"
                ALU("INCREMENT")
            Case "DEC"
                ALU("DECREMENT")
        End Select

    End Sub

    Sub ALU(ByVal op As String)
        Select Case op
            Case "ADD"
                If OPERAND.StartsWith("#") Then
                    ACCUMULATOR = ACCUMULATOR + Mid(OPERAND, 2, 10)
                Else
                    ACCUMULATOR = ACCUMULATOR + memory(OPERAND)
                End If
            Case "SUBTRACT"
                If OPERAND.StartsWith("#") Then
                    ACCUMULATOR = ACCUMULATOR - Mid(OPERAND, 2, 10)
                Else
                    ACCUMULATOR = ACCUMULATOR - memory(OPERAND)
                End If
            Case "DIVIDE"
                If OPERAND.StartsWith("#") Then
                    ACCUMULATOR = ACCUMULATOR / Mid(OPERAND, 2, 10)
                Else
                    ACCUMULATOR = ACCUMULATOR / memory(OPERAND)
                End If
            Case "MULTIPLY"
                If OPERAND.StartsWith("#") Then
                    ACCUMULATOR = ACCUMULATOR * Mid(OPERAND, 2, 10)
                Else
                    ACCUMULATOR = ACCUMULATOR * memory(OPERAND)
                End If
            Case "INCREMENT"
                ACCUMULATOR = OPERAND + 1
            Case "DECREMENT"
                ACCUMULATOR = OPERAND - 1
        End Select
    End Sub

    Sub PrintMemory()
        Console.WriteLine()
        For counter As Integer = 0 To memory.Length - 1
            Console.Write(counter & ". ")
            Console.Write(memory(counter))
            Console.WriteLine()
        Next

    End Sub

    Sub PrintCode()
        Console.WriteLine()
        For counter As Integer = 0 To memory.Length - 1
            Console.Write(counter & ". ")
            Console.Write(memory(counter))
            Console.WriteLine()
        Next

        Menu()
    End Sub

    Sub TitleTop()
        Console.BackgroundColor = ConsoleColor.DarkMagenta
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Assembly Emulator")
        Console.ResetColor()
        Console.WriteLine()
    End Sub

    Sub Highlight()
        Console.BackgroundColor = ConsoleColor.Yellow
        Console.ForegroundColor = ConsoleColor.Black
    End Sub

    Sub StopHighlight()
        Console.ResetColor()
    End Sub

    Sub Close()

    End Sub

End Module

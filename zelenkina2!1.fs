open System

let mindigit (n: int) : int = // принимание n(число) и возвращает мин. цифру
    n.ToString() //предобразует в сторку
    |> Seq.map (fun c -> int c - int '0') // каждый символ в цифру
    |> Seq.min // ищем минимальную цифру

let minDList (number: List<int>) : List<int> = //принимает список чисел и возвращ новый
    number |> List.map mindigit

[<EntryPoint>]
let main argv =
    //  ввод от пользователя
    printfn "Введите натуральные числа, разделенные пробелом:"
    let input = Console.ReadLine()
    
    //  введенную строку преобр в список чисел
    let number =
        input.Split(' ') // разбиваем числа по пробелу
        |> Array.toList//эти числа пихаем в список
        |> List.choose (fun s -> //фильтр при соме
            match Int32.TryParse(s) with
            | (true, n) when n > 0 -> Some n // проверка на положительность числа
            | _ -> None)

    // получаем  минимальные 
    let minD = minDList number

    printfn "Минимальные цифры для введенных чисел: %A" minD

    0 // для завершения программы
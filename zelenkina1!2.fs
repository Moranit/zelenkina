open System

// Функция для нахождения первой цифры натурального числа рекурсивно лаба 1/2
let rec firstd (n: int) =
    if n < 10 then n //  если n меньше 10, то это первая цифра
    else firstd (n / 10) //  иначе рекурсия

let natchislo (n: int) =
    n > 0


// ввод числа
let rec inputnumber () =
    printfn "Введите число:"
    let input = Console.ReadLine()
    match Int32.TryParse(input) with
    | (true, n) when natchislo n -> Some n  // Если ввод корректен, возвращаем число
    | _ -> 
        printfn "Некорректный ввод. Пожалуйста, попробуйте снова." // если ввод некорректен 
        inputnumber ()  

// Основная программа
[<EntryPoint>]
let main argv =
    match inputnumber () with
    | Some n -> // если натуральное
        let digit = firstd n //  первая цифра
        printfn "Первая цифра числа %d: %d" n digit // Выводим первую цифру
    | None ->
        printfn "Пожалуйста, введите корректное натуральное число."

    0 // Возвращаем код завершения программы

    0 // Возвращаем код завершения программы
